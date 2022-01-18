using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPL_Manager.Library.Presenters;
using SPL_Manager.Library.Presenters.TxTabPresenters;
using SPL_Manager.Library.Views.TxTabViews;

namespace SPL_Manager.UI.Views.TxTabViews
{
    public partial class TxTab : UserControl,
        ITxTabView,
        IPlaylistsView
    {
        public TxTab()
        {
            InitializeComponent();

            if(LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                PacketServerPresenter = ContainerConfig.Resolve<PacketServerPresenter>();
                PacketServerPresenter.SetTxView(this);
                CreatePacketPresenter = ContainerConfig.Resolve<CreatePacketPresenter>();
                CreatePacketPresenter.SetView(this);

                TxSatGroupsCB.Items.AddRange(ProgramProps.groups.ToArray());
                TxSatGroupsCB.SelectedIndex = (int)ProgramProps.settings.defaultSatGroup;
            }
        }
        public void Init()
        {

        }

        
        public PacketServerPresenter PacketServerPresenter { get; set; }

        private void TxTypesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePacketPresenter.LoadTypeDesc();
            TxSubtypeDesc = "";
            CreatePacketPresenter.LoadSubtypes();

            TxPacketParamsDGV.Visible = false;
        }

        private void TxSubtypesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePacketPresenter.LoadSubtypeDesc();
            CreatePacketPresenter.LoadGridParams();
        }


        private async void TxCopyPacketBtn_Click(object sender, EventArgs e)
        {
            await CreatePacketPresenter.GeneratePacket("clipboard");
        }
        private async void TxSendPacketBtn_Click(object sender, EventArgs e)
        {
            await CreatePacketPresenter.GeneratePacket("testing");//TODO: change mode to satelite
            await PacketServerPresenter.SendCurrentPacket();
        }


        public List<string> TxPacketsTypesList
        {
            set
            {
                TxTypesCB.Items.Clear();
                TxTypesCB.Items.AddRange(value.ToArray());
            }
        }

        public List<string> TxPacketsSubtypesList
        {
            set
            {
                TxSubtypesCB.Items.Clear();
                TxSubtypesCB.Items.AddRange(value.ToArray());
            }
        }



        public string TxTypeDesc { set => TxDescTypeLbl.Text = "Description: " + value; }
        public string TxSubtypeDesc { set => TxDescSubtypeLbl.Text = "Description: " + value; }



        public int TxCurrentTypeIndex { get => TxTypesCB.SelectedIndex; set => TxTypesCB.SelectedIndex = value; }

        public int TxCurrentSubtypeIndex { get => TxSubtypesCB.SelectedIndex; set => TxSubtypesCB.SelectedIndex = value; }

        public string TxCurrentSatGroup => TxSatGroupsCB.Text;

        public string TxPacketHexStr
        {
            get => TxHexOutputTxb.Text;
            set => TxHexOutputTxb.Text = value;
        }



        public List<TxDgvLine> TxDgvParams
        {
            get
            {
                var Output = new List<TxDgvLine>();

                for (int i = 0; i < TxPacketParamsDGV.Rows.Count; i++)
                {
                    var row = TxPacketParamsDGV.Rows[i];
                    Output.Add(new TxDgvLine()
                    {
                        ParamName = row.Cells[0].Value?.ToString(),
                        ParamValues = new List<string>() { row.Cells[1].Value?.ToString() }
                    });
                }

                return Output;
            }



            set
            {
                TxPacketParamsDGV.Rows.Clear();
                if (value.Count == 0)
                {
                    TxPacketParamsDGV.Visible = false;
                    return;
                }
                TxPacketParamsDGV.Visible = true;


                bool HasBitLine = false;

                int i = -1;
                foreach (var line in value)
                {
                    i++;

                    string CellValue = "";

                    if (line.ParamType == "bytes") HasBitLine = true;

                    if (line.ParamType == "date") CellValue = DateTime.Now.ToString("d");
                    if (line.ParamType == "datetime") CellValue = DateTime.Now.ToString("G");

                    TxPacketParamsDGV.Rows.Add(line.ParamName, CellValue, line.ParamDescription);


                    if (line.ParamValues == null) continue;
                    if (line.ParamValues.Count == 0) continue;
                    if (line.ParamValues.Count == 1)
                    {
                        TxPacketParamsDGV.Rows[i].Cells[1].Value = line.ParamValues[0];
                        continue;
                    }
                    if (line.ParamValues.Count > 1)
                    {
                        var cell = new DataGridViewComboBoxCell();
                        line.ParamValues.ForEach(value => cell.Items.Add(value));

                        TxPacketParamsDGV.Rows[i].Cells[1] = cell;
                        TxPacketParamsDGV.Rows[i].Cells[1].Value = cell.Items[0];
                    }


                }

                TxPacketParamsDGV.AutoResizeColumns();

                TxPacketParamsDGV.Columns[1].Width = HasBitLine ? 456 : 200;

            }
        }

        private void TxPacketParamsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TxPacketParamsDGV.EditingControl is DataGridViewComboBoxEditingControl editingControl)
                editingControl.DroppedDown = true;
        }
        public void FillTxDgvValues(List<string> values)
        {
            int i = -1;
            foreach (var val in values)
            {
                i++;
                var row = TxPacketParamsDGV.Rows[i].Cells[1];
                row.Value = val;
            }
        }


        public CreatePacketPresenter CreatePacketPresenter { get; set; }











        // Play list view




        private void PlNewPlaylistBtn_Click(object sender, EventArgs e)
        {
            NewPlaylistForm form = new NewPlaylistForm(PlaylistsPresenter);
            form.ShowDialog();
            PlPlaylistsCB.SelectedIndex = PlPlaylistsCB.Items.Count - 1;
        }
        private async void TxAddToPlaylistBtn_Click(object sender, EventArgs e)
        {
            await PlaylistsPresenter.AddCurrentPacketToPlaylist();
            //TODO: show errors when failing to add
            PlPlaylistItemsLibx.SelectedIndex = PlPlaylistItemsLibx.Items.Count - 1;
        }

        private async void PlSaveListBtn_Click(object sender, EventArgs e)
        {
            if (!ProgramProps.GetIfOnline()) return; //TODO: error msg to user
            await PlaylistsPresenter.SaveCurrentPlaylist();
            //TODO: add a popup window - playlist saved.
        }
        private async void PlDeleteListBtn_Click(object sender, EventArgs e)
        {
            await PlaylistsPresenter.DeleteCurrentPlaylist();
        }
        private async void PlSendListBtn_Click(object sender, EventArgs e)
        {
            await PlaylistsPresenter.SendAllItems();
        }

        private void PlPlaylistsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlaylistsPresenter.UpdateCurrentPlaylist();
        }

        private void PlPlaylistItemsLibx_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlaylistsPresenter.UpdateCurrentPlaylistItem();
        }

        private void PlCmdSleepTxb_TextChanged(object sender, EventArgs e)
        {
            PlaylistsPresenter.UpdateCurrentCmdSleep();
        }




        private void PlMoveupBtn_Click(object sender, EventArgs e)
        {
            PlaylistsPresenter.MoveUpItem();
        }
        private void PlMonedownBtn_Click(object sender, EventArgs e)
        {
            PlaylistsPresenter.MoveDownItem();
        }
        private void PlDeleteItemBtn_Click(object sender, EventArgs e)
        {
            PlaylistsPresenter.DeleteItem();
        }



        public List<string> PlaylistsNames
        {
            get => PlPlaylistsCB.Items.Cast<string>().ToList();
            set
            {
                PlPlaylistsCB.Items.Clear();
                PlPlaylistsCB.Items.AddRange(value.ToArray());
            }
        }
        public List<string> PlayListItems
        {
            get => PlPlaylistItemsLibx.Items.Cast<string>().ToList();
            set
            {
                PlPlaylistItemsLibx.Items.Clear();
                PlPlaylistItemsLibx.Items.AddRange(value.ToArray());
            }
        }

        public string CmdSleepValue { get => PlCmdSleepTxb.Text; set => PlCmdSleepTxb.Text = value; }



        public int SelectedPlaylist
        {
            get => PlPlaylistsCB.SelectedIndex;
            set => PlPlaylistsCB.SelectedIndex = value;
        }
        public int SelectedPlaylistItem
        {
            get => PlPlaylistItemsLibx.SelectedIndex;
            set => PlPlaylistItemsLibx.SelectedIndex = value;
        }





        public PlaylistsPresenter PlaylistsPresenter { get; set; }

    }
}
