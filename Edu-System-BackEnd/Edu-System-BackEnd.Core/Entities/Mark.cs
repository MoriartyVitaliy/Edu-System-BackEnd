using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public abstract class Mark
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Оцінка повинна бути в діапазоні від 1 до 12.")]
        public int Grade { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
