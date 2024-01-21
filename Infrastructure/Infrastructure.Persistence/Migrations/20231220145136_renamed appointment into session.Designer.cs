﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231220145136_renamed appointment into session")]
    partial class renamedappointmentintosession
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("comment_id");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment_text");

                    b.Property<Guid>("PatientDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("patient_data_id");

                    b.Property<Guid>("PhysicianDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("physician_data_id");

                    b.Property<float>("Rate")
                        .HasColumnType("real")
                        .HasColumnName("rate");

                    b.HasKey("Id");

                    b.HasIndex("PatientDataId");

                    b.HasIndex("PhysicianDataId");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Diagnosis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("diagnosis_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("diagnosis", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("document_id");

                    b.Property<string>("DocumentObjectKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("document_object_key");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("documents", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.PatientData", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<int?>("Apartment")
                        .HasColumnType("integer")
                        .HasColumnName("apartment");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("house");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("institution");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("position");

                    b.Property<string>("Settlement")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("settlement");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("street");

                    b.HasKey("UserId");

                    b.ToTable("patient_data", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.PhysicianData", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Bio")
                        .HasColumnType("text")
                        .HasColumnName("bio");

                    b.Property<DateOnly?>("Experience")
                        .HasColumnType("date")
                        .HasColumnName("experience");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<float>("Rank")
                        .HasColumnType("real")
                        .HasColumnName("rank");

                    b.HasKey("UserId");

                    b.HasIndex("PositionId");

                    b.ToTable("physician_data", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("specialization");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("specialty");

                    b.HasKey("Id");

                    b.ToTable("positions", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("session_id");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<Guid>("PatientDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("patient_data_id");

                    b.Property<Guid>("PhysicianDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("physician_data_id");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.HasKey("Id");

                    b.HasIndex("PatientDataId");

                    b.HasIndex("PhysicianDataId");

                    b.ToTable("sessions", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDetail", b =>
                {
                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid")
                        .HasColumnName("session_id");

                    b.Property<string>("Complaints")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("complaints");

                    b.Property<Guid>("DiagnosisId")
                        .HasColumnType("uuid")
                        .HasColumnName("diagnosis_id");

                    b.Property<string>("Recommendations")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("recommendations");

                    b.Property<string>("Treatment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("treatment");

                    b.HasKey("SessionId");

                    b.HasIndex("DiagnosisId");

                    b.ToTable("session_details", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateOnly?>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("PhotoObjectKey")
                        .HasColumnType("text")
                        .HasColumnName("photo_object_key");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Sex")
                        .HasColumnType("text")
                        .HasColumnName("sex");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Comment", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.PatientData", "PatientData")
                        .WithMany()
                        .HasForeignKey("PatientDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Persistence.Entities.PhysicianData", "PhysicianData")
                        .WithMany()
                        .HasForeignKey("PhysicianDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientData");

                    b.Navigation("PhysicianData");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Document", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.PatientData", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.User", "User")
                        .WithOne("PatientData")
                        .HasForeignKey("Infrastructure.Persistence.Entities.PatientData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.PhysicianData", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.HasOne("Infrastructure.Persistence.Entities.User", "User")
                        .WithOne("PhysicianData")
                        .HasForeignKey("Infrastructure.Persistence.Entities.PhysicianData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Session", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.PatientData", "PatientData")
                        .WithMany()
                        .HasForeignKey("PatientDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Persistence.Entities.PhysicianData", "PhysicianData")
                        .WithMany()
                        .HasForeignKey("PhysicianDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientData");

                    b.Navigation("PhysicianData");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDetail", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.Diagnosis", "Diagnosis")
                        .WithMany()
                        .HasForeignKey("DiagnosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Persistence.Entities.Session", "Session")
                        .WithOne("SessionDetail")
                        .HasForeignKey("Infrastructure.Persistence.Entities.SessionDetail", "SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diagnosis");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Session", b =>
                {
                    b.Navigation("SessionDetail")
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.User", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("PatientData");

                    b.Navigation("PhysicianData");
                });
#pragma warning restore 612, 618
        }
    }
}
