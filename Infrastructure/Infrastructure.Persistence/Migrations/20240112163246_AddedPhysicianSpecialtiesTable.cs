using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhysicianSpecialtiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_physician_data_positions_position_id",
                table: "physician_data");

            migrationBuilder.DropIndex(
                name: "IX_physician_data_position_id",
                table: "physician_data");

            migrationBuilder.DropColumn(
                name: "specialization",
                table: "positions");

            migrationBuilder.DropColumn(
                name: "position_id",
                table: "physician_data");

            migrationBuilder.CreateTable(
                name: "physician_specialties",
                columns: table => new
                {
                    physician_data_id = table.Column<Guid>(type: "uuid", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_physician_specialties", x => new { x.physician_data_id, x.position_id });
                    table.ForeignKey(
                        name: "FK_physician_specialties_physician_data_physician_data_id",
                        column: x => x.physician_data_id,
                        principalTable: "physician_data",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_physician_specialties_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "position_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_physician_specialties_position_id",
                table: "physician_specialties",
                column: "position_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "physician_specialties");

            migrationBuilder.AddColumn<string>(
                name: "specialization",
                table: "positions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "position_id",
                table: "physician_data",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_physician_data_position_id",
                table: "physician_data",
                column: "position_id");

            migrationBuilder.AddForeignKey(
                name: "FK_physician_data_positions_position_id",
                table: "physician_data",
                column: "position_id",
                principalTable: "positions",
                principalColumn: "position_id");
        }
    }
}
