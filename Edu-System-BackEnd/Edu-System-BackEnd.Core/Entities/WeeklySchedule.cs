using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class WeeklySchedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public ICollection<DailySchedule> DailySchedules { get; set; } = new List<DailySchedule>();
    }
}
