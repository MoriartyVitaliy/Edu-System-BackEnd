namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> ClassSupervisions { get; set; }
        public List<string> Roles { get; set; }
    }
}
