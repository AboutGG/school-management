﻿using System.Globalization;
using backend.Models;
using Microsoft.EntityFrameworkCore;

public static class Seeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
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
            }
        );
        
        modelBuilder.Entity<Registry>().HasData(
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

        modelBuilder.Entity<Classroom>().HasData(
            new Classroom() { Id = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), Name = "1A" },
            new Classroom() { Id = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), Name = "1B" },
            new Classroom() { Id = Guid.Parse("70f432dc-2a6c-499b-9326-52d1506befa5"), Name = "2A" }
        );

        modelBuilder.Entity<Subject>().HasData(
            new Subject() {Id = Guid.Parse("be1816ff-41be-4620-a48c-ac18b71e3bf8"), Name = "Italiano"},
            new Subject() {Id = Guid.Parse("a907ec00-1577-4a50-ab10-579e071f1e59"), Name = "Inglese"},
            new Subject() {Id = Guid.Parse("46fd8c9d-b689-47cb-b9fd-44a19c5291a4"), Name = "Matematica"},
            new Subject() {Id = Guid.Parse("b55de490-fcdd-43d3-9146-94774e96cfe6"), Name = "Storia"},
            new Subject() {Id = Guid.Parse("336d920e-273f-40bd-aed3-17212e2fb2a3"), Name = "Geografia"},
            new Subject() {Id = Guid.Parse("47e8b0b5-1b53-46be-a0a9-9954958d3071"), Name = "Spagnola"}
        );

        modelBuilder.Entity<Student>().HasData(
            new Student()
            {
                Id = Guid.Parse("007d3bca-d81d-42bd-9194-9c1d9f1f5ed7"),
                UserId = Guid.Parse("8af66697-aaf2-44d3-ac9e-b051451fa2ea"),
                RegistryId = Guid.Parse("c976d8c8-3aa5-4164-be7c-884ebe29ee1e"),
                ClassroomId = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
            },
            new Student()
            {
                Id = Guid.Parse("8767fd02-7891-4b47-8b02-3cc0d07ac334"),
                UserId = Guid.Parse("37ce79ab-5b93-44ce-8189-e49ab8e377e2"),
                RegistryId = Guid.Parse("f833e6a7-f617-4683-a772-b5bcd1971da8"),
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new Student()
            {
                Id = Guid.Parse("78362ba2-29ea-472b-9878-f55dad233e21"),
                UserId = Guid.Parse("c98b3291-bd68-4f9e-a906-1a273ac9046b"),
                RegistryId = Guid.Parse("634477e4-1eeb-4a0d-bb07-c9bd2e3f9702"),
                ClassroomId = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
            }
        );

        modelBuilder.Entity<Teacher>().HasData(
            new Teacher()
            {
                Id = Guid.Parse("cc3f629e-ae6b-448e-be46-afce1fa9e31d"),
                RegistryId = Guid.Parse("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), //Giordana
                UserId = Guid.Parse("1346712f-a66d-4b25-9ff6-cf6b7cd8c954")
            },
            new Teacher()
            {
                Id = Guid.Parse("54ff5a4a-1469-4f07-afcb-9b1864dcb335"),
                RegistryId = Guid.Parse("153afc1d-f63f-45aa-ae55-534d4ceeb737"),
                UserId = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c")
            }
        );

        modelBuilder.Entity<TeacherSubjectClassroom>().HasData(
            new TeacherSubjectClassroom()
            {
                TeacherId = Guid.Parse("54ff5a4a-1469-4f07-afcb-9b1864dcb335"),
                SubjectId = Guid.Parse("a907ec00-1577-4a50-ab10-579e071f1e59"),
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new TeacherSubjectClassroom()
            {
                TeacherId = Guid.Parse("cc3f629e-ae6b-448e-be46-afce1fa9e31d"),
                SubjectId = Guid.Parse("be1816ff-41be-4620-a48c-ac18b71e3bf8"),
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new TeacherSubjectClassroom()
            {
                TeacherId = Guid.Parse("cc3f629e-ae6b-448e-be46-afce1fa9e31d"),
                SubjectId = Guid.Parse("336d920e-273f-40bd-aed3-17212e2fb2a3"),
                ClassroomId = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27")
            },
            new TeacherSubjectClassroom()
            {
                TeacherId = Guid.Parse("54ff5a4a-1469-4f07-afcb-9b1864dcb335"),
                SubjectId = Guid.Parse("a907ec00-1577-4a50-ab10-579e071f1e59"),
                ClassroomId = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e")
            }
        );
        
        

    }
}