using API.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("[controller]")]
    public class StudentController : AppControllerBase<IStudentService, StudentController>
    {
        public StudentController(IStudentService studentService, ILogger<StudentController> logger) : base(studentService, logger)
        {
        }
    }
}
