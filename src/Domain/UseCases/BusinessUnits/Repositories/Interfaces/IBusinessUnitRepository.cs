using Api.Domain.UseCases.BusinessUnits.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.BusinessUnits.Repositories.Interfaces;

public interface IBusinessUnitRepository
{
    public IQueryable<BusinessUnit> getAll();
    public Task<BusinessUnit?> getByIdAsync(int id);
    public Task addAsync(BusinessUnit businessUnit);
    public Task updateAsync(BusinessUnit businessUnit);
    public Task softDeleteAsync(BusinessUnit businessUnit);
    public Task<bool> existsByNameAsync(string name);
}