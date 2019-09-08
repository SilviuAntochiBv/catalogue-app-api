using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Dtos.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("classes")]
    public class ClassController : AppControllerBase<IClassService, ClassController>
    {
        public ClassController(IClassService service, ILogger<ClassController> logger) : base(service, logger)
        {
        }
        
        [HttpGet]
        [Produces(typeof(ClassResultDto))]
        public async Task<IActionResult> Test()
        {
            var result = new List<ClassResultDto>
            {
                new ClassResultDto
                {
                    Id = 1,
                    Name = "XII C",
                    Students = new List<StudentResultDto>
                    {
                        new StudentResultDto
                        {
                            Id = 1,
                            Age = 27,
                            FirstName = "Silviu",
                            LastName = "Antochi"
                        }
                    }
                }
            };

            return Ok(result);
        }
    }
}
