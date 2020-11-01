using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TicketReSail.Core.Test
{
    public static class DbContextMock
    {
        public static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList)
            where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(s => s.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(s => s.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(s => s.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(s => s.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet;
        }
    }
}
