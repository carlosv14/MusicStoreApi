using MusicStore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MusicStore.Database.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        public ArtistRepository(MusicStoreContext context)
        {
            Context = context;
        }

        public MusicStoreContext Context { get; protected set; }

        public IQueryable<Artist> All()
        {
            return this.Context.Artist;
        }

        public IQueryable<Artist> Filter(Expression<Func<Artist, bool>> predicate)
        {
            return this.All().Where(predicate);
        }
    }
}
