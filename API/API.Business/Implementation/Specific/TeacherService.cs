using API.Business.Interfaces;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace API.Business.Implementation.Specific
{
    public class TeacherService : Service<Teacher, ITeacherRepository>, ITeacherService
    {
        public ILogger<ITeacherService> Logger { get; }

        public TeacherService(IUnitOfWork unitOfWork, IValidator<Teacher> validator, IMapper mapper, ILogger<ITeacherService> logger) : base(unitOfWork, validator, mapper)
        {
            Logger = logger;
        }

        
    }
}
