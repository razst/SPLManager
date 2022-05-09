using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPL_Manager.Library.Shared;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.PacketLifecycle.Send
{
    public abstract class PacketServer
    {
        public bool IsRunning { get; protected set; } = false;
        public abstract void Start();

        public abstract Task<bool> Send(string rawPacket);

        public event Action<string> OnPacketRecived;
        protected void EmitPacketRecivedEvent(string rawPacket)
        {
            OnPacketRecived.Invoke(rawPacket);
        }

        public abstract void Stop();


        private static PacketServer _instance;
        public static PacketServer Instance 
        { 
            get
            {
                if (_instance != null) return _instance;
                string packetServerMode = (string)ProgramProps.settings.serverMode;
                _instance = packetServerMode.ToUpper() switch
                {
                    "TCP" => new TCPServer(),
                    "UDP" => new UDPServer(),
                    _ => throw new ArgumentException("Server mode unsuported")
                };
                return _instance;
            }
        }
    }



    public class TCPServer : PacketServer
    {
        private TcpClient client = null;
        private TcpListener server = null;

        public TCPServer()
        {
        }
        public override void Start()
        {
            if (IsRunning) return;
            Task.Factory.StartNew(Server_Thread, TaskCreationOptions.LongRunning);
        }



        public override Task<bool> Send(string msg)
        {
            if (!IsRunning) return Task.FromResult(false);
            byte[] j = ConvertHexToBytes(msg);
            var command = new { Type = "RawTelecommand", Content = j };
            var toSend = JObject.FromObject(command);
            byte[] dataToSend = Encoding.Default.GetBytes(toSend.ToString());
            client.GetStream().WriteAsync(dataToSend, 0, dataToSend.Length);
            return Task.FromResult(true);
        }
        private byte[] ConvertHexToBytes(string str)
        {
            string[] bytesAsStrings = str.Split(' ');
            byte[] toReturn = new byte[bytesAsStrings.Length];
            for (int idx = 0; idx < toReturn.Length; idx++)
            {
                bool isSuccessful = byte.TryParse(bytesAsStrings[idx], System.Globalization.NumberStyles.HexNumber, null, out byte b);
                if (!isSuccessful) return null;
                toReturn[idx] = b;
            }
            return toReturn;
        }


        public override void Stop()
        {
            if (!IsRunning) return;
            client.Close();
            server.Stop();
            IsRunning = false;
        }


        class Msg
        {
            public string Type;
            public string Content;
        };

        private Task Server_Thread()
        {
            try
            {
                // Set the TcpListener on port 13000.
                int port = 61015;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);
                Console.WriteLine("" + Environment.NewLine);
                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                byte[] bytes = new byte[256];
                string data;
                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    IsRunning = true;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        Console.WriteLine("");
                        try
                        {
                            // Translate data bytes to a ASCII string.
                            data = Encoding.ASCII.GetString(bytes, 0, i);
                            Msg msg1 = JsonConvert.DeserializeObject<Msg>(data);
                            Console.WriteLine($"Received: {data}");


                            Console.WriteLine("");
                            switch (msg1.Type)
                            {
                                case "EndNode":
                                    // Process the data sent by the client.
                                    data = $@"{{'Type': 'ClientId', 'Content': {(int)ProgramProps.settings.tcpPortNumber}}}";
                                    byte[] msg = Encoding.ASCII.GetBytes(data);

                                    // Send back a response.
                                    stream.Write(msg, 0, msg.Length);
                                    Console.WriteLine("Sent: {0}", data);
                                    break;


                                case "RawTelemetry":
                                    byte[] bbb = (byte[])(JValue)msg1.Content;
                                    string[] sArr = new string[bbb.Length];
                                    for (int k = 0; k < bbb.Length; k++)
                                    {
                                        sArr[k] = bbb[k].ToString("X2");
                                    }

                                    string packetRecived = string.Join(" ", sArr);
                                    EmitPacketRecivedEvent(packetRecived);
                                    break;
                            }
                        }
                        finally
                        {
                            Console.WriteLine("*******************");
                        }

                    }
                }
            }
            finally
            {
                Stop();
            }
        }


    }




    public class UDPServer : PacketServer
    {
        private UdpClient _serverSocket;
        private UdpClient _cilentSocket;





        public UDPServer()
        {
            _cilentSocket = new UdpClient("127.0.0.1", (int)ProgramProps.settings.UDP_ports.client)
            {
                EnableBroadcast = true
            };
            _serverSocket = new UdpClient((int)ProgramProps.settings.UDP_ports.server)
            {
                EnableBroadcast = true
            };
        }
        public override void Start()
        {
            IsRunning = true;
            _serverSocket.BeginReceive(ReceiveCallback, null);
        }




        public override Task<bool> Send(string text)
        {
            if (!IsRunning) return Task.FromResult(false);

            byte[] data = Encoding.ASCII.GetBytes(text);

            _cilentSocket.SendAsync(data, data.Length);

            return Task.FromResult(true);
        }
        public override void Stop()
        {
            if (!IsRunning) return;
            _cilentSocket.Close();
            _serverSocket.Close();
            IsRunning = false;
        }

        private void ReceiveCallback(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            byte[] received = _serverSocket.EndReceive(res, ref RemoteIpEndPoint);

            string packet = Encoding.ASCII.GetString(received, 0, received.Length);
            EmitPacketRecivedEvent(packet);

            _serverSocket.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }
    }
}