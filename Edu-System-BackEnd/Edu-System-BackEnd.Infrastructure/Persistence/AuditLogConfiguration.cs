using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            // Ключ
            builder.HasKey(a => a.Id);

            // Обязательные поля
            builder.Property(a => a.Id)
                .IsRequired();

            builder.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PrimaryKey)
                .HasMaxLength(50);

            // Настройка enum, храним его как byte
            builder.Property(a => a.TrailType)
                .IsRequired()
                .HasConversion<byte>();

            builder.Property(a => a.DateUtc)
                .IsRequired();

            // Конвертер для словарей (OldValues, NewValues) с использованием System.Text.Json
            var dictionaryConverter = new ValueConverter<Dictionary<string, object?>, string>(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
                v => JsonSerializer.Deserialize<Dictionary<string, object?>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new Dictionary<string, object?>()
            );

            builder.Property(a => a.OldValues)
                .HasConversion(dictionaryConverter);

            builder.Property(a => a.NewValues)
                .HasConversion(dictionaryConverter);

            // Конвертер для списка изменённых столбцов
            var listConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
                v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<string>()
            );

            builder.Property(a => a.ChangedColumns)
                .HasConversion(listConverter);

            // Настройка статуса операции
            builder.Property(a => a.IsSuccess)
                .IsRequired();

            builder.Property(a => a.ErrorMessage)
                .HasMaxLength(500);

            // Связь с пользователем (опционально)
            builder.HasOne(a => a.User)
                .WithMany() // Если в сущности User есть коллекция AuditLogs, можно указать .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
