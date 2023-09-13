using System.Globalization;
using backend.Models;
using Microsoft.EntityFrameworkCore;

public static class Seeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        #region Roles Seed

        modelBuilder.Entity<Role>().HasData(
            new Role()
            {
                Id = Guid.Parse("21598c0d-1177-4d73-8975-6052fd9dbd5f"),
                Name = "Teacher"
            },
            new Role()
            {
                Id = Guid.Parse("4d53602a-92af-42c8-95b0-42f15844f1f6"),
                Name = "Student"
            },
            new Role()
            {
                Id = Guid.Parse("308150a3-6975-4251-bf9a-f199b01e13a6"),
                Name = "Administrator"
            }
        );

        #endregion

        #region Users seed

        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = Guid.Parse("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"),
                Username = "giop5",
                Password = "123"
            },
            new User()
            {
                Id = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c"),
                Username = "aboutgg",
                Password = "123"
            },
            new User()
            {
                Id = Guid.Parse("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                Username = "sidectrl",
                Password = "nonloso"
            },
            new User()
            {
                Id = Guid.Parse("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                Username = "angelarmstrong",
                Password = "nonticonosco"
            },
            new User()
            {
                Id = Guid.Parse("37ce79ab-5b93-44ce-8189-e49ab8e377e2"),
                Username = "donnafrancisca",
                Password = "ilsegreto"
            },
            new User()
            {
                Id = Guid.Parse("d0158fc5-3704-4e89-9cd6-11b7e956c774"),
                Username = "unicornosupremodellemeraviglie",
                Password = "admin"
            }
        );

        #endregion

        #region Registries seed

        modelBuilder.Entity<Registry>().HasData(
            new Registry()
            {
                Id = Guid.Parse("d2d307bf-eb4e-4a58-afb9-aca5f4c23fce"),
                Name = "Mr. Unicorno",
                Surname = "Magico",
                Gender = "Unicorno",
                Birth = DateOnly.Parse("01/01/2001")
            },

            new Registry()
            {
                Id = Guid.Parse("d7f23f33-ebf2-4716-8c3f-b997ba2da125"),
                Name = "Giordana",
                Surname = "Pistorio",
                Gender = "Vipera",
                Birth = DateOnly.Parse("15/09/1996")
            },
            new Registry()
            {
                Id = Guid.Parse("153afc1d-f63f-45aa-ae55-534d4ceeb737"),
                Name = "Gabriele",
                Surname = "Giuliano",
                Gender = "Sirenetta",
                Birth = DateOnly.Parse("03/01/2002")
            },
            new Registry()
            {
                Id = Guid.Parse("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"),
                Name = "Francesco",
                Surname = "Limonelli",
                Gender = "M",
                Birth = DateOnly.Parse("25/09/2001")
            },
            new Registry()
            {
                Id = Guid.Parse("f833e6a7-f617-4683-a772-b5bcd1971da8"),
                Name = "Francesca",
                Surname = "Scollo",
                Gender = "F",
                Birth = DateOnly.Parse("06/05/1993")
            },
            new Registry()
            {
                Id = Guid.Parse("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"),
                Name = "Angelo",
                Surname = "Lombardo",
                Gender = "M",
                Birth = DateOnly.Parse("23/09/2001")
            }
        );

        #endregion

        #region Classrooms & subjects seed

        modelBuilder.Entity<Classroom>().HasData(
            new Classroom() { Id = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), Name = "1A" },
            new Classroom() { Id = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), Name = "1B" },
            new Classroom() { Id = Guid.Parse("70f432dc-2a6c-499b-9326-52d1506befa5"), Name = "2A" }
        );

        modelBuilder.Entity<Subject>().HasData(
            new Subject() { Id = Guid.Parse("be1816ff-41be-4620-a48c-ac18b71e3bf8"), Name = "Italiano" },
            new Subject() { Id = Guid.Parse("a907ec00-1577-4a50-ab10-579e071f1e59"), Name = "Inglese" },
            new Subject() { Id = Guid.Parse("46fd8c9d-b689-47cb-b9fd-44a19c5291a4"), Name = "Matematica" },
            new Subject() { Id = Guid.Parse("b55de490-fcdd-43d3-9146-94774e96cfe6"), Name = "Storia" },
            new Subject() { Id = Guid.Parse("336d920e-273f-40bd-aed3-17212e2fb2a3"), Name = "Geografia" },
            new Subject() { Id = Guid.Parse("47e8b0b5-1b53-46be-a0a9-9954958d3071"), Name = "Spagnola" }
        );

        #endregion

        #region Student seed

        modelBuilder.Entity<Student>().HasData(
            new Student()
            {
                UserId = Guid.Parse("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                ClassroomId = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
            },
            new Student()
            {
                UserId = Guid.Parse("37ce79ab-5b93-44ce-8189-e49ab8e377e2"), //donna francisca
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new Student()
            {
                UserId = Guid.Parse("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                ClassroomId = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
            }
        );

        #endregion

        #region TeachersSubjectsClassrooms seed

        modelBuilder.Entity<TeacherSubjectClassroom>().HasData(
            new TeacherSubjectClassroom()
            {
                Id = Guid.Parse("0f69c148-07ab-47a8-838a-0c9dfce974bf"),
                UserId = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c"),
                SubjectId = Guid.Parse("a907ec00-1577-4a50-ab10-579e071f1e59"),
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new TeacherSubjectClassroom()
            {
                Id = Guid.Parse("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"),
                UserId = Guid.Parse("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"),
                SubjectId = Guid.Parse("be1816ff-41be-4620-a48c-ac18b71e3bf8"),
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new TeacherSubjectClassroom()
            {
                Id = Guid.Parse("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"),
                UserId = Guid.Parse("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"),
                SubjectId = Guid.Parse("336d920e-273f-40bd-aed3-17212e2fb2a3"),
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new TeacherSubjectClassroom()
            {
                Id = Guid.Parse("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"),
                UserId = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c"),
                SubjectId = Guid.Parse("a907ec00-1577-4a50-ab10-579e071f1e59"),
                ClassroomId = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
            },
            new TeacherSubjectClassroom()
            {
                Id = Guid.Parse("0ac0626c-802a-4e59-a54d-8ddc3eab0b61"),
                UserId = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c"),
                SubjectId = Guid.Parse("a907ec00-1577-4a50-ab10-579e071f1e59"),
                ClassroomId = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
            }
        );

        #endregion

        #region Exams seed

        modelBuilder.Entity<Exam>().HasData(
            new Exam
            {
                Id = Guid.Parse("55988821-2bc3-4122-aa50-e0fb3b8f42ad"),
                ExamDate = DateOnly.Parse("06/09/2023"),
                TeacherSubjectClassroomId = Guid.Parse("0f69c148-07ab-47a8-838a-0c9dfce974bf"),
                IsWritten = true
            },
            new Exam
            {
                Id = Guid.Parse("06dec5ca-003e-4b39-af43-c745746d23e0"),
                ExamDate = DateOnly.Parse("10/09/2023"),
                TeacherSubjectClassroomId = Guid.Parse("a0d8bde6-4ece-4eaa-96bd-6da7d2db7daa"),
                IsWritten = true
            },
            new Exam
            {
                Id = Guid.Parse("20ad1b3e-af97-4a45-815b-af9f34e52dc3"),
                ExamDate = DateOnly.Parse("25/09/2023"),
                TeacherSubjectClassroomId = Guid.Parse("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"),
                IsWritten = false
            }
        );

        #endregion

        #region UsersRoles seed

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole()
            {
                RoleId = Guid.Parse("308150a3-6975-4251-bf9a-f199b01e13a6"),
                UserId = Guid.Parse("d0158fc5-3704-4e89-9cd6-11b7e956c774")
            },
            new UserRole()
            {
                RoleId = Guid.Parse("21598c0d-1177-4d73-8975-6052fd9dbd5f"),
                UserId = Guid.Parse("1346712f-a66d-4b25-9ff6-cf6b7cd8c954")
            },
            new UserRole()
            {
                RoleId = Guid.Parse("21598c0d-1177-4d73-8975-6052fd9dbd5f"),
                UserId = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c")
            },
            new UserRole()
            {
                RoleId = Guid.Parse("4d53602a-92af-42c8-95b0-42f15844f1f6"),
                UserId = Guid.Parse("8af66697-aaf2-44d3-ac9e-b051451fa2ea")
            },
            new UserRole()
            {
                RoleId = Guid.Parse("4d53602a-92af-42c8-95b0-42f15844f1f6"),
                UserId = Guid.Parse("37ce79ab-5b93-44ce-8189-e49ab8e377e2")
            },
            new UserRole()
            {
                RoleId = Guid.Parse("4d53602a-92af-42c8-95b0-42f15844f1f6"),
                UserId = Guid.Parse("c98b3291-bd68-4f9e-a906-1a273ac9046b")
            }
        );

        #endregion

        #region StudentsExams seed

        modelBuilder.Entity<StudentExam>().HasData(
            new StudentExam()
            {
                UserId = Guid.Parse("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                ExamId = Guid.Parse("1c510518-5a4f-496e-9d27-57a23ff4ccb0"),
                Grade = 6
            },
            new StudentExam()
            {
                UserId = Guid.Parse("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                ExamId = Guid.Parse("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"),
                Grade = 9
            },
            new StudentExam()
            {
                UserId = Guid.Parse("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                ExamId = Guid.Parse("7fb36228-d263-43d7-ba9a-58e7f6ff5f0d"),
                Grade = 5
            }
        );

        #endregion

    }
}