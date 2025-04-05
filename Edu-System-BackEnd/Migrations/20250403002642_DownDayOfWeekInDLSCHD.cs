using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DownDayOfWeekInDLSCHD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "day_of_week",
                table: "daily_schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "day_of_week",
                table: "daily_schedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
