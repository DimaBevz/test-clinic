using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedtestcriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestCriteriaId",
                table: "test_results",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TotalScore",
                table: "test_results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "test_criteria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    min = table.Column<int>(type: "integer", nullable: false),
                    max = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    test_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_criteria", x => x.id);
                    table.ForeignKey(
                        name: "FK_test_criteria_tests_test_id",
                        column: x => x.test_id,
                        principalTable: "tests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_test_results_TestCriteriaId",
                table: "test_results",
                column: "TestCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_test_criteria_test_id",
                table: "test_criteria",
                column: "test_id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_results_test_criteria_TestCriteriaId",
                table: "test_results",
                column: "TestCriteriaId",
                principalTable: "test_criteria",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_results_test_criteria_TestCriteriaId",
                table: "test_results");

            migrationBuilder.DropTable(
                name: "test_criteria");

            migrationBuilder.DropIndex(
                name: "IX_test_results_TestCriteriaId",
                table: "test_results");

            migrationBuilder.DropColumn(
                name: "TestCriteriaId",
                table: "test_results");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "test_results");
        }
    }
}
