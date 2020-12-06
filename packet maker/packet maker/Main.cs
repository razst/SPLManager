//#define DB

using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
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
using Microsoft.Office.Interop;
using Nest;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;

namespace packet_maker
{
    public partial class Main : Form
    {

        private packetObject po;
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
        private TypeList options;
        private TypeList transOptions;
        private bool success = false;
        private readonly RadioServer RadioServer = new RadioServer();
        static public Main frm = null;

        enum Packet_Mode
        {
            /// <summary>
            /// the packet is sent to the satelite
            /// </summary>
            satelite = 1,

            /// <summary>
            /// the packet is sent to the database
            /// </summary>
            database = 2,

            /// <summary>
            /// the packet is not sent anywhere
            /// </summary>
            none = 0
        }


        public Main()
        {
            InitializeComponent();
        }


        #region setup


        private  void Form1_Load(object sender, EventArgs e)
        {
            //http://jsonviewer.stack.hu/
            StreamReader rx = new StreamReader(@"rx.json");
            StreamReader tx = new StreamReader(@"tx.json");

            JsonSerializer Serializer = new JsonSerializer();

            options = (TypeList)Serializer.Deserialize(tx, typeof(TypeList));
            transOptions = (TypeList)Serializer.Deserialize(rx, typeof(TypeList));

            if (!Program.settings.enableImage)
            {
                TabControl.TabPages.Remove(ImageTab);
            }

            if (Program.settings.dataBaseEnabled)
            {
                PoupolatePL();
            }

            //filling comboboxes
            foreach (Type t in options)
            {
                typeCB.Items.Add(t.name);
            }
            foreach (string s in groups)
            {
                groupsCB.Items.Add(s);
                RxGroupsCB.Items.Add(s);
                qrySatCB.Items.Add(s);
                MainSatCB.Items.Add(s);
            }
            foreach (Type t in transOptions)
            {
                foreach (SubType st in t)
                {
                    qrySubtypeCB.Items.Add(st.name);
                    subTypes.Add(st);
                }
            }
            //

            minExportDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            maxExportDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            qryMinDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            qryMaxDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            qryCondvalDtp.CustomFormat = "dd/MM/yyyy HH:mm";

            imgIdTxb.Text = Program.settings.pacCurId.ToString();

            groupsCB.SelectedIndex = Program.settings.defultSatGroup;
            RxGroupsCB.SelectedIndex = Program.settings.defultSatGroup;
            MainSatCB.SelectedIndex = Program.settings.defultSatGroup;
            DBLimitCB.SelectedItem = "100";
            qrySubtypeCB.SelectedItem = "All";
            qrySatCB.SelectedIndex = Program.settings.defultSatGroup;
            qryConditionCB.SelectedIndex = 1;
            qryLimitCB.SelectedIndex = 1;

            frm = this;
            everySecTimer.Start();

            //next pass setup
            nextPassLabels[0] = this.nextPass1Label;
            nextPassLabels[1] = this.nextPass2Label;
            nextPassLabels[2] = this.nextPass3Label;
            nextPassLabels[3] = this.nextPass4Label;
            //
            
            VBatGauge.ColorofG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = 7.5F,maxVal = 9 , threshVal = 8.25F, threshPrecent = 16.66F }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 0,maxVal = 7 , threshVal = 3.5F , threshPrecent = 77.77F}},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 7,maxVal = 7.5F , threshVal = 7.25F, threshPrecent = 5.55F}}


            };
            OBCGauge.ColorofG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = -15F,maxVal = 30, threshVal = 7.5F, threshPrecent= 39 }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 45,maxVal = 80 , threshVal = 62.5F , threshPrecent = 32 }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 30,maxVal = 45 , threshVal = 37.5F , threshPrecent = 13}},
                {"bad2", new AquaControls.valuesOfColors{color = Color.Red , minVal = -40,maxVal = -20 , threshVal = -30F , threshPrecent = 20 }},
                {"notgreat2", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = -20,maxVal = -15 , threshVal = -17.5F , threshPrecent = 4.6F}},
            };
            BatTempGauge.ColorofG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = -15F,maxVal = 30, threshVal = 7.5F, threshPrecent= 39 }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 45,maxVal = 80 , threshVal = 62.5F , threshPrecent = 32 }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 30,maxVal = 45 , threshVal = 37.5F , threshPrecent = 13}},
                {"bad2", new AquaControls.valuesOfColors{color = Color.Red , minVal = -40,maxVal = -20 , threshVal = -30F , threshPrecent = 20 }},
                {"notgreat2", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = -20,maxVal = -15 , threshVal = -17.5F , threshPrecent = 4.6F}},
            };
            FreeSpaceGauge.ColorofG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = 800,maxVal = 2200, threshVal = 1500, threshPrecent= 63.63F }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 0,maxVal = 300 , threshVal = 150 , threshPrecent = 13.63F }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 300 ,maxVal = 800 , threshVal = 550 , threshPrecent = 22.72F}},
            };
        }



        private void Main_Shown(object sender, EventArgs e)
        {
            RadioServer.Start();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RadioServer.Stop();
            }
            catch
            {

            }
        }

        #endregion

        #region general 
        private async Task<int> getSplCurIdAsync(int groupDex)
        {
            if (Program.settings.dataBaseEnabled)
            {

                var searchResponse = await Program.db.SearchAsync<Dictionary<string, object>>(s => s
                .Query(q => q.Ids(c => c.Values($"SAT{groupDex}")))
                .Index("local-data"));

                Program.settings.pacCurId = int.Parse(searchResponse.Documents.First().First().Value.ToString());

                Program.settings.pacCurId += 1;

                Dictionary<string, object> dd = new Dictionary<string, object> { { "CommandId", Program.settings.pacCurId } };
                var indexResponse = await Program.db.IndexAsync(new IndexRequest<Dictionary<string, object>>(dd, "local-data", $"SAT{groupDex}"));
            }
            else
            {
                Program.settings.pacCurId = 20;
            }


            return Program.settings.pacCurId;
        }

        private string ConvertToHexBytes(int value, int numberOfBytes)
        {
            string format = "X" + (numberOfBytes * 2);
            string Tid = value.ToString(format);
            string[] TidArr = Regex.Replace(Tid, ".{2}", "$0 ").Trim().Split(' ');
            Array.Reverse(TidArr);
            return String.Join(" ", TidArr).Trim();
        }
        private async Task Upload_Packet(string COLLECTION_NAME, packetObject ogPacket)
        {

            if (Program.settings.dataBaseEnabled)
            {
                Dictionary<string, object> DBPacket = new Dictionary<string, object>
                {
                    {"packetString",ogPacket.rawPacket },
                    {"time",DateTime.UtcNow }
                };


                if (ogPacket.type != -1)
                {
                    DBPacket.AddRange(new Dictionary<string, object>
                    {
                        {"splID",ogPacket.id },
                        {"type",ogPacket.getTypeName() },
                        {"subtype",ogPacket.getSubTypeName() },
                        {"lenght",ogPacket.length },
                        {"satId",ogPacket.getSatDex() },
                        {"satName", ogPacket.sateliteGroup }
                        
                    });
                    DBPacket.AddRange(ogPacket.dataCatalog);
                }
                else
                {
                    DBPacket.Add("type", "Error");
                }

                var indexResponse = await Program.db.IndexAsync(new IndexRequest<Dictionary<string, object>>(DBPacket, COLLECTION_NAME));

            }
        }

        private void addItemToListbox(string diaplay, ListBox list)
        {
            if (list.SelectedIndex != -1)
            {
                list.Items.Add(diaplay);


                if (list.Items.Count - 2 == privHex.SelectedIndex)
                    list.SelectedIndex = privHex.Items.Count - 1;
            }
            else
            {
                list.Items.Add(diaplay);
                list.SelectedIndex = 0;
            }
        }

        private  void everySecTimer_Tick(object sender, EventArgs e)
        {
                mainUtcLabel.Text = DateTime.UtcNow.ToString();
        }

        #endregion





        #region txTab

        #region playlists

        #region objects

        private List<PLInfo> Playlists = new List<PLInfo>();
        private List<packetObject> commands = new List<packetObject>();


        #endregion

        #region controls

        #region main controls

        private void NewListBtn_Click(object sender, EventArgs e)
        {
            NewPlaylistDialog playlistDialog = new NewPlaylistDialog();
            playlistDialog.ShowDialog();

            //GOTO newPLitem() in funcs
        }


        private async void SendListBtn_Click(object sender, EventArgs e)
        {
            if (RadioServer.isOnline)
            {
                var tem = Playlists[PlaylistCB.SelectedIndex].commands;

                int i = 0;
                Action<int> f = sendAutoWorker;
                foreach (var packet in tem)
                {
                    string pac = (string)Invoke(f, i);
                    await Task.Delay(int.Parse(sleepCmdTxb.Text));

                    po = new packetObject(options, makeOut.Text.Trim());
                    rawTxPacHisList.Add(po);
                    addItemToListbox(po.ToHeaderString(DateTime.Now), TxPacLibx);
                    i++;

                }
            }
            else
            {
                MessageBox.Show("server is not online", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void add2PLBtn_Click(object sender, EventArgs e)
        {

            CreateTxPacket(256, Packet_Mode.database);
            packetObject po = new packetObject(options, makeOut.Text.Trim());
            PLitemsLibx.Items.Add(po.getSubTypeName());
            commands.Add(po);
            Playlists[PlaylistCB.SelectedIndex].commands.Add(po.rawPacket);
            PLitemsLibx.SelectedIndex = PLitemsLibx.Items.Count - 1;
        }

        private async void SaveListBtn_Click(object sender, EventArgs e)
        {
            if (Program.settings.dataBaseEnabled)
            {
                Playlists[PlaylistCB.SelectedIndex].sleepBetweenCommands = int.Parse(sleepCmdTxb.Text);



                var response = await Program.db.IndexAsync(new IndexRequest<PLInfo>(Playlists[PlaylistCB.SelectedIndex], "playlist-info", PlaylistCB.SelectedItem.ToString()));


            }
        }

        private async void DelListBtn_Click(object sender, EventArgs e)
        {
            if (PlaylistCB.SelectedIndex != -1 && Program.settings.dataBaseEnabled)
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PLitemsLibx.Items.Clear();
                    sleepCmdTxb.Text = "";
                    Playlists.RemoveAt(PlaylistCB.SelectedIndex);
                    var DelResult = await Program.db.DeleteAsync<dynamic>(
                        PlaylistCB.SelectedItem.ToString(),
                        d => d.Index("playlist-info"));

                    PlaylistCB.Items.RemoveAt(PlaylistCB.SelectedIndex);
                }
        }

        #endregion


        private void PlaylistCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            PLitemsLibx.Items.Clear();
            commands.Clear();
            foreach (var item in Playlists[PlaylistCB.SelectedIndex].commands)
            {
                packetObject po = new packetObject(options, item);
                PLitemsLibx.Items.Add(po.getSubTypeName());
                commands.Add(po);
            }

            sleepCmdTxb.Text = Playlists[PlaylistCB.SelectedIndex].sleepBetweenCommands.ToString();
        }

        private void PLitemsLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex != -1)
            {
                CastToDGV(commands[PLitemsLibx.SelectedIndex]);

                CreateTxPacket(Program.settings.pacCurId, Packet_Mode.none);
            }
        }

        private void DelLItemBtn_Click(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex != -1)
            {
                int T = PLitemsLibx.SelectedIndex;
                commands.RemoveAt(PLitemsLibx.SelectedIndex);
                Playlists[PlaylistCB.SelectedIndex].commands.RemoveAt(PLitemsLibx.SelectedIndex);
                PLitemsLibx.Items.RemoveAt(PLitemsLibx.SelectedIndex);

                if (PLitemsLibx.Items.Count != 0)
                {
                    if (T == PLitemsLibx.Items.Count) PLitemsLibx.SelectedIndex = T - 1;
                    else PLitemsLibx.SelectedIndex = T;
                }
            }
        }

        private void MoveupBtn_Click(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex != -1 && PLitemsLibx.SelectedIndex != 0)
            {
                commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex - 1);
                Playlists[PlaylistCB.SelectedIndex].commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex - 1);

                PLitemsLibx.Items.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex - 1);
                PLitemsLibx.SelectedIndex--;
            }
        }

        private void MonedownBtn_Click(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex != -1 && PLitemsLibx.SelectedIndex != PLitemsLibx.Items.Count - 1)
            {
                commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex + 1);
                Playlists[PlaylistCB.SelectedIndex].commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex + 1);

                PLitemsLibx.Items.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex + 1);
                PLitemsLibx.SelectedIndex++;
            }
        }
        #endregion


        #region funcs
        public async void PoupolatePL()
        {
            var searchResponse = await Program.db.SearchAsync<PLInfo>(s => s
                    .Index("playlist-info")
                    .Query(q => q.MatchAll()));

            Playlists = searchResponse.Documents.ToList();

            foreach (var list in Playlists)
            {
                PlaylistCB.Items.Add(list.name);
            }
        }

        private void CastToDGV(packetObject packet)
        {
            typeCB.SelectedItem = packet.getTypeName();
            subtypeCB.SelectedItem = packet.getSubTypeName();

            int i = 0;
            if (packet.dataCatalog.Count != 0)
                foreach (DataGridViewRow item in dataTypesDGV.Rows)
                {
                    item.Cells[1].Value = packet.dataCatalog.ElementAt(i).Value;
                    i++;
                }
        }

        private async void sendAutoWorker(int index)
        {
            await getSplCurIdAsync(groupsCB.SelectedIndex);
            PLitemsLibx.SelectedIndex = -1;
            PLitemsLibx.SelectedIndex = index;
        }

        public async void newPLItem(string item)
        {
            PlaylistCB.Items.Add(item);
            Playlists.Add(new PLInfo { commands = new List<string>(), name = item, sleepBetweenCommands = 0 });
            PlaylistCB.SelectedIndex = PlaylistCB.Items.Count - 1;

            var indexResponse = await Program.db.IndexAsync(new IndexRequest<PLInfo>(
                new PLInfo { commands = new List<string>(), name = item, sleepBetweenCommands = 0 },
                "playListInfo",
                "item"));

        }

        #endregion

        #endregion

        #region sending tx packets


        #region funcs

        private void TX(int CurrId, Packet_Mode mode)
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
                        length += (dataTypesDGV.Rows[0].Cells[1].Value.ToString().Trim().Length + 1) / 3;
                        break;
                }
            }

            DateTime dt = new DateTime();
            long unix = 0;

            string type = ConvertToHexBytes(options.typenum[typeCB.SelectedIndex].id, 1);
            string subtype = ConvertToHexBytes(options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].id, 1);
            string len = ConvertToHexBytes(length, 2);
            string id = ConvertToHexBytes(CurrId, 3);
            string satNum = ConvertToHexBytes(groupsCB.SelectedIndex, 1);



            if (length != 0)
            {
                string data = "";
                int CBi = cList.Count - 1;
                for (int i = dataTypesDGV.Rows.Count - 1; i >= 0; i--)
                {
                    string curValue = dataTypesDGV.Rows[i].Cells[1].Value.ToString();
                    switch (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type)
                    {
                        case "int":
                            data += Convert.ToInt32(curValue).ToString("X8");
                            break;

                        case "char":
                            if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].values == null)
                            {
                                data += Convert.ToInt32(curValue).ToString("X2");
                            }
                            else
                            {
                                data += Convert.ToInt32(options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].values[cList[CBi].Items.IndexOf(curValue)].id).ToString("X2");
                                CBi--;
                            }
                            break;

                        case "date":
                        case "datetime":
                            if (mode != Packet_Mode.database)
                            {
                                if (curValue.Substring(0, 3) != "now")
                                {
                                    dt = DateTime.Parse(curValue);
                                    unix = (int)(dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                }
                                else if (dataTypesDGV.Rows[i].Cells[1].Value.ToString() == "now")
                                {
                                    dt = DateTime.Now;
                                    unix = (int)(dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                }
                                else
                                {
                                    unix = curValue[3].ToString().Operator((int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds, int.Parse(curValue.Substring(4)));
                                }

                                data += Convert.ToInt64(unix).ToString("X8");
                            }
                            else
                            {
                                if (curValue.Substring(0, 3) != "now")
                                {
                                    dt = DateTime.Parse(curValue);
                                    unix = (int)(dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                    data += Convert.ToInt64(unix).ToString("X8");
                                }
                                else if (curValue == "now")
                                {
                                    data += "000000NN";
                                }
                                else
                                {
                                    string p = ConvertToHexBytes(int.Parse(curValue.Substring(4)), 3).Replace(" ", String.Empty) + "N" + curValue[3];
                                    //String h = int.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString().Substring(4)).ToString("X6").Trim() + "NN";
                                    data += p;
                                }
                            }

                            break;

                        case "short":
                            data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X4");
                            break;

                        case "bytes":
                            makeOut.Text = id + " " + satNum + " " + type + " " + subtype + " " + len + " " + dataTypesDGV.Rows[i].Cells[1].Value.ToString().ToUpper();
                            return;

                    }
                }
                string[] revData = Regex.Replace(data, ".{2}", "$0 ").Trim().Split(' ');
                Array.Reverse(revData);
                makeOut.Text = id + " " + satNum + " " + type + " " + subtype + " " + len + " " + String.Join(" ", revData);
            }
            else
            {
                makeOut.Text = (id + " " + satNum + " " + type + " " + subtype + " " + len);
            }
        }

        private async void CreateTxPacket(int id, Packet_Mode mode)
        {
            success = false;
            try
            {
                TX(id, mode);
                success = true;
            }
            catch
            {
                MessageBox.Show("invaled input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }
            finally
            {
                if (success && mode == Packet_Mode.satelite)
                {
                    await Upload_Packet("parsed-tx", new packetObject(options, makeOut.Text));
                }
            }
        }


        #endregion


        #region controls


        #region main controls

        private async void copyBTN_Click(object sender, EventArgs e)
        {
            CreateTxPacket(await getSplCurIdAsync(groupsCB.SelectedIndex), Packet_Mode.none);

            if (makeOut.Text != null && makeOut.Text != "")
                Clipboard.SetText(makeOut.Text.ToString());
        }
        private async void sendPacketBtn_Click(object sender, EventArgs e)
        {
            if (RadioServer.isOnline)
            {
                CreateTxPacket(await getSplCurIdAsync(groupsCB.SelectedIndex), Packet_Mode.satelite);

                po = new packetObject(options, makeOut.Text.Trim());
                rawTxPacHisList.Add(po);
                addItemToListbox(po.ToHeaderString(DateTime.Now), TxPacLibx);

                await RadioServer.Send(makeOut.Text.Trim());
                TabControl.SelectedTab = RxTab;
            }
            else
            {
                MessageBox.Show("server is not online", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion


        private void dataTypesDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataTypesDGV.EditingControl is DataGridViewComboBoxEditingControl editingControl)
                editingControl.DroppedDown = true;
        }

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
                    dataTypesDGV.Columns[1].Width = 456;
                }
                else
                {
                    dataTypesDGV.Columns[1].Width = 200;
                }

            }
            else
            {
                dataTypesDGV.Visible = false;

            }
        }

        #endregion

        #region objects
        private List<DataGridViewComboBoxCell> cList = new List<DataGridViewComboBoxCell>();
        #endregion

        #endregion
        #endregion






        #region qryTab
        #region controls

        #region main controls
        private async void qryStartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                qryClearBtn.PerformClick();
                UseWaitCursor = true;
                clearRxBtn.PerformClick();

                int qrySize = 20000;
                if (qryLimitCB.SelectedItem.ToString() != "No Limit")
                {
                    qrySize = int.Parse(qryLimitCB.SelectedItem.ToString());
                }

                int rxQryCount = 0;
                int txQryCount = 0;

                if (qryRxChbx.Checked)
                {
                    #region rxHistory


                    //define query
                    var RxQryFilters = new List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>>
                    {
                        q => q.DateRange(
                            dr => dr
                            .Field("time")
                            .LessThanOrEquals(qryMaxDtp.Value.ToUniversalTime())
                            .GreaterThanOrEquals(qryMinDtp.Value.ToUniversalTime()))
                    };


                    if (qrySatCB.SelectedIndex != 0)
                    {
                        RxQryFilters.Add(fq => fq.Match(
                            m => m.Field("satId")
                                  .Query(qrySatCB.SelectedIndex.ToString())));
                    }
                    if (qrySubtypeCB.SelectedItem.ToString() != "All")
                    {
                        RxQryFilters.Add(fq => fq.Term(
                            f => f["subtype"].Suffix("keyword"),
                            qrySubtypeCB.SelectedItem.ToString()));
                    }
                    if (fieldOptionsPnl.Visible && qryFieldCB.SelectedIndex != -1 && (qryCondvalDtp.Visible || qryCondvalTxb.Text != ""))
                    {
                        switch (qryConditionCB.SelectedItem.ToString())
                        {
                            case "<":
                                if (qryCondvalDtp.Visible)
                                {
                                    RxQryFilters.Add(q => q.DateRange(
                                        dr => dr
                                        .Field(qryFieldCB.SelectedItem.ToString())
                                        .LessThan(qryCondvalDtp.Value.ToUniversalTime())));
                                }
                                else
                                {
                                    RxQryFilters.Add(q => q.Range(
                                        r => r
                                        .Field(qryFieldCB.SelectedItem.ToString())
                                        .LessThan(int.Parse(qryCondvalTxb.Text))));
                                }
                                break;
                            case ">":
                                if (qryCondvalDtp.Visible)
                                {
                                    RxQryFilters.Add(q => q.DateRange(
                                        dr => dr
                                        .Field(qryFieldCB.SelectedItem.ToString())
                                        .GreaterThan(qryCondvalDtp.Value.ToUniversalTime())));
                                }
                                else
                                {
                                    RxQryFilters.Add(q => q.Range(
                                        r => r
                                        .Field(qryFieldCB.SelectedItem.ToString())
                                        .GreaterThan(int.Parse(qryCondvalTxb.Text))));
                                }
                                break;
                            case "=":
                                if (qryCondvalDtp.Visible)
                                {
                                    RxQryFilters.Add(q => q.Match(
                                        dr => dr
                                        .Field(qryFieldCB.SelectedItem.ToString())
                                        .Query(qryCondvalDtp.Value.ToUniversalTime().ToString())));
                                }
                                else
                                {
                                    RxQryFilters.Add(q => q.Match(
                                        r => r
                                        .Field(qryFieldCB.SelectedItem.ToString())
                                        .Query(qryCondvalTxb.Text)));
                                }
                                break;
                            case "!=":
                                if (qryCondvalTxb.Visible)
                                {
                                    RxQryFilters.Add(q => q.Bool(
                                        b => b.MustNot(
                                            g => g.Match(
                                                mc => mc
                                                .Field(qryFieldCB.SelectedItem.ToString())
                                                .Query(qryCondvalTxb.Text)))));
                                }
                                break;
                        }
                    }



                    dynamic RxSnapshot = null;
                    //
                    if (!qryIdChbx.Checked)
                    {
                        RxSnapshot = await Program.db.SearchAsync<Dictionary<string, object>>(s => s

                            .Query(q => q.Bool(ft => ft.Must(RxQryFilters)))
                            .Index("parsed-rx")
                            .Size(qrySize)
                            .Sort(q => q.Ascending(obj => obj["time"])));

                    }
                    else
                    {
                        RxSnapshot = await Program.db.SearchAsync<Dictionary<string, object>>(s => s

                            .Query(q => q.Bool(ft => ft.Must(qry => qry.MatchPhrase(mt => mt.Field("splID").Query(qryIdTxb.Text)))))
                            .Index("parsed-rx")
                            .Size(qrySize)
                            .Sort(q => q.Ascending(obj => obj["time"])));
                    }

                    foreach (var doc in RxSnapshot.Documents)
                    {
                        po = await Task.Run(() =>
                        {
                            return new packetObject(transOptions, doc["packetString"].ToString());
                        });

                        RxPacQryList.Add(po);

                        RxPacQryLibx.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                    }
                    #endregion

                    rxQryCount = RxSnapshot.Documents.Count;
                }

                if (qryTxChbx.Checked)
                {
                    #region txHistory


                    //define query
                    var TxQryFilters = new List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>>
                {
                    q => q.DateRange(
                        dr => dr
                        .Field("time")
                        .LessThanOrEquals(qryMaxDtp.Value.ToUniversalTime())
                        .GreaterThanOrEquals(qryMinDtp.Value.ToUniversalTime()))
                };


                    if (qrySatCB.SelectedIndex != 0)
                    {
                        TxQryFilters.Add(fq => fq.MatchPhrase(
                            m => m.Field("satId")
                                  .Query(qrySatCB.SelectedIndex.ToString())));
                    }
                    //

                    var TxSnapshot = await Program.db.SearchAsync<Dictionary<string, object>>(s => s

                        .Query(q => q.Bool(ft => ft.Must(TxQryFilters)))
                        .Index("parsed-tx")
                        .Size(qrySize)
                        .Sort(q => q.Ascending(obj => obj["time"])));

                    foreach (var doc in TxSnapshot.Documents)
                    {
                        po = await Task.Run(() =>
                        {
                            return new packetObject(options, doc["packetString"].ToString());
                        });

                        TxPacQryList.Add(po);

                        TxPacQryLibx.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                    }
                    #endregion

                    txQryCount = TxSnapshot.Documents.Count;
                }

                MessageBox.Show($"{rxQryCount} RX packet were recived. {Environment.NewLine}{txQryCount} TX packet were recived.", "Query finished", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                UseWaitCursor = false;
            }

        }

        #endregion

        private void qryFieldChbx_CheckedChanged(object sender, EventArgs e)
        {
            fieldOptionsPnl.Visible = !fieldOptionsPnl.Visible;
        }

        private void qryIdChbx_CheckedChanged(object sender, EventArgs e)
        {
            qryIdTxb.ReadOnly = !qryIdTxb.ReadOnly;
        }

        private void qryFieldCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (currentParams[qryFieldCB.SelectedIndex].type)
            {
                case "date":
                case "datetime":
                    qryCondvalDtp.Visible = true;
                    qryCondvalTxb.Visible = false;
                    break;
                default:
                    qryCondvalDtp.Visible = false;
                    qryCondvalTxb.Visible = true;
                    break;
            }
        }


        private void TxPacQryLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            PacQryOutput.Text = TxPacQryList[TxPacQryLibx.SelectedIndex].ToString();
        }

        private void RxPacQryLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            PacQryOutput.Text = RxPacQryList[RxPacQryLibx.SelectedIndex].ToString();
        }

        private void qryClearBtn_Click(object sender, EventArgs e)
        {
            RxPacQryLibx.Items.Clear();
            TxPacQryLibx.Items.Clear();
            RxPacQryList.Clear();
            TxPacQryList.Clear();
            PacQryOutput.Clear();
        }

        private void qryTx2RxBtn_Click(object sender, EventArgs e)
        {
            if (TxPacQryLibx.SelectedIndex != -1)
            {
                int dex = RxPacQryList.FindIndex(x => x.id == TxPacQryList[TxPacQryLibx.SelectedIndex].id);
                if (dex != -1)
                {
                    RxPacQryLibx.SelectedIndex = dex;
                }
            }
        }

        private void qryRx2TxBtn_Click(object sender, EventArgs e)
        {
            if (RxPacQryLibx.SelectedIndex != -1)
            {
                int dex = TxPacQryList.FindIndex(x => x.id == RxPacQryList[RxPacQryLibx.SelectedIndex].id);
                if (dex != -1)
                {
                    TxPacQryLibx.SelectedIndex = dex;
                }
            }
        }

        private void qrySubtypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentParams.Clear();
            qryFieldCB.Items.Clear();
            if (qrySubtypeCB.SelectedIndex != 0)
            {
                currentParams = subTypes[qrySubtypeCB.SelectedIndex - 1].parmas;
                foreach (var field in currentParams)
                {
                    qryFieldCB.Items.Add(field.name);
                }
            }

        }

        #endregion



        #region objects
        public List<packetObject> RxPacQryList = new List<packetObject>();
        public List<packetObject> TxPacQryList = new List<packetObject>();
        private List<SubType> subTypes = new List<SubType>();
        private List<Params> currentParams = new List<Params>();
        #endregion
        #endregion

        #region rxTab
        #region controls

        #region main controls
        private void trasBtn_Click(object sender, EventArgs e) => trasBtn_click(transIn.Text);

        //
        private async void loadDbBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                clearRxBtn.PerformClick();

                int qrySize = 20000;
                if (DBLimitCB.SelectedItem.ToString() != "No Limit")
                {
                    qrySize = int.Parse(DBLimitCB.SelectedItem.ToString());
                }

                #region rxHistory


                //define query
                var RxQryFilters = new List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>>
                {
                    q => q.DateRange(
                        dr => dr
                        .Field("time")
                        .LessThanOrEquals(maxExportDateDtp.Value.ToUniversalTime())
                        .GreaterThanOrEquals(minExportDateDtp.Value.ToUniversalTime()))
                };


                if (RxGroupsCB.SelectedIndex != 0)
                {
                    RxQryFilters.Add(fq => fq.Match(
                        m => m.Field("satId")
                              .Query(RxGroupsCB.SelectedIndex.ToString())));
                }
                if (qrySubtypeCB.SelectedItem.ToString() != "All")
                {
                    RxQryFilters.Add(fq => fq.Match(
                        m => m.Field("subtype")
                                .Query(qrySubtypeCB.SelectedItem.ToString())));
                }
                //

                var RxSnapshot = await Program.db.SearchAsync<Dictionary<string, object>>(s => s

                    .Query(q => q.Bool(ft => ft.Must(RxQryFilters)))
                    .Index("parsed-rx")
                    .Size(qrySize)
                    .Sort(q => q.Ascending(obj => obj["time"])));

                foreach (var doc in RxSnapshot.Documents)
                {
                    po = await Task.Run(() => {
                        return new packetObject(transOptions, doc["packetString"].ToString());
                    });

                    rawRxPacHisList.Add(po);

                    privHex.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                }
                #endregion

                #region txHistory


                //define query
                var TxQryFilters = new List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>>
                {
                    q => q.DateRange(
                        dr => dr
                        .Field("time")
                        .LessThanOrEquals(maxExportDateDtp.Value.ToUniversalTime())
                        .GreaterThanOrEquals(minExportDateDtp.Value.ToUniversalTime()))
                };


                if (RxGroupsCB.SelectedIndex != 0)
                {
                    TxQryFilters.Add(fq => fq.Match(
                        m => m.Field("satId")
                              .Query(RxGroupsCB.SelectedIndex.ToString())));
                }
                //

                var TxSnapshot = await Program.db.SearchAsync<Dictionary<string, object>>(s => s

                    .Query(q => q.Bool(ft => ft.Must(RxQryFilters)))
                    .Index("parsed-tx")
                    .Size(qrySize)
                    .Sort(q => q.Ascending(obj => obj["time"])));

                foreach (var doc in TxSnapshot.Documents)
                {
                    po = await Task.Run(() => {
                        return new packetObject(options, doc["packetString"].ToString());
                    });

                    rawTxPacHisList.Add(po);

                    TxPacLibx.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                }
                #endregion

                MessageBox.Show($"{RxSnapshot.Documents.Count} RX packet were recived. {Environment.NewLine}{TxSnapshot.Documents.Count} TX packet were recived.", "Query finished", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                UseWaitCursor = false;
            }

        }
        //

        #endregion





        private void privHex_SelectedIndexChanged(object sender, EventArgs e)
        {
            transIn.Text = rawRxPacHisList[privHex.SelectedIndex].rawPacket;
            transOut.Text = rawRxPacHisList[privHex.SelectedIndex].ToString();
        }

        private void TxPacLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            transIn.Text = rawTxPacHisList[TxPacLibx.SelectedIndex].rawPacket;
            transOut.Text = rawTxPacHisList[TxPacLibx.SelectedIndex].ToString();
        }

        private void clearRxBtn_Click(object sender, EventArgs e)
        {
            privHex.Items.Clear();
            TxPacLibx.Items.Clear();
            transIn.Clear();
            transOut.Clear();
            rawRxPacHisList.Clear();
            rawTxPacHisList.Clear();
        }

        private void pasteBTN_Click(object sender, EventArgs e) => transIn.Text = Clipboard.GetText();

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connectBtn.Enabled = false;
            try
            {
                System.Diagnostics.Process.Start(Program.settings.endNodePath);
            }
            catch
            { connectBtn.Enabled = true; }
        }

        private void tx2rxBtn_Click(object sender, EventArgs e)
        {
            if (TxPacLibx.SelectedIndex != -1)
            {
                int dex = rawRxPacHisList.FindIndex(x => x.id == rawTxPacHisList[TxPacLibx.SelectedIndex].id);
                if (dex != -1)
                {
                    privHex.SelectedIndex = dex;
                }
            }
        }

        private void rx2txBtn_Click(object sender, EventArgs e)
        {
            if (privHex.SelectedIndex != -1)
            {
                int dex = rawTxPacHisList.FindIndex(x => x.id == rawRxPacHisList[privHex.SelectedIndex].id);
                if (dex != -1)
                {
                    TxPacLibx.SelectedIndex = dex;
                }
            }
        }

        private async void resendTxBtn_Click(object sender, EventArgs e)
        {
            string id = ConvertToHexBytes(await getSplCurIdAsync(RxGroupsCB.SelectedIndex), 3);
            string tPac = $"{id} 0{groupsCB.SelectedIndex}{rawTxPacHisList[TxPacLibx.SelectedIndex].rawPacket.Substring(11)}";
            await RadioServer.Send(tPac); //0-9 only

            po = new packetObject(options, tPac);
            rawTxPacHisList.Add(po);
            addItemToListbox(po.ToHeaderString(DateTime.Now), TxPacLibx);
            await Upload_Packet("tx-packets", po);
        }
        #endregion


        #region funcs
        public async void trasBtn_click(string msg)
        {
            string mess = msg.Trim();
            po = new packetObject(transOptions, mess);
            if (po.type != -1)
            {
                if (po.sateliteGroup == RxGroupsCB.SelectedItem.ToString() || RxGroupsCB.SelectedIndex == 0)
                {
                    rawRxPacHisList.Add(po);
                    addItemToListbox(po.ToHeaderString(DateTime.Now), privHex);
                }
            }
            else
            {
                rawRxPacHisList.Add(po);
                addItemToListbox(po.ToHeaderString(DateTime.Now), privHex);
            }
            await Upload_Packet("parsed-rx", po);
        }

        #endregion


        #region objects
        public List<packetObject> rawRxPacHisList = new List<packetObject>();
        public List<packetObject> rawTxPacHisList = new List<packetObject>();
        #endregion
        #endregion

        #region menu


        #region export
        #region controls
        private void toAFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.settings.enableExcel)
            {
                saveFileDialog.Filter = "excel|*.xlsx";
            }
            else
            {
                saveFileDialog.Filter = "excel|*.csv";
            }
            saveFileDialog.Title = "Export history to a File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                int j = 0;
                List<List<string>> T = new List<List<string>> { new List<string> { "Date", "Id", "SatGroup", "Type", "Subtype", "Lenght", "Raw packet", "Data" } };
                foreach (var packet in rawRxPacHisList)
                {
                    if (packet.type != -1)
                    {
                        string dataStr = "";
                        for (int i = 0; i < packet.dataCatalog.Count; i++)
                        {
                            dataStr += $"{packet.dataCatalog.ElementAt(i).Key}: {packet.dataCatalog.ElementAt(i).Value}. ";
                        }
                        T.Add(new List<string> { privHex.Items[j].ToString().Split('[', ']')[1], packet.id.ToString(), packet.sateliteGroup, packet.getTypeName(), packet.getSubTypeName(), packet.length.ToString(), packet.rawPacket, dataStr });
                    }
                    else
                    {
                        T.Add(new List<string> { privHex.Items[j].ToString().Split('[', ']')[1], "-1", "-", "ERROR", "-", "-", packet.rawPacket, "manager was not able to translate this packet" });
                    }


                    j++;
                }
                try
                {
                    Application.UseWaitCursor = true;
                    CreateCSV(T, saveFileDialog.FileName);
                }
                finally
                {
                    Application.UseWaitCursor = false;
                }
            }
        }

        private void toAFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "|";
            saveFileDialog.Title = "Export history to a Folder";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<FilePacket> type_subtype = new List<FilePacket>();
                List<List<string>> errorTable = new List<List<string>> { new List<string> { "Date", "Packet" } };

                //sorting packets to files
                int j = 0;
                foreach (var packet in rawRxPacHisList)
                {
                    string timeStr = privHex.Items[j].ToString().Split('[', ']')[1];

                    if (packet.type != -1)
                    {
                        int match = type_subtype.FindIndex(x => x.packetPrefix == $"{packet.getTypeName()}_{packet.getSubTypeName()}");
                        if (match != -1)
                        {
                            type_subtype[match].packets.Add(new PacketWithTime(packet, timeStr));
                        }
                        else
                        {
                            var colStr = new List<string> { "Date", "Id", "SatGroup", "Type", "Subtype", "Lenght", "Raw data" };
                            colStr.AddRange(packet.dataCatalog.Keys.ToList());

                            type_subtype.Add(new FilePacket
                            {
                                packetPrefix = $"{packet.getTypeName()}_{packet.getSubTypeName()}",
                                packets = new List<PacketWithTime> { new PacketWithTime(packet, timeStr) },
                                firstColumn = colStr
                            });
                        }
                    }
                    else
                    {
                        errorTable.Add(new List<string> { timeStr, packet.rawPacket });
                    }


                    j++;
                }
                //

                try
                {
                    Application.UseWaitCursor = true;
                    //creating all files
                    foreach (var file in type_subtype)
                    {
                        List<List<string>> fileTble = new List<List<string>> { file.firstColumn };
                        foreach (var pac in file.packets)
                        {
                            List<string> tm = new List<string>
                            {
                                pac.time,
                                pac.id.ToString(),
                                pac.sateliteGroup,
                                pac.getTypeName(),
                                pac.getSubTypeName(),
                                pac.length.ToString(),
                                pac.rawPacket
                            };

                            foreach (var item in pac.dataCatalog.Values)
                            {
                                tm.Add(item.ToString());
                            }
                            fileTble.Add(tm);
                        }

                        if (Program.settings.enableExcel)
                        {
                            CreateCSV(fileTble, $"{saveFileDialog.FileName}_{file.packetPrefix}.xlsx");
                        }
                        else
                        {
                            CreateCSV(fileTble, $"{saveFileDialog.FileName}_{file.packetPrefix}.csv");
                        }
                    }
                    //


                    //creating errors file
                    if (errorTable.Count > 1)
                    {
                        if (Program.settings.enableExcel)
                        {
                            CreateCSV(errorTable, $"{saveFileDialog.FileName}_Errors.xlsx");
                        }
                        else
                        {
                            CreateCSV(errorTable, $"{saveFileDialog.FileName}_Errors.csv");
                        }
                    }
                }
                finally
                {
                    Application.UseWaitCursor = false;
                }

            }
        }

        #endregion


        #region funcs
        private void CreateCSV(List<List<string>> table, string fileName)
        {
            try
            {
                if (Program.settings.enableExcel)
                {
                    Microsoft.Office.Interop.Excel.Application oXL;
                    Microsoft.Office.Interop.Excel._Workbook oWB;
                    Microsoft.Office.Interop.Excel._Worksheet oSheet;


                    //Start Excel and get Application object.
                    oXL = new Microsoft.Office.Interop.Excel.Application
                    {
                        Visible = false,
                        UserControl = false
                    };

                    object m = System.Type.Missing;

                    //Get a new workbook.
                    oWB = oXL.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                    oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                    oSheet.DisplayRightToLeft = false;

                    for (int i = 0; i < table.Count; i++)
                    {
                        for (int j = 0; j < table[i].Count; j++)
                        {
                            oSheet.Cells[i + 1, j + 1] = table[i][j];
                        }
                    }

                    oSheet.Columns.AutoFit();

                    oWB.SaveAs(
                        fileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault,
                        m, m, m, m,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        m, m, m, m, m);


                    oWB.Close();
                    oXL.Quit();
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(
                        fileName,
                        FileMode.Create,
                        FileAccess.Write)))
                    {
                        writer.WriteLine("sep=,");
                        foreach (var line in table)
                        {
                            writer.WriteLine(String.Join(", ", line));
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("SPL manager was not able to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region objects
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        #endregion
        #endregion

        #region controls

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm.Hide();
            frm2.ShowDialog();
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

        #endregion




        #region objects
        public static About frm2 = new About();
        #endregion

        #endregion




        #region main tab


        #region controls

        #region main controls
        private async void nextPassTimer_Tick(object sender, EventArgs e)
        {
            bool stopped = false;
            string temp = "00:00:00";
            if (!passOccured)
            await Task.Run(() => {
                secondsTilPass--;
                if (secondsTilPass == 0)
                {
                    passOccured = true;
                }
                else if (secondsTilPass == 60)
                {
                    nextPassTimer.Stop();

                    var dt =  GetNextPass().GetAwaiter().GetResult();
                    secondsTilPass = GetSecondsTil(dt);
                    UpdatePassesLabels();

                    TimeSpan time = TimeSpan.FromSeconds(secondsTilPass);
                    temp = time.ToString(@"hh\:mm\:ss");

                    nextUpdateTimer.Stop();
                    nextUpdateTimer.Start();
                    stopped = true;
                }
                else
                {
                    TimeSpan time = TimeSpan.FromSeconds(secondsTilPass);
                    temp = time.ToString(@"hh\:mm\:ss");
                }

            });
            nextPassLabel.Text = temp;
            if (stopped)
            {
                nextPassTimer.Start();
            }
            if (passOccured)
            {
                if ((long)passesData.passes[0].endUTC + 60 < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                {
                    nextPassTimer.Stop();
                    mainLastPassLabel.Text = passesData.passes[0].endUTC.ToString();
                    var dt = await GetNextPass();
                    secondsTilPass = GetSecondsTil(dt);
                    UpdatePassesLabels();
                    nextUpdateTimer.Start();
                    passOccured = false;
                    nextPassTimer.Start();
                }
            }
        }


        private async void MainSatCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainLastRxLabel.Text = "---";
            mainLastTxLabel.Text = "---";
            await UpdateControlFormDB("parsed-rx", mainLastRxLabel);
            await UpdateControlFormDB("parsed-tx", mainLastTxLabel);
             await UpdateControlFormDB("parsed-rx", mainLastBeaconLabel,"Beacon");
            if(MainSatCB.SelectedIndex != 0)
            {
                nextPassDate = await GetNextPass();
                secondsTilPass = GetSecondsTil(nextPassDate);
                UpdatePassesLabels();
                nextPassTimer.Start();

            }
            else
            {
                nextPassTimer.Stop();
            }    

            nextUpdateTimer.Start();
        }
        #endregion

        private async void nextUpdateTimer_Tick(object sender, EventArgs e)
        {
            nextPassTimer.Stop();
            var dt = await GetNextPass();
            secondsTilPass = GetSecondsTil(dt);
            UpdatePassesLabels();
            nextPassTimer.Start();
        }

        #endregion


        #region funcs
        private async Task<DateTime> GetNextPass()
        {
            DateTime dt = new DateTime();
            dynamic t;
            try
            {
                 t = ((IEnumerable<dynamic>)Program.settings.satInfo).AsEnumerable().ToList().Find(x => x.id == MainSatCB.SelectedIndex).noradID;
            }
            catch
            {
                t = 43199;
                //TODO: show error
            }

            var t2 = Program.settings.groundStationLocation;
            dynamic data = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://api.n2yo.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response =  await client.GetAsync($"/rest/v1/satellite/radiopasses/{t}/{t2.lat}/{t2.lng}/{t2.alt}/7/{t2.minElevation}/&apiKey=78C29S-25XY9W-E8SWZD-4L2F");
                if (response.IsSuccessStatusCode)
                {
                    var txtResponse =  await response.Content.ReadAsStringAsync();

                    data = Json.Decode(txtResponse);
                }
            }
            if (data != null)
            {
                long time = (long)data.passes[0].startUTC;

                dt = DateTimeOffset.FromUnixTimeSeconds(time).LocalDateTime;

                passesData.info = data.info;
                passesData.passes = ((IEnumerable<dynamic>)data.passes).ToList();


            }

            return dt;
        }
        private long GetSecondsTil(DateTime dt)
        {
            return (dt.Ticks - DateTime.Now.Ticks)/ TimeSpan.TicksPerSecond;
        }

        private void UpdatePassesLabels()
        {
            for (int i = 0; i < 4; i++)
            {
                nextPassLabels[i].Text = DateTimeOffset.FromUnixTimeSeconds((long)passesData.passes[i + 1].startUTC).LocalDateTime.ToString();
            }
        }

        private async Task UpdateControlFormDB(string collection, Label label, string type = null)
        {
            var QryFilters = new List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>>
            {
                q => q.Match(m => m.Field("satId").Query(MainSatCB.SelectedIndex.ToString()))
            };
            if(type != null)
            {
                QryFilters.Add(fq => fq.Term(f => f["subtype"].Suffix("keyword"),type));
            }

            var lastTimeSnapshot = await Program.db.SearchAsync<Dictionary<string, object>>(s => s

                            .Query(q => q.Bool(m => m.Must(QryFilters)))
                            .Index(collection)
                            .Size(10)
                            .Sort(q => q.Descending(obj => obj["time"])));

            if (lastTimeSnapshot.Documents.Count > 0)
            {
                var temp = DateTime.Parse(lastTimeSnapshot.Documents.First()["time"].ToString()).ToLocalTime().ToString();
                label.Text = temp;
                if(type == "Beacon")
                {
                    po = new packetObject(transOptions, lastTimeSnapshot.Documents.First()["packetString"].ToString());
                    VBatGauge.Value =  float.Parse(po.dataCatalog["vbat"].ToString())/1000;
                    OBCGauge.Value = float.Parse(po.dataCatalog["MCU Temperature"].ToString())/100;
                    BatTempGauge.Value = float.Parse(po.dataCatalog["Battery Temperature"].ToString()) / 1000;
                    FreeSpaceGauge.Value = float.Parse(po.dataCatalog["free_memory"].ToString()) / 1000000;
                    MainSatTimeLabel.Text = DateTime.Parse(lastTimeSnapshot.Documents.First()["sat_time"].ToString()).ToLocalTime().ToString();
                    MainSatResetsLabel.Text = lastTimeSnapshot.Documents.First()["number_of_resets"].ToString();
                    MainCmdRestesLabel.Text = lastTimeSnapshot.Documents.First()["number_of_cmd_resets"].ToString();
                    MainCorruptLabel.Text = lastTimeSnapshot.Documents.First()["corrupt_bytes"].ToString();
                    var tempTS = TimeSpan.FromSeconds(int.Parse( lastTimeSnapshot.Documents.First()["sat_uptime"].ToString()));
                    MainSatUptimeLabel.Text = $"{tempTS.Days}:{tempTS.Hours}:{tempTS.Seconds + tempTS.Minutes*60}";

                }
            }

        }
        #endregion


        #region objects
        private DateTime nextPassDate;
        private long secondsTilPass;
        private Label[] nextPassLabels = new Label[4];
        private bool passOccured = false;


        private PassReport passesData = new PassReport();
        class PassReport
        {
            public dynamic info { get; set; }

            public List<dynamic> passes { get; set; }
        }






        #endregion

        #endregion
    }
}

