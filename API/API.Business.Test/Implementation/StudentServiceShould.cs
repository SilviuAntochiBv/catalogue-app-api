using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Business.Test.Implementation
{
    public class StudentServiceShould : ServiceShould<IStudentService, Student, IStudentRepository>
    {
        private readonly IStudentService _service;

        public StudentServiceShould()
        {
            _service = new StudentService(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance, LoggerMock.Object);
        }
    }
}
