using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public abstract class Mark
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [Range(1, 12)]
        public int Grade { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
