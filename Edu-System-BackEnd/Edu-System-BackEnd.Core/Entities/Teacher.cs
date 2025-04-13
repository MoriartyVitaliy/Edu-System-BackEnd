namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Teacher
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<SchoolClass> ClassSupervisions { get; set; } = [];
    }
}
