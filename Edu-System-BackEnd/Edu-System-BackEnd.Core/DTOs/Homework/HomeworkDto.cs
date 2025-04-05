namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Homework
{
    public class HomeworkDto
    {
        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
