﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.Models;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backend.Models.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<DateOnly>("ExamDate")
                        .HasColumnType("date")
                        .HasColumnName("exam_date")
                        .HasAnnotation("Relational:JsonPropertyName", "exam_date");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_subject")
                        .HasAnnotation("Relational:JsonPropertyName", "id_subject");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("exams");
                });

            modelBuilder.Entity("backend.Models.Registry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address")
                        .HasAnnotation("Relational:JsonPropertyName", "address");

                    b.Property<DateOnly?>("Birth")
                        .HasColumnType("date")
                        .HasColumnName("birth")
                        .HasAnnotation("Relational:JsonPropertyName", "birth");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender")
                        .HasAnnotation("Relational:JsonPropertyName", "gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname")
                        .HasAnnotation("Relational:JsonPropertyName", "surname");

                    b.Property<string>("Telephone")
                        .HasColumnType("text")
                        .HasColumnName("telephone")
                        .HasAnnotation("Relational:JsonPropertyName", "telephone");

                    b.HasKey("Id");

                    b.ToTable("registries");
                });

            modelBuilder.Entity("backend.Models.RegistryExam", b =>
                {
                    b.Property<Guid>("ExamId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_exam")
                        .HasAnnotation("Relational:JsonPropertyName", "id_exam");

                    b.Property<Guid>("RegistryId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_registry")
                        .HasAnnotation("Relational:JsonPropertyName", "id_registry");

                    b.Property<int?>("Grade")
                        .HasColumnType("integer")
                        .HasColumnName("grade")
                        .HasAnnotation("Relational:JsonPropertyName", "grade");

                    b.HasKey("ExamId", "RegistryId");

                    b.HasIndex("RegistryId");

                    b.ToTable("registries_exams");
                });

            modelBuilder.Entity("backend.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Classroom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("classroom")
                        .HasAnnotation("Relational:JsonPropertyName", "classroom");

                    b.Property<Guid>("RegistryId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_registry")
                        .HasAnnotation("Relational:JsonPropertyName", "id_registry");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user")
                        .HasAnnotation("Relational:JsonPropertyName", "id_user");

                    b.HasKey("Id");

                    b.HasIndex("RegistryId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("students");
                });

            modelBuilder.Entity("backend.Models.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("backend.Models.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<Guid>("RegistryId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_registry")
                        .HasAnnotation("Relational:JsonPropertyName", "id_regitry");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user")
                        .HasAnnotation("Relational:JsonPropertyName", "id_user");

                    b.HasKey("Id");

                    b.HasIndex("RegistryId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("teachers");
                });

            modelBuilder.Entity("backend.Models.TeacherSubject", b =>
                {
                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_teacher")
                        .HasAnnotation("Relational:JsonPropertyName", "id_techer");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_subject")
                        .HasAnnotation("Relational:JsonPropertyName", "id_subject");

                    b.Property<string>("Classroom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("classroom")
                        .HasAnnotation("Relational:JsonPropertyName", "classroom");

                    b.HasKey("TeacherId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("teachers_subjects");
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username")
                        .HasAnnotation("Relational:JsonPropertyName", "username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("backend.Models.Exam", b =>
                {
                    b.HasOne("backend.Models.Subject", "Subject")
                        .WithMany("Exams")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("backend.Models.RegistryExam", b =>
                {
                    b.HasOne("backend.Models.Exam", "Exam")
                        .WithMany("RegistryExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Registry", "Registry")
                        .WithMany("RegistryExams")
                        .HasForeignKey("RegistryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Registry");
                });

            modelBuilder.Entity("backend.Models.Student", b =>
                {
                    b.HasOne("backend.Models.Registry", "Registry")
                        .WithOne("Student")
                        .HasForeignKey("backend.Models.Student", "RegistryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("backend.Models.Student", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Registry");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.Teacher", b =>
                {
                    b.HasOne("backend.Models.Registry", "Registry")
                        .WithOne("Teacher")
                        .HasForeignKey("backend.Models.Teacher", "RegistryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.User", "User")
                        .WithOne("Teacher")
                        .HasForeignKey("backend.Models.Teacher", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Registry");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.TeacherSubject", b =>
                {
                    b.HasOne("backend.Models.Subject", "Subject")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Teacher", "Teacher")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("backend.Models.Exam", b =>
                {
                    b.Navigation("RegistryExams");
                });

            modelBuilder.Entity("backend.Models.Registry", b =>
                {
                    b.Navigation("RegistryExams");

                    b.Navigation("Student")
                        .IsRequired();

                    b.Navigation("Teacher")
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Models.Subject", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("TeacherSubjects");
                });

            modelBuilder.Entity("backend.Models.Teacher", b =>
                {
                    b.Navigation("TeacherSubjects");
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.Navigation("Student")
                        .IsRequired();

                    b.Navigation("Teacher")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
