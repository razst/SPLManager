using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;



namespace packet_maker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TypeList options;
        private TypeList transOptions;
        private string[] groups =
        {
            "Any",
            "Ofakim",
            "Yerucham",
            "Kiryat Ata",
            "Shaar HaNegev",
            "Nazareth",
            "Maale Adomin",
            "Guvat Shmuel"
        };

        private void OkBtn_Click(object sender, EventArgs e)
        {
            int length = 0;
            foreach (Params par in options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas)
            {
                if (par.type == "int")
                {
                    length += 4;
                }
                if (par.type == "char")
                {
                    length++;
                }
            }



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
                for (int i = dataTypesDGV.Rows.Count - 1; i >= 0; i--)
                {
                    if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas[i].type == "int")
                    {
                        data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X8");
                    }
                    else
                    {
                        data += Convert.ToInt32(dataTypesDGV.Rows[i].Cells[1].Value).ToString("X2");
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




        }



        private void trasBtn_Click(object sender, EventArgs e)
        {
            transOut.Text = "";
            string[] bitarr = transIn.Text.Split(' ');


            int Idarr = Convert.ToInt32(bitarr[2] + bitarr[1] + bitarr[0], 16);
          
            int typearr = Convert.ToInt32(bitarr[4], 16);
            int subtypearr = Convert.ToInt32(bitarr[5], 16);
            int lenarr = Convert.ToInt32(bitarr[9] + bitarr[8] + bitarr[7] + bitarr[6], 16);

            int typeDex = transOptions.typenum.FindIndex(item => item.id == typearr);
            int subtypeDex = transOptions.typenum[typeDex].subTypes.FindIndex(item => item.id == subtypearr);
            int j = 10;
            List<int> DataArr = new List<int>();
            foreach (Params par in transOptions.typenum[typeDex].subTypes[subtypeDex].parmas)
            {
                if (par.type == "int")
                {
                    DataArr.Add(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16));
                    j += 4;
                }
                else if (par.type == "char")
                {
                    DataArr.Add(Convert.ToInt32(bitarr[j], 16));
                    j++;
                }
            }


            transOut.AppendText("Satlite: " + groups[Convert.ToInt32(bitarr[3])] +Environment.NewLine);
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



        private void Form1_Load(object sender, EventArgs e)
        {
            //http://jsonviewer.stack.hu/
            StreamReader re = new StreamReader(@"packet.json");
            StreamReader ree = new StreamReader(@"packet.json");
            JsonTextReader reader = new JsonTextReader(re);
            JsonTextReader reeader = new JsonTextReader(ree);
            JsonSerializer Serializer = new JsonSerializer();
            object parsedData = Serializer.Deserialize(reader);
            object parseedData = Serializer.Deserialize(reeader);
            string jsonString = @parsedData.ToString();
            string jsonString2 = parseedData.ToString();
            options = JsonConvert.DeserializeObject<TypeList>(jsonString);
            transOptions=JsonConvert.DeserializeObject<TypeList>(jsonString2);

            foreach (Type t in options)
            {
                typeCB.Items.Add(t.name);
            }

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
        }

        private void subtypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            descSubType.Text = "Description: " + options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].desc;
            if (options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas.Count() != 0)
            {
                dataTypesDGV.Rows.Clear();
                // Console.WriteLine(dataTypesDGV.Rows[0].Cells[2].Value.ToString());
                dataTypesDGV.Visible = true;
                int i = 0;
                foreach (Params par in options.typenum[typeCB.SelectedIndex].subTypes[subtypeCB.SelectedIndex].parmas)
                {
                    dataTypesDGV.Rows.Add(par.name, "", par.desc);
                    if (par.values != null)
                    {
                        DataGridViewComboBoxCell c = new DataGridViewComboBoxCell();
                        
                        foreach (string item in par.values)
                        {
                            c.Items.Add(item);
                        }
                        dataTypesDGV.Rows[i].Cells[1] = c;
                    }
                    if (par.type == "date")
                    {
                        dataTypesDGV.Rows[i].Cells[1].Value = DateTime.Now.ToShortDateString();
                    }
                    if (par.type == "datetime")
                    {
                        dataTypesDGV.Rows[i].Cells[1].Value = DateTime.Now.ToString();
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


    }
}
