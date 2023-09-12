using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_registries_id_registry",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_exams_students_id_student",
                table: "students_exams");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_subjects_classrooms_teachers_id_teacher",
                table: "teachers_subjects_classrooms");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_id_registry",
                table: "students");

            migrationBuilder.DeleteData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("70f432dc-2a6c-499b-9326-52d1506befa5"));

            migrationBuilder.DeleteData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("06dec5ca-003e-4b39-af43-c745746d23e0"));

            migrationBuilder.DeleteData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"));

            migrationBuilder.DeleteData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyColumnType: "uuid",
                keyValue: new Guid("007d3bca-d81d-42bd-9194-9c1d9f1f5ed7"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyColumnType: "uuid",
                keyValue: new Guid("78362ba2-29ea-472b-9878-f55dad233e21"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyColumnType: "uuid",
                keyValue: new Guid("8767fd02-7891-4b47-8b02-3cc0d07ac334"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("46fd8c9d-b689-47cb-b9fd-44a19c5291a4"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("47e8b0b5-1b53-46be-a0a9-9954958d3071"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("b55de490-fcdd-43d3-9146-94774e96cfe6"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"));

            migrationBuilder.DeleteData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"));

            migrationBuilder.DeleteData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"));

            migrationBuilder.DropColumn(
                name: "id",
                table: "students");

            migrationBuilder.DropColumn(
                name: "id_registry",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "id_teacher",
                table: "teachers_subjects_classrooms",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "IX_teachers_subjects_classrooms_id_teacher",
                table: "teachers_subjects_classrooms",
                newName: "IX_teachers_subjects_classrooms_id_user");

            migrationBuilder.RenameColumn(
                name: "id_student",
                table: "students_exams",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "IX_students_exams_id_student",
                table: "students_exams",
                newName: "IX_students_exams_id_user");

            migrationBuilder.AddColumn<Guid>(
                name: "RegistryId",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "is_written",
                table: "exams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                columns: new[] { "id_user", "id_classroom" });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users_roles",
                columns: table => new
                {
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    id_role = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_roles", x => new { x.id_user, x.id_role });
                    table.ForeignKey(
                        name: "FK_users_roles_roles_id_role",
                        column: x => x.id_role,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_roles_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_RegistryId",
                table: "users",
                column: "RegistryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_roles_id_role",
                table: "users_roles",
                column: "id_role");

            migrationBuilder.AddForeignKey(
                name: "FK_students_exams_users_id_user",
                table: "students_exams",
                column: "id_user",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_subjects_classrooms_users_id_user",
                table: "teachers_subjects_classrooms",
                column: "id_user",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_registries_RegistryId",
                table: "users",
                column: "RegistryId",
                principalTable: "registries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_exams_users_id_user",
                table: "students_exams");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_subjects_classrooms_users_id_user",
                table: "teachers_subjects_classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_users_registries_RegistryId",
                table: "users");

            migrationBuilder.DropTable(
                name: "users_roles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropIndex(
                name: "IX_users_RegistryId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropColumn(
                name: "RegistryId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_written",
                table: "exams");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "teachers_subjects_classrooms",
                newName: "id_teacher");

            migrationBuilder.RenameIndex(
                name: "IX_teachers_subjects_classrooms_id_user",
                table: "teachers_subjects_classrooms",
                newName: "IX_teachers_subjects_classrooms_id_teacher");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "students_exams",
                newName: "id_student");

            migrationBuilder.RenameIndex(
                name: "IX_students_exams_id_user",
                table: "students_exams",
                newName: "IX_students_exams_id_student");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id_registry",
                table: "students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "id");

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

            migrationBuilder.InsertData(
                table: "classrooms",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), "1B" },
                    { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), "1A" },
                    { new Guid("70f432dc-2a6c-499b-9326-52d1506befa5"), "2A" }
                });

            migrationBuilder.InsertData(
                table: "registries",
                columns: new[] { "id", "address", "birth", "email", "gender", "name", "surname", "telephone" },
                values: new object[,]
                {
                    { new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"), null, new DateOnly(2002, 1, 3), null, "Sirenetta", "Gabriele", "Giuliano", null },
                    { new Guid("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"), null, new DateOnly(2001, 9, 23), null, "M", "Angelo", "Lombardo", null },
                    { new Guid("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"), null, new DateOnly(2001, 9, 25), null, "M", "Francesco", "Limonelli", null },
                    { new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), null, new DateOnly(1996, 9, 15), null, "Vipera", "Giordana", "Pistorio", null },
                    { new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"), null, new DateOnly(1993, 5, 6), null, "F", "Francesca", "Scollo", null }
                });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"), "Geografia" },
                    { new Guid("46fd8c9d-b689-47cb-b9fd-44a19c5291a4"), "Matematica" },
                    { new Guid("47e8b0b5-1b53-46be-a0a9-9954958d3071"), "Spagnola" },
                    { new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), "Inglese" },
                    { new Guid("b55de490-fcdd-43d3-9146-94774e96cfe6"), "Storia" },
                    { new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"), "Italiano" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "password", "username" },
                values: new object[,]
                {
                    { new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"), "123", "giop5" },
                    { new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2"), "ilsegreto", "donnafrancisca" },
                    { new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"), "nonloso", "sidectrl" },
                    { new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"), "123", "aboutgg" },
                    { new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"), "nonticonosco", "angelarmstrong" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "id", "id_classroom", "id_registry", "id_user" },
                values: new object[,]
                {
                    { new Guid("007d3bca-d81d-42bd-9194-9c1d9f1f5ed7"), new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"), new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea") },
                    { new Guid("78362ba2-29ea-472b-9878-f55dad233e21"), new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b") },
                    { new Guid("8767fd02-7891-4b47-8b02-3cc0d07ac334"), new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"), new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2") }
                });

            migrationBuilder.InsertData(
                table: "teachers",
                columns: new[] { "id", "id_registry", "id_user" },
                values: new object[,]
                {
                    { new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335"), new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"), new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c") },
                    { new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d"), new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954") }
                });

            migrationBuilder.InsertData(
                table: "teachers_subjects_classrooms",
                columns: new[] { "id", "id_classroom", "id_subject", "id_teacher" },
                values: new object[,]
                {
                    { new Guid("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"), new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") },
                    { new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf"), new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") },
                    { new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"), new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") },
                    { new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"), new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") }
                });

            migrationBuilder.InsertData(
                table: "exams",
                columns: new[] { "id", "exam_date", "id_teacherSubjectClassroom" },
                values: new object[,]
                {
                    { new Guid("06dec5ca-003e-4b39-af43-c745746d23e0"), new DateOnly(2023, 9, 10), new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa") },
                    { new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"), new DateOnly(2023, 9, 25), new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d") },
                    { new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"), new DateOnly(2023, 9, 6), new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_id_registry",
                table: "students",
                column: "id_registry",
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

            migrationBuilder.AddForeignKey(
                name: "FK_students_registries_id_registry",
                table: "students",
                column: "id_registry",
                principalTable: "registries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_exams_students_id_student",
                table: "students_exams",
                column: "id_student",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_subjects_classrooms_teachers_id_teacher",
                table: "teachers_subjects_classrooms",
                column: "id_teacher",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
