using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Database.Models;

namespace MusicStore.Core.Artist
{
    public interface IArtistService
    {
        Task<IEnumerable<Database.Models.Artist>> GetArtistsByGenreAsync(string genre);
    }
}