using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent
{
    public class CreateParentDto
    {
        public CreateUserDto User { get; set; }
        public List<Guid>? StudentIds { get; set; }
    }
}
