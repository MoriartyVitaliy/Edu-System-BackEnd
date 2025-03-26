using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities
{
    public class AuditLog
    {
        [Key]   
        public required Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public User? User { get; set; }

        public TrailType TrailType { get; set; }

        public DateTime DateUtc { get; set; }

        public required string EntityName { get; set; }

        public string? PrimaryKey { get; set; }

        public Dictionary<string, object?> OldValues { get; set; } = [];

        public Dictionary<string, object?> NewValues { get; set; } = [];

        public List<string> ChangedColumns { get; set; } = [];
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
