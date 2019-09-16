using System;
using System.Collections.Generic;

namespace MusicStore.Database.Models
{
    public partial class Album
    {
        public Album()
        {
            Ratings = new HashSet<Rating>();
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Review { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Copyright { get; set; }
        public int ArtistId { get; set; }
        public string ThumbnailUrl { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
