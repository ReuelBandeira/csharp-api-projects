using Api.Domain.UseCases.BusinessUnits.Models;

namespace Api.Domain.UseCases.BusinessUnits.Seeders;

public static class BusinessUnitSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessUnit>().HasData(
            new BusinessUnit { busId = 1, busName = "Manaus", createdAt = DateTime.UtcNow },
            new BusinessUnit { busId = 2, busName = "Brasília", createdAt = DateTime.UtcNow },
            new BusinessUnit { busId = 3, busName = "Rio de Janeiro", createdAt = DateTime.UtcNow },
            new BusinessUnit { busId = 4, busName = "São Paulo", createdAt = DateTime.UtcNow },
            new BusinessUnit { busId = 5, busName = "Fortaleza", createdAt = DateTime.UtcNow }
        );
    }
}