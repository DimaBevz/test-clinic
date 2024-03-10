using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BugReportFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bug_report_users_user_id",
                table: "bug_report");

            migrationBuilder.DropForeignKey(
                name: "FK_bug_report_photo_bug_report_bug_report_id",
                table: "bug_report_photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bug_report_photo",
                table: "bug_report_photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bug_report",
                table: "bug_report");

            migrationBuilder.RenameTable(
                name: "bug_report_photo",
                newName: "bug_report_photos");

            migrationBuilder.RenameTable(
                name: "bug_report",
                newName: "bug_reports");

            migrationBuilder.RenameIndex(
                name: "IX_bug_report_photo_bug_report_id",
                table: "bug_report_photos",
                newName: "IX_bug_report_photos_bug_report_id");

            migrationBuilder.RenameIndex(
                name: "IX_bug_report_user_id",
                table: "bug_reports",
                newName: "IX_bug_reports_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bug_report_photos",
                table: "bug_report_photos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bug_reports",
                table: "bug_reports",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_bug_report_photos_bug_reports_bug_report_id",
                table: "bug_report_photos",
                column: "bug_report_id",
                principalTable: "bug_reports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bug_reports_users_user_id",
                table: "bug_reports",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bug_report_photos_bug_reports_bug_report_id",
                table: "bug_report_photos");

            migrationBuilder.DropForeignKey(
                name: "FK_bug_reports_users_user_id",
                table: "bug_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bug_reports",
                table: "bug_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bug_report_photos",
                table: "bug_report_photos");

            migrationBuilder.RenameTable(
                name: "bug_reports",
                newName: "bug_report");

            migrationBuilder.RenameTable(
                name: "bug_report_photos",
                newName: "bug_report_photo");

            migrationBuilder.RenameIndex(
                name: "IX_bug_reports_user_id",
                table: "bug_report",
                newName: "IX_bug_report_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_bug_report_photos_bug_report_id",
                table: "bug_report_photo",
                newName: "IX_bug_report_photo_bug_report_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bug_report",
                table: "bug_report",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bug_report_photo",
                table: "bug_report_photo",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_bug_report_users_user_id",
                table: "bug_report",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bug_report_photo_bug_report_bug_report_id",
                table: "bug_report_photo",
                column: "bug_report_id",
                principalTable: "bug_report",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
