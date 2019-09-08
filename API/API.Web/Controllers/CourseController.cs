using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Dtos.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("courses")]
    public class CourseController : AppControllerBase<ICourseService, CourseController>
    {
        public CourseController(ICourseService courseService, ILogger<CourseController> logger) : base(courseService, logger)
        {
        }

        [HttpGet]
        [Produces(typeof(CourseResultDto))]
        public async Task<IActionResult> Test()
        {
            var result = new CourseResultDto
            {
                Id = 1,
                Name = "Informatica cls. 6",
                Subject = new SubjectResultDto
                {
                    Id = 1,
                    Name = "Informatica"
                },
                Teacher = new TeacherResultDto
                {
                    Id = 1,
                    FirstName = "Manuela",
                    LastName = "Serban"
                }
            };

            return Ok(result);
        }
    }
}
