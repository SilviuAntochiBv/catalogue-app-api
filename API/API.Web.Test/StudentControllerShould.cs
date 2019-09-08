using API.Business.Interfaces;
using API.Web.Controllers;

namespace API.Web.Test
{
    public class StudentControllerShould : ControllerShould<IStudentService, StudentController>
    {
        private readonly StudentController _studentController;

        public StudentControllerShould()
        {
            _studentController = new StudentController(ServiceMock.Object, LoggerMock.Object);
        }
    }
}
