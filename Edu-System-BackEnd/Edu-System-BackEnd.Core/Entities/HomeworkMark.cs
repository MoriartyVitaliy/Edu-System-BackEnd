using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class HomeworkMark : Mark
    {
        public Guid HomeworkSubmissionId { get; set; }
        public HomeworkSubmission HomeworkSubmission { get; set; }
    }
}
