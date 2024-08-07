﻿using SPL_Manager.Library.PacketLifecycle.History;
using SPL_Manager.Library.PacketLifecycle.Query.Simple;
using SPL_Manager.Library.PacketLifecycle.Send;
using SPL_Manager.Library.Shared;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SPL_Manager.UI.Views.RxTabViews
{
    public partial class RxTab : UserControl,
        IPacketHistoryView,
        ISimpleQueryView
    {
        public RxTab()
        {
            InitializeComponent();

            if(!DesignMode)
            {
                RxTabPresenter = new PacketHistoryPresenter();
                PacketServerPresenter = PacketServerPresenter.Instance;
                RxQueryPresenter = new SimpleQueryPresenter();

                RxTabPresenter.SetView(this);
                RxQueryPresenter.SetView(this);
                PacketServerPresenter.SetRxView(this);

                RxGroupsCB.Items.AddRange(ProgramProps.groups.ToArray());
                RxGroupsCB.SelectedIndex = 0;
                RxLimitCB.SelectedIndex = 0;
            }

        }

        private PacketServerPresenter PacketServerPresenter;
        public PacketHistoryPresenter RxTabPresenter { get; private set; }
        public SimpleQueryPresenter RxQueryPresenter { get; private set; }




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
        private async void RxTranslatePacketBtn_Click(object sender, EventArgs e)
        {
            await RxTabPresenter.AddTranslatedPacket();
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
            //TODO - change to latest item - sometimes...
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

        public void AlertUser(string title, string message)
        {
            CustomViewImplementation.AlertUser(title, message);
        }

        public void NotifyUser(string title, string message)
        {
            CustomViewImplementation.NotifyUser(title, message);
        }

        public string AskUserForFileName(string title, string defaultFileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "|",
                Title = title,
                FileName = defaultFileName
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return null;
            return saveFileDialog.FileName;
        }


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
