using Api.Domain.UseCases.EquipmentFamilys.Models;

namespace Api.Domain.UseCases.EquipmentFamilys.Seeders;

public static class EquipmentFamilySeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquipmentFamily>().HasData(
            new EquipmentFamily { eqFamId = 1, eqFamName = "VisionLine", status = "ativo", createdAt = DateTime.UtcNow },
            new EquipmentFamily { eqFamId = 2, eqFamName = "SmartCore", status = "ativo", createdAt = DateTime.UtcNow },
            new EquipmentFamily { eqFamId = 3, eqFamName = "PowerMax", status = "ativo", createdAt = DateTime.UtcNow },
            new EquipmentFamily { eqFamId = 4, eqFamName = "SoundPro", status = "ativo", createdAt = DateTime.UtcNow },
            new EquipmentFamily { eqFamId = 5, eqFamName = "HomeConnect", status = "ativo", createdAt = DateTime.UtcNow }
        );
    }
}