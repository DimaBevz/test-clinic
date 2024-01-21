using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDiagnosisIdToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointment_details_diagnosis_diagnosis_id",
                table: "session_details");

            migrationBuilder.AlterColumn<Guid>(
                name: "diagnosis_id",
                table: "session_details",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_details_diagnosis_diagnosis_id",
                table: "session_details",
                column: "diagnosis_id",
                principalTable: "diagnosis",
                principalColumn: "diagnosis_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointment_details_diagnosis_diagnosis_id",
                table: "session_details");

            migrationBuilder.AlterColumn<Guid>(
                name: "diagnosis_id",
                table: "session_details",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_details_diagnosis_diagnosis_id",
                table: "session_details",
                column: "diagnosis_id",
                principalTable: "diagnosis",
                principalColumn: "diagnosis_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
