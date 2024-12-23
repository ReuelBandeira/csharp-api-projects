using Api.Domain.UseCases.BusinessUnits.Dtos;
using Api.Domain.UseCases.BusinessUnits.Models;
using Api.Domain.UseCases.BusinessUnits.Repositories.Interfaces;
using Api.Domain.UseCases.BusinessUnits.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.BusinessUnits.Services;

public class BusinessUnitService(IBusinessUnitRepository repository) : IBusinessUnitService
{
    private readonly IBusinessUnitRepository _repository = repository;

    public async Task<PaginatedList<BusinessUnit>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams)
    {
        var query = _repository.getAll();

        query = query.Where(x => x.deletedAt == null);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(filterParams.name))
        {
            query = query.Where(x => x.busName.Contains(filterParams.name));
        }

        if (filterParams.createdAfter.HasValue)
        {
            query = query.Where(x => x.createdAt >= filterParams.createdAfter.Value);
        }

        if (filterParams.createdBefore.HasValue)
        {
            query = query.Where(x => x.createdAt <= filterParams.createdBefore.Value);
        }

        // Retornar resultados paginados
        return await PaginatedList<BusinessUnit>.CreateAsync(query, paginationParams.pageNumber, paginationParams.pageSize);
    }

    public async Task<BusinessUnit?> getByIdAsync(int id)
    {
        return await _repository.getByIdAsync(id);
    }

    public async Task<BusinessUnit> addAsync(CreateBusinessUnitDto dto)
    {
        var businessUnit = new BusinessUnit { busName = dto.busName };
        await _repository.addAsync(businessUnit);
        return businessUnit;
    }

    public async Task<BusinessUnit?> updateAsync(int id, UpdateBusinessUnitDto dto)
    {
        var businessUnit = await _repository.getByIdAsync(id);
        if (businessUnit == null) return null;

        businessUnit.busName = dto.busName;
        await _repository.updateAsync(businessUnit);
        return businessUnit;
    }

    public async Task<bool> softDeleteAsync(int id)
    {
        var businessUnit = await _repository.getByIdAsync(id);
        if (businessUnit == null) return false;

        await _repository.softDeleteAsync(businessUnit);
        return true;
    }
}