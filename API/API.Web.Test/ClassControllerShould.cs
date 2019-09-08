using API.Business.Interfaces;
using API.Web.Controllers;

namespace API.Web.Test
{
    public class ClassControllerShould : ControllerShould<IClassService, ClassController>
    {
        private readonly ClassController _classController;

        public ClassControllerShould()
        {
            _classController = new ClassController(ServiceMock.Object, LoggerMock.Object);
        }
    }
}
