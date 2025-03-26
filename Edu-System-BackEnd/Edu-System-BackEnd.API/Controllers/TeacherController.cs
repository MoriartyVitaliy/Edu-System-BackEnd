using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(_mapper.Map<IEnumerable<TeacherDto>>(teachers));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) return NotFound();

            return Ok(_mapper.Map<TeacherDto>(teacher));
        }
        [HttpPost]
        public async Task<ActionResult<TeacherDto>> Create([FromBody] CreateTeacherDto createTeacherDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacher = _mapper.Map<Teacher>(createTeacherDto);
            await _teacherService.CreateTeacherAsync(teacher);

            var teacherResponse = _mapper.Map<TeacherDto>(teacher);
            return CreatedAtAction(nameof(GetById), new { id = teacherResponse.Id }, teacherResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherDto teacherDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != teacherDto.Id) return BadRequest("ID in path and body do not match.");

            var teacher = _mapper.Map<Teacher>(teacherDto);
            await _teacherService.UpdateTeacherAsync(teacher);

            return NoContent();
        }
        [HttpPut("update-class")]
        public async Task<IActionResult> UpdateClass(Guid teacherId, Guid classId)
        {
            await _teacherService.UpdateTeacherClassAsync(teacherId, classId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) return NotFound();

            await _teacherService.DeleteTeacherAsync(id);
            return NoContent();
        }
    }
}
