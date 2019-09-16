using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Api.Models
{
    public class AlbumDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Copyright { get; set; }

        public string Review { get; set; }

        public IEnumerable<SongViewModel> Songs { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public double Rating { get; set; }

        public string ArtistName { get; set; }

        public string ThumbnailUrl { get; set; }

        public int TotalVotes { get; set; }
    }
}
