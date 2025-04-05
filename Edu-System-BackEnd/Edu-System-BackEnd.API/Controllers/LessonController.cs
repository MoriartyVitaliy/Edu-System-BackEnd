using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        [HttpGet]
        public async Task<IActionResult> GetLessonsAsync()
        {
            var lessons = await _lessonService.GetAllLessonsAsync();
            return Ok(lessons);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonByIdAsync(Guid id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }
        [HttpPost]
        public async Task<IActionResult> AddLessonAsync([FromBody] CreateLessonDto createLessonDto)
        {
            var lessonDto = await _lessonService.CreateLessonAsync(createLessonDto);
            return CreatedAtAction(nameof(GetLessonByIdAsync), new { id = lessonDto.Id }, createLessonDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLessonAsync(Guid id, [FromBody] UpdateLessonDto updateLessonDto)
        {
            if(id != updateLessonDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _lessonService.UpdateLessonAsync(updateLessonDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLessonAsync(Guid id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return NoContent();
        }
    }
}
