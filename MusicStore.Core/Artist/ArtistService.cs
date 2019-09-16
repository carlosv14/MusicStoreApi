using Microsoft.EntityFrameworkCore;
using MusicStore.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Core.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Database.Models.Artist> artistRepository;

        public ArtistService(IRepository<Database.Models.Artist> artistRepository)
        {
            this.artistRepository = artistRepository;
        }
        public async Task<IEnumerable<Database.Models.Artist>> GetArtistsByGenreAsync(string genre)
        {
            return await this.artistRepository.Filter(c => c.Genre.Name.Contains(genre)).Include(c => c.Albums).ToListAsync();
        }
    }
}
