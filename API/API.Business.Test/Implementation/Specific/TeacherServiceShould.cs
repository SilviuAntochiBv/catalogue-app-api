using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Business.Test.Implementation
{
    public class TeacherServiceShould : ServiceShould<ITeacherService, Teacher, ITeacherRepository>
    {
        private readonly ITeacherService _service;

        public TeacherServiceShould()
        {
            _service = new TeacherService(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance, LoggerMock.Object);
        }
    }
}
