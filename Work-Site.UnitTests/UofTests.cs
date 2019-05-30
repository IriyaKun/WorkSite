using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Work_Site.DAL;
using Work_Site.DAL.Models;
using Xunit;

namespace Work_Site.UnitTests
{
    public class UofTests
    {

        private sealed class MockDbSet<TEntity> : Mock<DbSet<TEntity>> where TEntity : class
        {
            private ICollection<TEntity> BackingStore { get; set; }

            public MockDbSet()
            {
                var queryable = (this.BackingStore ?? (this.BackingStore = new List<TEntity>())).AsQueryable();

                this.As<IQueryable<TEntity>>().Setup(e => e.Provider).Returns(queryable.Provider);
                this.As<IQueryable<TEntity>>().Setup(e => e.Expression).Returns(queryable.Expression);
                this.As<IQueryable<TEntity>>().Setup(e => e.ElementType).Returns(queryable.ElementType);
                this.As<IQueryable<TEntity>>().Setup(e => e.GetEnumerator()).Returns(() => queryable.GetEnumerator());

                // Mock the insertion of entities
                this.Setup(e => e.Add(It.IsAny<TEntity>())).Returns((TEntity entity) =>
                {
                    this.BackingStore.Add(entity);

                    return entity;
                });

                this.Setup(e => e.Remove(It.IsAny<TEntity>())).Returns((TEntity entity) =>
                {
                    this.BackingStore.Remove(entity);

                    return entity;
                });

                this.Setup(e => e.Find(It.IsAny<TEntity>())).Returns((TEntity n) => 
                {
                        return this.BackingStore.First(entity => entity.Equals(n));
                });
            }
        }

        [Fact]
        public void ShouldCreateUserInMemory()
        {
            var user = new User()
            {
                Email = "email@email.com",
                Guid = System.Guid.NewGuid().ToString(),
                HashedPassword = "hash",
                Name = "name",
                Role = "admin",
                Surname = "dksk"
            };

            var mockSet = new MockDbSet<User>();

            var mockContext = new Mock<WorkSiteDbContext>();
            mockContext.Setup(c => c.Set<User>()).Returns(mockSet.Object);

            using (var uow = new WorkSiteUow(mockContext.Object))
            {
                uow.Users.Create(user);
                uow.Save();
                mockSet.Verify(u => u.Add(It.IsAny<User>()), Times.Once);
            }

            
        }

        [Fact]
        public void ShouldRemoveUserInMemory()
        {
            var user = new User()
            {
                Email = "email@email.com",
                Guid = System.Guid.NewGuid().ToString(),
                HashedPassword = "hash",
                Name = "name",
                Role = "admin",
                Surname = "dksk"
            };

            var mockSet = new MockDbSet<User>();

            var mockContext = new Mock<WorkSiteDbContext>();
            mockContext.Setup(c => c.Set<User>()).Returns(mockSet.Object);

            using (var uow = new WorkSiteUow(mockContext.Object))
            {
                uow.Users.Create(user);
                uow.Save();
                uow.Users.Delete(user.Guid);
                uow.Save();
            }

            mockSet.Verify(u => u.Remove(It.IsNotIn(user)), Times.Never);
        }

        [Fact]
        public void ShouldFindUser()
        {
            var user = new User()
            {
                Email = "email@email.com",
                Guid = System.Guid.NewGuid().ToString(),
                HashedPassword = "hash",
                Name = "name",
                Role = "admin",
                Surname = "dksk"
            };

            var mockSet = new MockDbSet<User>();

            var mockContext = new Mock<WorkSiteDbContext>();
            mockContext.Setup(c => c.Set<User>()).Returns(mockSet.Object);

            User res;

            using (var uow = new WorkSiteUow(mockContext.Object))
            {
                uow.Users.Create(user);
                uow.Save();
                res = uow.Users.Read(user.Guid);
            }
            Assert.Null(res);
        }


        
    }
}