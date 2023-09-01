using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStudentExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registries_exams");

            migrationBuilder.CreateTable(
                name: "students_exams",
                columns: table => new
                {
                    id_student = table.Column<Guid>(type: "uuid", nullable: false),
                    id_exam = table.Column<Guid>(type: "uuid", nullable: false),
                    grade = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students_exams", x => new { x.id_exam, x.id_student });
                    table.ForeignKey(
                        name: "FK_students_exams_exams_id_exam",
                        column: x => x.id_exam,
                        principalTable: "exams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_students_exams_students_id_student",
                        column: x => x.id_student,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_exams_id_student",
                table: "students_exams",
                column: "id_student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students_exams");

            migrationBuilder.CreateTable(
                name: "registries_exams",
                columns: table => new
                {
                    id_exam = table.Column<Guid>(type: "uuid", nullable: false),
                    id_registry = table.Column<Guid>(type: "uuid", nullable: false),
                    grade = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registries_exams", x => new { x.id_exam, x.id_registry });
                    table.ForeignKey(
                        name: "FK_registries_exams_exams_id_exam",
                        column: x => x.id_exam,
                        principalTable: "exams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_registries_exams_registries_id_registry",
                        column: x => x.id_registry,
                        principalTable: "registries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_registries_exams_id_registry",
                table: "registries_exams",
                column: "id_registry");
        }
    }
}
