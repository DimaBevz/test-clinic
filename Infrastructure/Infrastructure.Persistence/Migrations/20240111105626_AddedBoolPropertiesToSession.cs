using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoolPropertiesToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_session_details_SessionDetailSessionId",
                table: "documents");

            migrationBuilder.RenameColumn(
                name: "SessionDetailSessionId",
                table: "documents",
                newName: "session_id");

            migrationBuilder.RenameIndex(
                name: "IX_documents_SessionDetailSessionId",
                table: "documents",
                newName: "IX_documents_session_id");

            migrationBuilder.AddColumn<bool>(
                name: "is_archived",
                table: "sessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "sessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_session_details_session_id",
                table: "documents",
                column: "session_id",
                principalTable: "session_details",
                principalColumn: "session_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_session_details_session_id",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "is_archived",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "sessions");

            migrationBuilder.RenameColumn(
                name: "session_id",
                table: "documents",
                newName: "SessionDetailSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_documents_session_id",
                table: "documents",
                newName: "IX_documents_SessionDetailSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_session_details_SessionDetailSessionId",
                table: "documents",
                column: "SessionDetailSessionId",
                principalTable: "session_details",
                principalColumn: "session_id");
        }
    }
}
