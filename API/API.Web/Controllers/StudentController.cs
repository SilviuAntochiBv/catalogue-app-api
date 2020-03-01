using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : AppControllerBase<IStudentService, StudentController>
    {
        public StudentController(
            IStudentService studentService, 
            ILogger<StudentController> logger) 
            : base(studentService, logger)
        {
        }

        [HttpPost]
        [Produces(typeof(StudentResultDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewStudent([FromBody] StudentInputDto student)
        {
            try
            {
                var result = await Service.Add(student);

                if (!result.IsValid)
                {
                    return BadRequest();
                }

                return Ok(result.Data);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<StudentResultDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var result = await Service.GetAll();

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet("{studentId}")]
        [Produces(typeof(StudentResultDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            try
            {
                var result = await Service.GetById(studentId);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
