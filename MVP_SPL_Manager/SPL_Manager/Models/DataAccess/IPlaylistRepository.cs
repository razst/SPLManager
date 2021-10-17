using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Models.DataAccess
{
    public interface IPlaylistRepository
    {
        public Task<List<PlaylistData>> GetAllPlaylists();

        public Task UploadPlayList(PlaylistData playlist);

        public Task DeletePlaylist(string PlName);
    }

    public class PlaylistData
    {
        public int sleepBetweenCommands;
        public string name;
        public List<string> commands;
    }
}
