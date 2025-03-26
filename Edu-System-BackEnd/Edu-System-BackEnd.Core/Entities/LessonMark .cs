using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class LessonMark : Mark
    {
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
