using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSessionIdToDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SessionDetailSessionId",
                table: "documents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_documents_SessionDetailSessionId",
                table: "documents",
                column: "SessionDetailSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_session_details_SessionDetailSessionId",
                table: "documents",
                column: "SessionDetailSessionId",
                principalTable: "session_details",
                principalColumn: "session_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_session_details_SessionDetailSessionId",
                table: "documents");

            migrationBuilder.DropIndex(
                name: "IX_documents_SessionDetailSessionId",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "SessionDetailSessionId",
                table: "documents");
        }
    }
}
