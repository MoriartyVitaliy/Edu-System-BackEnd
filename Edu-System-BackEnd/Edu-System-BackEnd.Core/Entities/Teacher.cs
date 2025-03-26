namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Teacher : User
    {
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<SchoolClass> ClassSupervisions { get; set; } = [];// Классы, где он классный руководитель
    }
}
