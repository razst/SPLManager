//#define DB
#if DB 
using Google.Cloud.Firestore; 
#endif
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace packet_maker
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        #region setup
        private List<DataGridViewComboBoxCell> cList = new List<DataGridViewComboBoxCell>();
        private TypeList options;
        private TypeList transOptions;
        private bool success = false;
        private Packet packet = new Packet();

        private string[] groups =
        {
            "Any",
            "T1OFK (Ofakim)",
            "T2YRC (Yerucham)",
            "T3TYB (Taybe)",
            "T4ATA (Kiryat Ata)",
            "T5SNG (Shaar HaNegev)",
            "T6NZR (Nazareth)",
            "T7ADM (Maale Adomim)",
            "T8GBS (Guvat Shmuel)"
        };
        public static About frm2 = new About();
        static public Main frm = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            //http://jsonviewer.stack.hu/
            StreamReader rx = new StreamReader(@"rx.json");
            StreamReader tx = new StreamReader(@"tx.json");

            JsonSerializer Serializer = new JsonSerializer();

            options = (TypeList)Serializer.Deserialize(tx, typeof(TypeList));
            transOptions = (TypeList)Serializer.Deserialize(rx, typeof(TypeList));


            foreach (Type t in options)
            {
                typeCB.Items.Add(t.name);
            }
            foreach (string s in groups)
            {
                groupsCB.Items.Add(s);
            }


            groupsCB.SelectedIndex = 2;
            frm = this;
        }

        #endregion


        private async void Upload_Packet(string COLLECTION_NAME, string id, string packetString)
        {
            await Task.Run(() => {

#if DB
                            if (!Program.testMode )
                            {
                                packet.packetString = packetString;

                                DocumentReference docRef = Program.db.Collection(COLLECTION_NAME).Document();

                                docRef.SetAsync(packet);
                            }
#endif

            });

        }

        private void TX()
        {
            int length = 0;
            foreach (Params par in options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas)
            {
                switch (par.type)
                {
                    case "int":
                    case "date":
                    case "datetime":
                        length += 4;
                        break;

                    case "char":
                        length++;
                        break;

                    case "short":
                        length += 2;
                        break;

                    case "bytes":
                        length += (dataTypesDGV.Rows[0].Cells[1].Value.ToString().Length + 1) / 3;
                        break;
                }
            }

            DateTime dt = new DateTime();
            long unix = 0;

            string type = Convert.ToInt32(options.typenum[typeCB.SelectedIndex].id).ToString("X2");
            string subtype = Convert.ToInt32(options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].id).ToString("X2");
            string len = length.ToString("X8");
            string id = Convert.ToInt32(IDTxb.Text).ToString("X6");
            string satNum = groupsCB.SelectedIndex.ToString("X2");


            string hexID = Regex.Replace(id, ".{2}", "$0 ");
            string hexLen = Regex.Replace(len, ".{2}", "$0 ");


            string[] revLen = hexLen.Split(' ');
            string[] revID = hexID.Split(' ');


            Array.Reverse(revID);
            Array.Reverse(revLen);


            if (length != 0)
            {
                string data = "";
                int CBi = cList.Count - 1;
                for (int i = dataTypesDGV.Rows.Count - 1; i >= 0; i--)
                {
                    switch (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type)
                    {
                        case "int":
                            data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X8");
                            break;

                        case "char":
                            if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].values == null)
                            {
                                data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X2");
                            }
                            else
                            {
                                data += Convert.ToInt32(options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].values[cList[CBi].Items.IndexOf(dataTypesDGV.Rows[i].Cells[1].Value.ToString())].id).ToString("X2");
                                CBi--;
                            }
                            break;

                        case "date":
                        case "datetime":
                            dt = DateTime.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString());
                            unix = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                            data += Convert.ToInt64(unix).ToString("X8");
                            break;

                        case "short":
                            data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X4");
                            break;

                        case "bytes":
                            makeOut.Text = String.Join(" ", revID) + " " + satNum + " " + type + " " + subtype + String.Join(" ", revLen) + " " + dataTypesDGV.Rows[i].Cells[1].Value.ToString().ToUpper();
                            makeOut.Text = makeOut.Text.ToString().Substring(1);
                            return;

                    }
                }
                string hexData = Regex.Replace(data, ".{2}", "$0 ");
                string[] revData = hexData.Split(' ');
                Array.Reverse(revData);
                makeOut.Text = (String.Join(" ", revID) + " " + satNum + " " + type + " " + subtype + String.Join(" ", revLen) + String.Join(" ", revData)).Substring(1);
            }
            else
            {
                makeOut.Text = (String.Join(" ", revID) + " " + satNum + " " + type + " " + subtype + String.Join(" ", revLen)).Substring(1);
            }
            //string t = (string)(JValue)"////";
        }
        private void RX(string transMsg)
        {
            mess = transMsg.Trim();
            mess = mess.Replace(" ", String.Empty);
            mess = Regex.Replace(mess, ".{2}", "$0 ");

            packetObject po = packetObject.create(transOptions, mess);
            traID = po.id;




            transOut.Items.Clear();
            transOut.Items.Add("Satlite: " + po.sateliteGroup);
            transOut.Items.Add("ID: " + po.id);
            transOut.Items.Add("type: " + po.getTypeName());
            transOut.Items.Add("subtype: " + po.getSubTypeName());
            transOut.Items.Add("length: " + po.length);
            transOut.Items.Add("");
            transOut.Items.Add("");
            if (po.data.Count != 0)
            {
                transOut.Items.Add("Data:");
                transOut.Items.Add("");
                for (int i = 0; i < po.data.Count; i++)
                {
                    transOut.Items.Add(po.jsonObject.typenum[po.getTypeDex()].subTypes[po.getSubTypeDex()].parmas[i].name + ": " + po.data[i]);
                }
            }
        }



        #region click events

        private void OkBtn_click()
        {
            success = false;
            if (Program.testMode)
            {
                TX();
                success = true;
            }
            else
            {
                try
                {
                    TX();
                    success = true;
                }
                catch
                {
                    MessageBox.Show("invaled input", "error");
                    success = false;
                }
            }


            if (success)
            {
                Upload_Packet("tx packets", IDTxb.Text, makeOut.Text);
            }
        }
        private void trasBtn_click(string msg)
        {
            success = false;
            if (Program.testMode)
            {
                RX(msg);
                success = true;
            }
            else
            {
                try
                {
                    RX(msg);
                    success = true;
                }

                catch
                {
                    MessageBox.Show("manager was not able to translate this packet", "error");
                    success = false;
                }
            }

            if (success)
            {
                Upload_Packet("rx packets", traID.ToString(), msg);
                if (privHex.SelectedIndex != -1)
                {
                    if (mess != privHex.Items[privHex.SelectedIndex].ToString())
                    {
                        privHex.Items.Add(mess);
                        privHex.SelectedIndex = privHex.Items.Count - 1;
                    }
                }
                else
                {
                    privHex.Items.Add(mess);
                    privHex.SelectedIndex = 0;
                }
            }

        }

        private void OkBtn_Click(object sender, EventArgs e) { OkBtn_click(); }

        private int traID;
        private string mess;
        private void trasBtn_Click(object sender, EventArgs e) { trasBtn_click(transIn.Text); }

        #endregion



        #region rest of code




        private void typeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            subtypeCB.Items.Clear();
            foreach (SubType sub in options.typenum[typeCB.SelectedIndex])
            {
                subtypeCB.Items.Add(sub.name);
            }
            descType.Text = "Description: " + options.typenum[typeCB.SelectedIndex].desc;
            descSubType.Text = "Description:";
            subtypeCB.Text = "";
            dataTypesDGV.Visible = false;
            dataTypesDGV.Rows.Clear();
            subtypeCB.SelectedIndex = 0;
        }

        private void subtypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            descSubType.Text = "Description: " + options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].desc;
            if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas.Count() != 0)
            {
                dataTypesDGV.Rows.Clear();
                dataTypesDGV.Visible = true;
                int i = 0;
                bool bit = false;
                cList.Clear();
                foreach (Params par in options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas)
                {
                    dataTypesDGV.Rows.Add(par.name, "", par.desc);
                    if (par.type == "bytes")
                    {
                        bit = true;
                        break;
                    }
                    if (par.values != null)
                    {
                        cList.Add(new DataGridViewComboBoxCell());
                        foreach (vars item in par.values)
                        {
                            cList[i].Items.Add(item.name);
                        }
                        dataTypesDGV.Rows[i].Cells[1] = cList[i];
                        dataTypesDGV.Rows[i].Cells[1].Value = cList[i].Items[0];
                    }
                    if (par.type == "date")
                    {
                        dataTypesDGV.Rows[i].Cells[1].Value = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    if (par.type == "datetime")
                    {
                        dataTypesDGV.Rows[i].Cells[1].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    }
                    i++;

                }
                dataTypesDGV.AutoResizeColumns();
                if (bit)
                {
                    this.dataTypesDGV.Columns[1].Width = 456;
                }
                else
                {
                    this.dataTypesDGV.Columns[1].Width = 200;
                }

            }
            else
            {
                dataTypesDGV.Visible = false;

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            transIn.Text = privHex.Items[privHex.SelectedIndex].ToString();
            trasBtn.PerformClick();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm.Hide();
            frm2.ShowDialog();
        }

        private void copyBTN_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(makeOut.Text);
        }

        private void pasteBTN_Click(object sender, EventArgs e)
        {
            transIn.Text = Clipboard.GetText();
        }



        private void dataTypesDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var editingControl = this.dataTypesDGV.EditingControl as DataGridViewComboBoxEditingControl;
            if (editingControl != null)
                editingControl.DroppedDown = true;
        }

        #endregion


        private void tXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jsonFrm frm = new jsonFrm
            {
                usedJson = options
            };
            frm.ShowDialog();
        }

        private void rXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jsonFrm frm = new jsonFrm
            {
                usedJson = transOptions
            };
            frm.ShowDialog();
        }

        private async void connectBtn_Click(object sender, EventArgs e)
        {
            connectBtn.Enabled = false;
            await Task.Run(() => {

                TcpListener server = null;
                try
                {
                    // Set the TcpListener on port 13000.
                    Int32 port = 61015;
                    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                    // TcpListener server = new TcpListener(port);
                    server = new TcpListener(localAddr, port);

                    // Start listening for client requests.
                    server.Start();

                    // Buffer for reading data
                    Byte[] bytes = new Byte[256];
                    String data = null;

                    // Enter the listening loop.
                    while (true)
                    {
                        MessageBox.Show("Waiting for a connection... ");



                        // Perform a blocking call to accept requests.
                        // You could also use server.AcceptSocket() here.
                        TcpClient client = server.AcceptTcpClient();
                        MessageBox.Show("Connected!");

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
                                    //data = data.ToUpper();
                                    data = "{'Type': 'ClientId', 'Content': 4}";
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
                                    Console.WriteLine(packetRecived);
                                    Console.WriteLine("");
                                    break;
                            }
                            Console.WriteLine("*******************");
                        }

                        // Shutdown and end connection
                        client.Close();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show($"SocketException: {ex}");
                }
                finally
                {
                    // Stop listening for new clients.
                    server.Stop();
                }
            });
        }
    }
}

