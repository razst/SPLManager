using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace SPL_Manager.Library.Models.DataAccess
{
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