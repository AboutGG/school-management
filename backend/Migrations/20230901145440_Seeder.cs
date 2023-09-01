using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Seeder : Migration
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
                columns: new[] { "id_classroom", "id_subject", "id_teacher" },
                values: new object[,]
                {
                    { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") },
                    { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") },
                    { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") }
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
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("007d3bca-d81d-42bd-9194-9c1d9f1f5ed7"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("78362ba2-29ea-472b-9878-f55dad233e21"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("8767fd02-7891-4b47-8b02-3cc0d07ac334"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("46fd8c9d-b689-47cb-b9fd-44a19c5291a4"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("b55de490-fcdd-43d3-9146-94774e96cfe6"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumns: new[] { "id_classroom", "id_subject", "id_teacher" },
                keyValues: new object[] { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") });

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumns: new[] { "id_classroom", "id_subject", "id_teacher" },
                keyValues: new object[] { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") });

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumns: new[] { "id_classroom", "id_subject", "id_teacher" },
                keyValues: new object[] { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") });

            migrationBuilder.DeleteData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"));

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
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"));

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"));

            migrationBuilder.DeleteData(
                table: "teachers",
                keyColumn: "id",
                keyValue: new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335"));

            migrationBuilder.DeleteData(
                table: "teachers",
                keyColumn: "id",
                keyValue: new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d"));

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
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"));

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"));
        }
    }
}
