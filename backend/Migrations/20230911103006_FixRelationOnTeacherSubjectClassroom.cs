using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationOnTeacherSubjectClassroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_exams_id_teacherSubjectClassroom",
                table: "exams");

            migrationBuilder.CreateIndex(
                name: "IX_exams_id_teacherSubjectClassroom",
                table: "exams",
                column: "id_teacherSubjectClassroom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_exams_id_teacherSubjectClassroom",
                table: "exams");

            migrationBuilder.CreateIndex(
                name: "IX_exams_id_teacherSubjectClassroom",
                table: "exams",
                column: "id_teacherSubjectClassroom",
                unique: true);
        }
    }
}
