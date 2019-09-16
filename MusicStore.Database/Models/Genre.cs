using System;
using System.Collections.Generic;

namespace MusicStore.Database.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Artists = new HashSet<Artist>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}
