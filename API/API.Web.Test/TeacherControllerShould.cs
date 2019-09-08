using API.Business.Interfaces;
using API.Web.Controllers;

namespace API.Web.Test
{
    public class TeacherControllerShould : ControllerShould<ITeacherService, TeacherController>
    {
        private readonly TeacherController _teacherController;

        public TeacherControllerShould()
        {
            _teacherController = new TeacherController(ServiceMock.Object, LoggerMock.Object);
        }
    }
}
