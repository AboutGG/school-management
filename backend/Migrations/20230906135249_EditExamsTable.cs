using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class EditExamsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exams_subjects_id_subject",
                table: "exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers_subjects_classrooms",
                table: "teachers_subjects_classrooms");

            migrationBuilder.DropIndex(
                name: "IX_exams_id_subject",
                table: "exams");

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
                keyValues: new object[] { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") });

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumns: new[] { "id_classroom", "id_subject", "id_teacher" },
                keyValues: new object[] { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") });

            migrationBuilder.RenameColumn(
                name: "id_subject",
                table: "exams",
                newName: "id_teacherSubjectClassroom");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "teachers_subjects_classrooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers_subjects_classrooms",
                table: "teachers_subjects_classrooms",
                column: "id");

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
                name: "IX_teachers_subjects_classrooms_id_teacher",
                table: "teachers_subjects_classrooms",
                column: "id_teacher");

            migrationBuilder.CreateIndex(
                name: "IX_exams_id_teacherSubjectClassroom",
                table: "exams",
                column: "id_teacherSubjectClassroom",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_exams_teachers_subjects_classrooms_id_teacherSubjectClassro~",
                table: "exams",
                column: "id_teacherSubjectClassroom",
                principalTable: "teachers_subjects_classrooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exams_teachers_subjects_classrooms_id_teacherSubjectClassro~",
                table: "exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers_subjects_classrooms",
                table: "teachers_subjects_classrooms");

            migrationBuilder.DropIndex(
                name: "IX_teachers_subjects_classrooms_id_teacher",
                table: "teachers_subjects_classrooms");

            migrationBuilder.DropIndex(
                name: "IX_exams_id_teacherSubjectClassroom",
                table: "exams");

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
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyColumnType: "uuid",
                keyValue: new Guid("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyColumnType: "uuid",
                keyValue: new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyColumnType: "uuid",
                keyValue: new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumn: "id",
                keyColumnType: "uuid",
                keyValue: new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"));

            migrationBuilder.DropColumn(
                name: "id",
                table: "teachers_subjects_classrooms");

            migrationBuilder.RenameColumn(
                name: "id_teacherSubjectClassroom",
                table: "exams",
                newName: "id_subject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers_subjects_classrooms",
                table: "teachers_subjects_classrooms",
                columns: new[] { "id_teacher", "id_subject", "id_classroom" });

            migrationBuilder.InsertData(
                table: "teachers_subjects_classrooms",
                columns: new[] { "id_classroom", "id_subject", "id_teacher" },
                values: new object[,]
                {
                    { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") },
                    { new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"), new Guid("54ff5a4a-1469-4f07-afcb-9b1864dcb335") },
                    { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") },
                    { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_exams_id_subject",
                table: "exams",
                column: "id_subject");

            migrationBuilder.AddForeignKey(
                name: "FK_exams_subjects_id_subject",
                table: "exams",
                column: "id_subject",
                principalTable: "subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
