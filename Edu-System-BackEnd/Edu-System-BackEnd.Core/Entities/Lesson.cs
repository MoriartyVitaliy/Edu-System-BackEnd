using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }

        public DateTime LessonDate { get; set; }

        [StringLength(20)]
        public string Classroom { get; set; }

        public ICollection<LessonMark> LessonMarks { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
    }

}
