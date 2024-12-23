using Api.Domain.UseCases.BusinessUnits.Models;
using Api.Domain.UseCases.BusinessUnits.Seeders;
using Api.Domain.UseCases.EquipmentFamilys.Models;
using Api.Domain.UseCases.TypesEquipments.Models;
using Api.Domain.UseCases.TypesChecklists.Models;
using Api.Domain.UseCases.EquipmentFamilys.Seeders;
using Api.Domain.UseCases.TypesCheklists.Seeders;

namespace Api.Infra.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BusinessUnit> BusinessUnits { get; set; } = null!;
    public DbSet<EquipmentFamily> EquipmentFamilys { get; set; } = null!;
    public DbSet<TypesEquipment> TypeEquipments { get; set; } = null!;
    public DbSet<TypesChecklist> TypeChecklists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BusinessUnitSeeder.Seed(modelBuilder);
        EquipmentFamilySeeder.Seed(modelBuilder);
        TypesChecklistSeeder.Seed(modelBuilder);
    }
}
