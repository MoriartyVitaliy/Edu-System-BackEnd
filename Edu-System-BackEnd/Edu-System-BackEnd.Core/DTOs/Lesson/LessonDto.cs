using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string Classroom { get; set; }
        public string Type { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class CreateLessonDto
    {
        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        public Guid TeacherId { get; set; }

        [Required]
        public Guid SchoolClassId { get; set; }

        [Required]
        public Guid DailyScheduleId { get; set; }

        public string Classroom { get; set; }
        public LessonType Type { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class UpdateLessonDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? SchoolClassId { get; set; }
        public string? Classroom { get; set; }
        public LessonType? Type { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
