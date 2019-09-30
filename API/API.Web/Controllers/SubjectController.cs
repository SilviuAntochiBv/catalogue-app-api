using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Dtos.Result;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{subjectId}")]
        [ProducesResponseType(typeof(SubjectResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubject(int subjectId)
        {
            var result = new SubjectResultDto
            {
                Id = 1,
                Name = "Informatica"
            };

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubjectResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSubjects()
        {
            var result = new List<SubjectResultDto>
            {
                new SubjectResultDto
                {
                    Id = 1,
                    Name = "Informatica"
                },
                new SubjectResultDto
                {
                    Id = 2,
                    Name = "Limba rusa"
                }
            };

            return Ok(result);
        }

        [HttpPatch("{subjectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditSubject(int subjectId)
        {
            return NoContent();
        }
    }
}
