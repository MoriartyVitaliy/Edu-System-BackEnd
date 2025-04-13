using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Phone number must be in the format +380XXXXXXXXX.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }

}
