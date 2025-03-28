using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            return Ok(_mapper.Map<StudentDto>(student));
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentDto studentDto)
        {
            var createdStudent = await _studentService.CreateStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentDto studentDto)
        {
            if(id != studentDto.Id) 
                return BadRequest(new { message = "ID in path and body do not match." });
            
            await _studentService.UpdateStudentAsync(studentDto);
            return NoContent();
        }

        [HttpPut("update-class")]
        public async Task<IActionResult> UpdateStudentClassAsync(Guid studentId, Guid classId)
        {
            await _studentService.UpdateStudentClassAsync(studentId, classId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
