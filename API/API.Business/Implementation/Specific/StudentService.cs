using API.Business.Interfaces;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace API.Business.Implementation.Specific
{
    public class StudentService : Service<Student, IStudentRepository>, IStudentService
    {
        public ILogger<IStudentService> Logger { get; }

        public StudentService(IUnitOfWork unitOfWork, IValidator<Student> validator, IMapper mapper, ILogger<IStudentService> logger) : base(unitOfWork, validator, mapper)
        {
            Logger = logger;
        }

    }
}
