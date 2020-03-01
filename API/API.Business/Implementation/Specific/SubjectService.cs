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
using static API.Business.Implementation.ReponseCreationMethods;

namespace API.Business.Implementation.Specific
{
    public class SubjectService : Service<Subject, ISubjectRepository>, ISubjectService
    {
        public ILogger<ISubjectService> Logger { get; }

        public SubjectService(
            IUnitOfWork unitOfWork, 
            IValidator<Subject> validator, 
            IMapper mapper, 
            ILogger<ISubjectService> logger)
            : base(unitOfWork, validator, mapper)
        {
            Logger = logger;
        }

        public async Task<Response<SubjectResultDto>> Add(SubjectInputDto subjectInput)
        {
            var subjectEntity = Mapper.Map<SubjectInputDto, Subject>(subjectInput);

            var validationResult = await AddToRepository(subjectEntity);

            if (!validationResult.IsValid)
                return CreateInvalidResponse<SubjectResultDto>(validationResult);

            var result = Mapper.Map<Subject, SubjectResultDto>(subjectEntity);

            return CreateValidResponse(result);
        }

        public async Task<SubjectResultDto> GetById(object id)
        {
            var entity = await GetByIdFromRepository(id);

            return entity != null ? Mapper.Map<Subject, SubjectResultDto>(entity) : null;
        }

        public async Task<IEnumerable<SubjectResultDto>> GetAll()
        {
            var entities = await GetAllFromRepository();

            return Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectResultDto>>(entities);
        }

        public async Task<Response<SubjectResultDto>> Update(int subjectId, SubjectInputDto entity)
        {
            var subject = await GetByIdFromRepository(subjectId);

            if (subject == null)
                return null;

            return null;
        }
    }
}
