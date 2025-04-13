using System.Text.Json.Serialization;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Student
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public ICollection<LessonMark> LessonMarks { get; set; }
        public ICollection<HomeworkMark> HomeworkMarks { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<StudentParent> StudentParents { get; set; }
        public ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
}
