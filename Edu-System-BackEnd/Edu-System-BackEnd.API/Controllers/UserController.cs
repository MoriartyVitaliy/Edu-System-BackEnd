using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin,Teacher,Student,Parent")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin,Teacher,Student,Parent")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto createUserDto)
        {
            await _userService.AddAsync(createUserDto);
            return NoContent();
        }
        [Authorize(Roles = "Admin,Teacher,Student,Parent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });
            await _userService.UpdateAsync(updateUserDto);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found." });
            await _userService.DeleteAsync(id);
            return NoContent();
        }   
    }
}
