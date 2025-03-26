using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.External
{
    public class AuditableInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null) return result;

            var auditLogs = GetAuditLogs(context);
            context.AddRange(auditLogs);

            return result;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return result;

            var auditLogs = GetAuditLogs(context);
            await context.AddRangeAsync(auditLogs, cancellationToken);

            return result;
        }

        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            var context = eventData.Context;
            if (context == null) return;

            var exception = eventData.Exception;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted)
                {
                    var log = new AuditLog
                    {
                        Id = Guid.NewGuid(),
                        UserId = GetCurrentUserId(context), // TODO: Подключи механизм получения текущего пользователя
                        EntityName = entry.Entity.GetType().Name,
                        DateUtc = DateTime.UtcNow,
                        TrailType = GetTrailType(entry.State),
                        PrimaryKey = GetPrimaryKey(entry),
                        IsSuccess = false,
                        ErrorMessage = exception.Message
                    };

                    context.Add(log);
                }
            }
        }

        private List<AuditLog> GetAuditLogs(DbContext context)
        {
            var logs = new List<AuditLog>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var log = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = GetCurrentUserId(context), // TODO: Подключи механизм получения текущего пользователя
                    EntityName = entry.Entity.GetType().Name,
                    DateUtc = DateTime.UtcNow,
                    TrailType = GetTrailType(entry.State),
                    PrimaryKey = GetPrimaryKey(entry),
                    OldValues = entry.State == EntityState.Added ? new() : GetValues(entry.OriginalValues),
                    NewValues = entry.State == EntityState.Deleted ? new() : GetValues(entry.CurrentValues),
                    ChangedColumns = GetChangedColumns(entry),
                    IsSuccess = true
                };

                logs.Add(log);
            }

            return logs;
        }

        private static TrailType GetTrailType(EntityState state) =>
            state switch
            {
                EntityState.Added => TrailType.Create,
                EntityState.Modified => TrailType.Update,
                EntityState.Deleted => TrailType.Delete,
                _ => TrailType.None
            };

        private static string? GetPrimaryKey(EntityEntry entry)
        {
            var key = entry.Metadata.FindPrimaryKey();
            if (key == null) return null;

            var values = key.Properties.Select(p => entry.Property(p.Name).CurrentValue?.ToString());
            return string.Join(",", values);
        }

        private static Dictionary<string, object?> GetValues(PropertyValues values)
        {
            var dictionary = new Dictionary<string, object?>();
            foreach (var property in values.Properties)
            {
                dictionary[property.Name] = values[property.Name];
            }
            return dictionary;
        }

        private static List<string> GetChangedColumns(EntityEntry entry)
        {
            return entry.Properties
                .Where(p => p.IsModified)
                .Select(p => p.Metadata.Name)
                .ToList();
        }
        private Guid? GetCurrentUserId(DbContext context)
        {
            // Здесь можно получить текущего пользователя, например, через HttpContext или JWT
            return null; // Пока заглушка
        }
    }
}
