using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class HomeworkSubmission
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid HomeworkId { get; set; }
        public Homework Homework { get; set; }
        public Guid HomeworkMarkId { get; set; }
        public HomeworkMark HomeworkMark { get; set; }


        [Required]
        public string FilePath { get; set; } // Путь к загруженному файлу

        public DateTime SubmittedAt { get; set; }
        public Status Status { get; set; }

}
}
