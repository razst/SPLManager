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
    public partial class NewPlaylistDialog : Form
    {
        public NewPlaylistDialog()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Main.frm.newPLItem(playlistNameTxb.Text);
            Close();
        }
    }
}
