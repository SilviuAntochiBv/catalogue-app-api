using API.Business.Interfaces;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace API.Business.Implementation.Specific
{
    public class ClassService : Service<Class, IClassRepository>, IClassService
    {
        public ILogger<IClassService> Logger { get; }

        public ClassService(IUnitOfWork unitOfWork, IValidator<Class> validator, IMapper mapper, ILogger<IClassService> logger) : base(unitOfWork, validator, mapper)
        {
            Logger = logger;
        }

        
    }
}
