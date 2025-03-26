using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class UpdateTeacherDto
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }
            
        [Required]
        public string MiddleName { get; set; }
            
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string PasswordHash { get; set; }

    }
}
