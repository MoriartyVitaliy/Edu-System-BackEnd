using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class CreateStudentDto
    {
        public CreateUserDto User { get; set; }
        public Guid SchoolClassId { get; set; }
    }
}
