using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "registries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    birth = table.Column<DateOnly>(type: "date", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    telephone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_subject = table.Column<Guid>(type: "uuid", nullable: false),
                    exam_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exams", x => x.id);
                    table.ForeignKey(
                        name: "FK_exams_subjects_id_subject",
                        column: x => x.id_subject,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    classroom = table.Column<string>(type: "text", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    id_registry = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_registries_id_registry",
                        column: x => x.id_registry,
                        principalTable: "registries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_students_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_registry = table.Column<Guid>(type: "uuid", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.id);
                    table.ForeignKey(
                        name: "FK_teachers_registries_id_registry",
                        column: x => x.id_registry,
                        principalTable: "registries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teachers_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registries_exams",
                columns: table => new
                {
                    id_registry = table.Column<Guid>(type: "uuid", nullable: false),
                    id_exam = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "IX_exams_id_subject",
                table: "exams",
                column: "id_subject");

            migrationBuilder.CreateIndex(
                name: "IX_registries_exams_id_registry",
                table: "registries_exams",
                column: "id_registry");

            migrationBuilder.CreateIndex(
                name: "IX_students_id_registry",
                table: "students",
                column: "id_registry",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_id_user",
                table: "students",
                column: "id_user",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_id_registry",
                table: "teachers",
                column: "id_registry",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_id_user",
                table: "teachers",
                column: "id_user",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_subjects_id_subject",
                table: "teachers_subjects",
                column: "id_subject");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registries_exams");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "teachers_subjects");

            migrationBuilder.DropTable(
                name: "exams");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "registries");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
