using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddWeeklyAndDailySchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "daily_schedule_id",
                table: "lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "weekly_schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_weekly_schedules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "daily_schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    school_class_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    day_of_week = table.Column<int>(type: "INTEGER", nullable: false),
                    weekly_schedule_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_daily_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_daily_schedules_school_classes_school_class_id",
                        column: x => x.school_class_id,
                        principalTable: "school_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_daily_schedules_weekly_schedules_weekly_schedule_id",
                        column: x => x.weekly_schedule_id,
                        principalTable: "weekly_schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_lessons_daily_schedule_id",
                table: "lessons",
                column: "daily_schedule_id");

            migrationBuilder.CreateIndex(
                name: "ix_daily_schedules_school_class_id",
                table: "daily_schedules",
                column: "school_class_id");

            migrationBuilder.CreateIndex(
                name: "ix_daily_schedules_weekly_schedule_id",
                table: "daily_schedules",
                column: "weekly_schedule_id");

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_daily_schedules_daily_schedule_id",
                table: "lessons",
                column: "daily_schedule_id",
                principalTable: "daily_schedules",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_lessons_daily_schedules_daily_schedule_id",
                table: "lessons");

            migrationBuilder.DropTable(
                name: "daily_schedules");

            migrationBuilder.DropTable(
                name: "weekly_schedules");

            migrationBuilder.DropIndex(
                name: "ix_lessons_daily_schedule_id",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "daily_schedule_id",
                table: "lessons");

            migrationBuilder.AddColumn<Guid>(
                name: "schedule_id",
                table: "lessons",
                type: "TEXT",
                nullable: true);

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
    }
}
