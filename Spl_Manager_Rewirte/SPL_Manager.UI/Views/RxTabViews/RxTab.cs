using SPL_Manager.Library.Presenters;
using SPL_Manager.Library.Presenters.RxTabPresenters;
using SPL_Manager.Library.Views.RxTabViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SPL_Manager.UI.Views.RxTabViews
{
    public partial class RxTab : UserControl,
        IPacketDisplayView,
        IRxQueryView
    {
        public RxTab()
        {
            InitializeComponent();

            if(LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                RxTabPresenter = ContainerConfig.Resolve<RxTabPresenter>();
                PacketServerPresenter = ContainerConfig.Resolve<PacketServerPresenter>();
                RxQueryPresenter = ContainerConfig.Resolve<RxQueryPresenter>();

                RxTabPresenter.SetView(this);
                RxQueryPresenter.SetView(this);
                PacketServerPresenter.SetRxView(this);

                RxGroupsCB.Items.AddRange(ProgramProps.groups.ToArray());
                RxGroupsCB.SelectedIndex = 0;
                RxLimitCB.SelectedIndex = 0;
            }

        }
        public void Init()
        {
            //RxQueryPresenter = new RxQueryPresenter(this);


        }

        private PacketServerPresenter PacketServerPresenter;
        public RxTabPresenter RxTabPresenter { get; set; }
        public RxQueryPresenter RxQueryPresenter { get; set; }




        ////   Events

        private void RxGroupsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            RxTabPresenter.UpdateCurrentRxGroup();
        }


        private void RxSentPacketsLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RxTabPresenter.UpdateDisplayedPacket("Sent");
        }
        private void RxRecivedPacketsLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RxTabPresenter.UpdateDisplayedPacket("Recived");
        }



        private void RxConnectBtn_Click(object sender, EventArgs e)
        {
            PacketServerPresenter.OpenEndNode();
        }
        private async void RxResendTxBtn_Click(object sender, EventArgs e)
        {
            await PacketServerPresenter.ResendCurrentTxItem();
        }
        private void RxTranslatePacketBtn_Click(object sender, EventArgs e)
        {
            RxTabPresenter.AddTranslatedPacket();
        }

        private void RxClearBtn_Click(object sender, EventArgs e)
        {
            RxTabPresenter.ClearRxTab();
        }
        private void RxPasteBtn_Click(object sender, EventArgs e)
        {
            RxPacketHexTxb.Text = Clipboard.GetText().Trim();
        }
        private async void RxLoadDBBtn_Click(object sender, EventArgs e)
        {
            await RxQueryPresenter.PrefomQuery();
        }
        private void RxTxToRxBtn_Click(object sender, EventArgs e)
        {
            RxTabPresenter.FindMatchingRx();
        }
        private void RxRxToTxBtn_Click(object sender, EventArgs e)
        {
            RxTabPresenter.FindMatchingTx();
        }


        public void AddTxItem(string DisplayText)
        {
            //TODO - idk yet
            RxSentPacketsLibx.Items.Add(DisplayText);
            //RxSentPacketsLibx.SelectedIndex = RxSentPacketsLibx.Items.Count - 1;


            //TabControl.SelectedTab = RxTab; 
        }



        public void AddRxItem(string DisplayText)
        {
            RxRecivedPacketsLibx.Items.Add(DisplayText);
        }

        public void ClearPacketDisplay()
        {
            RxSentPacketsLibx.Items.Clear();
            RxRecivedPacketsLibx.Items.Clear();
            RxPacketHexTxb.Clear();
            RxPacketDetailsTxb.Clear();
        }



        //// View Props

        public int RxGroupIndex => RxGroupsCB.SelectedIndex;

        public int TxItemsIndex { get => RxSentPacketsLibx.SelectedIndex; set => RxSentPacketsLibx.SelectedIndex = value; }
        public int RxItemsIndex { get => RxRecivedPacketsLibx.SelectedIndex; set => RxRecivedPacketsLibx.SelectedIndex = value; }


        public string RxPacketHex { get => RxPacketHexTxb.Text; set => RxPacketHexTxb.Text = value; }
        public string PacketDetalis { set => RxPacketDetailsTxb.Text = value; }

        public string RxQueryLimit => RxLimitCB.Text;

        public DateTime RxQueryMinDate => RxQueryMinDateDtp.Value.ToUniversalTime();

        public DateTime RxQueryMaxDate => RxQueryMaxDateDtp.Value.ToUniversalTime();
    }
}
