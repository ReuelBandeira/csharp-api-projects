using Api.Domain.UseCases.BusinessUnits.Repositories.Interfaces;
using Api.Domain.UseCases.BusinessUnits.Models;
using Api.Infra.Database;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.BusinessUnits.Repositories;

public class BusinessUnitRepository(AppDbContext dbContext) : IBusinessUnitRepository
{
    private readonly AppDbContext _context = dbContext;

    public IQueryable<BusinessUnit> getAll()
    {
        return _context.BusinessUnits.AsQueryable();
    }

    public async Task<BusinessUnit?> getByIdAsync(int id)
    {
        return await _context.BusinessUnits.FirstOrDefaultAsync(x => x.busId == id && x.deletedAt == null);
    }

    public async Task addAsync(BusinessUnit businessUnit)
    {
        businessUnit.createdAt = DateTime.UtcNow;
        await _context.BusinessUnits.AddAsync(businessUnit);
        await _context.SaveChangesAsync();
    }

    public async Task updateAsync(BusinessUnit businessUnit)
    {
        businessUnit.updatedAt = DateTime.UtcNow;
        _context.BusinessUnits.Update(businessUnit);
        await _context.SaveChangesAsync();
    }

    public async Task softDeleteAsync(BusinessUnit businessUnit)
    {
        businessUnit.deletedAt = DateTime.UtcNow;
        await updateAsync(businessUnit);
    }

    public async Task<bool> existsByNameAsync(string name)
    {
        return await _context.BusinessUnits
            .AnyAsync(x => x.busName == name && x.deletedAt == null);
    }
}