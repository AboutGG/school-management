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

            modelBuilder.Entity("backend.Models.Classroom", b =>
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

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("classrooms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"),
                            Name = "1A"
                        },
                        new
                        {
                            Id = new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"),
                            Name = "1B"
                        },
                        new
                        {
                            Id = new Guid("70f432dc-2a6c-499b-9326-52d1506befa5"),
                            Name = "2A"
                        });
                });

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

                    b.Property<bool>("IsWritten")
                        .HasColumnType("boolean")
                        .HasColumnName("is_written")
                        .HasAnnotation("Relational:JsonPropertyName", "is_written");

                    b.Property<Guid>("TeacherSubjectClassroomId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_teacherSubjectClassroom")
                        .HasAnnotation("Relational:JsonPropertyName", "id_teacherSubjectClassroom");

                    b.HasKey("Id");

                    b.HasIndex("TeacherSubjectClassroomId");

                    b.ToTable("exams");

                    b.HasData(
                        new
                        {
                            Id = new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"),
                            ExamDate = new DateOnly(2023, 9, 6),
                            IsWritten = true,
                            TeacherSubjectClassroomId = new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf")
                        },
                        new
                        {
                            Id = new Guid("06dec5ca-003e-4b39-af43-c745746d23e0"),
                            ExamDate = new DateOnly(2023, 9, 10),
                            IsWritten = true,
                            TeacherSubjectClassroomId = new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa")
                        },
                        new
                        {
                            Id = new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"),
                            ExamDate = new DateOnly(2023, 9, 25),
                            IsWritten = false,
                            TeacherSubjectClassroomId = new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d")
                        });
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("d2d307bf-eb4e-4a58-afb9-aca5f4c23fce"),
                            Birth = new DateOnly(2001, 1, 1),
                            Gender = "Unicorno",
                            Name = "Mr. Unicorno",
                            Surname = "Magico"
                        },
                        new
                        {
                            Id = new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"),
                            Birth = new DateOnly(1996, 9, 15),
                            Gender = "Vipera",
                            Name = "Giordana",
                            Surname = "Pistorio"
                        },
                        new
                        {
                            Id = new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"),
                            Birth = new DateOnly(2002, 1, 3),
                            Gender = "Sirenetta",
                            Name = "Gabriele",
                            Surname = "Giuliano"
                        },
                        new
                        {
                            Id = new Guid("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"),
                            Birth = new DateOnly(2001, 9, 25),
                            Gender = "M",
                            Name = "Francesco",
                            Surname = "Limonelli"
                        },
                        new
                        {
                            Id = new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"),
                            Birth = new DateOnly(1993, 5, 6),
                            Gender = "F",
                            Name = "Francesca",
                            Surname = "Scollo"
                        },
                        new
                        {
                            Id = new Guid("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"),
                            Birth = new DateOnly(2001, 9, 23),
                            Gender = "M",
                            Name = "Angelo",
                            Surname = "Lombardo"
                        });
                });

            modelBuilder.Entity("backend.Models.Role", b =>
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

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f"),
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6"),
                            Name = "Student"
                        },
                        new
                        {
                            Id = new Guid("308150a3-6975-4251-bf9a-f199b01e13a6"),
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("backend.Models.Student", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user")
                        .HasAnnotation("Relational:JsonPropertyName", "id_user");

                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_classroom")
                        .HasAnnotation("Relational:JsonPropertyName", "id_classroom");

                    b.HasKey("UserId", "ClassroomId");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("students");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                            ClassroomId = new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
                        },
                        new
                        {
                            UserId = new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2"),
                            ClassroomId = new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
                        },
                        new
                        {
                            UserId = new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                            ClassroomId = new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
                        });
                });

            modelBuilder.Entity("backend.Models.StudentExam", b =>
                {
                    b.Property<Guid>("ExamId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_exam")
                        .HasAnnotation("Relational:JsonPropertyName", "id_exam");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user")
                        .HasAnnotation("Relational:JsonPropertyName", "id_user");

                    b.Property<int?>("Grade")
                        .HasColumnType("integer")
                        .HasColumnName("grade")
                        .HasAnnotation("Relational:JsonPropertyName", "grade");

                    b.HasKey("ExamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("students_exams");

                    b.HasData(
                        new
                        {
                            ExamId = new Guid("55988821-2bc3-4122-aa50-e0fb3b8f42ad"),
                            UserId = new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                            Grade = 6
                        },
                        new
                        {
                            ExamId = new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"),
                            UserId = new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                            Grade = 9
                        },
                        new
                        {
                            ExamId = new Guid("20ad1b3e-af97-4a45-815b-af9f34e52dc3"),
                            UserId = new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                            Grade = 5
                        });
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"),
                            Name = "Italiano"
                        },
                        new
                        {
                            Id = new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"),
                            Name = "Inglese"
                        },
                        new
                        {
                            Id = new Guid("46fd8c9d-b689-47cb-b9fd-44a19c5291a4"),
                            Name = "Matematica"
                        },
                        new
                        {
                            Id = new Guid("b55de490-fcdd-43d3-9146-94774e96cfe6"),
                            Name = "Storia"
                        },
                        new
                        {
                            Id = new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"),
                            Name = "Geografia"
                        },
                        new
                        {
                            Id = new Guid("47e8b0b5-1b53-46be-a0a9-9954958d3071"),
                            Name = "Spagnola"
                        });
                });

            modelBuilder.Entity("backend.Models.TeacherSubjectClassroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_classroom")
                        .HasAnnotation("Relational:JsonPropertyName", "id_classroom");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_subject")
                        .HasAnnotation("Relational:JsonPropertyName", "id_subject");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user")
                        .HasAnnotation("Relational:JsonPropertyName", "id_user");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("teachers_subjects_classrooms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0f69c148-07ab-47a8-838a-0c9dfce974bf"),
                            ClassroomId = new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"),
                            SubjectId = new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"),
                            UserId = new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c")
                        },
                        new
                        {
                            Id = new Guid("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"),
                            ClassroomId = new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"),
                            SubjectId = new Guid("be1816ff-41be-4620-a48c-ac18b71e3bf8"),
                            UserId = new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954")
                        },
                        new
                        {
                            Id = new Guid("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"),
                            ClassroomId = new Guid("0ed3811a-0a5c-4ed0-b7db-53090199aa27"),
                            SubjectId = new Guid("336d920e-273f-40bd-aed3-17212e2fb2a3"),
                            UserId = new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954")
                        },
                        new
                        {
                            Id = new Guid("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"),
                            ClassroomId = new Guid("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"),
                            SubjectId = new Guid("a907ec00-1577-4a50-ab10-579e071f1e59"),
                            UserId = new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c")
                        });
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

                    b.Property<Guid>("RegistryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username")
                        .HasAnnotation("Relational:JsonPropertyName", "username");

                    b.HasKey("Id");

                    b.HasIndex("RegistryId")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"),
                            Password = "123",
                            RegistryId = new Guid("d7f23f33-ebf2-4716-8c3f-b997ba2da125"),
                            Username = "giop5"
                        },
                        new
                        {
                            Id = new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"),
                            Password = "123",
                            RegistryId = new Guid("153afc1d-f63f-45aa-ae55-534d4ceeb737"),
                            Username = "aboutgg"
                        },
                        new
                        {
                            Id = new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                            Password = "nonloso",
                            RegistryId = new Guid("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"),
                            Username = "sidectrl"
                        },
                        new
                        {
                            Id = new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                            Password = "nonticonosco",
                            RegistryId = new Guid("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"),
                            Username = "angelarmstrong"
                        },
                        new
                        {
                            Id = new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2"),
                            Password = "ilsegreto",
                            RegistryId = new Guid("f833e6a7-f617-4683-a772-b5bcd1971da8"),
                            Username = "donnafrancisca"
                        },
                        new
                        {
                            Id = new Guid("d0158fc5-3704-4e89-9cd6-11b7e956c774"),
                            Password = "admin",
                            RegistryId = new Guid("d2d307bf-eb4e-4a58-afb9-aca5f4c23fce"),
                            Username = "unicornosupremodellemeraviglie"
                        });
                });

            modelBuilder.Entity("backend.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user")
                        .HasAnnotation("Relational:JsonPropertyName", "id_user");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_role")
                        .HasAnnotation("Relational:JsonPropertyName", "id_role");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("users_roles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("d0158fc5-3704-4e89-9cd6-11b7e956c774"),
                            RoleId = new Guid("308150a3-6975-4251-bf9a-f199b01e13a6")
                        },
                        new
                        {
                            UserId = new Guid("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"),
                            RoleId = new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f")
                        },
                        new
                        {
                            UserId = new Guid("affab63e-dec6-4626-abfb-1e52b258cc6c"),
                            RoleId = new Guid("21598c0d-1177-4d73-8975-6052fd9dbd5f")
                        },
                        new
                        {
                            UserId = new Guid("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                            RoleId = new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6")
                        },
                        new
                        {
                            UserId = new Guid("37ce79ab-5b93-44ce-8189-e49ab8e377e2"),
                            RoleId = new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6")
                        },
                        new
                        {
                            UserId = new Guid("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                            RoleId = new Guid("4d53602a-92af-42c8-95b0-42f15844f1f6")
                        });
                });

            modelBuilder.Entity("backend.Models.Exam", b =>
                {
                    b.HasOne("backend.Models.TeacherSubjectClassroom", "TeacherSubjectClassroom")
                        .WithMany("Exam")
                        .HasForeignKey("TeacherSubjectClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeacherSubjectClassroom");
                });

            modelBuilder.Entity("backend.Models.Student", b =>
                {
                    b.HasOne("backend.Models.Classroom", "Classroom")
                        .WithMany("Students")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("backend.Models.Student", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.StudentExam", b =>
                {
                    b.HasOne("backend.Models.Exam", "Exam")
                        .WithMany("StudentExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.User", "User")
                        .WithMany("StudentsExams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.TeacherSubjectClassroom", b =>
                {
                    b.HasOne("backend.Models.Classroom", "Classroom")
                        .WithMany("TeachersSubjectsClassrooms")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Subject", "Subject")
                        .WithMany("TeacherSubjectClassrooms")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.User", "User")
                        .WithMany("TeachersSubjectsClassrooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.HasOne("backend.Models.Registry", "Registry")
                        .WithOne("User")
                        .HasForeignKey("backend.Models.User", "RegistryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Registry");
                });

            modelBuilder.Entity("backend.Models.UserRole", b =>
                {
                    b.HasOne("backend.Models.Role", "Role")
                        .WithMany("UsersRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.User", "User")
                        .WithMany("UsersRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.Classroom", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("TeachersSubjectsClassrooms");
                });

            modelBuilder.Entity("backend.Models.Exam", b =>
                {
                    b.Navigation("StudentExams");
                });

            modelBuilder.Entity("backend.Models.Registry", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Models.Role", b =>
                {
                    b.Navigation("UsersRoles");
                });

            modelBuilder.Entity("backend.Models.Subject", b =>
                {
                    b.Navigation("TeacherSubjectClassrooms");
                });

            modelBuilder.Entity("backend.Models.TeacherSubjectClassroom", b =>
                {
                    b.Navigation("Exam");
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.Navigation("Student")
                        .IsRequired();

                    b.Navigation("StudentsExams");

                    b.Navigation("TeachersSubjectsClassrooms");

                    b.Navigation("UsersRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
