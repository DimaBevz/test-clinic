using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renamedappointmentintosession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "appointments",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "appointment_details",
                newName: "session_details");

            migrationBuilder.RenameColumn(
                name: "appointment_id",
                table: "sessions",
                newName: "session_id");

            migrationBuilder.RenameColumn(
                name: "appointment_id",
                table: "session_details",
                newName: "session_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "session_id",
                table: "session_details",
                newName: "appointment_id");

            migrationBuilder.RenameColumn(
                name: "session_id",
                table: "sessions",
                newName: "appointment_id");

            migrationBuilder.RenameTable(
                name: "session_details",
                newName: "appointment_details");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "appointments");
        }
    }
}
