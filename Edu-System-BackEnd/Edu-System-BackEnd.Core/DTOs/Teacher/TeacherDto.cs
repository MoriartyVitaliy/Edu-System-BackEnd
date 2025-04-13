namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class TeacherDto
    {
        public UserDto User { get; set; } = new UserDto();
        public List<string>? ClassSupervisions { get; set; }
    }
}
