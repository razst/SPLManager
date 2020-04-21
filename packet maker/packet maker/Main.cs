using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace packet_maker
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private List<DataGridViewComboBoxCell> cList = new List<DataGridViewComboBoxCell>();
        private TypeList options;
        private TypeList transOptions;
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
        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int length = 0;
                foreach (Params par in options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas)
                {
                    if (par.type == "int" || par.type == "date" || par.type == "datetime")
                    {
                        length += 4;
                    }
                    else if (par.type == "char")
                    {
                        length++;
                    }
                    else if(par.type == "short")
                    {
                        length += 2;
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
                    //DataGridViewComboBoxCell curCBcell = new DataGridViewComboBoxCell();
                    string data = "";
                    int CBi = cList.Count - 1;
                    for (int i = dataTypesDGV.Rows.Count - 1; i >= 0; i--)
                    {
                        if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type == "int")
                        {
                            data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X8");
                        }
                        else if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type == "char")
                        {
                            if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].values == null)
                            {
                                data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X2");
                            }
                            else
                            {
                                data += Convert.ToInt32(options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].values[cList[CBi].Items.IndexOf(dataTypesDGV.Rows[i].Cells[1].Value.ToString())].id).ToString("X2");
                                CBi--;
                            }

                        }
                        else if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type == "date")
                        {
                            dt = DateTime.ParseExact(dataTypesDGV.Rows[i].Cells[1].Value.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            unix = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                            data += Convert.ToInt64(unix).ToString("X8");
                        }
                        else if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type == "datetime")
                        {
                            dt = DateTime.ParseExact(dataTypesDGV.Rows[i].Cells[1].Value.ToString(), "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            unix = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                            data += Convert.ToInt64(unix).ToString("X8");
                        }
                        else if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type == "short")
                        {
                            data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X4");
                        }

                    }
                    string hexData = Regex.Replace(data, ".{2}", "$0 ");
                    string[] revData = hexData.Split(' ');
                    Array.Reverse(revData);
                    makeOut.Text = String.Join(" ", revID) + " " + satNum + " " + type + " " + subtype + String.Join(" ", revLen) + String.Join(" ", revData);
                }
                else
                {
                    makeOut.Text = String.Join(" ", revID) + " " + satNum + " " + type + " " + subtype + String.Join(" ", revLen);
                }
                string txt = makeOut.Text;
                txt = txt.Substring(1);
                makeOut.Text = txt;
            }
            catch
            {
                MessageBox.Show("invaled input", "error");
            }
        }        


        private void trasBtn_Click(object sender, EventArgs e)
        {
            try
            {

                string[] bitarr = transIn.Text.Split(' ');


                int Idarr = Convert.ToInt32(bitarr[2] + bitarr[1] + bitarr[0], 16);

                int typearr = Convert.ToInt32(bitarr[4], 16);
                int subtypearr = Convert.ToInt32(bitarr[5], 16);
                int lenarr = Convert.ToInt32(bitarr[9] + bitarr[8] + bitarr[7] + bitarr[6], 16);

                int typeDex = transOptions.typenum.FindIndex(item => item.id == typearr);
                int subtypeDex = transOptions.typenum[typeDex].subTypes.FindIndex(item => item.id == subtypearr);
                int j = 10;
                List<string> DataArr = new List<string>();
                foreach (Params par in transOptions.typenum[typeDex].subTypes[subtypeDex].parmas)
                {
                    if (par.type == "int")
                    {
                        DataArr.Add(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16).ToString());
                        j += 4;
                    }
                    else if (par.type == "char")
                    {
                        if (par.values == null)
                        {
                            DataArr.Add(Convert.ToInt32(bitarr[j], 16).ToString());
                        }
                        else
                        {
                            DataArr.Add(par.values[par.values.FindIndex(item => item.id == Convert.ToInt32(bitarr[j], 16).ToString())].name);
                        }
                        j++;
                    }
                    else if (par.type == "short")
                    {
                        DataArr.Add(Convert.ToInt32(bitarr[j + 1] + bitarr[j], 16).ToString());
                        j += 2;
                    }
                    else if (par.type == "date"||par.type == "datetime")
                    {
                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16)).ToLocalTime();
                        DataArr.Add(dtDateTime.ToString());
                        j += 4;
                    }
                }

                transOut.Text = "";
                transOut.AppendText("Satlite: " + groups[Convert.ToInt32(bitarr[3])] + Environment.NewLine);
                transOut.AppendText("ID: " + Idarr + Environment.NewLine);
                transOut.AppendText("type: " + transOptions.typenum[typeDex].name + Environment.NewLine);
                transOut.AppendText("subtype: " + transOptions.typenum[typeDex].subTypes[subtypeDex].name + Environment.NewLine);
                transOut.AppendText("length: " + lenarr + Environment.NewLine + Environment.NewLine);
                if (DataArr.Count != 0)
                {
                    transOut.AppendText("Data:" + Environment.NewLine);
                    for (int i = 0; i < DataArr.Count; i++)
                    {
                        transOut.AppendText(transOptions.typenum[typeDex].subTypes[subtypeDex].parmas[i].name + ": " + DataArr[i] + Environment.NewLine);
                    }
                }
            }

            catch
            {
                MessageBox.Show("manager was not able to translate this packet", "error");
            }

            if (privHex.SelectedIndex != -1)
            {
                if (transIn.Text != privHex.Items[privHex.SelectedIndex].ToString())
                {
                    privHex.Items.Add(transIn.Text);
                    privHex.SelectedIndex = privHex.Items.Count - 1;
                }
            }
            else
            {
                privHex.Items.Add(transIn.Text);
                privHex.SelectedIndex = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //http://jsonviewer.stack.hu/
            StreamReader rx = new StreamReader(@"rx.json");
            StreamReader tx = new StreamReader(@"tx.json");
            JsonTextReader RXReader = new JsonTextReader(rx);
            JsonTextReader TXReader = new JsonTextReader(tx);
            JsonSerializer Serializer = new JsonSerializer();
            object parsedTX = Serializer.Deserialize(TXReader);
            object parseedRX = Serializer.Deserialize(RXReader);
            string jsonStringTX = parsedTX.ToString();
            string jsonStringRX = parseedRX.ToString();
            options = JsonConvert.DeserializeObject<TypeList>(jsonStringTX);
            transOptions = JsonConvert.DeserializeObject<TypeList>(jsonStringRX);

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
                cList.Clear();
                foreach (Params par in options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas)
                {
                    dataTypesDGV.Rows.Add(par.name, "", par.desc);
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
    }
}
