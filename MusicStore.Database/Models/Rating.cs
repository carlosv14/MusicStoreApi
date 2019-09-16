using System;
using System.Collections.Generic;

namespace MusicStore.Database.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
