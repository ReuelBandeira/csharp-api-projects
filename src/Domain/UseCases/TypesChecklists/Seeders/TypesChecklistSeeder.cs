using Api.Domain.UseCases.TypesChecklists.Models;

namespace Api.Domain.UseCases.TypesCheklists.Seeders;

public static class TypesChecklistSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TypesChecklist>().HasData(
            new TypesChecklist { chTypId = 1, chTypName = "Teste1", chTypStatus = "ativo", createdAt = DateTime.UtcNow }
        );
    }
}