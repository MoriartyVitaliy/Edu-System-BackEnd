using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/parents")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentDto>>> GetAll()
        {
            var parents = await _parentService.GetAllParentsAsync();
            return Ok(parents);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ParentDto>> GetById(Guid id)
        {
            var parent = await _parentService.GetParentByIdAsync(id);
            return Ok(parent);
        }
        [HttpPost]
        public async Task<ActionResult<ParentDto>> Create([FromBody] CreateParentDto createParentDto)
        {
            var createdParent = await _parentService.CreateParentAsync(createParentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdParent.Id }, createdParent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateParentDto updateParentDto)
        {
            if (id != updateParentDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _parentService.UpdateParentAsync(updateParentDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _parentService.DeleteParentAsync(id);
            return NoContent();
        }
        [HttpPut("update-parent-student")]
        public async Task<IActionResult> UpdateStudentParent(Guid parentId, Guid studentId)
        {
            await _parentService.UpdateParentStudentAsync(parentId, studentId);
            return NoContent();
        }
        [HttpGet("{parentId}/students")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentParent(Guid parentId)
        {
            var students = await _parentService.GetParentStudentAsync(parentId);
            if (students == null) return NotFound();
            return Ok(students);
        }
    }
}
