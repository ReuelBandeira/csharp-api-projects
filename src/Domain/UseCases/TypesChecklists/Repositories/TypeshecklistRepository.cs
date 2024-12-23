using Api.Domain.UseCases.TypesChecklists.Repositories.Interfaces;
using Api.Domain.UseCases.TypesChecklists.Models;
using Api.Infra.Database;

namespace Api.Domain.UseCases.TypesChecklists.Repositories;

public class TypesChecklistRepository(AppDbContext dbContext) : ITypesChecklistRepository
{
    private readonly AppDbContext _context = dbContext;

    public IQueryable<TypesChecklist> getAll()
    {
        return _context.TypeChecklists.AsQueryable();
    }

    public async Task<TypesChecklist?> getByIdAsync(int id)
    {
        return await _context.TypeChecklists.FirstOrDefaultAsync(x => x.chTypId == id && x.deletedAt == null);
    }

    public async Task addAsync(TypesChecklist typesChecklist)
    {
        typesChecklist.createdAt = DateTime.UtcNow;
        await _context.TypeChecklists.AddAsync(typesChecklist);
        await _context.SaveChangesAsync();
    }

    public async Task updateAsync(TypesChecklist typesChecklist)
    {
        typesChecklist.updatedAt = DateTime.UtcNow;
        _context.TypeChecklists.Update(typesChecklist);
        await _context.SaveChangesAsync();
    }

    public async Task softDeleteAsync(TypesChecklist typesChecklist)
    {
        typesChecklist.deletedAt = DateTime.UtcNow;
        await updateAsync(typesChecklist);
    }

    public async Task<bool> existsByNameAsync(string name)
    {
        return await _context.TypeChecklists
            .AnyAsync(x => x.chTypName == name && x.deletedAt == null);
    }
}