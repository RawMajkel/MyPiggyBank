using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using MyPiggyBank.Data.Model.Base;

namespace MyPiggyBank.Test
{
    public static class MockingExtension
    {
        public static Mock<DbSet<T>> AsDbSetMock<T>(this List<T> list) where T : BaseEntity
        {
            var query = list.AsQueryable();

            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(query.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(query.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(query.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(query.GetEnumerator());
            dbSetMock
                .Setup(e => e.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .Callback((T entity, CancellationToken token) =>
                {
                    list.Add(entity);
                })
                .ReturnsAsync(new Mock<EntityEntry<T>>().Object);

            return dbSetMock;
        }
    }
}
