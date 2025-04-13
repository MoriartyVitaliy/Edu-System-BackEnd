using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class UpdateTeacherDto
    {
        public UpdateUserDto User { get; set; } = new UpdateUserDto();
        public List<Guid>? SupervisedClassIds { get; set; }
    }
}
