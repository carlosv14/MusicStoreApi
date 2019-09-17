using Microsoft.EntityFrameworkCore;
using MusicStore.Database.Models;
using MusicStore.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Core.Song
{
    public class SongService : ISongService
    {
        private readonly IRepository<Database.Models.Song> songRepository;

        public SongService(IRepository<Database.Models.Song> songRepository)
        {
            this.songRepository = songRepository;
        }
        public async Task<IEnumerable<Database.Models.Song>> GetSongsByNameAndAlbumAsync(string name, int albumId)
        {
            return await this.songRepository.Filter(c => c.AlbumId == albumId && c.Name.Contains(name))
                .Include(c => c.Artist)
                .ToListAsync();
        }

        public async Task<Database.Models.Song> GetSongById(int id)
        {
            return await this.songRepository.All().FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
