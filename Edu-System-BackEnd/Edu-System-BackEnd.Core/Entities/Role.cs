using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}

