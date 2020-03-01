using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;

namespace API.Business.Test.Implementation
{
    public class CourseServiceShould : ServiceShould<ICourseService, Course, ICourseRepository>
    {
        private readonly ICourseService _courseService;

        public CourseServiceShould()
        {
            _courseService = new CourseService(
                UnitOfWorkMock.Object, 
                ValidatorMock.Object, 
                MapperInstance, 
                LoggerMock.Object);
        }
    }
}
