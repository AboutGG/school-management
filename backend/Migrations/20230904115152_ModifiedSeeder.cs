using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("47e8b0b5-1b53-46be-a0a9-9954958d3071"), "Spagnola" });

            migrationBuilder.InsertData(
                table: "teachers_subjects_classrooms",
                columns: new[] { "id_classroom", "id_subject", "id_teacher" },
                values: new object[] { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: new Guid("47e8b0b5-1b53-46be-a0a9-9954958d3071"));

            migrationBuilder.DeleteData(
                table: "teachers_subjects_classrooms",
                keyColumns: new[] { "id_classroom", "id_subject", "id_teacher" },
                keyValues: new object[] { new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"), new Guid("cc3f629e-ae6b-448e-be46-afce1fa9e31d") });
        }
    }
}
