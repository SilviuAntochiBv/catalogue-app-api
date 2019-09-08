using API.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("[controller]")]
    public class CourseController : AppControllerBase<ICourseService, CourseController>
    {
        public CourseController(ICourseService courseService, ILogger<CourseController> logger) : base(courseService, logger)
        {
        }
    }
}
