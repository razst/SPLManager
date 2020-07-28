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


        enum Packet_Mode
        {
            satelite = 1,
            database = 2,

            none = 0
        }

        private string[] imageTypes = 
        {
            "XXL",
            "XL",
            "L",
            "M",
            "S",
            "hitmap"
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

            if (!Program.settings.enableImage) 
            {
                TabControl.TabPages.Remove(ImageTab);
            }
            if (Program.settings.dataBaseEnabled)
            {
                if(Program.settings.enableImage)
                fillImgTable();

                PoupolatePL();
            }


            //filling comboboxes
            foreach(string cell in imageTypes)
            {
                imgTypeCB.Items.Add(cell);
            }
            foreach (Type t in options)
            {
                typeCB.Items.Add(t.name);
            }
            foreach (string s in groups)
            {
                groupsCB.Items.Add(s);
            }
            //

            IDTxb.Text = Program.settings.pacCurId.ToString();
            imgIdTxb.Text = Program.settings.pacCurId.ToString();
            groupsCB.SelectedIndex = 2;
            frm = this;
        }

        #endregion

        private int getSplCurId()
        {
            Program.settings.pacCurId += 1;
            IDTxb.Text = Program.settings.pacCurId.ToString();
            imgIdTxb.Text = Program.settings.pacCurId.ToString();
            return Program.settings.pacCurId - 1;
        }

        private string ConvertToHexBytes(int value, int numberOfBytes)
        {
            string format = "X" + (numberOfBytes*2);
            string Tid = value.ToString(format);
            string[] TidArr = Regex.Replace(Tid, ".{2}", "$0 ").Trim().Split(' ');
            Array.Reverse(TidArr);
            return String.Join(" ",TidArr).Trim();
        }
        private async Task Upload_Packet(string COLLECTION_NAME, string id, string packetString)
        {
            await Task.Run(() => {

                if (Program.settings.dataBaseEnabled)
                {
                    packet.packetString = packetString;
                    packet.time = DateTime.UtcNow;

                    DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix + COLLECTION_NAME).Document();

                    docRef.SetAsync(packet);
                }
            });
        }

        #region TX/RX



        private void TX(int CurrId,Packet_Mode mode)
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
            string subtype = ConvertToHexBytes(options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].id,1);
            string len = ConvertToHexBytes(length,2);
            string id = ConvertToHexBytes(CurrId,3);
            string satNum = ConvertToHexBytes(groupsCB.SelectedIndex,1);



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
                            if(mode != Packet_Mode.database)
                            {
                                if (dataTypesDGV.Rows[i].Cells[1].Value.ToString().Substring(0, 3) != "now")
                                {
                                    dt = DateTime.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString());
                                    unix = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                                }
                                else if (dataTypesDGV.Rows[i].Cells[1].Value.ToString() == "now")
                                {
                                    dt = DateTime.Now;
                                    unix = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                                }
                                else
                                {
                                    unix = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() - int.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString().Substring(4));
                                }

                                data += Convert.ToInt64(unix).ToString("X8");
                            }
                            else
                            {
                                if (dataTypesDGV.Rows[i].Cells[1].Value.ToString().Substring(0, 3) != "now")
                                {
                                    dt = DateTime.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString());
                                    unix = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                                    data += Convert.ToInt64(unix).ToString("X8");
                                }
                                else if (dataTypesDGV.Rows[i].Cells[1].Value.ToString() == "now")
                                {
                                    data += "000000NN";
                                }
                                else
                                {
                                    string p = ConvertToHexBytes(int.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString().Substring(4)),3).Replace(" ", String.Empty)+"NN";
                                    //String h = int.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString().Substring(4)).ToString("X6").Trim() + "NN";
                                    data += p;
                                }
                            }

                            break;

                        case "short":
                            data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X4");
                            break;

                        case "bytes":
                            makeOut.Text = id + " " + satNum + " " + type + " " + subtype +" "+ len + " " + dataTypesDGV.Rows[i].Cells[1].Value.ToString().ToUpper();
                            return;

                    }
                }
                string[] revData = Regex.Replace(data, ".{2}", "$0 ").Trim().Split(' ');
                Array.Reverse(revData);
                makeOut.Text = id + " " + satNum + " " + type + " " + subtype +" "+ len+ " " + String.Join(" ", revData);
            }
            else
            {
                makeOut.Text = (id + " " + satNum + " " + type + " " + subtype +" "+ len);
            }
        }


        private packetObject po = new packetObject();
        private async void RX(string transMsg)
        {
            mess = transMsg.Trim();
            mess = mess.Replace(" ", String.Empty);
            mess = Regex.Replace(mess, ".{2}", "$0 ");
            po = await Task.Run(() =>
            {
                return packetObject.create(transOptions, mess);
            });
            //po = packetObject.create(transOptions, mess);
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


        #endregion

        #region click events

        private async void OkBtn_click(int id,Packet_Mode mode)
        {
            success = false;
            try
            {
                TX(id,mode);
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
                    await Upload_Packet("tx packets", IDTxb.Text, makeOut.Text);
                }
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
                string[] TArr = mess.Split(' ');

                if(TArr.Length >= 6)
                if (TArr[4] == "02" && (TArr[5] == "E1" || TArr[5] == "E2"))
                {
                        HandleNewImagePacket(TArr);
                        return;
                }


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
            OkBtn_click(getSplCurId(),Packet_Mode.satelite);

            if(makeOut.Text != null && makeOut.Text != "")
            Clipboard.SetText(makeOut.Text.ToString());
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



        private void clearRxBtn_Click(object sender, EventArgs e)
        {
            privHex.Items.Clear();
            transIn.Text = "";
            transOut.Items.Clear();
            rawRxPacHisList.Clear();
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
                DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix+"local data").Document("packetCurId");
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
            OkBtn_click(getSplCurId(),Packet_Mode.satelite);
            await RadioServer.Send(makeOut.Text.Trim());
            TabControl.SelectedTab = RxTab;
        }




        #endregion





        #region Images
        private async Task<List<imagePropeties>> GetImageInfos()
        {
            var propList = new List<imagePropeties>();
            await Task.Run(async () => {

                Query capitalQuery = Program.db.Collection(Program.settings.collectionPrefix + "imageInfo");
                QuerySnapshot capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();

                foreach (var docSnap in capitalQuerySnapshot.Documents)
                {
                    propList.Add(new imagePropeties { Inf = docSnap.ConvertTo<imageInfo>(), chunks = new List<imageData>() });
                }

            });
            return propList;
        }

        private async Task<List<imageData>> GetImageDataOf(int splId)
        {
            List<imageData> imageDatas = new List<imageData>();

            await Task.Run(async () => {

                Query capitalQuery = Program.db.Collection(Program.settings.collectionPrefix + "imageData").WhereEqualTo("splId",splId);
                QuerySnapshot capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();

                foreach (var docSnap in capitalQuerySnapshot.Documents)
                {
                    imageDatas.Add(docSnap.ConvertTo<imageData>());
                }

            });

            return imageDatas;
        }


        private async Task<List<string[]>> DownloadTable()
        {
            var result = new List<string[]>();
            var currentImgs = await GetImageInfos();

            foreach (var img in currentImgs)
            {
                if (img.Inf.recivedChuncks == img.Inf.TotalChunks)
                {
                    result.Add(new string[] { img.Inf.splDocId, img.Inf.imageId.ToString(), imageTypes[img.Inf.imageId - 1], img.Inf.TotalChunks.ToString(), "none","show image","0" });
                }
                else if (img.Inf.recivedChuncks == -1)
                {
                    result.Add(new string[] { img.Inf.splDocId, img.Inf.imageId.ToString(), imageTypes[img.Inf.imageId - 1], img.Inf.TotalChunks.ToString(), "chunk weren't requsted","get all chunks","0" });
                }
                else
                {
                    result.Add(new string[] { img.Inf.splDocId, img.Inf.imageId.ToString(), imageTypes[img.Inf.imageId - 1], img.Inf.TotalChunks.ToString(), (img.Inf.TotalChunks - img.Inf.recivedChuncks).ToString(),"get missing chunks","0" });
                }
            }
            return result;
        }


        private async void sendImgReqBtn_Click(object sender, EventArgs e)
        {
            string id = ConvertToHexBytes(getSplCurId(), 3);
            
           await RadioServer.Send($"{id} 02 02 E1 01 00 {ConvertToHexBytes(imgTypeCB.SelectedIndex+1,1)}".Trim());
        }

        
        private void HandleNewImagePacket(string[] packet)
        {
            switch (packet[5])
            {
                case "E1":
                    HandleImageReqResult(packet);
                    break;

                case "E2":
                    HandleNewChunk(packet);
                    break;
            }
        }


        private async void HandleImageReqResult(string[] result)
        {
            int SplId = Convert.ToInt32(result[2] + result[1] + result[0], 16);
            string type = imageTypes[Convert.ToInt32(result[12], 16) - 1];
            int totalChunks = Convert.ToInt32(result[11] + result[10], 16);
            int imgId = Convert.ToInt32(result[9]+result[8], 16);
            ImageDataDGV.Rows.Add(SplId,imgId ,type, totalChunks, "chunks weren't reqeusted", "get all chunks");
            ImageDataDGV.AutoResizeColumns();

            if (Program.settings.dataBaseEnabled)
                await Task.Run(() => {
                    imageInfo imageInfo = new imageInfo
                    {
                        imageId = imgId,
                        splDocId = SplId.ToString(),
                        TotalChunks = totalChunks,
                        when = DateTime.UtcNow,
                        imageType = type,
                        recivedChuncks = -1
                    };
                    DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix + "imageInfo").Document(SplId.ToString());
                    docRef.SetAsync(imageInfo);
                });
        }

        private async void fillImgTable()
        {
            var table = await Task.Run(() => {
                return DownloadTable();
            });
            
            foreach(var row in table)
            {
                ImageDataDGV.Rows.Add(row);
            }
        }

        private async void HandleNewChunk(string[] result)
        {

            if(Program.settings.dataBaseEnabled)
                await Task.Run(() => 
                {
                    string temp = "";
                    for(int i = 10; i < result.Length; i++)
                    {
                        temp += result[i] + " ";
                    }


                    DocumentReference docref = Program.db.Collection(Program.settings.collectionPrefix + "imageData").Document();
                    imageData thisChunk = new imageData 
                    {
                        chunkId = Convert.ToInt32(result[9] + result[8], 16),
                        splId = Convert.ToInt32(result[2] + result[1] + result[0], 16),
                        when = DateTime.UtcNow,
                        chunkData = temp
                    };

                    docref.SetAsync(thisChunk);
                });
        }
        

        private async void ImageDataDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                string len;
                string id;
                //senderGrid.SelectedCells[0].Value.ToString();
                switch (senderGrid.SelectedCells[0].Value.ToString())
                {
                    case "get all chunks":
                        //Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString())
                        id = ConvertToHexBytes(int.Parse(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString()), 3);
                        string imgId = ConvertToHexBytes(int.Parse(senderGrid.Rows[e.RowIndex].Cells[1].Value.ToString()), 2);

                        await RadioServer.Send($"{id} 02 02 E2 02 00 {imgId}".Trim());
                        break;

                        case "get missing chunks":
                        var chunks = await GetImageDataOf(int.Parse(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        chunks = chunks.OrderBy(o => o.chunkId).ToList();
                        Console.WriteLine(chunks.Count);
                        List<int> missingChunksDex = new List<int>();

                        int chunkDex = 0;
                        for (int i = 1; i < int.Parse(senderGrid.Rows[e.RowIndex].Cells[3].Value.ToString())+1; i++)
                        {
                            if (chunks[chunkDex].chunkId == i)
                            {
                                if (chunkDex < chunks.Count - 1)
                                    chunkDex++;
                            }
                            else
                            {
                                missingChunksDex.Add(i);
                            }
                        }

                        len = ConvertToHexBytes(missingChunksDex.Count * 2, 2);
                        string data = null;



                        foreach (int dex in missingChunksDex)
                        {
                            data += ConvertToHexBytes(dex, 2)+" ";
                        }
                        id = ConvertToHexBytes(int.Parse(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString()), 3);


                        await RadioServer.Send($"{id} 02 02 E2 {len} {data}".Trim());
                        break;
                }
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e) 
        {
            ImageDataDGV.Rows.Clear();
            fillImgTable();
        }


        #endregion






        #region playlist

        private List<PLInfo> Playlists = new List<PLInfo>();

        public async Task<List<PLInfo>> DowloadPL()
        {
            List<PLInfo> playLists = new List<PLInfo>();
            Query capitalQuery = Program.db.Collection(Program.settings.collectionPrefix + "playListInfo");
            QuerySnapshot capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();

            foreach(var doc in capitalQuerySnapshot)
            {
                playLists.Add(doc.ConvertTo<PLInfo>());
            }
            return playLists;
        }

        public async void PoupolatePL()
        {
            Playlists = await Task.Run(async () => {
                return await DowloadPL();
            });

            foreach(var list in Playlists)
            {
                PlaylistCB.Items.Add(list.name);
            }
        }


        private List<packetObject> commands = new List<packetObject>();
        private void PlaylistCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            PLitemsLibx.Items.Clear();
            commands.Clear();
            foreach(var item in Playlists[PlaylistCB.SelectedIndex].commands)
            {
                packetObject po = packetObject.create(options, item);
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

                OkBtn_click(int.Parse(IDTxb.Text),Packet_Mode.none);
            }
        }

        private void CastToDGV(packetObject packet)
        {
            groupsCB.SelectedItem = packet.sateliteGroup;
            typeCB.SelectedItem = packet.getTypeName();
            subtypeCB.SelectedItem = packet.getSubTypeName();

            int i = 0;
            if (packet.data.Count != 0)
                foreach (DataGridViewRow item in dataTypesDGV.Rows)
                {
                    item.Cells[1].Value = packet.data[i];
                    i++;
                }
        }

        private void add2PLBtn_Click(object sender, EventArgs e)
        {

            OkBtn_click(getSplCurId(),Packet_Mode.database);
            packetObject po = packetObject.create(options, makeOut.Text.Trim());
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

                DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix + "playListInfo").Document(PlaylistCB.SelectedItem.ToString());

                await docRef.SetAsync(Playlists[PlaylistCB.SelectedIndex]);
            }
        }

        private async void DelListBtn_Click(object sender, EventArgs e)
        {
            if(PlaylistCB.SelectedIndex != -1 && Program.settings.dataBaseEnabled)
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                    PLitemsLibx.Items.Clear();
                    sleepCmdTxb.Text = "";

                    DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix + "playListInfo").Document(PlaylistCB.SelectedItem.ToString());

                    await docRef.DeleteAsync();

                    PlaylistCB.Items.RemoveAt(PlaylistCB.SelectedIndex);
            }
        }

        private void NewListBtn_Click(object sender, EventArgs e)
        {
            NewPlaylistDialog playlistDialog = new NewPlaylistDialog();
            playlistDialog.ShowDialog();
        }


        public async void newPLItem(string item)
        {
            
            PlaylistCB.Items.Add(item);
            Playlists.Add(new PLInfo { commands = new List<string>(), name = item, sleepBetweenCommands = 0 });
            PlaylistCB.SelectedIndex = PlaylistCB.Items.Count - 1;


            DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix + "playListInfo").Document(item);

            await docRef.SetAsync(new PLInfo { commands = new List<string>(), name = item, sleepBetweenCommands = 0 });
        }


        private void DelLItemBtn_Click(object sender, EventArgs e)
        {
            if(PLitemsLibx.SelectedIndex != -1)
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
            if(PLitemsLibx.SelectedIndex != -1 && PLitemsLibx.SelectedIndex != 0)
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

        private string sendAutoWorker(int index)
        {
            PLitemsLibx.SelectedIndex = -1;
            PLitemsLibx.SelectedIndex = index;
            return makeOut.Text;
        }


        private async void SendListBtn_Click(object sender, EventArgs e)
        {
            if (RadioServer.isOnline())
            {
                var tem = Playlists[PlaylistCB.SelectedIndex].commands;

                await Task.Run(async () =>
                {
                    int i = 0;
                    Func<int, string> f = sendAutoWorker;
                    foreach (var packet in tem)
                    {
                        string pac = (string)Invoke(f,i);
                        await RadioServer.Send(pac);
                        Task.Delay(int.Parse(sleepCmdTxb.Text)).GetAwaiter().GetResult();
                        i++;
                        
                    }
                });
            }
            else
            {
                MessageBox.Show("server is not online", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion
    }




























    public static class ExtensionMethods
    {
        public static void Swap(this ListBox.ObjectCollection list, int index1, int index2)
        {
            object temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}

