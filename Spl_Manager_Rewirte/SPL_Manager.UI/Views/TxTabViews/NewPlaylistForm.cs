using SPL_Manager.Library.PacketsPlaylists;
using System;
using System.Windows.Forms;

namespace SPL_Manager.UI
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
