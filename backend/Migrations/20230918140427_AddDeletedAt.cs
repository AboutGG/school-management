using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "users",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "teachers_subjects_classrooms",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "teachers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "subjects",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "students_exams",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "students",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "registries",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "exams",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "classrooms",
                type: "date",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "classrooms",
                keyColumn: "id",
                keyValue: new Guid("70f432dc-2a6c-499b-9326-52d1506befa5"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("06dec5ca-003e-4b39-af43-c745746d23e0"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "exams",
                keyColumn: "id",
                keyValue: new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "id",
                keyValue: new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("007d3bca-d81d-42bd-9194-9c1d9f1f5ed7"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("78362ba2-29ea-472b-9878-f55dad233e21"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("8767fd02-7891-4b47-8b02-3cc0d07ac334"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("46fd8c9d-b689-47cb-b9fd-44a19c5291a4"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("47e8b0b5-1b53-46be-a0a9-9954958d3071"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("b55de490-fcdd-43d3-9146-94774e96cfe6"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "teachers",
                keyColumn: "id",
                keyValue: new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "teachers",
                keyColumn: "id",
                keyValue: new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyValue: new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                column: "deleted_at",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "teachers_subjects_classrooms");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "students_exams");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "students");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "registries");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "exams");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "classrooms");
        }
    }
}
