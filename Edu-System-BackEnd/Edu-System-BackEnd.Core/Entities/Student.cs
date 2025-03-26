using System.Text.Json.Serialization;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Student : User
    {
        public Guid SchoolClassId { get; set; }
        [JsonIgnore]
        public SchoolClass SchoolClass { get; set; }
        public ICollection<LessonMark> LessonMarks { get; set; }
        public ICollection<HomeworkMark> HomeworkMarks { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<StudentParent> StudentParents { get; set; }
        public ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
}
