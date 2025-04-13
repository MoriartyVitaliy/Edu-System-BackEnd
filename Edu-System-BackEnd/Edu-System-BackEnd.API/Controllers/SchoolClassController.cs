using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/school-classes")]
    public class SchoolClassController : ControllerBase
    {
        private readonly ISchoolClassService _schoolClassService;
        public SchoolClassController(ISchoolClassService schoolClassService)
        {
            _schoolClassService = schoolClassService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolClassDto>>> GetAll()
        {
            var schoolClasses = await _schoolClassService.GetAllSchoolClassAsync();
            return Ok(schoolClasses);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolClassDto>> GetById(Guid id)
        {
            var schoolClass = await _schoolClassService.GetSchoolClassByIdAsync(id);
            return Ok(schoolClass);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<SchoolClassDto>> Create([FromBody] CreateSchoolClassDto createSchoolClassDto)
        {
            var createSchoolClass = await _schoolClassService.CreateSchoolClassAsync(createSchoolClassDto);
            return CreatedAtAction(nameof(GetById), new { id = createSchoolClass.Id }, createSchoolClass);

        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSchoolClassDto updateSchoolClassDto)
        {
            if(id != updateSchoolClassDto.Id)
                return BadRequest(new { message = "ID in path and body do not match." });

            await _schoolClassService.UpdateSchoolClassAsync(updateSchoolClassDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _schoolClassService.DeleteSchoolClassAsync(id);
            return NoContent();
        }
    }
}
