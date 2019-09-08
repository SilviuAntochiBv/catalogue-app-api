using API.Business.Interfaces;
using API.Web.Controllers;

namespace API.Web.Test
{
    public class SubjectControllerShould : ControllerShould<ISubjectService, SubjectController>
    {
        private readonly SubjectController _subjectController;

        public SubjectControllerShould()
        {
            _subjectController = new SubjectController(ServiceMock.Object, LoggerMock.Object);
        }
    }
}
