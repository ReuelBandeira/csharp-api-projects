using Api.Domain.UseCases.TypesEquipments.Repositories.Interfaces;
using Api.Domain.UseCases.TypesEquipments.Models;
using Api.Infra.Database;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesEquipments.Repositories;

public class TypesEquipmentRepository(AppDbContext dbContext) : ITypesEquipmentRepository
{
    private readonly AppDbContext _context = dbContext;

    public IQueryable<TypesEquipment> getAll()
    {
        return _context.TypeEquipments.AsQueryable();
    }

    public async Task<TypesEquipment?> getByIdAsync(int id)
    {
        return await _context.TypeEquipments.FirstOrDefaultAsync(x => x.eqTypId == id && x.deletedAt == null);
    }

    public async Task addAsync(TypesEquipment typesEquipment)
    {
        typesEquipment.createdAt = DateTime.UtcNow;
        await _context.TypeEquipments.AddAsync(typesEquipment);
        await _context.SaveChangesAsync();
    }

    public async Task updateAsync(TypesEquipment typesEquipment)
    {
        typesEquipment.updatedAt = DateTime.UtcNow;
        _context.TypeEquipments.Update(typesEquipment);
        await _context.SaveChangesAsync();
    }

    public async Task softDeleteAsync(TypesEquipment typesEquipment)
    {
        typesEquipment.deletedAt = DateTime.UtcNow;
        await updateAsync(typesEquipment);
    }

    public async Task<bool> existsByNameAsync(string name)
    {
        return await _context.TypeEquipments
            .AnyAsync(x => x.eqTypName == name && x.deletedAt == null);
    }
}