using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ReworkMigrationManyToManyTeacherSchoolClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes");

            migrationBuilder.DropTable(
                name: "class_supervisions");

            migrationBuilder.AddForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes");

            migrationBuilder.CreateTable(
                name: "class_supervisions",
                columns: table => new
                {
                    teacher_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    class_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_class_supervisions", x => new { x.teacher_id, x.class_id });
                    table.ForeignKey(
                        name: "fk_class_supervisions_school_classes_class_id",
                        column: x => x.class_id,
                        principalTable: "school_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_class_supervisions_teachers_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_class_supervisions_class_id",
                table: "class_supervisions",
                column: "class_id");

            migrationBuilder.AddForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id");
        }
    }
}
