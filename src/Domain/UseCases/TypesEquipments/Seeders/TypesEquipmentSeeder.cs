using Api.Domain.UseCases.TypesEquipments.Models;

namespace Api.Domain.UseCases.TypesEquipments.Seeders;

public static class TypesEquipmentSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TypesEquipment>().HasData(
            new TypesEquipment { eqTypId = 1, eqTypName = "VisionLine", status = "ativo", createdAt = DateTime.UtcNow }
        );
    }
}