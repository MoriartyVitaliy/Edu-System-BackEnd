using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolClassToWKSCH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "school_class_id",
                table: "weekly_schedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_weekly_schedules_school_class_id",
                table: "weekly_schedules",
                column: "school_class_id");

            migrationBuilder.AddForeignKey(
                name: "fk_weekly_schedules_school_classes_school_class_id",
                table: "weekly_schedules",
                column: "school_class_id",
                principalTable: "school_classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_weekly_schedules_school_classes_school_class_id",
                table: "weekly_schedules");

            migrationBuilder.DropIndex(
                name: "ix_weekly_schedules_school_class_id",
                table: "weekly_schedules");

            migrationBuilder.DropColumn(
                name: "school_class_id",
                table: "weekly_schedules");
        }
    }
}
