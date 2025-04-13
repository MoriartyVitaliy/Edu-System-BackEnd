using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Role;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(Guid roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);
            return Ok(role);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("by-name/{roleName}")]
        public async Task<IActionResult> GetRoleByName(string roleName)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName);
            return Ok(role);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRolesByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest("Invalid user ID.");

            var roles = await _roleService.GetRolesByUserIdAsync(userId);

            if (roles == null)
                return NotFound($"No roles found for user with ID {userId}");

            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleDto createRoleDto)
        {
            if (createRoleDto == null)
                return BadRequest("Role data is required.");
            var createdRole = await _roleService.CreateRoleAsync(createRoleDto);
            return CreatedAtAction(nameof(GetRoleById), new { roleId = createdRole.Id }, createdRole);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(Guid roleId, [FromBody] UpdateRoleDto roleDto)
        {
            if (roleId != roleDto.Id)
                return BadRequest("Role ID mismatch.");
            await _roleService.UpdateRoleAsync(roleDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {
            if (roleId == Guid.Empty)
                return BadRequest("Invalid role ID.");

            await _roleService.DeleteRoleAsyncById(roleId);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-user-role")]
        public async Task<IActionResult> AssignUserRole(Guid userId, string roleName)
        {
            if (userId == Guid.Empty || string.IsNullOrEmpty(roleName))
                return BadRequest("Invalid user ID or role name.");

            var result = await _roleService.AssignUserRole(userId, roleName);
            
            if(!result)
                return NotFound($"Role '{roleName}' not found.");
            
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("remove-user-role")]
        public async Task<IActionResult> RemoveUserRole(Guid userId, string roleName)
        {
            if (userId == Guid.Empty || string.IsNullOrEmpty(roleName))
                return BadRequest("Invalid user ID or role name.");

            var result = await _roleService.RemoveUserRoleAsync(userId, roleName);

            if (!result)
                return NotFound($"User with ID {userId} does not have the role '{roleName}'.");
            
            return NoContent();
        }
    }
}

