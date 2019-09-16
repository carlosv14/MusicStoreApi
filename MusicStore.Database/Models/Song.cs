using System;
using System.Collections.Generic;

namespace MusicStore.Database.Models
{
    public partial class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Popularity { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? AlbumId { get; set; }
        public int TrackNumber { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Album Album { get; set; }
    }
}
