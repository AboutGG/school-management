using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTeacherSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teachers_subjects");

            migrationBuilder.DropColumn(
                name: "classroom",
                table: "students");

            migrationBuilder.AddColumn<Guid>(
                name: "id_classroom",
                table: "students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "classrooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classrooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teachers_subjects_classrooms",
                columns: table => new
                {
                    id_teacher = table.Column<Guid>(type: "uuid", nullable: false),
                    id_subject = table.Column<Guid>(type: "uuid", nullable: false),
                    id_classroom = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers_subjects_classrooms", x => new { x.id_teacher, x.id_subject, x.id_classroom });
                    table.ForeignKey(
                        name: "FK_teachers_subjects_classrooms_classrooms_id_classroom",
                        column: x => x.id_classroom,
                        principalTable: "classrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teachers_subjects_classrooms_subjects_id_subject",
                        column: x => x.id_subject,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teachers_subjects_classrooms_teachers_id_teacher",
                        column: x => x.id_teacher,
                        principalTable: "teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_id_classroom",
                table: "students",
                column: "id_classroom");

            migrationBuilder.CreateIndex(
                name: "IX_classrooms_name",
                table: "classrooms",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_subjects_classrooms_id_classroom",
                table: "teachers_subjects_classrooms",
                column: "id_classroom");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_subjects_classrooms_id_subject",
                table: "teachers_subjects_classrooms",
                column: "id_subject");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classrooms_id_classroom",
                table: "students",
                column: "id_classroom",
                principalTable: "classrooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classrooms_id_classroom",
                table: "students");

            migrationBuilder.DropTable(
                name: "teachers_subjects_classrooms");

            migrationBuilder.DropTable(
                name: "classrooms");

            migrationBuilder.DropIndex(
                name: "IX_students_id_classroom",
                table: "students");

            migrationBuilder.DropColumn(
                name: "id_classroom",
                table: "students");

            migrationBuilder.AddColumn<string>(
                name: "classroom",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "teachers_subjects",
                columns: table => new
                {
                    id_teacher = table.Column<Guid>(type: "uuid", nullable: false),
                    id_subject = table.Column<Guid>(type: "uuid", nullable: false),
                    classroom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers_subjects", x => new { x.id_teacher, x.id_subject });
                    table.ForeignKey(
                        name: "FK_teachers_subjects_subjects_id_subject",
                        column: x => x.id_subject,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teachers_subjects_teachers_id_teacher",
                        column: x => x.id_teacher,
                        principalTable: "teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teachers_subjects_id_subject",
                table: "teachers_subjects",
                column: "id_subject");
        }
    }
}
