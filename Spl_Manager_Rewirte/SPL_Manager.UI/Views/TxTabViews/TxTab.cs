using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SPL_Manager.Library.PacketLifecycle.Create;
using SPL_Manager.Library.PacketLifecycle.Send;
using SPL_Manager.Library.PacketsPlaylists;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.UI.Views.TxTabViews
{
    public partial class TxTab : UserControl,
        ICreatePacketView,
        IPlaylistsView
    {
        public PacketServerPresenter PacketServerPresenter { get; set; }
        public CreatePacketPresenter CreatePacketPresenter { get; set; }
        public PlaylistsPresenter PlaylistsPresenter { get; set; }


        public TxTab()
        {
            InitializeComponent();

            if(LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                PacketServerPresenter = new PacketServerPresenter();
                PacketServerPresenter.SetTxView(this);
                CreatePacketPresenter = new CreatePacketPresenter();
                CreatePacketPresenter.SetView(this);
                PlaylistsPresenter = new PlaylistsPresenter();
                PlaylistsPresenter.SetTxView(this);
                PlaylistsPresenter.SetPlView(this);

                TxSatGroupsCB.Items.AddRange(ProgramProps.groups.ToArray());
                TxSatGroupsCB.SelectedIndex = (int)ProgramProps.settings.defaultSatGroup;
            }
        }

        private void TxTab_Load(object sender, EventArgs e)
        {
        }



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
            await CreatePacketPresenter.GeneratePacket("none");
            
            string result = TxPacketHexStr;
            if (result != null && result != "")
                Clipboard.SetText(result);
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



        public List<PacketParamView> TxDgvParams
        {
            get
            {
                var Output = new List<PacketParamView>();

                for (int i = 0; i < TxPacketParamsDGV.Rows.Count; i++)
                {
                    var row = TxPacketParamsDGV.Rows[i];
                    Output.Add(new PacketParamView()
                    {
                        Name = row.Cells[0].Value?.ToString(),
                        OptinalValues = new List<string>() { row.Cells[1].Value?.ToString() }
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

                    if (line.ValueType == "bytes") HasBitLine = true;

                    if (line.ValueType == "date") CellValue = DateTime.Now.ToString("d");
                    if (line.ValueType == "datetime") CellValue = DateTime.Now.ToString("G");

                    TxPacketParamsDGV.Rows.Add(line.Name, CellValue, line.Description);


                    if (line.OptinalValues == null) continue;
                    if (line.OptinalValues.Count == 0) continue;
                    if (line.OptinalValues.Count == 1)
                    {
                        TxPacketParamsDGV.Rows[i].Cells[1].Value = line.OptinalValues[0];
                        continue;
                    }
                    if (line.OptinalValues.Count > 1)
                    {
                        var cell = new DataGridViewComboBoxCell();
                        line.OptinalValues.ForEach(value => cell.Items.Add(value));

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

        private bool firstClick = false;
        private async void PlPlaylistsCB_Click(object sender, EventArgs e)
        {
            if (firstClick) return;
            firstClick = true;
            await PlaylistsPresenter.LoadPlayLists();
        }
    }
}
