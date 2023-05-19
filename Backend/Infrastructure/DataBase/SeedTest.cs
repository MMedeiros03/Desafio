using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase;

internal class SeedTest
{
    /// <summary>
    /// Método para configuração do modelo durante o teste.
    /// </summary>
    /// <param name="builder">Builder para configuração do modelo.</param>
    public static void OnModelCreating(ModelBuilder builder)
    {
        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableForeignKey? relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Popula dados de teste para a entidade Price
        builder.Entity<Price>().HasData(
        new Price
        {
            Id = 1,
            InitialDate = DateTime.Now,
            FinalDate = DateTime.Now.AddYears(1),
            InitialTime = 60,
            InitialTimeValue = 5,
            AdditionalHourlyValue = 1
        },
        new Price
        {
            Id = 2,
            InitialDate = DateTime.Now,
            FinalDate = DateTime.Now.AddYears(1),
            InitialTime = 60,
            InitialTimeValue = 5,
            AdditionalHourlyValue = 2
        },
        new Price
        {
            Id = 3,
            InitialDate = DateTime.Now,
            FinalDate = DateTime.Now.AddYears(1),
            InitialTime = 60,
            InitialTimeValue = 2,
            AdditionalHourlyValue = 1
        }
        );

        // Popula dados de teste para a entidade Parking
        builder.Entity<Parking>().HasData(
        new Parking
        {
            Id = 1,
            EntryDate = DateTime.Now,
            LicensePlate = "ABC-1234"
        },

        new Parking
        {
            Id = 2,
            EntryDate = DateTime.Now,
            LicensePlate = "DEF-5678"
        },

        new Parking
        {
            Id = 3,
            EntryDate = DateTime.Now,
            LicensePlate = "GHI-8910"
        },

        new Parking
        {
            Id = 4,
            EntryDate = DateTime.Now,
            LicensePlate = "JKL-1112"
        });
    }
}