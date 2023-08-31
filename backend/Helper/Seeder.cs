using System.Globalization;
using backend.Models;
using Microsoft.EntityFrameworkCore;

public static class MyDataSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.Parse("1346712f-a66d-4b25-9ff6-cf6b7cd8c954"), Username = "giop5", Password = "123"},
            new User { Id = Guid.Parse("affab63e-dec6-4626-abfb-1e52b258cc6c"), Username = "aboutgg", Password = "123"}
        );

        modelBuilder.Entity<Registry>().HasData(
            new Registry() {Id = Guid.Parse("d7f23f33-ebf2-4716-8c3f-b997ba2da125"), Name = "Giordana", Surname = "Pistorio", Gender = "Vipera", Birth = DateOnly.Parse("15/09/1996")}
        );
    }
}