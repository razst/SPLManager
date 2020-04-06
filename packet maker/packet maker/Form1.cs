using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace packet_maker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            string type = Convert.ToInt32(typeTxb.Text).ToString("X2");
            string subtype = Convert.ToInt32(subTypeTxb.Text).ToString("X2");
            string len = Convert.ToInt32(lenTxb.Text).ToString("X8");
            string id = Convert.ToInt32(IDTxb.Text).ToString("X8");
            string data = Convert.ToInt32(dataTxb.Text).ToString("X" + (int.Parse(lenTxb.Text) * 2).ToString());


            string hexData = Regex.Replace(data, ".{2}", "$0 ");
            string hexID = Regex.Replace(id, ".{2}", "$0 ");
            string hexLen = Regex.Replace(len, ".{2}", "$0 ");


            string[] revLen = hexLen.Split(' ');
            string[] revID = hexID.Split(' ');
            string[] revData = hexData.Split(' ');


            Array.Reverse(revData);
            Array.Reverse(revID);
            Array.Reverse(revLen);

            makeOut.Text = String.Join(" ", revID) + " " + type + " " + subtype + String.Join(" ", revLen) + String.Join(" ", revData);

        }

        private void createRB_CheckedChanged(object sender, EventArgs e)
        {
            if (createRB.Checked)
            {
                make.Visible = true;
                translate.Visible = false;
            }

        }

        private void trasBtn_Click(object sender, EventArgs e)
        {
            transOut.Text = "";
            string[] bitarr = transIn.Text.Split(' ');


            int Idarr = Convert.ToInt32(bitarr[3] + bitarr[2] + bitarr[1] + bitarr[0], 16);
            int typearr = Convert.ToInt32(bitarr[4], 16);
            int subtypearr = Convert.ToInt32(bitarr[5], 16);
            int lenarr = Convert.ToInt32(bitarr[9] + bitarr[8] + bitarr[7] + bitarr[6], 16);


            string temp= null;
            for (int i= bitarr.Length-1; i>=10; i--)
            {
                temp += bitarr[i];
            }
            temp = temp.Replace("\n", String.Empty);
            temp = temp.Replace("\r", String.Empty);
            temp = temp.Replace("\t", String.Empty);
            int dataarr = Convert.ToInt32(temp, 16);


            transOut.AppendText("ID: " +Idarr+ Environment.NewLine);
            transOut.AppendText("type: "+typearr + Environment.NewLine);
            transOut.AppendText("subtype: " + subtypearr + Environment.NewLine);
            transOut.AppendText("length: " + lenarr + Environment.NewLine);
            transOut.AppendText("data: " + dataarr + Environment.NewLine);

        }

        private void getRB_CheckedChanged(object sender, EventArgs e)
        {
            if (getRB.Checked)
            {
                make.Visible = false;
                translate.Visible = true;
            }
        }
    }
}
