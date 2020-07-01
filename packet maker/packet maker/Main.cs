//#define DB

using Google.Cloud.Firestore; 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        public List<string> rawRxPacHisList = new List<string>();

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

            IDTxb.Text = Program.settings.pacCurId.ToString();
            groupsCB.SelectedIndex = 2;
            frm = this;
        }

        #endregion


        private async Task Upload_Packet(string COLLECTION_NAME, string id, string packetString)
        {
            await Task.Run(() => {

                if (Program.settings.dataBaseEnabled)
                {
                    packet.packetString = packetString;
                    packet.time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

                    DocumentReference docRef = Program.db.Collection(COLLECTION_NAME).Document();

                    docRef.SetAsync(packet);
                }
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
            string len = length.ToString("X4");
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
        }


        private packetObject po = new packetObject();
        private void RX(string transMsg)
        {
            mess = transMsg.Trim();
            mess = mess.Replace(" ", String.Empty);
            mess = Regex.Replace(mess, ".{2}", "$0 ");

            po = packetObject.create(transOptions, mess);
            traID = po.id;




            transOut.Items.Clear();
            transOut.Items.Add("Satlite: " + po.sateliteGroup);
            transOut.Items.Add("ID: " + po.id);
            transOut.Items.Add("type: " + po.getTypeName());
            transOut.Items.Add("subtype: " + po.getSubTypeName());
            transOut.Items.Add("length: " + po.length);
            if (po.data.Count != 0)
            {
                transOut.Items.Add("*******************************");
                for (int i = 0; i < po.data.Count; i++)
                {
                    transOut.Items.Add(po.jsonObject.typenum[po.getTypeDex()].subTypes[po.getSubTypeDex()].parmas[i].name + ": " + po.data[i]);
                }
            }
        }



        #region click events

        private async void OkBtn_click()
        {
            success = false;
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

            if (success)
            {
                await Upload_Packet("tx packets", IDTxb.Text, makeOut.Text);
                IDTxb.Text = (int.Parse(IDTxb.Text)+1).ToString();
            }
        }
        public async void trasBtn_click(string msg)
        {
            mess = msg.Trim();
            if (packetObject.TestIfPacket(mess, transOptions))
            {
                await Upload_Packet("rx packets", traID.ToString(), mess);
                po = packetObject.create(transOptions, msg.Trim());
                rawRxPacHisList.Add(mess);
                addItemToPrivHex($"{po.getTypeName()} - {po.getSubTypeName()}  |||  ID:{po.id}");
            }
            else
            {
                rawRxPacHisList.Add(mess);
                addItemToPrivHex("ERROR");
            }
        }

        private void addItemToPrivHex(string diaplay)
        {
            if(privHex.SelectedIndex != -1)
            {
                privHex.Items.Add($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}]   {diaplay}");


                if (privHex.Items.Count - 2 == privHex.SelectedIndex)
                    privHex.SelectedIndex = privHex.Items.Count - 1;
            }
            else
            {
                privHex.Items.Add($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}]   {diaplay}");
                privHex.SelectedIndex = 0;
            }
        }

        private int traID;
        private string mess = "";
        private void trasBtn_Click(object sender, EventArgs e) => trasBtn_click(transIn.Text);

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


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm.Hide();
            frm2.ShowDialog();
        }

        private void copyBTN_Click(object sender, EventArgs e) 
        {
            OkBtn_click();
            Clipboard.SetText(makeOut.Text); 
        }


        private void pasteBTN_Click(object sender, EventArgs e) => transIn.Text = Clipboard.GetText();




        private void dataTypesDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataTypesDGV.EditingControl is DataGridViewComboBoxEditingControl editingControl)
                editingControl.DroppedDown = true;
        }



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

        private void privHex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (privHex.SelectedItem.ToString().Substring(24) != "ERROR") 
            {
                transIn.Text = rawRxPacHisList[privHex.SelectedIndex];
                RX(rawRxPacHisList[privHex.SelectedIndex]);
            }
            else
            {
                transOut.Items.Clear();
                transOut.Items.Add("Manager was not able to translate this packet:");
                transOut.Items.Add(rawRxPacHisList[privHex.SelectedIndex]);
            }

        }

        #endregion





        #region Server

        private readonly RadioServer RadioServer = new RadioServer();
        private void connectBtn_Click(object sender, EventArgs e) => RadioServer.Start();


        private async void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.settings.dataBaseEnabled)
            {
                IdDoc id = new IdDoc { id = int.Parse(IDTxb.Text) };
                DocumentReference docRef = Program.db.Collection("local data").Document("packetCurId");
                await docRef.SetAsync(id);
            }


            try
            {
                RadioServer.Stop();
            }
            catch
            {

            }
        }

        private async void sendPacketBtn_Click(object sender, EventArgs e)
        {
            OkBtn_click();
            await RadioServer.Send(makeOut.Text.Trim());
            TabControl.SelectedTab = RxTab;
        }




        #endregion

        private async void sendImgReqBtn_Click(object sender, EventArgs e)
        {
            string Tid = Convert.ToInt32(imgIdTxb.Text).ToString("X6");
            string[] TidArr = Regex.Replace(Tid, ".{2}", "$0 ").Split(' ');
            Array.Reverse(TidArr);
            
           await RadioServer.Send($"{String.Join(" ",TidArr)} 02 02 E1 01 00 {(imgTypeCB.SelectedIndex+1):X2}");
        }
    }
}

