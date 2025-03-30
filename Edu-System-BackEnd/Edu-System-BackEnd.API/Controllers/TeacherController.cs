using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            return Ok(teacher);
        }
        [HttpPost]
        public async Task<ActionResult<TeacherDto>> Create([FromBody] CreateTeacherDto createTeacherDto)
        {
            var createdTeacher = await _teacherService.CreateTeacherAsync(createTeacherDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTeacher.Id }, createdTeacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherDto updateTeacherDto)
        {
            if(id != updateTeacherDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _teacherService.UpdateTeacherAsync(updateTeacherDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) 
                return NotFound();

            await _teacherService.DeleteTeacherAsync(id);
            return NoContent();
        }

        [HttpGet("{teacherId}/classes")]
        public async Task<IActionResult> GetTeacherClasses(Guid teacherId)
        {
            var classes = await _teacherService.GetTeacherClassesAsync(teacherId);
            return Ok(classes);
        }

        [HttpPut("{teacherId}/classes/{classId}")]
        public async Task<IActionResult> UpdateTeacherClass(Guid teacherId, Guid classId)
        {
            await _teacherService.UpdateTeacherClassAsync(teacherId, classId);
            return NoContent();
        }

        [HttpDelete("{teacherId}/classes/{classId}")]
        public async Task<IActionResult> DeleteTeacherClass(Guid teacherId, Guid classId)
        {
            await _teacherService.DeleteTeacherClassAsync(teacherId, classId);
            return NoContent();
        }
    }
}
