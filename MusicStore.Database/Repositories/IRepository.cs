using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MusicStore.Database.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
    }
}
