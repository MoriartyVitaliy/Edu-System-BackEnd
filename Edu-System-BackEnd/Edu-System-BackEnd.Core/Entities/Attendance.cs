namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Attendance
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public bool IsPresent { get; set; }
    }
}
