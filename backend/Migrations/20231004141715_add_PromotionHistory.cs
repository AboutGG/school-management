using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class add_PromotionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promotion_histories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_student = table.Column<Guid>(type: "uuid", nullable: false),
                    id_previous_classroom = table.Column<Guid>(type: "uuid", nullable: false),
                    previous_school_year = table.Column<string>(type: "text", nullable: false),
                    final_graduation = table.Column<int>(type: "integer", nullable: false),
                    scholastic_behavior = table.Column<int>(type: "integer", nullable: false),
                    promoted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotion_histories", x => x.id);
                    table.ForeignKey(
                        name: "FK_promotion_histories_classrooms_id_previous_classroom",
                        column: x => x.id_previous_classroom,
                        principalTable: "classrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_promotion_histories_students_id_student",
                        column: x => x.id_student,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_promotion_histories_id_previous_classroom",
                table: "promotion_histories",
                column: "id_previous_classroom");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_histories_id_student",
                table: "promotion_histories",
                column: "id_student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promotion_histories");
        }
    }
}
