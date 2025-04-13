using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent
{
    public class UpdateParentDto
    {
        public UpdateUserDto User { get; set; } = new UpdateUserDto();

        public List<Guid>? StudentIds { get; set; }
    }

}
