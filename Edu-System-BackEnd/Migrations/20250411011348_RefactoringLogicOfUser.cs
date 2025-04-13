using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringLogicOfUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_parents_users_id",
                table: "parents");

            migrationBuilder.DropForeignKey(
                name: "fk_students_users_id",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "fk_teachers_users_id",
                table: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers",
                table: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_parents",
                table: "parents");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "teachers",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "students",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "parents",
                newName: "user_id");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "users",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_teachers",
                table: "teachers",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_students",
                table: "students",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_parents",
                table: "parents",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_parents_users_user_id",
                table: "parents",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_students_users_user_id",
                table: "students",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_teachers_users_user_id",
                table: "teachers",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_parents_users_user_id",
                table: "parents");

            migrationBuilder.DropForeignKey(
                name: "fk_students_users_user_id",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "fk_teachers_users_user_id",
                table: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_teachers",
                table: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_students",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "pk_parents",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "teachers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "students",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "parents",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers",
                table: "teachers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_parents",
                table: "parents",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_parents_users_id",
                table: "parents",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_students_users_id",
                table: "students",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_teachers_users_id",
                table: "teachers",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
