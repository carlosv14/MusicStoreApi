using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Database.Models;

namespace MusicStore.Core.Song
{
    public interface ISongService
    {
        Task<IEnumerable<Database.Models.Song>> GetSongsByNameAndAlbumAsync(string name, int albumId);

        Task<Database.Models.Song> GetSongById(int id);
    }
}