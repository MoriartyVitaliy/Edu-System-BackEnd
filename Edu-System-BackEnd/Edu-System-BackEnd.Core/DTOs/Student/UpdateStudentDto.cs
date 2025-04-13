using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs
{
    public class UpdateStudentDto
    {
        public UpdateUserDto User { get; set; } = new UpdateUserDto();

        public Guid? SchoolClassId { get; set; }

        public List<Guid>? ParentIds { get; set; }

    }
}
