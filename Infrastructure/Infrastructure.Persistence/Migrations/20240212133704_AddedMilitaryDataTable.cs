using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMilitaryDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "military_data",
                columns: table => new
                {
                    patient_data_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_veteran = table.Column<bool>(type: "boolean", nullable: false),
                    specialty = table.Column<string>(type: "text", nullable: false),
                    service_place = table.Column<string>(type: "text", nullable: false),
                    is_on_vacation = table.Column<bool>(type: "boolean", nullable: false),
                    has_disability = table.Column<bool>(type: "boolean", nullable: false),
                    disability_category = table.Column<int>(type: "integer", nullable: true),
                    health_problems = table.Column<string>(type: "text", nullable: false),
                    need_medical_or_psycho_care = table.Column<bool>(type: "boolean", nullable: false),
                    has_documents = table.Column<bool>(type: "boolean", nullable: false),
                    document_number = table.Column<string>(type: "text", nullable: false),
                    rehabilitation_and_support_needs = table.Column<string>(type: "text", nullable: false),
                    has_family_in_need = table.Column<bool>(type: "boolean", nullable: false),
                    how_learned_about_rehab_center = table.Column<string>(type: "text", nullable: false),
                    was_rehabilitated = table.Column<bool>(type: "boolean", nullable: false),
                    place_of_rehabilitation = table.Column<string>(type: "text", nullable: true),
                    result_of_rehabilitation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_military_data", x => x.patient_data_id);
                    table.ForeignKey(
                        name: "FK_military_data_patient_data_patient_data_id",
                        column: x => x.patient_data_id,
                        principalTable: "patient_data",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "military_data");
        }
    }
}
