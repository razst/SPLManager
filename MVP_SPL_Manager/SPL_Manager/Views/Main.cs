//#define DB

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPL_Manager.Presenters;
using SPL_Manager.Presenters.RxTabPresenters;
using SPL_Manager.Views.AboutView;

namespace SPL_Manager
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }


        private void Main_Load(object sender, EventArgs e)
        {


            PacketFilesPresenter = new PacketFilesPresenter(RxTabControl);

            PacketServerPresenter = new PacketServerPresenter(RxTabControl, TxTabControl);




            MainTabControl.Init();
            TxTabControl.Init(PacketServerPresenter);
            RxTabControl.Init(PacketServerPresenter);
            QueryTabControl.Init();



            PacketServerPresenter.StartServer();
        }


        public PacketServerPresenter PacketServerPresenter { get; set; }


        //file saving


        private PacketFilesPresenter PacketFilesPresenter;

        private void ExportToAFileMenuItem_Click(object sender, EventArgs e)
        {
            PacketFilesPresenter.SaveAllPacketsInOneFile();
        }

        private void ExportToAFolderMenuItem_Click(object sender, EventArgs e)
        {
            PacketFilesPresenter.SaveByPacketTypesInSeparateFiles();
        }


        // about form
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.Init(this);
            Hide();
            frm.ShowDialog();
        }
    }
}

