using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/parents")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentDto>>> GetAll()
        {
            var parents = await _parentService.GetAllAsync();
            return Ok(parents);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ParentDto>> GetById(Guid id)
        {
            var parent = await _parentService.GetByIdAsync(id);
            return Ok(parent);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ParentDto>> Create([FromBody] CreateParentDto createParentDto)
        {
            await _parentService.AddAsync(createParentDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateParentDto updateParentDto)
        {
            if (id != updateParentDto.User.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _parentService.UpdateAsync(updateParentDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _parentService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("update-parent-student")]
        public async Task<IActionResult> UpdateStudentParent(Guid parentId, Guid studentId)
        {
            await _parentService.LinkChildAsync(parentId, studentId);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Teacher,Parent")]
        [HttpGet("{parentId}/students")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentParent(Guid parentId)
        {
            var students = await _parentService.GetChildrenAsync(parentId);
            if (students == null) 
                return NotFound();

            return Ok(students);
        }
    }
}
