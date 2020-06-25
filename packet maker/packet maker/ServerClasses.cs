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
        private delegate void delegateCall(String txt);
        private Thread childThread = null;
        private TcpClient client = null;
        private TcpListener server = null;



        public void Start()
        {
            Main.frm.connectBtn.Enabled = false;
            try
            {
                System.Diagnostics.Process.Start(@"C:\Users\pc\Desktop\GSC\GSC-EndNode\GSC-EndNode.exe");
            }
            catch { }

            if (childThread == null)
            {
                ThreadStart childref = new ThreadStart(Server_Thread);
                childThread = new Thread(childref);
                childThread.Start();
            }
        }

        private byte[] ConvertHexToBytes(string str)
        {
            string[] bytesAsStrings = str.Split(' ');
            byte[] toReturn = new byte[bytesAsStrings.Length];
            for (int idx = 0; idx < toReturn.Length; idx++)
            {
                bool isSuccessful = byte.TryParse(bytesAsStrings[idx], System.Globalization.NumberStyles.HexNumber, null, out byte b);
                if (!isSuccessful)
                {
                    return null;
                }
                toReturn[idx] = b;
            }
            return toReturn;
        }


        public void Stop() => Kill_Server_Thread("");
        public  async void Send(string msg)
        {
            await Task.Run(() => {
                if (server != null && client != null)
                {
                    byte[] j = ConvertHexToBytes(msg);
                    cmd command = new cmd { Type = "RawTelecommand", Content = j };
                    var toSend = JObject.FromObject(command);
                    byte[] dataToSend = Encoding.Default.GetBytes(toSend.ToString());
                    client.GetStream().Write(dataToSend, 0, dataToSend.Length);
                }
                else
                {
                    MessageBox.Show("server is not online", "error");
                }
            });
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

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {

                        Console.WriteLine("");
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Msg msg1 = JsonConvert.DeserializeObject<Msg>(data);
                        Console.WriteLine($"Received: {data}");


                        Console.WriteLine("");
                        switch (msg1.Type)
                        {
                            case "EndNode":
                                // Process the data sent by the client.
                                data = @"{'Type': 'ClientId', 'Content': 4}";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

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
                                delegateCall delegateCall = new delegateCall(Main.frm.trasBtn_click);
                                Main.frm.BeginInvoke(delegateCall, packetRecived);
                                break;
                        }
                        Console.WriteLine("*******************");
                    }
                }
            }
            catch
            {
            }
            delegateCall Call = new delegateCall(Kill_Server_Thread);
            Main.frm.BeginInvoke(Call, "");
        }

        private void Kill_Server_Thread(string h)
        {
            if (childThread != null)
            {
                childThread.Abort();
            }
            if (client != null && server != null)
            {
                client.Close();
                server.Stop();
            }

            client = null;
            server = null;
            childThread = null;
            Main.frm.connectBtn.Enabled = true;
        }


    }
}
