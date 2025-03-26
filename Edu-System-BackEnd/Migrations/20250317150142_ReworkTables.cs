using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ReworkTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_attendances_students_student_id",
                table: "attendances");

            migrationBuilder.DropForeignKey(
                name: "fk_homework_submissions_students_student_id",
                table: "homework_submissions");

            migrationBuilder.DropForeignKey(
                name: "fk_lessons_subjects_subject_id",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "fk_lessons_teachers_teacher_id",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "fk_marks_users_student_id",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes");

            migrationBuilder.DropForeignKey(
                name: "fk_student_parents_parents_parent_id",
                table: "student_parents");

            migrationBuilder.DropForeignKey(
                name: "fk_student_parents_students_student_id",
                table: "student_parents");

            migrationBuilder.DropForeignKey(
                name: "fk_users_school_classes_school_class_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_school_class_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "discriminator",
                table: "users");

            migrationBuilder.DropColumn(
                name: "name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "school_class_id",
                table: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateTable(
                name: "parents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parents", x => x.id);
                    table.ForeignKey(
                        name: "fk_parents_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    school_class_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "fk_students_school_classes_school_class_id",
                        column: x => x.school_class_id,
                        principalTable: "school_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_students_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.id);
                    table.ForeignKey(
                        name: "fk_subjects_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.id);
                    table.ForeignKey(
                        name: "fk_teachers_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_students_school_class_id",
                table: "students",
                column: "school_class_id");

            migrationBuilder.AddForeignKey(
                name: "fk_attendances_students_student_id",
                table: "attendances",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_homework_submissions_students_student_id",
                table: "homework_submissions",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_subjects_subject_id",
                table: "lessons",
                column: "subject_id",
                principalTable: "subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_teachers_teacher_id",
                table: "lessons",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_marks_students_student_id",
                table: "marks",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_student_parents_parents_parent_id",
                table: "student_parents",
                column: "parent_id",
                principalTable: "parents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_student_parents_students_student_id",
                table: "student_parents",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_attendances_students_student_id",
                table: "attendances");

            migrationBuilder.DropForeignKey(
                name: "fk_homework_submissions_students_student_id",
                table: "homework_submissions");

            migrationBuilder.DropForeignKey(
                name: "fk_lessons_subjects_subject_id",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "fk_lessons_teachers_teacher_id",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "fk_marks_students_student_id",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes");

            migrationBuilder.DropForeignKey(
                name: "fk_student_parents_parents_parent_id",
                table: "student_parents");

            migrationBuilder.DropForeignKey(
                name: "fk_student_parents_students_student_id",
                table: "student_parents");

            migrationBuilder.DropTable(
                name: "parents");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "discriminator",
                table: "users",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "users",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "school_class_id",
                table: "users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_users_school_class_id",
                table: "users",
                column: "school_class_id");

            migrationBuilder.AddForeignKey(
                name: "fk_attendances_students_student_id",
                table: "attendances",
                column: "student_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_homework_submissions_students_student_id",
                table: "homework_submissions",
                column: "student_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_subjects_subject_id",
                table: "lessons",
                column: "subject_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_teachers_teacher_id",
                table: "lessons",
                column: "teacher_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_marks_users_student_id",
                table: "marks",
                column: "student_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_school_classes_teachers_teacher_id",
                table: "school_classes",
                column: "teacher_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_student_parents_parents_parent_id",
                table: "student_parents",
                column: "parent_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_student_parents_students_student_id",
                table: "student_parents",
                column: "student_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_school_classes_school_class_id",
                table: "users",
                column: "school_class_id",
                principalTable: "school_classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
