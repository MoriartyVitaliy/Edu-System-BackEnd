using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Homework 
    {
        public Guid Id { get; set; } = Guid.NewGuid();             

        public Guid LessonId { get; set; }               
        public Lesson Lesson { get; set; }              

        [Required, StringLength(200)]
        public string Title { get; set; }          

        [Required]
        public string Description { get; set; }       

        public DateTime AssignedData { get; set; } 
        [Required]
        public DateTime DueDate { get; set; }           

        public ICollection<HomeworkSubmission> Submissions { get; set; } = new List<HomeworkSubmission>();
    }
}

