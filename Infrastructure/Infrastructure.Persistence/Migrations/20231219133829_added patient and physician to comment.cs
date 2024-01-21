using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedpatientandphysiciantocomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_user_id",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "comments",
                newName: "physician_data_id");

            migrationBuilder.RenameIndex(
                name: "IX_comments_user_id",
                table: "comments",
                newName: "IX_comments_physician_data_id");

            migrationBuilder.AddColumn<Guid>(
                name: "patient_data_id",
                table: "comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_comments_patient_data_id",
                table: "comments",
                column: "patient_data_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_patient_data_patient_data_id",
                table: "comments",
                column: "patient_data_id",
                principalTable: "patient_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_physician_data_physician_data_id",
                table: "comments",
                column: "physician_data_id",
                principalTable: "physician_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_patient_data_patient_data_id",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_physician_data_physician_data_id",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_patient_data_id",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "patient_data_id",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "physician_data_id",
                table: "comments",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_comments_physician_data_id",
                table: "comments",
                newName: "IX_comments_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_user_id",
                table: "comments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
