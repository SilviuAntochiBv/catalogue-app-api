﻿using System;
using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain.Dtos.Parameter;
using API.Domain.Dtos.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [Route("students")]
    public class StudentController : AppControllerBase<IStudentService, StudentController>
    {
        public StudentController(IStudentService studentService, ILogger<StudentController> logger) : base(studentService, logger)
        {
        }

        [HttpPost]
        [Produces(typeof(StudentResultDto))]
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
        [Produces(typeof(StudentResultDto))]
        public async Task<IActionResult> Test()
        {
            var result = new StudentResultDto
            {
                Id = 1,
                Age = 27,
                FirstName = "Silviu",
                LastName = "Antochi"
            };

            return Ok(result);
        }
    }
}