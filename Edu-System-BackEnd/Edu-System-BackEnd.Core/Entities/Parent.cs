namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Parent
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<StudentParent> StudentParents { get; set; }
    }

}
