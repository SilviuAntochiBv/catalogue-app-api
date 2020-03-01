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
    public class StudentService : Service<Student, IStudentRepository>, IStudentService
    {
        public ILogger<IStudentService> Logger { get; }

        public StudentService(
            IUnitOfWork unitOfWork,
            IValidator<Student> validator,
            IMapper mapper,
            ILogger<IStudentService> logger)
            : base(unitOfWork, validator, mapper)
        {
            Logger = logger;
        }

        public async Task<Response<StudentResultDto>> Add(StudentInputDto input)
        {
            var studentEntity = Mapper.Map<StudentInputDto, Student>(input);

            var validationResult = await AddToRepository(studentEntity);

            if (!validationResult.IsValid)
            {
                return CreateInvalidResponse<StudentResultDto>(validationResult);
            }

            var studentResultDto = Mapper.Map<Student, StudentResultDto>(studentEntity);

            return CreateValidResponse(studentResultDto);
        }

        public async Task<IEnumerable<StudentResultDto>> GetAll()
        {
            var entities = await GetAllFromRepository();

            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentResultDto>>(entities);
        }

        public async Task<StudentResultDto> GetById(object id)
        {
            var studentEntity = await GetByIdFromRepository(id);

            return studentEntity != null ? Mapper.Map<Student, StudentResultDto>(studentEntity) : null;
        }
    }
}
