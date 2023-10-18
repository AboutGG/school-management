using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "school_year",
                table: "students",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("007d3bca-d81d-42bd-9194-9c1d9f1f5ed7"),
                column: "school_year",
                value: null);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("78362ba2-29ea-472b-9878-f55dad233e21"),
                column: "school_year",
                value: null);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "id",
                keyValue: new Guid("8767fd02-7891-4b47-8b02-3cc0d07ac334"),
                column: "school_year",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "school_year",
                table: "students");
        }
    }
}
