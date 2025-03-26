namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class SchoolClassDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public List<string> Students { get; set; }
    }
}
