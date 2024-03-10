using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedSessionTemplateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "session_templates");

            migrationBuilder.DropTable(
                name: "session_day_templates");

            migrationBuilder.CreateTable(
                name: "session_template_days",
                columns: table => new
                {
                    session_template_day_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    timetable_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_template_days", x => x.session_template_day_id);
                    table.ForeignKey(
                        name: "FK_session_template_days_timetable_templates_timetable_id",
                        column: x => x.timetable_id,
                        principalTable: "timetable_templates",
                        principalColumn: "timetable_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session_template_times",
                columns: table => new
                {
                    session_template_times_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    session_template_day_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_template_times", x => x.session_template_times_id);
                    table.ForeignKey(
                        name: "FK_session_template_times_session_template_days_session_templa~",
                        column: x => x.session_template_day_id,
                        principalTable: "session_template_days",
                        principalColumn: "session_template_day_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_session_template_days_timetable_id",
                table: "session_template_days",
                column: "timetable_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_template_times_session_template_day_id",
                table: "session_template_times",
                column: "session_template_day_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "session_template_times");

            migrationBuilder.DropTable(
                name: "session_template_days");

            migrationBuilder.CreateTable(
                name: "session_day_templates",
                columns: table => new
                {
                    session_day_template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "session_templates",
                columns: table => new
                {
                    session_template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    session_day_template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_templates", x => x.session_template_id);
                    table.ForeignKey(
                        name: "FK_session_templates_session_day_templates_session_day_templat~",
                        column: x => x.session_day_template_id,
                        principalTable: "session_day_templates",
                        principalColumn: "session_day_template_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_session_day_templates_timetable_id",
                table: "session_day_templates",
                column: "timetable_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_templates_session_day_template_id",
                table: "session_templates",
                column: "session_day_template_id");
        }
    }
}
