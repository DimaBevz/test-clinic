using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentsTableAndAlteredUserPositionDocumentPhysicianDataAndPatientDataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointment_details_diagnosis_fk_diagnosis",
                table: "appointment_details");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patient_data_fk_patient_data",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_physician_data_fk_physician_data",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_documents_users_fk_user",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_physician_data_positions_fk_position",
                table: "physician_data");

            migrationBuilder.DropForeignKey(
                name: "FK_timetables_physician_data_PhysicianDataId",
                table: "timetables");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_fk_role",
                table: "users");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropIndex(
                name: "IX_users_fk_role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "fk_role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "PhysicianDataId",
                table: "timetables",
                newName: "physician_data_id");

            migrationBuilder.RenameIndex(
                name: "IX_timetables_PhysicianDataId",
                table: "timetables",
                newName: "IX_timetables_physician_data_id");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "positions",
                newName: "specialty");

            migrationBuilder.RenameColumn(
                name: "fk_position",
                table: "physician_data",
                newName: "position_id");

            migrationBuilder.RenameIndex(
                name: "IX_physician_data_fk_position",
                table: "physician_data",
                newName: "IX_physician_data_position_id");

            migrationBuilder.RenameColumn(
                name: "fk_user",
                table: "documents",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "documents",
                newName: "document_object_key");

            migrationBuilder.RenameIndex(
                name: "IX_documents_fk_user",
                table: "documents",
                newName: "IX_documents_user_id");

            migrationBuilder.RenameColumn(
                name: "fk_physician_data",
                table: "appointments",
                newName: "physician_data_id");

            migrationBuilder.RenameColumn(
                name: "fk_patient_data",
                table: "appointments",
                newName: "patient_data_id");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_fk_physician_data",
                table: "appointments",
                newName: "IX_appointments_physician_data_id");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_fk_patient_data",
                table: "appointments",
                newName: "IX_appointments_patient_data_id");

            migrationBuilder.RenameColumn(
                name: "fk_diagnosis",
                table: "appointment_details",
                newName: "diagnosis_id");

            migrationBuilder.RenameIndex(
                name: "IX_appointment_details_fk_diagnosis",
                table: "appointment_details",
                newName: "IX_appointment_details_diagnosis_id");

            migrationBuilder.AlterColumn<string>(
                name: "patronymic",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "photo_object_key",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "specialization",
                table: "positions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bio",
                table: "physician_data",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "apartment",
                table: "patient_data",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "house",
                table: "patient_data",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "institution",
                table: "patient_data",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "position",
                table: "patient_data",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "settlement",
                table: "patient_data",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "street",
                table: "patient_data",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment_text = table.Column<string>(type: "text", nullable: false),
                    rate = table.Column<float>(type: "real", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_comments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_user_id",
                table: "comments",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_details_diagnosis_diagnosis_id",
                table: "appointment_details",
                column: "diagnosis_id",
                principalTable: "diagnosis",
                principalColumn: "diagnosis_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_data_patient_data_id",
                table: "appointments",
                column: "patient_data_id",
                principalTable: "patient_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_physician_data_physician_data_id",
                table: "appointments",
                column: "physician_data_id",
                principalTable: "physician_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_users_user_id",
                table: "documents",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_physician_data_positions_position_id",
                table: "physician_data",
                column: "position_id",
                principalTable: "positions",
                principalColumn: "position_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_timetables_physician_data_physician_data_id",
                table: "timetables",
                column: "physician_data_id",
                principalTable: "physician_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointment_details_diagnosis_diagnosis_id",
                table: "appointment_details");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patient_data_patient_data_id",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_physician_data_physician_data_id",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_documents_users_user_id",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_physician_data_positions_position_id",
                table: "physician_data");

            migrationBuilder.DropForeignKey(
                name: "FK_timetables_physician_data_physician_data_id",
                table: "timetables");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropColumn(
                name: "photo_object_key",
                table: "users");

            migrationBuilder.DropColumn(
                name: "role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "specialization",
                table: "positions");

            migrationBuilder.DropColumn(
                name: "bio",
                table: "physician_data");

            migrationBuilder.DropColumn(
                name: "apartment",
                table: "patient_data");

            migrationBuilder.DropColumn(
                name: "house",
                table: "patient_data");

            migrationBuilder.DropColumn(
                name: "institution",
                table: "patient_data");

            migrationBuilder.DropColumn(
                name: "position",
                table: "patient_data");

            migrationBuilder.DropColumn(
                name: "settlement",
                table: "patient_data");

            migrationBuilder.DropColumn(
                name: "street",
                table: "patient_data");

            migrationBuilder.RenameColumn(
                name: "physician_data_id",
                table: "timetables",
                newName: "PhysicianDataId");

            migrationBuilder.RenameIndex(
                name: "IX_timetables_physician_data_id",
                table: "timetables",
                newName: "IX_timetables_PhysicianDataId");

            migrationBuilder.RenameColumn(
                name: "specialty",
                table: "positions",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "position_id",
                table: "physician_data",
                newName: "fk_position");

            migrationBuilder.RenameIndex(
                name: "IX_physician_data_position_id",
                table: "physician_data",
                newName: "IX_physician_data_fk_position");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "documents",
                newName: "fk_user");

            migrationBuilder.RenameColumn(
                name: "document_object_key",
                table: "documents",
                newName: "type");

            migrationBuilder.RenameIndex(
                name: "IX_documents_user_id",
                table: "documents",
                newName: "IX_documents_fk_user");

            migrationBuilder.RenameColumn(
                name: "physician_data_id",
                table: "appointments",
                newName: "fk_physician_data");

            migrationBuilder.RenameColumn(
                name: "patient_data_id",
                table: "appointments",
                newName: "fk_patient_data");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_physician_data_id",
                table: "appointments",
                newName: "IX_appointments_fk_physician_data");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_patient_data_id",
                table: "appointments",
                newName: "IX_appointments_fk_patient_data");

            migrationBuilder.RenameColumn(
                name: "diagnosis_id",
                table: "appointment_details",
                newName: "fk_diagnosis");

            migrationBuilder.RenameIndex(
                name: "IX_appointment_details_diagnosis_id",
                table: "appointment_details",
                newName: "IX_appointment_details_fk_diagnosis");

            migrationBuilder.AlterColumn<string>(
                name: "patronymic",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "fk_role",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", maxLength: 20, nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_fk_role",
                table: "users",
                column: "fk_role");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_details_diagnosis_fk_diagnosis",
                table: "appointment_details",
                column: "fk_diagnosis",
                principalTable: "diagnosis",
                principalColumn: "diagnosis_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patient_data_fk_patient_data",
                table: "appointments",
                column: "fk_patient_data",
                principalTable: "patient_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_physician_data_fk_physician_data",
                table: "appointments",
                column: "fk_physician_data",
                principalTable: "physician_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_users_fk_user",
                table: "documents",
                column: "fk_user",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_physician_data_positions_fk_position",
                table: "physician_data",
                column: "fk_position",
                principalTable: "positions",
                principalColumn: "position_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_timetables_physician_data_PhysicianDataId",
                table: "timetables",
                column: "PhysicianDataId",
                principalTable: "physician_data",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_fk_role",
                table: "users",
                column: "fk_role",
                principalTable: "roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
