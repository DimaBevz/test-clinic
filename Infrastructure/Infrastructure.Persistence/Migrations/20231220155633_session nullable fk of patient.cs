using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class sessionnullablefkofpatient : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_data_patient_data_id",
                table: "sessions",
                column: "patient_data_id",
                principalTable: "patient_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
