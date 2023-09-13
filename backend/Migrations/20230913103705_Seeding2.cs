using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Seeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("d2d307bf-eb4e-4a58-afb9-aca5f4c23fce"), null, new DateOnly(2001, 1, 1), null, "Unicorno", "Mr. Unicorno", "Magico", null },
                    { new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), null, new DateOnly(1996, 9, 15), null, "Vipera", "Giordana", "Pistorio", null },
                    { new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"), null, new DateOnly(1993, 5, 6), null, "F", "Francesca", "Scollo", null }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f"), "Teacher" },
                    { new Guid("308150a3-6975-4251-bf9a-f199b01e13a6"), "Administrator" },
                    { new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"), "Student" }
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
                columns: new[] { "id", "password", "RegistryId", "username" },
                values: new object[,]
                {
                    { new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"), "123", new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), "giop5" },
                    { new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2"), "ilsegreto", new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"), "donnafrancisca" },
                    { new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"), "nonloso", new Guid("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"), "sidectrl" },
                    { new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"), "123", new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"), "aboutgg" },
                    { new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"), "nonticonosco", new Guid("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"), "angelarmstrong" },
                    { new Guid("d0158fc5-3704-4e89-9cd6-11b7e956c774"), "admin", new Guid("d2d307bf-eb4e-4a58-afb9-aca5f4c23fce"), "unicornosupremodellemeraviglie" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "id_classroom", "id_user" },
                values: new object[,]
                {
                    { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2") },
                    { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea") },
                    { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b") }
                });

            migrationBuilder.InsertData(
                table: "teachers_subjects_classrooms",
                columns: new[] { "id", "id_classroom", "id_subject", "id_user" },
                values: new object[,]
                {
                    { new Guid("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"), new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c") },
                    { new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf"), new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c") },
                    { new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"), new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"), new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954") },
                    { new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"), new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"), new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954") }
                });

            migrationBuilder.InsertData(
                table: "users_roles",
                columns: new[] { "id_role", "id_user" },
                values: new object[,]
                {
                    { new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f"), new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954") },
                    { new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"), new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2") },
                    { new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"), new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea") },
                    { new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f"), new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c") },
                    { new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b") },
                    { new Guid("308150a3-6975-4251-bf9a-f199b01e13a6"), new Guid("d0158fc5-3704-4e89-9cd6-11b7e956c774") }
                });

            migrationBuilder.InsertData(
                table: "exams",
                columns: new[] { "id", "exam_date", "is_written", "id_teacherSubjectClassroom" },
                values: new object[,]
                {
                    { new Guid("06dec5ca-003e-4b39-af43-c745746d23e0"), new DateOnly(2023, 9, 10), true, new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa") },
                    { new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"), new DateOnly(2023, 9, 25), false, new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d") },
                    { new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"), new DateOnly(2023, 9, 6), true, new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf") }
                });

            migrationBuilder.InsertData(
                table: "students_exams",
                columns: new[] { "id_exam", "id_user", "grade" },
                values: new object[,]
                {
                    { new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"), new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"), 5 },
                    { new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"), 9 },
                    { new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"), 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("70f432dc-2a6c-499b-9326-52d1506befa5"));

            migrationBuilder.DeleteData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("06dec5ca-003e-4b39-af43-c745746d23e0"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumns: new[] { "id_classroom", "id_user" },
                keyValues: new object[] { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2") });

            migrationBuilder.DeleteData(
                table: "students",
                keyColumns: new[] { "id_classroom", "id_user" },
                keyValues: new object[] { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea") });

            migrationBuilder.DeleteData(
                table: "students",
                keyColumns: new[] { "id_classroom", "id_user" },
                keyValues: new object[] { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b") });

            migrationBuilder.DeleteData(
                table: "students_exams",
                keyColumns: new[] { "id_exam", "id_user" },
                keyValues: new object[] { new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"), new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea") });

            migrationBuilder.DeleteData(
                table: "students_exams",
                keyColumns: new[] { "id_exam", "id_user" },
                keyValues: new object[] { new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b") });

            migrationBuilder.DeleteData(
                table: "students_exams",
                keyColumns: new[] { "id_exam", "id_user" },
                keyValues: new object[] { new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b") });

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
                table: "users_roles",
                keyColumns: new[] { "id_role", "id_user" },
                keyValues: new object[] { new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f"), new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954") });

            migrationBuilder.DeleteData(
                table: "users_roles",
                keyColumns: new[] { "id_role", "id_user" },
                keyValues: new object[] { new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"), new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2") });

            migrationBuilder.DeleteData(
                table: "users_roles",
                keyColumns: new[] { "id_role", "id_user" },
                keyValues: new object[] { new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"), new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea") });

            migrationBuilder.DeleteData(
                table: "users_roles",
                keyColumns: new[] { "id_role", "id_user" },
                keyValues: new object[] { new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f"), new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c") });

            migrationBuilder.DeleteData(
                table: "users_roles",
                keyColumns: new[] { "id_role", "id_user" },
                keyValues: new object[] { new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"), new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b") });

            migrationBuilder.DeleteData(
                table: "users_roles",
                keyColumns: new[] { "id_role", "id_user" },
                keyValues: new object[] { new Guid("308150a3-6975-4251-bf9a-f199b01e13a6"), new Guid("d0158fc5-3704-4e89-9cd6-11b7e956c774") });

            migrationBuilder.DeleteData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"));

            migrationBuilder.DeleteData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"));

            migrationBuilder.DeleteData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("308150a3-6975-4251-bf9a-f199b01e13a6"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"));

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
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d0158fc5-3704-4e89-9cd6-11b7e956c774"));

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
                keyValue: new Guid("d2d307bf-eb4e-4a58-afb9-aca5f4c23fce"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"));

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
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"));
        }
    }
}
