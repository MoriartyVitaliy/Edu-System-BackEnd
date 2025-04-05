using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class LessonsDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "lessons");
        }
    }
}
