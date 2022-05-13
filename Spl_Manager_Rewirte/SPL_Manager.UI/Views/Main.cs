//#define DB

using System;
using System.ComponentModel;
using System.Windows.Forms;
using SPL_Manager.Library.PacketLifecycle.Send;
using SPL_Manager.Library.PacketsFiles;
using SPL_Manager.UI.Views.AboutView;

namespace SPL_Manager.UI
{
    public partial class Main : Form
    {
        private PacketFilesPresenter PacketFilesPresenter;

        private PacketServerPresenter PacketServerPresenter;

        public Main()
        {
            InitializeComponent();

            if(!DesignMode)
            {
                PacketFilesPresenter = new PacketFilesPresenter();
                PacketFilesPresenter.SetView(RxTabControl);
                PacketServerPresenter = PacketServerPresenter.Instance;
            }
        }


        private void Main_Load(object sender, EventArgs e)
        {
            PacketServerPresenter.StartServer();
        }




        //file saving

        private void ExportToAFileMenuItem_Click(object sender, EventArgs e)
        {
            PacketFilesPresenter.SaveAllPacketsInOneFile();
        }

        private void ExportToAFolderMenuItem_Click(object sender, EventArgs e)
        {
            PacketFilesPresenter.SaveByPacketTypesInFolder();
        }



        // 'about us' form
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.Init(this);
            Hide();
            frm.ShowDialog();
        }
    }
}

