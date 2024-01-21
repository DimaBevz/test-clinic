using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeparatedDaysFromSessionTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_session_templates_timetable_templates_timetable_id",
                table: "session_templates");

            migrationBuilder.DropColumn(
                name: "day_of_week",
                table: "session_templates");

            migrationBuilder.RenameColumn(
                name: "timetable_id",
                table: "session_templates",
                newName: "session_day_template_id");

            migrationBuilder.RenameIndex(
                name: "IX_session_templates_timetable_id",
                table: "session_templates",
                newName: "IX_session_templates_session_day_template_id");

            migrationBuilder.CreateTable(
                name: "session_day_templates",
                columns: table => new
                {
                    session_day_template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    is_disabled = table.Column<bool>(type: "boolean", nullable: false),
                    timetable_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_day_templates", x => x.session_day_template_id);
                    table.ForeignKey(
                        name: "FK_session_day_templates_timetable_templates_timetable_id",
                        column: x => x.timetable_id,
                        principalTable: "timetable_templates",
                        principalColumn: "timetable_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_session_day_templates_timetable_id",
                table: "session_day_templates",
                column: "timetable_id");

            migrationBuilder.AddForeignKey(
                name: "FK_session_templates_session_day_templates_session_day_templat~",
                table: "session_templates",
                column: "session_day_template_id",
                principalTable: "session_day_templates",
                principalColumn: "session_day_template_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_session_templates_session_day_templates_session_day_templat~",
                table: "session_templates");

            migrationBuilder.DropTable(
                name: "session_day_templates");

            migrationBuilder.RenameColumn(
                name: "session_day_template_id",
                table: "session_templates",
                newName: "timetable_id");

            migrationBuilder.RenameIndex(
                name: "IX_session_templates_session_day_template_id",
                table: "session_templates",
                newName: "IX_session_templates_timetable_id");

            migrationBuilder.AddColumn<int>(
                name: "day_of_week",
                table: "session_templates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_session_templates_timetable_templates_timetable_id",
                table: "session_templates",
                column: "timetable_id",
                principalTable: "timetable_templates",
                principalColumn: "timetable_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
