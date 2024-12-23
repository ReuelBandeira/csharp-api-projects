using Api.Domain.UseCases.BusinessUnits.Dtos;
using Api.Domain.UseCases.BusinessUnits.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.BusinessUnits.Services.Interfaces;

public interface IBusinessUnitService
{
    public Task<PaginatedList<BusinessUnit>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams);
    public Task<BusinessUnit?> getByIdAsync(int id);
    public Task<BusinessUnit> addAsync(CreateBusinessUnitDto dto);
    public Task<BusinessUnit?> updateAsync(int id, UpdateBusinessUnitDto dto);
    public Task<bool> softDeleteAsync(int id);
}