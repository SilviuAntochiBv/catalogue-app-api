using API.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("[controller]")]
    public class TeacherController : AppControllerBase<ITeacherService, TeacherController>
    {
        public TeacherController(ITeacherService service, ILogger<TeacherController> logger) : base(service, logger)
        {
        }
    }
}
