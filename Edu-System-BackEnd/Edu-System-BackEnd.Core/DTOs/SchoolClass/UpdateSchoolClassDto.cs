using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class UpdateSchoolClassDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid? TeacherId { get; set; }
    }
}
