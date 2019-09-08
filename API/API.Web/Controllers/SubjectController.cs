using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Dtos.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("subjects")]
    public class SubjectController : AppControllerBase<ISubjectService, SubjectController>
    {
        public SubjectController(ISubjectService service, ILogger<SubjectController> logger) : base(service, logger)
        {
        }

        [HttpGet]
        [Produces(typeof(SubjectResultDto))]
        public async Task<IActionResult> Test()
        {
            var result = new SubjectResultDto
            {
                Id = 1,
                Name = "Informatica"
            };

            return Ok(result);
        }
    }
}
