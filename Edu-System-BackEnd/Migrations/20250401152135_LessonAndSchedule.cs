using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class LessonAndSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "lesson_date",
                table: "lessons");

            migrationBuilder.AddColumn<Guid>(
                name: "schedule_id",
                table: "lessons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "lessons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    school_class_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    day_of_week = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_schedules_school_classes_school_class_id",
                        column: x => x.school_class_id,
                        principalTable: "school_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_lessons_schedule_id",
                table: "lessons",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "ix_schedules_school_class_id",
                table: "schedules",
                column: "school_class_id");

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_schedules_schedule_id",
                table: "lessons",
                column: "schedule_id",
                principalTable: "schedules",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_lessons_schedules_schedule_id",
                table: "lessons");

            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropIndex(
                name: "ix_lessons_schedule_id",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "schedule_id",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "type",
                table: "lessons");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "lesson_date",
                table: "lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
