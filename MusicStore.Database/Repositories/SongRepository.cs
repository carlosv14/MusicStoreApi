using MusicStore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MusicStore.Database.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        public SongRepository(MusicStoreContext context)
        {
            Context = context;
        }

        public MusicStoreContext Context { get; protected set; }

        public IQueryable<Song> All()
        {
            return this.Context.Song;
        }

        public IQueryable<Song> Filter(Expression<Func<Song, bool>> predicate)
        {
            return this.All().Where(predicate);
        }
    }
}
