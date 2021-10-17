using SPL_Manager.Presenters.TxTabPresenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPL_Manager
{
    public partial class NewPlaylistForm : Form
    {
        private PlaylistsPresenter _presenter;
        public NewPlaylistForm(PlaylistsPresenter presenter)
        {
            _presenter = presenter;
            InitializeComponent();
        }


        private void OkBtn_Click(object sender, EventArgs e)
        {
            _presenter.AddNewPlaylist(PlaylistNameTxb.Text);
            Close();
        }
    }
}
