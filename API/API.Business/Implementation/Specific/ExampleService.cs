using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using API.Business.Interfaces;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Dtos.Parameter;
using API.Domain.Entities;
using API.Domain.Logging;

namespace API.Business.Implementation.Specific
{
    public class ExampleService : Service<BaseEntity<long>, IExampleRepository>, IExampleService, ILoggable<ExampleService>
    {
        public ExampleService(
            IUnitOfWork unitOfWork,
            IValidator<BaseEntity<long>> exampleValidator,
            IMapper mapper,
            ILogger<ExampleService> logger)
            : base(unitOfWork, exampleValidator, mapper)
        {
            Logger = logger;
        }

        public ILogger<ExampleService> Logger { get; }

        public async Task<IEnumerable<BaseEntityDto>> GetAll()
        {
            Logger.LogInformation("Calling GetAll in ExampleService");

            var repositoryValues = await GetAllFromRepository();

            var result = Mapper.Map<IEnumerable<BaseEntityDto>>(repositoryValues);

            return result;
        }

        public async Task<BaseEntityDto> GetById(object id)
        {
            var repositoryValue = await GetByIdFromRepository(id);

            var result = Mapper.Map<BaseEntityDto>(repositoryValue);

            return result;
        }
    }
}
