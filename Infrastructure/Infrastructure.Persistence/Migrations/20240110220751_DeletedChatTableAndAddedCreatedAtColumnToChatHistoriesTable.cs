using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeletedChatTableAndAddedCreatedAtColumnToChatHistoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_histories_chats_chat_id",
                table: "chat_histories");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.RenameColumn(
                name: "chat_id",
                table: "chat_histories",
                newName: "session_id");

            migrationBuilder.RenameIndex(
                name: "IX_chat_histories_chat_id",
                table: "chat_histories",
                newName: "IX_chat_histories_session_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "chat_histories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_chat_histories_sessions_session_id",
                table: "chat_histories",
                column: "session_id",
                principalTable: "sessions",
                principalColumn: "session_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_histories_sessions_session_id",
                table: "chat_histories");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "chat_histories");

            migrationBuilder.RenameColumn(
                name: "session_id",
                table: "chat_histories",
                newName: "chat_id");

            migrationBuilder.RenameIndex(
                name: "IX_chat_histories_session_id",
                table: "chat_histories",
                newName: "IX_chat_histories_chat_id");

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_opened = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.chat_id);
                    table.ForeignKey(
                        name: "FK_chats_sessions_session_id",
                        column: x => x.session_id,
                        principalTable: "sessions",
                        principalColumn: "session_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chats_session_id",
                table: "chats",
                column: "session_id");

            migrationBuilder.AddForeignKey(
                name: "FK_chat_histories_chats_chat_id",
                table: "chat_histories",
                column: "chat_id",
                principalTable: "chats",
                principalColumn: "chat_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
