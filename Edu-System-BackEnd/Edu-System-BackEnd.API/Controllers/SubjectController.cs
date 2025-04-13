using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Subject;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<SubjectDto>> GetAll()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
            
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetById(Guid id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            return Ok(subject);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubjectAsync([FromBody] CreateSubjectDto createSubjectDto)
        {
            var createdSubject = await _subjectService.CreateSubjectAsync(createSubjectDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSubject.Id }, createdSubject);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubjectAsync(Guid id, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            if(id != updateSubjectDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _subjectService.UpdateSubjectAsync(updateSubjectDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectAsync(Guid id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return NoContent();
        }
    }
}
