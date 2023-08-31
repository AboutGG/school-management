using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "registries",
                columns: new[] { "id", "address", "birth", "email", "gender", "name", "surname", "telephone" },
                values: new object[] { new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), null, new DateOnly(1996, 9, 15), null, "Vipera", "Giordana", "Pistorio", null });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "password", "username" },
                values: new object[,]
                {
                    { new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"), "123", "giop5" },
                    { new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"), "123", "aboutgg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
