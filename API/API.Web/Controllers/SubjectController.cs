using API.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("[controller]")]
    public class SubjectController : AppControllerBase<ISubjectService, SubjectController>
    {
        public SubjectController(ISubjectService service, ILogger<SubjectController> logger) : base(service, logger)
        {
        }
    }
}
