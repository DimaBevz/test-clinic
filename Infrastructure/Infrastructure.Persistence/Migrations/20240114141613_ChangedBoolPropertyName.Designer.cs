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
    [Migration("20240114141613_ChangedBoolPropertyName")]
    partial class ChangedBoolPropertyName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Persistence.Entities.ChatHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("chat_history_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid")
                        .HasColumnName("session_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.HasIndex("UserId");

                    b.ToTable("chat_histories", (string)null);
                });

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

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("PatientDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("patient_data_id");

                    b.Property<Guid>("PhysicianDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("physician_data_id");

                    b.Property<float>("Rating")
                        .HasColumnType("real")
                        .HasColumnName("rating");

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

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<string>("DocumentObjectKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("document_object_key");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires_at");

                    b.Property<string>("PresignedUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("presigned_url");

                    b.Property<Guid?>("SessionDetailSessionId")
                        .HasColumnType("uuid")
                        .HasColumnName("session_id");

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

                    b.HasIndex("SessionDetailSessionId");

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

                    b.Property<float>("Rating")
                        .HasColumnType("real")
                        .HasColumnName("rating");

                    b.HasKey("UserId");

                    b.ToTable("physician_data", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.PhysicianSpecialty", b =>
                {
                    b.Property<Guid>("PhysicianDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("physician_data_id");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.HasKey("PhysicianDataId", "PositionId");

                    b.HasIndex("PositionId");

                    b.ToTable("physician_specialties", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

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

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

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

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDayTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("session_day_template_id");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer")
                        .HasColumnName("day_of_week");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<Guid>("TimetableTemplateId")
                        .HasColumnType("uuid")
                        .HasColumnName("timetable_id");

                    b.HasKey("Id");

                    b.HasIndex("TimetableTemplateId");

                    b.ToTable("session_day_templates", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDetail", b =>
                {
                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid")
                        .HasColumnName("session_id");

                    b.Property<int>("AveragePainScaleLastMonth")
                        .HasColumnType("integer")
                        .HasColumnName("average_pain_last_month");

                    b.Property<string>("Complaints")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("complaints");

                    b.Property<int>("CurrentPainScale")
                        .HasColumnType("integer")
                        .HasColumnName("current_pain");

                    b.Property<Guid?>("DiagnosisId")
                        .HasColumnType("uuid")
                        .HasColumnName("diagnosis_id");

                    b.Property<int>("HighestPainScaleLastMonth")
                        .HasColumnType("integer")
                        .HasColumnName("highest_pain_last_month");

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

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("session_template_id");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("end_time");

                    b.Property<Guid>("SessionDayTemplateId")
                        .HasColumnType("uuid")
                        .HasColumnName("session_day_template_id");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("start_time");

                    b.HasKey("Id");

                    b.HasIndex("SessionDayTemplateId");

                    b.ToTable("session_templates", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.TimetableTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("timetable_id");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<Guid>("PhysicianDataId")
                        .HasColumnType("uuid")
                        .HasColumnName("physician_data_id");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("PhysicianDataId")
                        .IsUnique();

                    b.ToTable("timetable_templates", (string)null);
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

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Sex")
                        .HasColumnType("text")
                        .HasColumnName("sex");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.UserPhotoData", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires_at");

                    b.Property<string>("PhotoObjectKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("photo_object_key");

                    b.Property<string>("PresignedUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("presigned_url");

                    b.HasKey("UserId");

                    b.ToTable("user_photo_data", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.ChatHistory", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.Session", "Session")
                        .WithMany("ChatHistories")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Persistence.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Comment", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.PatientData", "PatientData")
                        .WithMany()
                        .HasForeignKey("PatientDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Persistence.Entities.PhysicianData", "PhysicianData")
                        .WithMany("Comments")
                        .HasForeignKey("PhysicianDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientData");

                    b.Navigation("PhysicianData");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Document", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.SessionDetail", null)
                        .WithMany("Documents")
                        .HasForeignKey("SessionDetailSessionId");

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
                    b.HasOne("Infrastructure.Persistence.Entities.User", "User")
                        .WithOne("PhysicianData")
                        .HasForeignKey("Infrastructure.Persistence.Entities.PhysicianData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.PhysicianSpecialty", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.PhysicianData", "PhysicianData")
                        .WithMany("PhysicianSpecialties")
                        .HasForeignKey("PhysicianDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Persistence.Entities.Position", "Position")
                        .WithMany("PhysicianSpecialties")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhysicianData");

                    b.Navigation("Position");
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

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDayTemplate", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.TimetableTemplate", null)
                        .WithMany("SessionDayTemplates")
                        .HasForeignKey("TimetableTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDetail", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.Diagnosis", "Diagnosis")
                        .WithMany()
                        .HasForeignKey("DiagnosisId");

                    b.HasOne("Infrastructure.Persistence.Entities.Session", "Session")
                        .WithOne("SessionDetail")
                        .HasForeignKey("Infrastructure.Persistence.Entities.SessionDetail", "SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diagnosis");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionTemplate", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.SessionDayTemplate", "SessionDayTemplate")
                        .WithMany("SessionTemplates")
                        .HasForeignKey("SessionDayTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SessionDayTemplate");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.TimetableTemplate", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.PhysicianData", "PhysicianData")
                        .WithOne("TimetableTemplate")
                        .HasForeignKey("Infrastructure.Persistence.Entities.TimetableTemplate", "PhysicianDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhysicianData");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.UserPhotoData", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.User", "User")
                        .WithOne("UserPhotoData")
                        .HasForeignKey("Infrastructure.Persistence.Entities.UserPhotoData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.PhysicianData", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PhysicianSpecialties");

                    b.Navigation("TimetableTemplate");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Position", b =>
                {
                    b.Navigation("PhysicianSpecialties");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.Session", b =>
                {
                    b.Navigation("ChatHistories");

                    b.Navigation("SessionDetail");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDayTemplate", b =>
                {
                    b.Navigation("SessionTemplates");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.SessionDetail", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.TimetableTemplate", b =>
                {
                    b.Navigation("SessionDayTemplates");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.User", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("PatientData");

                    b.Navigation("PhysicianData");

                    b.Navigation("UserPhotoData");
                });
#pragma warning restore 612, 618
        }
    }
}
