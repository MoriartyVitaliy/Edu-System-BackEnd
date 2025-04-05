using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class DailySchedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        public Guid WeeklyScheduleId { get; set; }
        public WeeklySchedule WeeklySchedule { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
