using Api.Domain.UseCases.EquipmentFamilys.Repositories.Interfaces;
using Api.Domain.UseCases.EquipmentFamilys.Models;
using Api.Infra.Database;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.EquipmentFamilys.Repositories;

public class EquipmentFamilyRepository(AppDbContext dbContext) : IEquipmentFamilyRepository
{
    private readonly AppDbContext _context = dbContext;

    public IQueryable<EquipmentFamily> getAll()
    {
        return _context.EquipmentFamilys.AsQueryable();
    }

    public async Task<EquipmentFamily?> getByIdAsync(int id)
    {
        return await _context.EquipmentFamilys.FirstOrDefaultAsync(x => x.eqFamId == id && x.deletedAt == null);
    }

    public async Task addAsync(EquipmentFamily equipmentFamily)
    {
        equipmentFamily.createdAt = DateTime.UtcNow;
        await _context.EquipmentFamilys.AddAsync(equipmentFamily);
        await _context.SaveChangesAsync();
    }

    public async Task updateAsync(EquipmentFamily equipmentFamily)
    {
        equipmentFamily.updatedAt = DateTime.UtcNow;
        _context.EquipmentFamilys.Update(equipmentFamily);
        await _context.SaveChangesAsync();
    }

    public async Task softDeleteAsync(EquipmentFamily equipmentFamily)
    {
        equipmentFamily.deletedAt = DateTime.UtcNow;
        await updateAsync(equipmentFamily);
    }

    public async Task<bool> existsByNameAsync(string name)
    {
        return await _context.EquipmentFamilys
            .AnyAsync(x => x.eqFamName == name && x.deletedAt == null);
    }
}