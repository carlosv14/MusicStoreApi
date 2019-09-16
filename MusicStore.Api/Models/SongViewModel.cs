using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Api.Models
{
    public class SongViewModel
    {
        public string Name { get; set; }

        public string ArtistName { get; set; }

        public TimeSpan? Duration { get; set; }

        public int Popularity { get; set; }

        public decimal Price { get; set; }

        public int TrackNumber { get; set; }
    }
}
