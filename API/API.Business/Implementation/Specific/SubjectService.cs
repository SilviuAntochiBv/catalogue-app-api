using API.Business.Interfaces;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace API.Business.Implementation.Specific
{
    public class SubjectService : Service<Subject, ISubjectRepository>, ISubjectService
    {
        public ILogger<ISubjectService> Logger { get; }

        public SubjectService(IUnitOfWork unitOfWork, IValidator<Subject> validator, IMapper mapper, ILogger<ISubjectService> logger) : base(unitOfWork, validator, mapper)
        {
            Logger = logger;
        }

    }
}
