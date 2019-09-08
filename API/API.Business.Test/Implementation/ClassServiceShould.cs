using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Business.Test.Implementation
{
    public class ClassServiceShould : ServiceShould<IClassService, Class, IClassRepository>
    {
        private readonly IClassService _service;

        public ClassServiceShould()
        {
            _service = new ClassService(UnitOfWorkMock.Object, ValidatorMock.Object, MapperInstance, LoggerMock.Object);
        }
    }
}
