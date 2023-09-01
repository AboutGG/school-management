using System.Globalization;
using backend.Models;
using Microsoft.EntityFrameworkCore;

public static class Seeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.Parse("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"), Username = "giop5", Password = "123"},
            new User { Id = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c"), Username = "aboutgg", Password = "123"}
        );

        modelBuilder.Entity<Registry>().HasData(
            new Registry() {Id = Guid.Parse("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), Name = "Giordana", Surname = "Pistorio", Gender = "Vipera", Birth = DateOnly.Parse("15/09/1996")},
            new Registry() {Id = Guid.Parse("153afc1d-f63f-45aa-ae55-534d4ceeb737"), Name = "Gabriele", Surname = "Giuliano", Gender = "Sirenetta", Birth = DateOnly.Parse("03/01/2002")}
        );

        modelBuilder.Entity<Classroom>().HasData(
            new Classroom() {Id = Guid.Parse("612ce7d2-c15f-4dca-ac34-676e93f6bb0e"), Name = "1A"},
            new Classroom() {Id = Guid.Parse("0ed3811a-0a5c-4ed0-b7db-53090199aa27"), Name = "1B"},
            new Classroom() {Id = Guid.Parse("70f432dc-2a6c-499b-9326-52d1506befa5"), Name = "2A"}
        );
        
        
    }
}