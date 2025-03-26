namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Parent : User
    {
        public ICollection<StudentParent> StudentParents { get; set; }
    }

}
