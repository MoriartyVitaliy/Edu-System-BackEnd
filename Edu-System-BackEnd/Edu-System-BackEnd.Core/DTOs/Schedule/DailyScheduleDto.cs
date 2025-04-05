using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule
{
    public class DailyScheduleDto
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string SchoolClassName { get; set; }
        public List<LessonDto> Lessons { get; set; }
    }

    public class CreateDailyScheduleDto
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public Guid WeeklyScheduleId { get; set; }
        public List<Guid> LessonIds { get; set; } = new List<Guid>();
    }

    public class UpdateDailyScheduleDto
    {
        [Required]
        public Guid Id { get; set; }

        public DateOnly? Date { get; set; }
        public List<Guid> LessonIds { get; set; } = new List<Guid>();
    }
}
