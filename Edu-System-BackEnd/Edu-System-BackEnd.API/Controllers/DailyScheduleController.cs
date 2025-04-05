using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DailyScheduleController : ControllerBase
    {
        private readonly IDailyScheduleService _dailyScheduleService;

        public DailyScheduleController(IDailyScheduleService dailyScheduleService)
        {
            _dailyScheduleService = dailyScheduleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDailySchedulesAsync()
        {
            var dailySchedules = await _dailyScheduleService.GetAllDailySchedulesAsync();
            return Ok(dailySchedules);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyScheduleByIdAsync(Guid id)
        {
            var dailySchedule = await _dailyScheduleService.GetDailyScheduleByIdAsync(id);
            if (dailySchedule == null)
                return NotFound();
            return Ok(dailySchedule);
        }
        [HttpPost]
        public async Task<IActionResult> AddDailyScheduleAsync([FromBody] CreateDailyScheduleDto createDailyScheduleDto)
        {
            var dailyScheduleDto = await _dailyScheduleService.CreateDailyScheduleAsync(createDailyScheduleDto);
            return CreatedAtAction(nameof(GetDailyScheduleByIdAsync), new { id = dailyScheduleDto.Id }, createDailyScheduleDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDailyScheduleAsync(Guid id, [FromBody] UpdateDailyScheduleDto updateDailyScheduleDto)
        {
            if (id != updateDailyScheduleDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });
            await _dailyScheduleService.UpdateDailyScheduleAsync(updateDailyScheduleDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyScheduleAsync(Guid id)
        {
            await _dailyScheduleService.DeleteDailyScheduleAsync(id);
            return NoContent();
        }
        [HttpGet("class/{classId}/date/{date}")]
        public async Task<IActionResult> GetDailySchedulesByClassAndDateAsync(Guid classId, DateOnly date)
        {
            var dailySchedule = await _dailyScheduleService.GetDailySchedulesByClassAndDateAsync(classId, date);
            if (dailySchedule == null)
                return NotFound();
            return Ok(dailySchedule);
        }
        [HttpGet("class/{classId}/today")]
        public async Task<IActionResult> GetDailySchedulesByClassTodayAsync(Guid classId)
        {
            var dailySchedule = await _dailyScheduleService.GetDailySchedulesByClassTodayAsync(classId);
            if (dailySchedule == null)
                return NotFound();
            return Ok(dailySchedule);
        }
    }
}
