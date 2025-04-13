using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class CreateTeacherDto
    {
        public CreateUserDto User { get; set; } = new CreateUserDto();
        public List<Guid>? SupervisedClassIds { get; set; }
    }
}
