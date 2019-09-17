using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicStore.Test.Utils
{
    public static class MockDbSetExtension
    {
        public static Mock<DbSet<TEntity>> ToMockDbSet<TEntity>(this IQueryable<TEntity> data) where TEntity : class
        {
            var mockSet = new Mock<DbSet<TEntity>>();
            mockSet.As<IAsyncEnumerable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(new TestAsyncEnumerator<TEntity>(data.GetEnumerator()));
            mockSet.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<TEntity>(data.Provider));
            mockSet.As<IQueryable<TEntity>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }
    }
}
