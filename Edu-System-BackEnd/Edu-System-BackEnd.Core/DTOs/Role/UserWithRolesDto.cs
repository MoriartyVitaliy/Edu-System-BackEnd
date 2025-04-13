namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Role
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
