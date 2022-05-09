using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.Library.PacketsPlaylists
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
        public string name = "";
        public List<string> commands = new List<string>();
    }


    public class PlaylistRepository : IPlaylistRepository
    {
        public PlaylistRepository()
        {

        }
        public async Task<List<PlaylistData>> GetAllPlaylists()
        {
            var searchResponse = await ProgramProps.Database.SearchAsync<PlaylistData>(s => s
                    .Index("playlist-info")
                    .Query(q => q.MatchAll()));

            return searchResponse.Documents.ToList();
        }

        public async Task UploadPlayList(PlaylistData playlist)
        {
            await ProgramProps.Database.IndexAsync(new IndexRequest<PlaylistData>(playlist, "playlist-info", playlist.name));
        }

        public async Task DeletePlaylist(string PlName)
        {
            await ProgramProps.Database.DeleteAsync<dynamic>(PlName, d => d.Index("playlist-info"));
        }
    }
}