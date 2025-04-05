using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeeklyScheduleController : ControllerBase
    {
        private readonly IWeeklyScheduleService _weeklyScheduleService;

        public WeeklyScheduleController(IWeeklyScheduleService weeklyScheduleService)
        {
            _weeklyScheduleService = weeklyScheduleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetWeeklySchedulesAsync()
        {
            var weeklySchedules = await _weeklyScheduleService.GetAllWeeklySchedulesAsync();
            return Ok(weeklySchedules);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeeklyScheduleByIdAsync(Guid id)
        {
            var weeklySchedule = await _weeklyScheduleService.GetWeeklyScheduleByIdAsync(id);
            if (weeklySchedule == null)
                return NotFound();
            return Ok(weeklySchedule);
        }
        [HttpPost]
        public async Task<IActionResult> AddWeeklyScheduleAsync([FromBody] CreateWeeklyScheduleDto createWeeklyScheduleDto)
        {
            var weeklyScheduleDto = await _weeklyScheduleService.CreateWeeklyScheduleAsync(createWeeklyScheduleDto);
            return CreatedAtAction(nameof(GetWeeklyScheduleByIdAsync), new { id = weeklyScheduleDto.Id }, createWeeklyScheduleDto);
        }

        [HttpPost("create-next-week")]
        public async Task<IActionResult> CreateNextWeekSchedule(Guid schoolClassId)
        {
            var result = await _weeklyScheduleService.CreateNextWeekScheduleAsync(schoolClassId);
            return Ok(result);
        }
        [HttpGet("current-week/{schoolClassId}")]
        public async Task<IActionResult> GetCurrentWeekSchedule(Guid schoolClassId)
        {
            var result = await _weeklyScheduleService.GetCurrentWeekScheduleAsync(schoolClassId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeeklyScheduleAsync(Guid id, [FromBody] UpdateWeeklyScheduleDto updateWeeklyScheduleDto)
        {
            if (id != updateWeeklyScheduleDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });
            await _weeklyScheduleService.UpdateWeeklyScheduleAsync(updateWeeklyScheduleDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeeklyScheduleAsync(Guid id)
        {
            await _weeklyScheduleService.DeleteWeeklyScheduleAsync(id);
            return NoContent();
        }
    }
}
