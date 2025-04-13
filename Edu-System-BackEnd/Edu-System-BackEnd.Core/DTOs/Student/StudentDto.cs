namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class StudentDto
    {
        public UserDto User { get; set; } = new UserDto();
        public string SchoolClassName { get; set; } = string.Empty;

    }
}
