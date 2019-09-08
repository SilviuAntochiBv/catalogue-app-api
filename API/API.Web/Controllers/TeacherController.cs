using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Dtos.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("teachers")]
    public class TeacherController : AppControllerBase<ITeacherService, TeacherController>
    {
        public TeacherController(ITeacherService service, ILogger<TeacherController> logger) : base(service, logger)
        {
        }

        [HttpGet]
        [Produces(typeof(TeacherResultDto))]
        public async Task<IActionResult> Test()
        {
            var result = new TeacherResultDto
            {
                Id = 1,
                FirstName = "Manuela",
                LastName = "Serban"
            };

            return Ok(result);
        }
    }
}
