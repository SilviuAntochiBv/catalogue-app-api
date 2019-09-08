using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace API.Data.Test.Implementation
{
    public abstract class RepositoryShould<TRepository, TEntity, TKey>
        where TRepository : IRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
        where TKey : IEquatable<TKey>, new()
    {
        protected APIDbContext InMemoryDbContext { get; set; }

        protected TRepository Repository { get; set; }

        protected RepositoryShould()
        {
            InMemoryDbContext = CreateInMemoryContext();
            Repository = CreateRepository(InMemoryDbContext);
        }

        protected abstract TRepository CreateRepository(APIDbContext inMemoryDbContext);

        protected abstract TKey CreateKey(bool other = false);

        protected void Commit()
        {
            InMemoryDbContext.SaveChanges();
        }

        private DbContextOptions<APIDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<APIDbContext>()
                             .UseInMemoryDatabase(Guid.NewGuid().ToString())
                             .Options;
        }

        private APIDbContext CreateInMemoryContext()
        {
            var dbContext = new APIDbContext(CreateOptions());

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        protected async Task<TEntity> Add(object id)
        {
            var ntt = new TEntity { Id = id };
            return await Add(ntt);
        }

        protected async Task<TEntity> Add(TEntity entity)
        {
            return await Repository.Add(entity);
        }

        [Fact]
        public async Task ReturnEntityWhenAdding()
        {
            // arrange
            var entity = new TEntity();

            // act
            var result = await Repository.Add(entity);

            // assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task HaveEntityInSetWhenAdding()
        {
            // arrange
            var entityId = CreateKey();
            var entity = await Add(entityId);

            // act
            Commit();

            // assert
            var set = InMemoryDbContext.Set<TEntity>();
            Assert.Equal(set.Find(entityId), entity);
        }

        [Fact]
        public async Task ReturnTrueWhenDeleteSucceeded()
        {
            // arrange
            var entity = await Add(CreateKey());
            Commit();

            // act
            var result = await Repository.Delete(entity);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task NotHaveEntityInSetWhenDeleting()
        {
            // arrange
            var entityId = CreateKey();
            var entity = new TEntity { Id = entityId };
            await Repository.Add(entity);
            Commit();

            // act
            await Repository.Delete(entity);
            Commit();

            // assert
            var foundEntity = InMemoryDbContext.Set<TEntity>().Find(entityId);
            Assert.Null(foundEntity);
        }

        [Fact]
        public async Task ReturnFalseIfEntityCannotBeFoundWhenDeletingById()
        {
            // act
            var result = await Repository.DeleteById(CreateKey());

            // assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReturnTrueIfEntityIsInSetWhenDeletingById()
        {
            // arrange
            var entityId = CreateKey();
            await Add(entityId);
            Commit();

            // act
            var result = await Repository.DeleteById(entityId);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task NotHaveEntityInSetWhenDeletingById()
        {
            // arrange
            var entityId = CreateKey();
            await Add(entityId);
            Commit();

            // act
            await Repository.DeleteById(entityId);
            Commit();

            // assert
            var foundEntity = InMemoryDbContext.Set<TEntity>().Find(entityId);
            Assert.Null(foundEntity);
        }

        [Fact]
        public async Task ReturnSpecificEntitiesBasedOnFilter()
        {
            // arrange
            object entityId = CreateKey();
            await Add(entityId);
            await Add(CreateKey(true));
            Commit();

            // act
            var result = await Repository.Filter(w => Equals(w.Id, entityId)).ToListAsync();

            // assert
            Assert.Equal(entityId, result.Single().Id);
        }

        [Fact]
        public async Task ReturnAllEntitiesWhenCallingGetAll()
        {
            // arrange
            var entityId = CreateKey();
            var entityId2 = CreateKey(true);
            await Add(entityId2);
            await Add(entityId);
            Commit();

            // act
            var result = await Repository.GetAll();

            // assert
            var listResult = result
                .ToList()
                .Select(e => e.Id);

            Assert.Equal(new object[] { entityId2, entityId }, listResult);
        }

        [Fact]
        public async Task ReturnEmptyWhenCallingGetAllWithNoInit()
        {
            // act
            var result = await Repository.GetAll();

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReturnEntityWithIdWhenCallingGetById()
        {
            // arrange
            var entityId = CreateKey();
            await Add(entityId);
            Commit();

            // act
            var result = await Repository.GetById(entityId);

            // assert
            Assert.Equal(entityId, result.Id);
        }

        [Fact]
        public async Task ReturnNullWhenCallingGetById()
        {
            // act
            var result = await Repository.GetById(CreateKey());

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateEntity()
        {
            // arrange
            var testEntity = await Add(1L);

            // act
            await Repository.Update(testEntity);

            // assert
            Assert.Equal(EntityState.Modified, InMemoryDbContext.Entry(testEntity).State);
        }
    }
}
