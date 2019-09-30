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
    [Route("subjects")]
    public class SubjectController : AppControllerBase<ISubjectService, SubjectController>
    {
        public SubjectController(ISubjectService service, ILogger<SubjectController> logger) : base(service, logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubjectResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var retrievedSubjects = await Service.GetAll();

                return Ok(retrievedSubjects);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet("{subjectId}")]
        [ProducesResponseType(typeof(SubjectResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubject(int subjectId)
        {
            try
            {
                var retrievedSubject = await Service.GetById(subjectId);

                if (retrievedSubject == null)
                    return NotFound();

                return Ok(retrievedSubject);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPatch("{subjectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditSubject(int subjectId, [FromBody]SubjectInputDto subjectInputDto)
        {
            if (!ModelState.IsValid || IsEditBodyInvalid(subjectInputDto))
                return BadRequest();

            try
            {
                var editSubjectResponse = await Service.Update(subjectId, subjectInputDto);

                if (editSubjectResponse == null)
                    return NotFound();

                if (!editSubjectResponse.IsValid)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        
        private bool IsEditBodyInvalid(SubjectInputDto subjectInputDto)
        {
            if (subjectInputDto == null)
                return true;

            return string.IsNullOrWhiteSpace(subjectInputDto.Name) && string.IsNullOrWhiteSpace(subjectInputDto.Description);
        }
    }
}
