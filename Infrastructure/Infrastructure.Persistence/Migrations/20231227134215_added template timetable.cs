using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedtemplatetimetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patient_data_patient_data_id",
                table: "sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "patient_data_id",
                table: "sessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "timetable_templates",
                columns: table => new
                {
                    timetable_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    physician_data_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "session_templates",
                columns: table => new
                {
                    session_template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    timetable_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_templates", x => x.session_template_id);
                    table.ForeignKey(
                        name: "FK_session_templates_timetable_templates_timetable_id",
                        column: x => x.timetable_id,
                        principalTable: "timetable_templates",
                        principalColumn: "timetable_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_session_templates_timetable_id",
                table: "session_templates",
                column: "timetable_id");

            migrationBuilder.CreateIndex(
                name: "IX_timetable_templates_physician_data_id",
                table: "timetable_templates",
                column: "physician_data_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_data_patient_data_id",
                table: "sessions",
                column: "patient_data_id",
                principalTable: "patient_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patient_data_patient_data_id",
                table: "sessions");

            migrationBuilder.DropTable(
                name: "session_templates");

            migrationBuilder.DropTable(
                name: "timetable_templates");

            migrationBuilder.AlterColumn<Guid>(
                name: "patient_data_id",
                table: "sessions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_data_patient_data_id",
                table: "sessions",
                column: "patient_data_id",
                principalTable: "patient_data",
                principalColumn: "user_id");
        }
    }
}
