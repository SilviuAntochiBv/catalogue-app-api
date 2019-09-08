using API.Business.Interfaces;
using API.Web.Controllers;

namespace API.Web.Test
{
    public class CourseControllerShould : ControllerShould<ICourseService, CourseController>
    {
        private readonly CourseController _courseController;

        public CourseControllerShould()
        {
            _courseController = new CourseController(ServiceMock.Object, LoggerMock.Object);
        }
    }
}
