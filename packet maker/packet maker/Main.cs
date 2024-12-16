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
using Nest;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using packet_maker.MainComponents;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace packet_maker
{
    public partial class Main : Form
    {

        private PacketObject po;

        private Dictionary<string, TypeList> SatRxOptions = new Dictionary<string, TypeList>();
        private TypeList options;
        private TypeList transOptions;
        private TypeList tauTransOptions;
        private readonly RadioServer RadioServer = new RadioServer();
        static public Main frm = null;
        private NextPassWorker NextPass;
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
            frm = this;
            //packet settings
            //http://jsonviewer.stack.hu/

            options = new TypeList(@"tx.json");
            transOptions = new TypeList(@"rx.json");
            tauTransOptions = new TypeList(@"TauRx.json");

            everySecTimer.Start();
            MainLabelsWorker.Start();

            SatRxOptions.Add("TEVEL", transOptions);
            SatRxOptions.Add("TAU", new TypeList(@"TauRx.json"));

            NextPass = new NextPassWorker(Program.settings.defaultSatGroup);
            RxHisTracker = new PacketHistoryTracker(RxGroupsCB, privHex, transOut);

            if (Program.settings.dataBaseEnabled) PoupolatePL();

            //filling comboboxes
            foreach (Type t in options)
            {
                typeCB.Items.Add(t.name);
            }
            foreach (Group s in Program.groups)
            {
                groupsCB.Items.Add(s.Str);
                RxGroupsCB.Items.Add(s.Str);
                qrySatCB.Items.Add(s.Str);
                MainSatCB.Items.Add(s.Str);
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

            //DatePickers
            minExportDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            maxExportDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            qryMinDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            qryMaxDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            qryCondvalDtp.CustomFormat = "dd/MM/yyyy HH:mm";

            //Combo boxes selected index
            groupsCB.SelectedIndex = Program.settings.defaultSatGroup;
            RxGroupsCB.SelectedIndex = Program.settings.defaultSatGroup;
            MainSatCB.SelectedIndex = Program.settings.defaultSatGroup;
            DBLimitCB.SelectedItem = "100";
            qrySubtypeCB.SelectedItem = "All";
            qrySatCB.SelectedIndex = Program.settings.defaultSatGroup;
            qryConditionCB.SelectedIndex = 1;
            qryLimitCB.SelectedIndex = 1;



            //next pass setup
            nextPassLabels[0] = this.nextPass1Label;
            nextPassLabels[1] = this.nextPass2Label;
            nextPassLabels[2] = this.nextPass3Label;
            nextPassLabels[3] = this.nextPass4Label;
            //



            //Gauges Setup
            VBatGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = 7.5F,maxVal = 9 , threshVal = 8.25F, threshPrecent = 16.66F }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 0,maxVal = 7 , threshVal = 3.5F , threshPrecent = 77.77F}},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 7,maxVal = 7.5F , threshVal = 7.25F, threshPrecent = 5.55F}}


            };
            OBCGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = -15F,maxVal = 30, threshVal = 7.5F, threshPrecent= 39 }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 45,maxVal = 80 , threshVal = 62.5F , threshPrecent = 32 }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 30,maxVal = 45 , threshVal = 37.5F , threshPrecent = 13}},
                {"bad2", new AquaControls.valuesOfColors{color = Color.Red , minVal = -40,maxVal = -20 , threshVal = -30F , threshPrecent = 20 }},
                {"notgreat2", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = -20,maxVal = -15 , threshVal = -17.5F , threshPrecent = 4.6F}},
            };
            BatTempGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = -15F,maxVal = 30, threshVal = 7.5F, threshPrecent= 39 }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 45,maxVal = 80 , threshVal = 62.5F , threshPrecent = 32 }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 30,maxVal = 45 , threshVal = 37.5F , threshPrecent = 13}},
                {"bad2", new AquaControls.valuesOfColors{color = Color.Red , minVal = -40,maxVal = -20 , threshVal = -30F , threshPrecent = 20 }},
                {"notgreat2", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = -20,maxVal = -15 , threshVal = -17.5F , threshPrecent = 4.6F}},
            };
            FreeSpaceGauge.ColorOfG = new Dictionary<string, AquaControls.valuesOfColors>
            {
                {"healthy", new AquaControls.valuesOfColors{color = Color.LawnGreen , minVal = 800,maxVal = 2200, threshVal = 1500, threshPrecent= 63.63F }},
                {"bad", new AquaControls.valuesOfColors{color = Color.Red , minVal = 0,maxVal = 300 , threshVal = 150 , threshPrecent = 13.63F }},
                {"notgreat", new AquaControls.valuesOfColors{color = Color.OrangeRed, minVal = 300 ,maxVal = 800 , threshVal = 550 , threshPrecent = 22.72F}},
            };
        }



        private void Main_Shown(object sender, EventArgs e)
        {
            RadioServer.Start(Program.settings.serverMode);
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
            if (!Program.settings.dataBaseEnabled)
            {
                Program.settings.pacCurId = 20;
                return Program.settings.pacCurId;

            }

            var dbCollection = Program.mongoDB.GetCollection<BsonDocument>("local_data");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "SAT"+ groupDex);
            BsonDocument firstDocument = dbCollection.Find(filter).FirstOrDefault();
            Program.settings.pacCurId = int.Parse(firstDocument.GetValue("CommandId").ToString());
            Program.settings.pacCurId += 1;
            var update = Builders<BsonDocument>.Update.Set("CommandId", Program.settings.pacCurId);
            dbCollection.UpdateOne(filter, update);

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
        private List<PacketObject> commands = new List<PacketObject>();


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

                    po = new PacketObject(options, makeOut.Text.Trim());
                    rawTxPacHisList.Add(po);
                    addItemToListbox(po.ToHeaderString(DateTime.Now), TxPacLibx);
                    i++;

                }
            }
            else
            {
                MessageBox.Show("EndNode is not online", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void add2PLBtn_Click(object sender, EventArgs e)
        {
            if (PlaylistCB.SelectedIndex < 0)
            {
                MessageBox.Show("No playlist selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CreateTxPacket(256, Packet_Mode.database);
            PacketObject po = new PacketObject(options, makeOut.Text.Trim());
            PLitemsLibx.Items.Add(po.GetSubTypeName());
            commands.Add(po);
            Playlists[PlaylistCB.SelectedIndex].commands.Add(po.RawPacket);
            PLitemsLibx.SelectedIndex = PLitemsLibx.Items.Count - 1;
        }

        private async void SaveListBtn_Click(object sender, EventArgs e)
        {
            if (Program.settings.dataBaseEnabled)
            {
                Playlists[PlaylistCB.SelectedIndex].sleepBetweenCommands = int.Parse(sleepCmdTxb.Text);


                BsonArray bArray = new BsonArray();
                foreach (var cmd in Playlists[PlaylistCB.SelectedIndex].commands)
                {
                    bArray.Add(cmd);
                }


                var document = new BsonDocument{{ "_id", Playlists[PlaylistCB.SelectedIndex].name},
                                                { "sleepBetweenCommands", int.Parse(sleepCmdTxb.Text) },
                                                {"commands",bArray} };


                var dbCollection = Program.mongoDB.GetCollection<BsonDocument>("playlist_info");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Playlists[PlaylistCB.SelectedIndex].name);
                var update = Builders<BsonDocument>.Update.Set("sleepBetweenCommands", int.Parse(sleepCmdTxb.Text)).Set("commands", bArray); ;
                

                UpdateOptions opts = new UpdateOptions()
                {
                    IsUpsert = true
                };
                dbCollection.UpdateOne(filter, update,opts);

            }
        }

        private async void DelListBtn_Click(object sender, EventArgs e)
        {
            if (PlaylistCB.SelectedIndex != -1 && Program.settings.dataBaseEnabled)
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PLitemsLibx.Items.Clear();
                    sleepCmdTxb.Text = "0";
                    Playlists.RemoveAt(PlaylistCB.SelectedIndex);
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", PlaylistCB.Text);
                    var dbCollection = Program.mongoDB.GetCollection<BsonDocument>("playlist_info");

                    dbCollection.DeleteOne(filter);

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
                PacketObject po = new PacketObject(options, item);
                PLitemsLibx.Items.Add(po.GetSubTypeName());
                commands.Add(po);
            }

            sleepCmdTxb.Text = Playlists[PlaylistCB.SelectedIndex].sleepBetweenCommands.ToString();
        }

        private void PLitemsLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex == -1) return;

            CastToDGV(commands[PLitemsLibx.SelectedIndex]);
            CreateTxPacket(Program.settings.pacCurId, Packet_Mode.none);
        }

        private void DelLItemBtn_Click(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex == -1) return;

            int T = PLitemsLibx.SelectedIndex;
            commands.RemoveAt(PLitemsLibx.SelectedIndex);
            Playlists[PlaylistCB.SelectedIndex].commands.RemoveAt(PLitemsLibx.SelectedIndex);
            PLitemsLibx.Items.RemoveAt(PLitemsLibx.SelectedIndex);

            if (PLitemsLibx.Items.Count == 0) return;
            if (T == PLitemsLibx.Items.Count) PLitemsLibx.SelectedIndex = T - 1;
            else PLitemsLibx.SelectedIndex = T;
        }

        private void MoveupBtn_Click(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex == -1 || PLitemsLibx.SelectedIndex == 0) return;

            commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex - 1);
            Playlists[PlaylistCB.SelectedIndex].commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex - 1);
            PLitemsLibx.Items.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex - 1);
            PLitemsLibx.SelectedIndex--;
        }

        private void MonedownBtn_Click(object sender, EventArgs e)
        {
            if (PLitemsLibx.SelectedIndex == -1 || PLitemsLibx.SelectedIndex == PLitemsLibx.Items.Count - 1) return;

            commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex + 1);
            Playlists[PlaylistCB.SelectedIndex].commands.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex + 1);
            PLitemsLibx.Items.Swap(PLitemsLibx.SelectedIndex, PLitemsLibx.SelectedIndex + 1);
            PLitemsLibx.SelectedIndex++;
        }
        #endregion


        #region funcs
        public async void PoupolatePL()
        {
            Playlists.Clear();
            var dbCollection = Program.mongoDB.GetCollection<BsonDocument>("playlist_info");
            var documents = dbCollection.Find(new BsonDocument()).ToList();
            foreach (BsonDocument doc in documents)
            {
                PLInfo p = new PLInfo { commands = new List<string>(), 
                                            name = doc.GetValue("_id").ToString(), 
                                            sleepBetweenCommands = doc.GetValue("sleepBetweenCommands").ToInt32() };
                //BsonArray bArray = doc.GetElement("_id").Value;
                BsonArray cmds = doc.GetElement("commands").Value.AsBsonArray;
                foreach (var c in cmds)
                {
                    p.commands.Add(c.ToString());
                }
                Playlists.Add(p);
                PlaylistCB.Items.Add(doc.GetValue("_id"));

            }
        }

        private void CastToDGV(PacketObject packet)
        {
            typeCB.SelectedItem = packet.GetTypeName();
            subtypeCB.SelectedItem = packet.GetSubTypeName();

            if (packet.DataCatalog.Count == 0) return;

            int i = 0;
            foreach (DataGridViewRow item in dataTypesDGV.Rows)
            {
                item.Cells[1].Value = packet.DataCatalog.ElementAt(i).Value;
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

        }

        #endregion

        #endregion

        #region sending tx packets


        #region funcs

        private void TX(int CurrId, Packet_Mode mode)
        {
            PacketObject txPac = new PacketObject()
            {
                JsonObject = options,
                Type = typeCB.SelectedIndex,
                Subtype = subtypeCB.SelectedIndex,
                Id = CurrId,
                SateliteGroup = groupsCB.Text
            };

            if(dataTypesDGV.Visible)
                for (int i = 0; i < dataTypesDGV.Rows.Count; i++)
                {
                    string curValue = dataTypesDGV.Rows[i].Cells[1].Value.ToString();
                    string parName = options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].name;
                    txPac.DataCatalog.Add(parName, curValue);
                }

            makeOut.Text = txPac.CastToString((int)mode);
        }

        private async void CreateTxPacket(int id, Packet_Mode mode)
        {
            bool success = false;
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
                    await new PacketObject(options, makeOut.Text).Upload("parsed_tx");
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
            if (!RadioServer.isOnline)
            {
                MessageBox.Show("server is not online", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             
            CreateTxPacket(await getSplCurIdAsync(groupsCB.SelectedIndex), Packet_Mode.satelite);

            po = new PacketObject(options, makeOut.Text.Trim());
            rawTxPacHisList.Add(po);
            addItemToListbox(po.ToHeaderString(DateTime.Now), TxPacLibx);

            await RadioServer.Send(makeOut.Text.Trim());
            TabControl.SelectedTab = RxTab;
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

            if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas.Count() == 0)
            {
                dataTypesDGV.Visible = false;
                return;
            }

            var cList = new List<DataGridViewComboBoxCell>();
            dataTypesDGV.Rows.Clear();
            dataTypesDGV.Visible = true;
            int i = 0;
            bool bit = false;
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

            dataTypesDGV.Columns[1].Width = bit ? 456 : 200;
        }

        #endregion

        #region objects
        #endregion

        #endregion
        #endregion


        #region rxTab
        #region controls

        #region main controls
        private void trasBtn_Click(object sender, EventArgs e) => trasBtn_click(transIn.Text);

        //
        private void loadDbBtn_Click(object sender, EventArgs e) { }
        //

        private void RxGroupsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearRxBtn.PerformClick();
            RxHisTracker.UpdateDisplayGroup();

        }

        #endregion





        private void privHex_SelectedIndexChanged(object sender, EventArgs e)
        {
            transIn.Text = RxHisTracker.GetCurrentPacket().RawPacket;
            transOut.Text = RxHisTracker.GetCurrentPacket().GetDescriptionString();
        }

        private void TxPacLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            transIn.Text = rawTxPacHisList[TxPacLibx.SelectedIndex].RawPacket;
            transOut.Text = rawTxPacHisList[TxPacLibx.SelectedIndex].GetDescriptionString();
        }

        private void clearRxBtn_Click(object sender, EventArgs e)
        {
            privHex.Items.Clear();
            TxPacLibx.Items.Clear();
            transIn.Clear();
            transOut.Clear();
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
            { 
                connectBtn.Enabled = true;
                MessageBox.Show("Unable to start EndNode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tx2rxBtn_Click(object sender, EventArgs e)
        {
            if (TxPacLibx.SelectedIndex == -1) return;
            var dex = RxHisTracker.FindPacketIndexById(rawTxPacHisList[TxPacLibx.SelectedIndex].Id);
            if (dex == -1) return;
            privHex.SelectedIndex = dex;
        }

        private void rx2txBtn_Click(object sender, EventArgs e)
        {
            if (privHex.SelectedIndex == -1) return;
            int dex = rawTxPacHisList.FindIndex(x => x.Id == RxHisTracker.GetCurrentPacket().Id);
            if (dex == -1) return;
            TxPacLibx.SelectedIndex = dex;
        }

        private async void resendTxBtn_Click(object sender, EventArgs e)
        {
            string id = ConvertToHexBytes(await getSplCurIdAsync(RxGroupsCB.SelectedIndex), 3);
            string tPac = $"{id} 0{groupsCB.SelectedIndex}{rawTxPacHisList[TxPacLibx.SelectedIndex].RawPacket.Substring(11)}";
            await RadioServer.Send(tPac); //0-9 only

            po = new PacketObject(options, tPac);
            rawTxPacHisList.Add(po);
            addItemToListbox(po.ToHeaderString(DateTime.Now), TxPacLibx);
            await po.Upload("parsed_tx");
        }
        #endregion


        #region funcs
        public async void trasBtn_click(string msg)
        {
            string mess = msg.Trim().Replace('-', ' ');

            foreach (var RxOption in SatRxOptions)
            {
                int currentGroupDex = -1;
                if (RxOption.Key == "TAU") currentGroupDex = 9;

                po = new PacketObject(RxOption.Value, mess, currentGroupDex);
                if (po.Type != -1) break;
            }

            RxHisTracker.AddPacket(po);

            await po.Upload("parsed_rx");
        }
        #endregion


        #region objects
        private PacketHistoryTracker RxHisTracker;

        public List<PacketObject> rawTxPacHisList = new List<PacketObject>();
        #endregion
        #endregion



        #region qryTab
        #region controls

        #region main controls
        private async void qryStartBtn_Click(object sender, EventArgs e)
        {
            if(!Program.settings.dataBaseEnabled)
            {
                MessageBox.Show("database is disabled in the settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (qrySatCB.SelectedIndex != 0)
            {
                //filter = filter & Builders<BsonDocument>.Filter.Eq("satId", qrySatCB.SelectedIndex.ToString());
            }


            try
            {
                qryClearBtn.PerformClick();
                UseWaitCursor = true;
                clearRxBtn.PerformClick();
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                int qrySize = 20000;
                if (qryLimitCB.SelectedItem.ToString() != "No Limit")
                {
                    qrySize = int.Parse(qryLimitCB.SelectedItem.ToString());
                }

                int rxQryCount = 0;
                int txQryCount = 0;

                if (qryRxChbx.Checked)
                {

                    var dbCollection = Program.mongoDB.GetCollection<BsonDocument>("parsed_rx");

                    //var filter = Builders<BsonDocument>.Filter.Gt("time", qryMinDtp.Value.ToUniversalTime()) &
                    //  Builders<BsonDocument>.Filter.Lt("time", qryMaxDtp.Value.ToUniversalTime());
                    var filter = Builders<BsonDocument>.Filter.Eq("satId", qrySatCB.SelectedIndex);
                          //& Builders<BsonDocument>.Filter.Eq("subtype", "Beacon");
                    var sort = Builders<BsonDocument>.Sort.Descending("time");
                    List<BsonDocument> documents = dbCollection.Find(filter).Limit(qrySize).Sort(sort).ToList(); ;
                    //var temp = DateTime.Parse(firstDocument.GetValue("time").ToString()).ToLocalTime().ToString();


                    foreach (BsonDocument doc in documents)
                    {
                        po = new PacketObject(transOptions, doc["packetString"].ToString());
                        RxPacQryList.Add(po);
                        RxPacQryLibx.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                    }


                    /*
                    #region rxHistory

                    //define worker logic
                    var RxQryWorker = new DataBaseWorker();
                    RxQryWorker.AddDateRangeFilter("time", qryMinDtp.Value.ToUniversalTime(), qryMaxDtp.Value.ToUniversalTime());
                    if (qrySatCB.SelectedIndex != 0)
                    {
                        RxQryWorker.AddMatchFilter("satId", qrySatCB.SelectedIndex.ToString());
                    }
                    if (qrySubtypeCB.SelectedItem.ToString() != "All")
                    {
                        RxQryWorker.AddTermFilter("subtype", qrySubtypeCB.SelectedItem.ToString());
                    }
                    if (fieldOptionsPnl.Visible && qryFieldCB.SelectedIndex != -1 && (qryCondvalDtp.Visible || qryCondvalTxb.Text != ""))
                    {
                        string field = qryFieldCB.SelectedItem.ToString();
                        var dtpValue = qryCondvalDtp.Value.ToUniversalTime();
                        switch (qryConditionCB.SelectedItem.ToString())
                        {
                            case "<":
                                if (qryCondvalDtp.Visible)
                                {
                                    RxQryWorker.AddDateRangeFilter(field, null, dtpValue);
                                }
                                else
                                {
                                    RxQryWorker.AddRangeFilter(field, null, int.Parse(qryCondvalTxb.Text));
                                }
                                break;
                            case ">":
                                if (qryCondvalDtp.Visible)
                                {
                                    RxQryWorker.AddDateRangeFilter(field, dtpValue, null);
                                }
                                else
                                {
                                    RxQryWorker.AddRangeFilter(field, int.Parse(qryCondvalTxb.Text), null);
                                }
                                break;
                            case "=":
                                string match = qryCondvalDtp.Visible? 
                                    dtpValue.ToString() : qryCondvalTxb.Text;
                                RxQryWorker.AddMatchFilter(field, match);
                                break;
                            case "!=":
                                if (qryCondvalTxb.Visible)
                                {
                                    RxQryWorker.AddMustNotMatchFilter(field, qryCondvalTxb.Text);
                                }
                                break;
                        }
                    }

                    //id only search
                    if (qryIdChbx.Checked)
                    {
                        RxQryWorker.QryFilters.Clear();
                        RxQryWorker.AddMatchPhraseFilter("splID", qryIdTxb.Text);
                    }

                    //search
                    await RxQryWorker.StartWork(qrySize, "parsed-rx", "time");


                    foreach (var doc in RxQryWorker.Documents)
                    {
                        po =  new PacketObject(transOptions, doc["packetString"].ToString());
                        Console.WriteLine(doc);
                        RxPacQryList.Add(po);
                        RxPacQryLibx.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                    }*/
                    #endregion


                    rxQryCount = documents.Count;
                
                }

                if (qryTxChbx.Checked)
                {
                    #region txHistory
                    var dbCollection = Program.mongoDB.GetCollection<BsonDocument>("parsed_tx");

                    //var filter = Builders<BsonDocument>.Filter.Gt("time", qryMinDtp.Value.ToUniversalTime()) &
                    //  Builders<BsonDocument>.Filter.Lt("time", qryMaxDtp.Value.ToUniversalTime());
                    var filter = Builders<BsonDocument>.Filter.Eq("satId", qrySatCB.SelectedIndex);
                    //& Builders<BsonDocument>.Filter.Eq("subtype", "Beacon");
                    var sort = Builders<BsonDocument>.Sort.Descending("time");
                    List<BsonDocument> documents = dbCollection.Find(filter).Limit(qrySize).Sort(sort).ToList(); ;
                    //var temp = DateTime.Parse(firstDocument.GetValue("time").ToString()).ToLocalTime().ToString();


                    foreach (BsonDocument doc in documents)
                    {
                        po = new PacketObject(transOptions, doc["packetString"].ToString());
                        TxPacQryList.Add(po);
                        TxPacQryLibx.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                    }

                    /*

                    //define query
                    var TxQryWorker = new DataBaseWorker();
                    TxQryWorker.AddDateRangeFilter("time", qryMinDtp.Value.ToUniversalTime(), qryMaxDtp.Value.ToUniversalTime());

                    if (qrySatCB.SelectedIndex != 0)
                    {
                        TxQryWorker.AddMatchPhraseFilter("satId", qrySatCB.SelectedIndex.ToString());
                    }
                    //

                    await TxQryWorker.StartWork(qrySize, "parsed-tx", "time");

                    foreach (var doc in TxQryWorker.Documents)
                    {
                        po = await Task.Run(() =>
                        {
                            return new PacketObject(options, doc["packetString"].ToString());
                        });

                        TxPacQryList.Add(po);

                        TxPacQryLibx.Items.Add(po.ToHeaderString(DateTime.Parse(doc["time"].ToString())));
                    }
                    #endregion */

                    txQryCount = documents.Count;
                }

                MessageBox.Show($"{rxQryCount} RX packet were recived. {Environment.NewLine}{txQryCount} TX packet were recived.", "Query finished", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                UseWaitCursor = false;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
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
            PacQryOutput.Text = TxPacQryList[TxPacQryLibx.SelectedIndex].GetDescriptionString();
        }

        private void RxPacQryLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            PacQryOutput.Text = RxPacQryList[RxPacQryLibx.SelectedIndex].GetDescriptionString();
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
            if (TxPacQryLibx.SelectedIndex == -1) return;
            int dex = RxPacQryList.FindIndex(x => x.Id == TxPacQryList[TxPacQryLibx.SelectedIndex].Id);
            if (dex == -1) return;
            RxPacQryLibx.SelectedIndex = dex;
        }

        private void qryRx2TxBtn_Click(object sender, EventArgs e)
        {
            if (RxPacQryLibx.SelectedIndex == -1) return;
            int dex = TxPacQryList.FindIndex(x => x.Id == RxPacQryList[RxPacQryLibx.SelectedIndex].Id);
            if (dex == -1) return;
            TxPacQryLibx.SelectedIndex = dex;
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
        public List<PacketObject> RxPacQryList = new List<PacketObject>();
        public List<PacketObject> TxPacQryList = new List<PacketObject>();
        private List<SubType> subTypes = new List<SubType>();
        private List<Params> currentParams = new List<Params>();
        #endregion
        #endregion

        

        #region menu


        #region export
        #region controls
        private void toAFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (RxPacQryLibx.Items.Count == 0)
            {
                MessageBox.Show("No RX query data to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                


            saveFileDialog.Filter = Program.settings.enableExcel ? "excel|*.xlsx" : "excel|*.csv";

            saveFileDialog.Title = "Export history to a File";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            int j = 0;
            List<List<string>> T = new List<List<string>> { new List<string> { "Date", "Id", "SatGroup", "Type", "Subtype", "Lenght", "Raw packet", "Data" } };
            foreach (var packet in RxPacQryList)
            //foreach (var packet in RxHisTracker.GetCurrentPacketList())
            {
                if (packet.Type == -1)
                {
                    //T.Add(new List<string> { privHex.Items[j].ToString().Split('[', ']')[1], "-1", "-", "ERROR", "-", "-", packet.RawPacket, "manager was not able to translate this packet" });
                    T.Add(new List<string> { RxPacQryLibx.Items[j].ToString().Split('[', ']')[1], "-1", "-", "ERROR", "-", "-", packet.RawPacket, "manager was not able to translate this packet" });
                    j++;
                    continue;
                }

                string dataStr = "";
                for (int i = 0; i < packet.DataCatalog.Count; i++)
                {
                    dataStr += $"{packet.DataCatalog.ElementAt(i).Key}: {packet.DataCatalog.ElementAt(i).Value}. ";
                }
                //T.Add(new List<string> { privHex.Items[j].ToString().Split('[', ']')[1], packet.Id.ToString(), packet.SateliteGroup, packet.GetTypeName(), packet.GetSubTypeName(), packet.Length.ToString(), packet.RawPacket, dataStr });
                T.Add(new List<string> { RxPacQryLibx.Items[j].ToString().Split('[', ']')[1], packet.Id.ToString(), packet.SateliteGroup, packet.GetTypeName(), packet.GetSubTypeName(), packet.Length.ToString(), packet.RawPacket, dataStr });



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

        private void toAFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "|";
            saveFileDialog.Title = "Export history to a Folder";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            List<FilePacket> type_subtype = new List<FilePacket>();
            List<List<string>> errorTable = new List<List<string>> { new List<string> { "Date", "Packet" } };

            //sorting packets to files
            int j = 0;
            
            //foreach (var packet in RxHisTracker.GetCurrentPacketList())
            foreach (var packet in RxPacQryList)
            {
                string timeStr = privHex.Items[j].ToString().Split('[', ']')[1];

                if (packet.Type == -1)
                {
                    errorTable.Add(new List<string> { timeStr, packet.RawPacket });
                    j++;
                    continue;
                }

                int match = type_subtype.FindIndex(x => x.PacketPrefix == $"{packet.GetTypeName()}_{packet.GetSubTypeName()}");
                if (match != -1)
                {
                    type_subtype[match].Packets.Add(new PacketWithTime(packet, timeStr));
                    j++;
                    continue;
                }


                var colStr = new List<string> { "Date", "Id", "SatGroup", "Type", "Subtype", "Lenght", "Raw data" };
                colStr.AddRange(packet.DataCatalog.Keys.ToList());

                type_subtype.Add(new FilePacket
                {
                    PacketPrefix = $"{packet.GetTypeName()}_{packet.GetSubTypeName()}",
                    Packets = new List<PacketWithTime> { new PacketWithTime(packet, timeStr) },
                    FirstColumn = colStr
                });
                j++;
            }
            //

            string stufix = Program.settings.enableExcel ? "xlsx" : "csv";
            try
            {
                Application.UseWaitCursor = true;
                //creating all files
                foreach (var file in type_subtype)
                {
                    List<List<string>> fileTble = new List<List<string>> { file.FirstColumn };
                    foreach (var pac in file.Packets)
                    {
                        List<string> tm = new List<string>
                            {
                                pac.Time,
                                pac.Id.ToString(),
                                pac.SateliteGroup,
                                pac.GetTypeName(),
                                pac.GetSubTypeName(),
                                pac.Length.ToString(),
                                pac.RawPacket
                            };

                        foreach (var item in pac.DataCatalog.Values)
                        {
                            tm.Add(item.ToString());
                        }
                        fileTble.Add(tm);
                    }

                    CreateCSV(fileTble, $"{saveFileDialog.FileName}_{file.PacketPrefix}.{stufix}");

                }
                //


                //creating errors file
                if (errorTable.Count <= 1) return;
                CreateCSV(errorTable, $"{saveFileDialog.FileName}_Errors.{stufix}");
            }
            finally
            {
                Application.UseWaitCursor = false;
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

        private void MainLabelsWorker_Tick(object sender, EventArgs e)
        {
            if (NextPass.passesArr == null) return;

            int listCount = 0;
            for (int i = 0; i < NextPass.passesArr.Count; i++)
            {
                if (NextPass.passesArr[i].maxEl >= 7)
                {
                    string txt = DateTimeOffset.FromUnixTimeSeconds(NextPass.passesArr[i].startUTC).LocalDateTime.ToString();
                    txt += " Max El:" + (int)NextPass.passesArr[i].maxEl;
                    int dur = (NextPass.passesArr[i].endUTC - NextPass.passesArr[i].startUTC)/60;
                    txt += " Duration:" + dur +" min";
                    nextPassLabels[listCount].Text = txt;
                    listCount++;
                    if (listCount >= 4) break;
                }
            }
            nextPassLabel.Text = NextPass.TimeTilPassStr;
            PassStatusLabel.Text = NextPass.PassStatus;
        }

        #region main controls

        private async void MainSatCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainLastRxLabel.Text = "-- : -- : --";
            mainLastTxLabel.Text = "-- : -- : --";
            mainLastBeaconLabel.Text = "-- : -- : --";
            MainSatTimeLabel.Text = "-- : -- : --";
            MainSatUptimeLabel.Text = "-- : -- : --";
            MainSatResetsLabel.Text = "---";
            MainCmdRestesLabel.Text = "---";
            MainCorruptLabel.Text = "---";


            await UpdateLastTimeLabel("parsed_rx", mainLastRxLabel);
            await UpdateLastTimeLabel("parsed_tx", mainLastTxLabel);
            await UpdateBeaconData();

            NextPass.SetCurrentGroup(MainSatCB.SelectedIndex);
        }
        #endregion

        #endregion


        #region funcs

        private async Task UpdateLastTimeLabel(string collection, Label label)
        {
            if (!Program.settings.dataBaseEnabled) return;
            if (MainSatCB.SelectedIndex == 9) return;
            BsonDocument firstDocument = null;
            //
            var dbCollection = Program.mongoDB.GetCollection<BsonDocument>(collection);
            var sort = Builders<BsonDocument>.Sort.Descending("time");
            if (MainSatCB.SelectedIndex == 0)
            {
                firstDocument = dbCollection.Find(new BsonDocument()).Sort(sort).FirstOrDefault();
            }
            else
            {
                var filter = Builders<BsonDocument>.Filter.Eq("satId", MainSatCB.SelectedIndex);
                firstDocument = dbCollection.Find(filter).Sort(sort).FirstOrDefault();
            }
            if (firstDocument != null)
            {
                var temp = DateTime.Parse(firstDocument.GetValue("time").ToString()).ToLocalTime().ToString();
                label.Text = temp;
            }


            return;
        }


        private async Task UpdateBeaconData()
        {
            if (!Program.settings.dataBaseEnabled) return;
            if (MainSatCB.SelectedIndex == 9) return;
            BsonDocument firstDocument = null;
            //
            var dbCollection = Program.mongoDB.GetCollection<BsonDocument>("parsed_rx");
            var sort = Builders<BsonDocument>.Sort.Descending("time");
            if (MainSatCB.SelectedIndex == 0)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("subtype", "Beacon");
                firstDocument = dbCollection.Find(new BsonDocument()).Sort(sort).FirstOrDefault();
                lblLAstBeacon.Text = "Last Beacon ("+ firstDocument.GetValue("satName").ToString()+")";
            }
            else
            {
                lblLAstBeacon.Text = "Last Beacon";
                var filter = Builders<BsonDocument>.Filter.Eq("satId", MainSatCB.SelectedIndex)
                     & Builders<BsonDocument>.Filter.Eq("subtype", "Beacon");
                firstDocument = dbCollection.Find(filter).Sort(sort).FirstOrDefault();
            }

            if (firstDocument != null)
            {
                
                var temp = DateTime.Parse(firstDocument.GetValue("time").ToString()).ToLocalTime().ToString();
                mainLastBeaconLabel.Text = temp;

                po = new PacketObject(transOptions, firstDocument.GetValue("packetString").ToString());
                VBatGauge.Value = float.Parse(po.DataCatalog["vbat"].ToString()) / 1000;
                OBCGauge.Value = float.Parse(po.DataCatalog["MCU Temperature"].ToString()) / 100;
                BatTempGauge.Value = float.Parse(po.DataCatalog["Battery Temperature"].ToString()) / 1000;
                FreeSpaceGauge.Value = float.Parse(po.DataCatalog["free_memory"].ToString()) / 1000000;
                MainSatTimeLabel.Text = DateTime.Parse(firstDocument.GetValue("sat_time").ToString()).ToLocalTime().ToString();
                MainSatResetsLabel.Text = firstDocument.GetValue("number_of_resets").ToString();
                MainCmdRestesLabel.Text = firstDocument.GetValue("number_of_cmd_resets").ToString();
                MainCorruptLabel.Text = firstDocument.GetValue("corrupt_bytes").ToString();
                Console.WriteLine(firstDocument.GetValue("sat_uptime").ToString());
                var tempTS = TimeSpan.FromSeconds(int.Parse(firstDocument.GetValue("sat_uptime").ToString()));
                MainSatUptimeLabel.Text = $"{tempTS.Days}:{tempTS.Hours}:{tempTS.Seconds + tempTS.Minutes * 60}";


            }



            return;

        }


        #endregion


        #region objects
        private Label[] nextPassLabels = new Label[4];

        #endregion

        #endregion


    }
}

