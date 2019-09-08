using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Business.Interfaces;
using API.Domain.Logging;

namespace API.Web.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase, ILoggable<ExampleController>
    {
        private readonly IExampleService _exampleService;

        public ILogger<ExampleController> Logger { get; }

        public ExampleController(IExampleService exampleService, ILogger<ExampleController> logger)
        {
            _exampleService = exampleService;
            Logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Logger.LogInformation($"Calling GetAll()");

            var results = await _exampleService.GetAll();

            if (results == null)
                return NotFound();

            return Ok(results);
        }
    }
}
