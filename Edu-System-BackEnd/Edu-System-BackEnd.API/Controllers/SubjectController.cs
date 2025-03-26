using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Subject;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<SubjectDto>> GetAll()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(_mapper.Map<IEnumerable<SubjectDto>>(subjects));
            
        }
        [HttpGet("{id}", Name = "GetSubjectByIdAsync")]
        public async Task<ActionResult<SubjectDto>> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            if (subject == null) return NotFound();

            return Ok(_mapper.Map<SubjectDto>(subject));
        }


        //ToDo: Findout why CreaatedAtAction is not working

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubjectAsync([FromBody] CreateSubjectDto createSubjectDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var subject = _mapper.Map<Subject>(createSubjectDto);
            await _subjectService.CreateSubjectAsync(subject);

            var subjectResponse = _mapper.Map<SubjectDto>(subject);
            return CreatedAtRoute("GetSubjectByIdAsync", new { id = subjectResponse.Id }, subjectResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubjectAsync(Guid id, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != updateSubjectDto.Id) return BadRequest("ID in path and body do not match.");

            var subject = _mapper.Map<Subject>(updateSubjectDto);
            await _subjectService.UpdateSubjectAsync(subject);
            
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectAsync(Guid id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            if(subject == null) return NotFound();

            await _subjectService.DeleteSubjectAsync(id);
            return NoContent();
        }
    }
}
