using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Business.Test.Implementation
{
    public class SubjectServiceShould : ServiceShould<ISubjectService, Subject, ISubjectRepository>
    {
        private readonly ISubjectService _service;

        public SubjectServiceShould()
        {
            _service = new SubjectService(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance, LoggerMock.Object);
        }
    }
}
