using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Database.Repositories;

namespace MusicStore.Core.Album
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Database.Models.Album> albumRepository;

        public AlbumService(IRepository<Database.Models.Album> albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public async Task<AlbumModel> GetAlbumByIdAsync(int id)
        {
            var album = await this.albumRepository.All()
                .Include(c => c.Ratings)
                .Include(c => c.Artist)
                .Include(c => c.Songs)
                .ThenInclude(c => c.Artist)
                .FirstOrDefaultAsync(c => c.Id == id);

            return new AlbumModel
            {
                ArtistName = album.Artist.Name,
                Copyright = album.Copyright,
                Name = album.Name,
                Id = album.Id,
                Price = album.Price,
                Rating = album.Ratings.Select(c => c.Value).DefaultIfEmpty(0).Average(),
                ReleaseDate = album.ReleaseDate,
                Review = album.Review,
                Songs = album.Songs,
                ThumbnailUrl = album.ThumbnailUrl,
                Artist = album.Artist,
                TotalVotes = album.Ratings.Count()
            };
        }
    }
}
