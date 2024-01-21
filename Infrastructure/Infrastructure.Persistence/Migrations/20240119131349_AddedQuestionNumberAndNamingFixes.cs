using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuestionNumberAndNamingFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_results_test_criteria_TestCriteriaId",
                table: "test_results");

            migrationBuilder.RenameColumn(
                name: "TotalScore",
                table: "test_results",
                newName: "total_score");

            migrationBuilder.RenameColumn(
                name: "TestCriteriaId",
                table: "test_results",
                newName: "test_criteria_id");

            migrationBuilder.RenameIndex(
                name: "IX_test_results_TestCriteriaId",
                table: "test_results",
                newName: "IX_test_results_test_criteria_id");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "test_criteria",
                newName: "verdict");

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "test_questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_test_results_test_criteria_test_criteria_id",
                table: "test_results",
                column: "test_criteria_id",
                principalTable: "test_criteria",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_results_test_criteria_test_criteria_id",
                table: "test_results");

            migrationBuilder.DropColumn(
                name: "number",
                table: "test_questions");

            migrationBuilder.RenameColumn(
                name: "total_score",
                table: "test_results",
                newName: "TotalScore");

            migrationBuilder.RenameColumn(
                name: "test_criteria_id",
                table: "test_results",
                newName: "TestCriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_test_results_test_criteria_id",
                table: "test_results",
                newName: "IX_test_results_TestCriteriaId");

            migrationBuilder.RenameColumn(
                name: "verdict",
                table: "test_criteria",
                newName: "value");

            migrationBuilder.AddForeignKey(
                name: "FK_test_results_test_criteria_TestCriteriaId",
                table: "test_results",
                column: "TestCriteriaId",
                principalTable: "test_criteria",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
