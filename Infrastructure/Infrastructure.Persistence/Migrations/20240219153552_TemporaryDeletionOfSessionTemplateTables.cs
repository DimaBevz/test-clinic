using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TemporaryDeletionOfSessionTemplateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "session_templates");

            migrationBuilder.DropTable(
                name: "session_day_templates");

            migrationBuilder.DropTable(
                name: "timetable_templates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "timetable_templates",
                columns: table => new
                {
                    timetable_id = table.Column<Guid>(type: "uuid", nullable: false),
                    physician_data_id = table.Column<Guid>(type: "uuid", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timetable_templates", x => x.timetable_id);
                    table.ForeignKey(
                        name: "FK_timetable_templates_physician_data_physician_data_id",
                        column: x => x.physician_data_id,
                        principalTable: "physician_data",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_timetable_templates_physician_data_id",
                table: "timetable_templates",
                column: "physician_data_id",
                unique: true);
        }
    }
}
