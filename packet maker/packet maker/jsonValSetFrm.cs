using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace packet_maker
{
    public partial class jsonValSetFrm : Form
    {
        public jsonValSetFrm()
        {
            InitializeComponent();
        }

        public int typeDex = 0;
        public int subtypeDex = 0;
        public string mode = null;

        public void SetObject(Type obj)
        {
            nameTxb.Text = obj.name;
            IdTxb.Text = obj.id.ToString();
            descTxb.Text = obj.desc;
        }

        public void SetObject(SubType obj)
        {
            nameTxb.Text = obj.name;
            IdTxb.Text = obj.id.ToString();
            descTxb.Text = obj.desc;
        }

        public void SetObject(Params obj)
        {
            IdTxb.Visible = false;
            label1.Visible = false;
            nameTxb.Text = obj.name;
            descTxb.Text = obj.desc;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
