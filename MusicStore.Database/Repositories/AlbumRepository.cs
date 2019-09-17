using MusicStore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MusicStore.Database.Repositories
{
    public class AlbumRepository : IRepository<Album>
    {
        public AlbumRepository(MusicStoreContext context)
        {
            Context = context;
        }

        public MusicStoreContext Context { get; }

        public IQueryable<Album> All()
        {
            return this.Context.Album;
        }

        public IQueryable<Album> Filter(Expression<Func<Album, bool>> predicate)
        {
            return this.All().Where(predicate);
        }
    }
}
