using SPL_Manager.Library.PacketLifecycle.Create;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.PacketModel.Converters;
using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPL_Manager.Library.PacketsPlaylists
{
    internal struct PlStruct
    {
        internal string Name;
        internal List<PacketObject> Items;
        internal int CmdSleep;
    }

    public class PlaylistsPresenter : GenericSingleton<PlaylistsPresenter>
    {
        private ICreatePacketView _txView;
        private IPlaylistsView _plView;
        private IPlaylistRepository _repository;

        private List<PlStruct> PlayLists = new List<PlStruct>();


        public PlaylistsPresenter(IPlaylistRepository repository)
        {
            _repository = repository;
        }
        public PlaylistsPresenter() : this(new PlaylistRepository()) { }

        public void SetTxView(ICreatePacketView txView)
        {
            _txView = txView;
        }
        public void SetPlView(IPlaylistsView plView)
        {
            _plView = plView;
        }

        public async Task LoadPlayLists()
        {
            if (!ProgramProps.DataBaseEnabled) return;

            var playlists = await _repository.GetAllPlaylists();

            PlayLists = playlists.ConvertAll(pl => new PlStruct()
            {
                Name = pl.name,
                CmdSleep = pl.sleepBetweenCommands,
                Items = pl.commands.ConvertAll(cmd => new PacketObject(ProgramProps.PacketJsonFiles["Tx"], cmd))
            });

            _plView.PlaylistsNames = PlayLists.ConvertAll(pl => pl.Name);
        }

        public void AddNewPlaylist(string name)
        {
            PlayLists.Add(new PlStruct() { Name = name, Items = new List<PacketObject>(), CmdSleep = 0 });

            _plView.PlaylistsNames = PlayLists.ConvertAll(pl => pl.Name);
            _plView.PlayListItems = new List<string>();
            _plView.CmdSleepValue = "0";
        }

        public async Task AddCurrentPacketToPlaylist()
        {
            if (_plView.SelectedPlaylist == -1) return;

            PacketObject po;
            try
            {
                po = await _txView.CreatePacketPresenter.CreatePacketFromView("database");
            }
            catch (Exception e)
            {
                _txView.AlertUser("Error", $"invaled input: \n-{e.Message}");
                return;
            }

            if (po.Type == -1) return;

            PlayLists[_plView.SelectedPlaylist].Items.Add(po);
            UpdateCurrentPlaylist();
        }

        #region Update

        public void UpdateCurrentPlaylist()
        {
            PlStruct plItem = GetCurrentPlaylist();
            _plView.PlayListItems = plItem.Items.ConvertAll(item => item?.GetSubTypeName() ?? "nill");
            _plView.CmdSleepValue = plItem.CmdSleep.ToString();
        }

        public void UpdateCurrentPlaylistItem()
        {
            if (_plView.SelectedPlaylist == -1) return;
            if (_plView.SelectedPlaylistItem == -1) return;

            _txView.CreatePacketPresenter.CastPacketToView(GetCurrentPlItem());
        }

        public void UpdateCurrentCmdSleep()
        {
            if (_plView.SelectedPlaylist == -1) return;
            bool isInt = int.TryParse(_plView.CmdSleepValue, out int cmd);
            if (!isInt) return;

            PlStruct plItem = PlayLists[_plView.SelectedPlaylist];
            PlayLists[_plView.SelectedPlaylist] = new PlStruct()
            {
                Name = plItem.Name,
                Items = plItem.Items,
                CmdSleep = cmd
            };
        }

        #endregion

        private PacketObject GetCurrentPlItem()
        {
            return GetCurrentPlaylist().Items[_plView.SelectedPlaylistItem];
        }
        private PlStruct GetCurrentPlaylist()
        {
            return PlayLists[_plView.SelectedPlaylist];
        }


        public async Task SaveCurrentPlaylist()
        {
            bool isOk = true;
            isOk &= _plView.SelectedPlaylist != -1;
            isOk &= ProgramProps.DataBaseEnabled;
            isOk &= ProgramProps.GetIfOnline();
            if (!isOk)
            {
                _plView.AlertUser(
                    "Error", "Something went worng! \n " +
                    "make sure you have internet access \n" +
                    "and db enabled in settings"
                    );
                return;
            }

            var CurrentPl = GetCurrentPlaylist();

            var PlData = new PlaylistData()
            {
                name = CurrentPl.Name,
                sleepBetweenCommands = CurrentPl.CmdSleep,

                commands = CurrentPl.Items.Select(item =>
                {

                    item.Type = item.GetTypeIndex();
                    item.Subtype = item.GetSubtypeIndex();
                    item.ToRawString("database");
                    return item.RawPacket;

                }).ToList()
            };

            await _repository.UploadPlayList(PlData);
            _plView.NotifyUser("Finished!", "Play list succesfully saved");
        }
        public async Task DeleteCurrentPlaylist()
        {
            if (_plView.SelectedPlaylist == -1) return;
            if (!ProgramProps.DataBaseEnabled) return;

            bool UserHasConfirmed = _plView.AskUserToConfirm();
            if (!UserHasConfirmed) return;


            await _repository.DeletePlaylist(GetCurrentPlaylist().Name);
            PlayLists.RemoveAt(_plView.SelectedPlaylist);
            _plView.Clear();
        }
        public async Task SendAllItems()
        {
            for (int i = 0; i < GetCurrentPlaylist().Items.Count; i++)
            {
                _plView.SelectedPlaylistItem = i;
                await _txView.CreatePacketPresenter.GeneratePacket("none");//TODO: change to satelite
                await _txView.PacketServerPresenter.SendCurrentPacket(i + 1);

                await Task.Delay(GetCurrentPlaylist().CmdSleep);
            }
            _plView.SelectedPlaylistItem = -1;

        }


        public void MoveUpItem()
        {
            if (_plView.SelectedPlaylist == -1) return;
            if (_plView.SelectedPlaylistItem == -1) return;
            if (GetCurrentPlaylist().Items.Count == 1) return;
            if (_plView.SelectedPlaylistItem == 0) return;

            int dex = _plView.SelectedPlaylistItem;
            GetCurrentPlaylist().Items.Swap(dex, dex - 1);
            UpdateCurrentPlaylist();

            _plView.SelectedPlaylistItem = dex - 1;
        }

        public void MoveDownItem()
        {
            if (_plView.SelectedPlaylist == -1) return;
            if (_plView.SelectedPlaylistItem == -1) return;
            if (GetCurrentPlaylist().Items.Count == 1) return;

            int dex = _plView.SelectedPlaylistItem;
            if (dex + 1 == GetCurrentPlaylist().Items.Count) return;


            GetCurrentPlaylist().Items.Swap(dex, dex + 1);
            UpdateCurrentPlaylist();

            _plView.SelectedPlaylistItem = dex + 1;
        }

        public void DeleteItem()
        {
            if (_plView.SelectedPlaylist == -1) return;
            if (_plView.SelectedPlaylistItem == -1) return;

            int dex = _plView.SelectedPlaylistItem;
            GetCurrentPlaylist().Items.RemoveAt(dex);
            UpdateCurrentPlaylist();

            _plView.SelectedPlaylistItem = dex - 1;
        }
    }
}