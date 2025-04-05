using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }

        public Guid DailyScheduleId { get; set; }
        public DailySchedule DailySchedule { get; set; }

        public string Classroom { get; set; }
        public LessonType Type { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<LessonMark> LessonMarks { get; set; } = new List<LessonMark>();
    }
}