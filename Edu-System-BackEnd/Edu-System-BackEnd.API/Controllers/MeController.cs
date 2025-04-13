using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IParentService _parentService;

        public MeController(
            IUserService userService,
            ITeacherService teacherService,
            IStudentService studentService,
            IParentService parentService)
        {
            _userService = userService;
            _teacherService = teacherService;
            _studentService = studentService;
            _parentService = parentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<object>> GetMeAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var user = await _userService.GetByEmailAsync(email);
            if (user == null) return NotFound(new { message = "User not found." });

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Roles.ToList()
            };

            var role = user.Roles.FirstOrDefault();

            return role switch
            {
                "Student" => Ok(new
                {
                    Role = "Student",
                    User = userDto,
                    RoleData = await _studentService.GetByIdAsync(user.Id) // повертає StudentDto
                }),
                "Teacher" => Ok(new
                {
                    Role = "Teacher",
                    User = userDto,
                    RoleData = await _teacherService.GetByIdAsync(user.Id) // повертає TeacherDto
                }),
                "Parent" => Ok(new
                {
                    Role = "Parent",
                    User = userDto,
                    RoleData = await _parentService.GetByIdAsync(user.Id) // повертає ParentDto
                }),
                _ => Ok(new
                {
                    Role = "Admin",
                    User = userDto,
                    RoleData = _userService.GetByIdAsync(user.Id) // повертає UserDto
                })
            };
        }
    }
}
