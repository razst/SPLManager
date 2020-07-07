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


            foreach (Type t in options)
            {
                typeCB.Items.Add(t.name);
            }
            foreach (string s in groups)
            {
                groupsCB.Items.Add(s);
            }

            IDTxb.Text = Program.settings.pacCurId.ToString();
            imgIdTxb.Text = Program.settings.pacCurId.ToString();
            groupsCB.SelectedIndex = 2;
            frm = this;
            if (!Program.settings.enableImage) 
            {
                TabControl.TabPages.Remove(ImageTab);
            }
            if (Program.settings.dataBaseEnabled)
            {
                fillImgTable();
            }
            foreach(string cell in imageTypes)
            {
                imgTypeCB.Items.Add(cell);
            }
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

            string type = ConvertToHexBytes(options.typenum[typeCB.SelectedIndex].id, 1);
            string subtype = ConvertToHexBytes(options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].id,1);
            string len = ConvertToHexBytes(length,2);
            string id = ConvertToHexBytes(getSplCurId(),3);
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
                            dt = DateTime.Parse(dataTypesDGV.Rows[i].Cells[1].Value.ToString());
                            unix = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                            data += Convert.ToInt64(unix).ToString("X8");
                            break;

                        case "short":
                            data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X4");
                            break;

                        case "bytes":
                            makeOut.Text = id + " " + satNum + " " + type + " " + subtype + len + " " + dataTypesDGV.Rows[i].Cells[1].Value.ToString().ToUpper();
                            makeOut.Text = makeOut.Text.ToString().Substring(1);
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


        #endregion

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
                if (TArr[4] == "02" && (TArr[5] == "E1" || TArr[5] == "E2"))
                {
                    HandleNewImagePacket(TArr);
                }
                else
                {
                    rawRxPacHisList.Add(mess);
                    addItemToPrivHex("ERROR");
                }
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


        private void viewPacketListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PacketListFrm frm = new PacketListFrm();
            frm.ShowDialog();
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
            OkBtn_click();
            await RadioServer.Send(makeOut.Text.Trim());
            TabControl.SelectedTab = RxTab;
        }




        #endregion

        #region Images
        private async Task<List<imagePropeties>> GetImageInfos()
        {
            var infoList = new List<imageInfo>();
            var dataList = new List<imageData>();
            var propList = new List<imagePropeties>();
            Query capitalQuery = Program.db.Collection("imageInfo");
            QuerySnapshot capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();

            foreach (var docSnap in capitalQuerySnapshot.Documents)
            {
                // infoList.Add(docSnap.ConvertTo<imageInfo>());
                propList.Add(new imagePropeties { Inf = docSnap.ConvertTo<imageInfo>(), chunks = new List<imageData>() });
            }

            capitalQuery = Program.db.Collection("imageData").OrderBy("when");
            capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();

            foreach (var docSnap in capitalQuerySnapshot.Documents)
            {
                var T = docSnap.ConvertTo<imageData>();

                propList.First(item => item.Inf.splDocId == T.splId.ToString()).chunks.Add(T);                
            }

            foreach (var prop in propList)
            {
                prop.chunks = prop.chunks.OrderBy(o => o.chunkId).ToList();
            }
            return propList;
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
                        imageType = type
                    };
                    DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix + "imageInfo").Document(SplId.ToString());
                    docRef.SetAsync(imageInfo);
                });
        }

        private List<int[]> allMissingChunk = new List<int[]>();
        private async void fillImgTable()
        {
            var currentImgs = await GetImageInfos();
            bool whereChunkReq = true;

            foreach (var img in currentImgs)
            {
                whereChunkReq = true;
                List<int> missingChunksDex = new List<int>();
                int chunkDex = 0;
                if(img.chunks.Count != 0)
                {
                    for(int i = 0; i < img.Inf.TotalChunks;i++)
                    {
                        if(img.chunks[chunkDex].chunkId == i)
                        {
                            if(chunkDex < img.chunks.Count - 1)
                            chunkDex++;
                        }
                        else
                        {
                            missingChunksDex.Add(i);
                        }
                    }
                    allMissingChunk.Add(missingChunksDex.ToArray());
                }
                else
                {
                    whereChunkReq = false;
                    allMissingChunk.Add(new int[img.Inf.TotalChunks]);
                }

                if(missingChunksDex.Count == 0 && whereChunkReq)
                {
                    ImageDataDGV.Rows.Add(img.Inf.splDocId,img.Inf.imageId ,imageTypes[img.Inf.imageId - 1], img.Inf.TotalChunks, "none", "show image");
                }
                else if (!whereChunkReq)
                {
                    ImageDataDGV.Rows.Add(img.Inf.splDocId, img.Inf.imageId, imageTypes[img.Inf.imageId - 1], img.Inf.TotalChunks, "chunk weren't requsted", "get all chunks");
                }
                else
                {
                    ImageDataDGV.Rows.Add(img.Inf.splDocId, img.Inf.imageId , imageTypes[img.Inf.imageId - 1], img.Inf.TotalChunks, missingChunksDex.Count, "get missing chunks");
                }
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
                       id = ConvertToHexBytes(getSplCurId(), 3);


                        await RadioServer.Send($"{id} 02 02 E2 02 00 05 00".Trim());//TODO: change 05 00 to real imgId
                        break;

                    case "get missing chunks":
                        len = ConvertToHexBytes(allMissingChunk[e.RowIndex].Length * 2, 2);
                        string data = null;


                        foreach (int dex in allMissingChunk[e.RowIndex])
                        {
                            data += ConvertToHexBytes(dex, 2);
                        }
                        id = ConvertToHexBytes(getSplCurId(), 3);


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


    }
}

