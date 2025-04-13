using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetWeeklySchedulesAsync()
        {
            var weeklySchedules = await _weeklyScheduleService.GetAllWeeklySchedulesAsync();
            return Ok(weeklySchedules);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWeeklyScheduleByIdAsync(Guid id)
        {
            var weeklySchedule = await _weeklyScheduleService.GetWeeklyScheduleByIdAsync(id);
            if (weeklySchedule == null)
                return NotFound();
            return Ok(weeklySchedule);
        }


        [HttpPost]
        [Authorize(Roles = "Teacher,Admin" )]
        public async Task<IActionResult> AddWeeklyScheduleAsync([FromBody] CreateWeeklyScheduleDto createWeeklyScheduleDto)
        {
            var weeklyScheduleDto = await _weeklyScheduleService.CreateWeeklyScheduleAsync(createWeeklyScheduleDto);
            return CreatedAtAction(nameof(GetWeeklyScheduleByIdAsync), new { id = weeklyScheduleDto.Id }, createWeeklyScheduleDto);
        }

        [HttpPost("create-next-week")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> CreateNextWeekSchedule(Guid schoolClassId)
        {
            var result = await _weeklyScheduleService.CreateNextWeekScheduleAsync(schoolClassId);
            return Ok(result);
        }
        [HttpGet("current-week/{schoolClassId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrentWeekSchedule(Guid schoolClassId)
        {
            var result = await _weeklyScheduleService.GetCurrentWeekScheduleAsync(schoolClassId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("class/by-name/{className}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WeeklyScheduleDto>>> GetSchedulesByClassName(string className)
        {
            var schedules = await _weeklyScheduleService.GetAllWeeklySchedulesByClassName(className);
            if (schedules == null || !schedules.Any())
                return NotFound();
            return Ok(schedules);
        }

        [HttpGet("class/by-id/{classId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WeeklyScheduleDto>>> GetSchedulesByClass(Guid classId)
        {
            var schedules = await _weeklyScheduleService.GetAllWeeklySchedulesByClassId(classId);

            if (schedules == null || !schedules.Any())
                return NotFound();

            return Ok(schedules);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> UpdateWeeklyScheduleAsync(Guid id, [FromBody] UpdateWeeklyScheduleDto updateWeeklyScheduleDto)
        {
            if (id != updateWeeklyScheduleDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });
            await _weeklyScheduleService.UpdateWeeklyScheduleAsync(updateWeeklyScheduleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> DeleteWeeklyScheduleAsync(Guid id)
        {
            await _weeklyScheduleService.DeleteWeeklyScheduleAsync(id);
            return NoContent();
        }
    }
}
