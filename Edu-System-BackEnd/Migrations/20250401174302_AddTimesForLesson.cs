using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesForLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "end_time",
                table: "lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "start_time",
                table: "lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_time",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "start_time",
                table: "lessons");
        }
    }
}
