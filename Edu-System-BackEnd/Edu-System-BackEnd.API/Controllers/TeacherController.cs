﻿using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            return Ok(teachers);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetById(Guid id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            return Ok(teacher);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<ActionResult<TeacherDto>> Create([FromBody] CreateTeacherDto createTeacherDto)
        {
            await _teacherService.AddAsync(createTeacherDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherDto updateTeacherDto)
        {
            if(id != updateTeacherDto.User.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _teacherService.UpdateAsync(updateTeacherDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null) 
                return NotFound();

            await _teacherService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("{teacherId}/classes")]
        public async Task<IActionResult> GetTeacherClasses(Guid teacherId)
        {
            var classes = await _teacherService.GetSupervisedClassesAsync(teacherId);
            return Ok(classes);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{teacherId}/classes/{classId}")]
        public async Task<IActionResult> UpdateTeacherClass(Guid teacherId, Guid classId)
        {
            await _teacherService.AddClassSupervisionAsync(teacherId, classId);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{teacherId}/classes/{classId}")]
        public async Task<IActionResult> DeleteTeacherClass(Guid teacherId, Guid classId)
        {
            await _teacherService.RemoveClassSupervisionAsync(teacherId, classId);
            return NoContent();
        }
    }
}
