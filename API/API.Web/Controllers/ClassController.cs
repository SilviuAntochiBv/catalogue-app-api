using API.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("[controller]")]
    public class ClassController : AppControllerBase<IClassService, ClassController>
    {
        public ClassController(IClassService service, ILogger<ClassController> logger) : base(service, logger)
        {
        }
    }
}
