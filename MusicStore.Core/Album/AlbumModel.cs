using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Core.Album
{
    public class AlbumModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Review { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Copyright { get; set; }

        public string ArtistName { get; set; }

        public double Rating { get; set; }

        public string ThumbnailUrl { get; set; }

        public int TotalVotes { get; set; }

        public Database.Models.Artist Artist { get; set; }

        public IEnumerable<Database.Models.Song> Songs { get; set; }
    }
}
