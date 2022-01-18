using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Models.PacketServer
{




    public class TCPServer: IPacketServer
    {
        public bool IsRunning { get; private set; } = false;
        private TcpClient client = null;
        private TcpListener server = null;
        public event Action<string> OnPacketRecived;

        public TCPServer()
        {
        }
        public void Start()
        {
            Task.Factory.StartNew(Server_Thread,TaskCreationOptions.LongRunning);
        }



        public Task Send(string msg)
        {

            byte[]? j = ConvertHexToBytes(msg);
            var command = new { Type = "RawTelecommand", Content = j };
            var toSend = JObject.FromObject(command);
            byte[] dataToSend = Encoding.Default.GetBytes(toSend.ToString());
            return client.GetStream().WriteAsync(dataToSend, 0, dataToSend.Length);
        }
        private byte[]? ConvertHexToBytes(string str)
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


        public void Stop()
        {
            if (!IsRunning) return;
            client.Close();
            server.Stop();
            IsRunning = false;
        }


        struct Msg
        {
            public string Type;
            public string Content;
        };

        private Task Server_Thread()
        {
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 61015;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);
                Console.WriteLine("" + Environment.NewLine);
                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data;
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

                                    string packetRecived = String.Join(" ", sArr);
                                    OnPacketRecived.Invoke(packetRecived);
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




    public class UDPServer: IPacketServer
    {
        private Socket _serverSocket;
        private Socket _cilentSocket;

        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        public bool IsRunning { get; private set; } = false;
        public event Action<string> OnPacketRecived;

        private class State
        {
            public byte[] buffer = new byte[bufSize];
        }


        public UDPServer()
        {
            _cilentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
        public void Start()
        {
            IsRunning = true;
            Server((int)ProgramProps.settings.UDP_ports.server);
            Client("127.0.0.1", (int)ProgramProps.settings.UDP_ports.client);
        }


        internal void Server(int port)
        {
            _serverSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _serverSocket.EnableBroadcast = true;
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            Receive(_serverSocket);
        }
        internal void Client(string address, int port)
        {
            _cilentSocket.Connect(IPAddress.Parse(address), port);
            Receive(_cilentSocket); //TODO: figure this [[HYPERLINK BLOCKED]] out - do we even need this?
        }


        public Task Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            _cilentSocket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State? so = (State?)ar.AsyncState;
                int bytes = _cilentSocket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, text);
            }, state);
            return Task.CompletedTask;
        }

        public void Stop()
        {
            if (!IsRunning) return;
            _cilentSocket.Close();
            _serverSocket.Close();
            IsRunning = false;
        }

        private void Receive(Socket socket)
        {
            socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State? so = (State?)ar.AsyncState;
                int bytes = socket.EndReceiveFrom(ar, ref epFrom);
                socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));

                string packet = Encoding.ASCII.GetString(so.buffer, 0, bytes);
                OnPacketRecived.Invoke(packet);

            }, state);
        }
    }
}