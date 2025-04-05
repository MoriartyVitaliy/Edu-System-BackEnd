using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule
{
    public class WeeklyScheduleDto
    {
        public Guid Id { get; set; }
        public string SchoolClassName { get; set; }
        public List<DailyScheduleDto> DailySchedules { get; set; }
    }

    public class CreateWeeklyScheduleDto
    {
        public Guid SchoolClassId { get; set; }
    }

    public class UpdateWeeklyScheduleDto
    {
        public Guid Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<UpdateLessonDto> Lessons { get; set; } = new();
    }
}
