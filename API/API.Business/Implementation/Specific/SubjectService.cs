using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Common;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
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

        public Task<SubjectResultDto> GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SubjectResultDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<SubjectResultDto>> Update(int subjectId, SubjectInputDto entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
