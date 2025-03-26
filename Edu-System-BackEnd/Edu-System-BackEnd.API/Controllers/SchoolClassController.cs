using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/school-classes")]
    public class SchoolClassController : ControllerBase
    {
        private readonly ISchoolClassService _schoolClassService;
        private readonly IMapper _mapper;
        public SchoolClassController(ISchoolClassService schoolClassService, IMapper mapper)
        {
            _schoolClassService = schoolClassService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolClassDto>>> GetAll()
        {
            var schoolClasses = await _schoolClassService.GetAll();
            return Ok(_mapper.Map<IEnumerable<SchoolClassDto>>(schoolClasses));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolClassDto>> GetById(Guid id)
        {
            var schoolClass = await _schoolClassService.GetById(id);
            if (schoolClass == null) return NotFound();
            return Ok(_mapper.Map<SchoolClassDto>(schoolClass));
        }
        [HttpPost]
        public async Task<ActionResult<SchoolClassDto>> Create([FromBody] CreateSchoolClassDto createSchoolClassDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var schoolClass = _mapper.Map<SchoolClass>(createSchoolClassDto);
            await _schoolClassService.Create(schoolClass);
            var schoolClassResponse = _mapper.Map<SchoolClassDto>(schoolClass);
            return CreatedAtAction(nameof(GetById), new { id = schoolClassResponse.Id }, schoolClassResponse);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSchoolClassDto updateSchoolClassDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != updateSchoolClassDto.Id) return BadRequest("ID in path and body do not match.");
            var schoolClass = _mapper.Map<SchoolClass>(updateSchoolClassDto);
            await _schoolClassService.Update(schoolClass);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _schoolClassService.Delete(id);
            return NoContent();
        }
    }
}
