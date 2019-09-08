﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using API.Business.Implementation;
using API.Business.Test.Utilities;
using API.Data.Interfaces;
using API.Domain.Entities.Interfaces;
using API.Domain.Interfaces;
using Xunit;
using Microsoft.Extensions.Logging;

namespace API.Business.Test.Implementation
{
    public abstract class ServiceShould<TService, TEntity, TRepo>
        where TService: class
        where TEntity : class, IBaseEntity
        where TRepo : class, IRepository<TEntity>
    {
        private class InternalService<TInternalEntity, TRepository> : Service<TInternalEntity, TRepository>,
            IAddable<TInternalEntity, ValidationResult>,
            IDeletable<TInternalEntity>,
            IInterrogable<TInternalEntity>,
            IUpdatable<TInternalEntity, ValidationResult>
            where TInternalEntity : class, IBaseEntity
            where TRepository : class, IRepository<TInternalEntity>
        {
            public InternalService(IUnitOfWork unitOfWork, IValidator<TInternalEntity> validator, IMapper mapper) : base(unitOfWork, validator, mapper)
            {
            }

            public async Task<ValidationResult> Add(TInternalEntity entity)
            {
                return await AddToRepository(entity);
            }

            public async Task<bool> Delete(TInternalEntity entity)
            {
                return await DeleteFromRepository(entity);
            }

            public async Task<bool> DeleteById(object id)
            {
                return await DeleteByIdFromRepository(id);
            }

            public async Task<IEnumerable<TInternalEntity>> GetAll()
            {
                return await GetAllFromRepository();
            }

            public async Task<TInternalEntity> GetById(object id)
            {
                return await GetByIdFromRepository(id);
            }

            public async Task<ValidationResult> Update(TInternalEntity entity)
            {
                return await UpdateToRepository(entity);
            }

            public async Task<IEnumerable<TInternalEntity>> Filter(Expression<Func<TInternalEntity, bool>> filter)
            {
                return await Task.FromResult(FilterFromRepository(filter));
            }

            public async Task<IEnumerable<TInternalEntity>> Include(params Expression<Func<TInternalEntity, object>>[] includeProperties)
            {
                return await Task.FromResult(IncludeFromRepository(includeProperties));
            }
        }

        private readonly InternalService<TEntity, TRepo> _internalService;

        protected Mock<ILogger<TService>> LoggerMock { get; }

        protected Mock<IUnitOfWork> UnitOfWorkMock { get; }

        protected Mock<IValidator<IBaseEntity>> ValidatorMock { get; }

        protected IMapper MapperInstance { get; }

        protected Mock<TRepo> RepositoryMock { get; }

        protected ServiceShould()
        {
            // startup
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            ValidatorMock = new Mock<IValidator<IBaseEntity>>();
            RepositoryMock = new Mock<TRepo>();

            UnitOfWorkMock
             .Setup(uow => uow.GetRepository<TRepo>())
             .Returns(RepositoryMock.Object);

            MapperInstance = AutoMapperUtilities.Init();

            LoggerMock = new Mock<ILogger<TService>>();

            _internalService = new InternalService<TEntity, TRepo>(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance);
        }

        [Fact]
        public async Task CallRepositoryWhenCallingGetAll()
        {
            // act
            await _internalService.GetAll();

            // assert
            RepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2000)]
        public async Task CallRepositoryWhenCallingGetById(int id)
        {
            // act
            await _internalService.GetById(id);

            // assert
            RepositoryMock.Verify(repo => repo.GetById(id), Times.Once);
        }

        [Fact]
        public async Task CallValidationWhenCallingAdd()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(new ValidationResult());

            // act
            await _internalService.Add(entity);

            // assert
            ValidatorMock.Verify(v => v.Validate(entity), Times.Once);
        }

        [Fact]
        public async Task CallRepositoryWhenCallingAddForGoodValidation()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;
            var validationResultMock = new Mock<ValidationResult>();
            validationResultMock.Setup(vrm => vrm.IsValid).Returns(true);

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(validationResultMock.Object);

            // act
            await _internalService.Add(entity);

            // assert
            RepositoryMock.Verify(repo => repo.Add(entity), Times.Once);
        }

        [Fact]
        public async Task NotCallRepositoryWhenCallingAddForBadValidation()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;
            var validationResultMock = new Mock<ValidationResult>();
            validationResultMock.Setup(vrm => vrm.IsValid).Returns(false);

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(validationResultMock.Object);

            // act
            await _internalService.Add(entity);

            // assert
            RepositoryMock.Verify(repo => repo.Add(entity), Times.Never);
        }

        [Fact]
        public async Task CallValidationWhenCallingUpdate()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(new ValidationResult());

            // act
            await _internalService.Update(entity);

            // assert
            ValidatorMock.Verify(v => v.Validate(entity), Times.Once);
        }

        [Fact]
        public async Task CallRepositoryWhenCallingUpdateForGoodValidation()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;
            var validationResultMock = new Mock<ValidationResult>();
            validationResultMock.Setup(vrm => vrm.IsValid).Returns(true);

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(validationResultMock.Object);

            // act
            await _internalService.Update(entity);

            // assert
            RepositoryMock.Verify(repo => repo.Update(entity), Times.Once);
        }

        [Fact]
        public async Task NotCallRepositoryWhenCallingUpdateForBadValidation()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;
            var validationResultMock = new Mock<ValidationResult>();
            validationResultMock.Setup(vrm => vrm.IsValid).Returns(false);

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(validationResultMock.Object);

            // act
            await _internalService.Update(entity);

            // assert
            RepositoryMock.Verify(repo => repo.Update(entity), Times.Never);
        }

        [Fact]
        public async Task CallRepositoryForDelete()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;

            // act
            await _internalService.Delete(entity);

            // assert
            RepositoryMock.Verify(repo => repo.Delete(entity), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(20000)]
        public async Task CallRepositoryForDeleteById(int id)
        {
            // act
            await _internalService.DeleteById(id);

            // assert
            RepositoryMock.Verify(repo => repo.DeleteById(id), Times.Once);
        }

        [Fact]
        public async Task CallSaveChangesAfterAdd()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;
            var validationResultMock = new Mock<ValidationResult>();
            validationResultMock.Setup(vrm => vrm.IsValid).Returns(true);

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(validationResultMock.Object);

            // act
            await _internalService.Add(entity);

            // assert
            UnitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
        }

        [Fact]
        public async Task CallSaveChangesAfterUpdate()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;
            var validationResultMock = new Mock<ValidationResult>();
            validationResultMock.Setup(vrm => vrm.IsValid).Returns(true);

            ValidatorMock
                .Setup(v => v.Validate(entity))
                .Returns(validationResultMock.Object);

            // act
            await _internalService.Update(entity);

            // assert
            UnitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
        }

        [Fact]
        public async Task CallSaveChangesAfterDelete()
        {
            // arrange
            var mockedEntity = new Mock<TEntity>();
            var entity = mockedEntity.Object;

            // act
            await _internalService.Delete(entity);

            // assert
            UnitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(20000)]
        public async Task CallSaveChangesAfterDeleteById(int id)
        {
            // act
            await _internalService.DeleteById(id);

            // assert
            UnitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
        }

        [Fact]
        public async Task CallFilterMethodFromRepositoryWhenUsingInternalFilterMethod()
        {
            // arrange
            Expression<Func<TEntity, bool>> filterExpression = x => x.Id != null;

            // act
            await _internalService.Filter(filterExpression);

            // assert
            RepositoryMock.Verify(repo => repo.Filter(It.Is<Expression<Func<TEntity, bool>>>(matcher => matcher == filterExpression)));
        }

        [Fact]
        public async Task CallIncludeMethodFromRepositoryWhenUsingInternalIncludeMethod()
        {
            // arrange
            Expression<Func<TEntity, object>> includeExpression = x => x.Id;

            // act
            await _internalService.Include(includeExpression);

            // assert
            RepositoryMock.Verify(repo => repo.Include(It.Is<Expression<Func<TEntity, object>>>(matcher => matcher == includeExpression)));
        }
    }
}
