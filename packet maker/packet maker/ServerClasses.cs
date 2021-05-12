using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace packet_maker
{
    class Msg
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
    }

    class cmd
    {
        public string Type { get; set; }
        public byte[] Content { get; set; }
    }

    class RadioServer
    {
        private Thread childThread = null;
        private TcpClient client = null;
        private TcpListener server = null;
        private UDPSocket UDPserver = null;
        private UDPSocket UDPclient = null;

        public bool isOnline;
        public string serverMode = null;


        public void Start(string mode)
        {
            serverMode = mode;
            switch (mode)
            {
                case "TCP":
                    if (childThread != null) break;

                    ThreadStart childref = new ThreadStart(Server_Thread);
                    childThread = new Thread(childref)
                    {
                        IsBackground = true
                    };
                    childThread.Start();
                    break;
                case "UDP":
                    UDPclient = new UDPSocket();
                    UDPserver = new UDPSocket();
                    UDPserver.Server(Program.settings.UDP_ports.server, Main.frm);
                    UDPclient.Client("127.0.0.1", Program.settings.UDP_ports.client);
                    isOnline = true;
                    break;
            }

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


        public void Stop() { if (serverMode == "TCP") Kill_Server_Thread(); }

        public async Task Send(string msg)
        {
            switch (serverMode)
            {
                case "TCP":
                    await Task.Run(() => {
                        if (isOnline)
                        {
                            byte[] j = ConvertHexToBytes(msg);
                            cmd command = new cmd { Type = "RawTelecommand", Content = j };
                            var toSend = JObject.FromObject(command);
                            byte[] dataToSend = Encoding.Default.GetBytes(toSend.ToString());
                            client.GetStream().Write(dataToSend, 0, dataToSend.Length);
                        }
                        else
                        {
                            MessageBox.Show("server is not online", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                    break;
                case "UDP":
                    await Task.Run(() => UDPclient.Send(msg));
                    break;
            }

        }

        private void Server_Thread()
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
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    isOnline = true;
                    data = null;

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
                                    data = $@"{{'Type': 'ClientId', 'Content': {@Program.settings.tcpPortNumber}}}";
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
                                    Action<string> clickCall = Main.frm.trasBtn_click;
                                    Main.frm.BeginInvoke(clickCall, packetRecived);
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
                try
                {
                    isOnline = false;
                    Action actionCall = Kill_Server_Thread;
                    Main.frm.BeginInvoke(actionCall);
                }
                finally
                {

                }
            }
        }

        private void Kill_Server_Thread()
        {
            if (isOnline)
            {
                client.Close();
                server.Stop();
            }

            if (childThread != null)
            {
                if (childThread.IsAlive)
                {
                    childThread.Interrupt();
                    childThread.Join(0);
                }
            }



            client = null;
            server = null;
            childThread = null;
            isOnline = false;
            Main.frm.connectBtn.Enabled = true;
        }

    }


    public class UDPSocket
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        private Main serverForm = null;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Server(int port, Main serverForm)
        {
            this.serverForm = serverForm;
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.EnableBroadcast = true;
            // _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            _socket.Bind(new IPEndPoint(IPAddress.Any, port));
            Receive();
        }

        public void Client(string address, int port)
        {
            _socket.Connect(IPAddress.Parse(address), port);
            Receive();
        }

        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, text);
            }, state);
        }

        private void Receive()
        {
            _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));


                Action<string> clickCall = serverForm.trasBtn_click;
                serverForm.BeginInvoke(clickCall, Encoding.ASCII.GetString(so.buffer, 0, bytes));


            }, state);
        }
    }

}
