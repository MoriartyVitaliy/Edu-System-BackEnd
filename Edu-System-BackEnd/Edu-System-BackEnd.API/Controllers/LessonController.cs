using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetLessonsAsync()
        {
            var lessons = await _lessonService.GetAllLessonsAsync();
            return Ok(lessons);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonByIdAsync(Guid id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddLessonAsync([FromBody] CreateLessonDto createLessonDto)
        {
            var lessonDto = await _lessonService.CreateLessonAsync(createLessonDto);
            return CreatedAtAction(nameof(GetLessonByIdAsync), new { id = lessonDto.Id }, createLessonDto);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLessonAsync(Guid id, [FromBody] UpdateLessonDto updateLessonDto)
        {
            if(id != updateLessonDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _lessonService.UpdateLessonAsync(updateLessonDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLessonAsync(Guid id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return NoContent();
        }
    }
}
