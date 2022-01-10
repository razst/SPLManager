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
using SPL_Manager.Library.Presenters;
using SPL_Manager.Library.Presenters.RxTabPresenters;
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

            if(LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                PacketFilesPresenter = ContainerConfig.Resolve<PacketFilesPresenter>();
                PacketServerPresenter = ContainerConfig.Resolve<PacketServerPresenter>();
            }
        }


        private void Main_Load(object sender, EventArgs e)
        {
            // link presenters to views
            //PacketFilesPresenter = new(RxTabControl);
            //PacketServerPresenter = new(RxTabControl, TxTabControl);

            // strat presenters
            TxTabControl.Init();


            PacketServerPresenter.StartServer();
        }




        //file saving

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

