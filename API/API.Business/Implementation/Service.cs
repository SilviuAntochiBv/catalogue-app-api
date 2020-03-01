using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using API.Data.Interfaces;
using API.Domain.Entities.Interfaces;
using API.Domain.Common;

namespace API.Business.Implementation
{
    public abstract class Service<TEntity, TRepository>
        where TEntity : class, IBaseEntity
        where TRepository : class, IRepository<TEntity>
    {
        protected Service(
            IUnitOfWork unitOfWork,
            IValidator<TEntity> validator,
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Validator = validator;
            Mapper = mapper;
        }

        protected IValidator<TEntity> Validator { get; }

        protected IUnitOfWork UnitOfWork { get; }

        protected IMapper Mapper { get; }

        protected virtual async Task<ValidationResult> AddToRepository(TEntity entity)
        {
            var repository = GetRepository();

            return await UpdateOrAdd(entity, repository.Add);
        }

        protected virtual async Task<bool> DeleteFromRepository(TEntity entity)
        {
            var repository = GetRepository();
            var result = await repository.Delete(entity);

            UnitOfWork.Save();

            return result;
        }

        protected virtual async Task<bool> DeleteByIdFromRepository(object id)
        {
            var repository = GetRepository();
            var result = await repository.DeleteById(id);

            UnitOfWork.Save();

            return result;
        }

        protected virtual IQueryable<TEntity> FilterFromRepository(Expression<Func<TEntity, bool>> filter)
        {
            var repository = GetRepository();
            return repository.Filter(filter);
        }

        protected virtual IQueryable<TEntity> IncludeFromRepository(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var repository = GetRepository();
            return repository.Include(includeProperties);
        }

        protected virtual async Task<IEnumerable<TEntity>> GetAllFromRepository()
        {
            var repository = GetRepository();
            return await repository.GetAll();
        }

        protected virtual async Task<TEntity> GetByIdFromRepository(object id)
        {
            var repository = GetRepository();
            return await repository.GetById(id);
        }

        protected virtual async Task<ValidationResult> UpdateToRepository(TEntity entity)
        {
            var repository = GetRepository();

            return await UpdateOrAdd(entity, repository.Update);
        }

        private TRepository GetRepository()
        {
            return UnitOfWork.GetRepository<TRepository>();
        }

        private async Task<ValidationResult> UpdateOrAdd(
            TEntity entity,
            Func<TEntity, Task<TEntity>> function)
        {
            var validationResult = Validator.Validate(entity);

            if (validationResult.IsValid)
            {
                await function(entity);
                UnitOfWork.Save();
            }

            return validationResult;
        }
    }
}
