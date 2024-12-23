using Api.Domain.UseCases.TypesEquipments.Dtos;
using Api.Domain.UseCases.TypesEquipments.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesEquipments.Services.Interfaces;

public interface ITypesEquipmentService
{
    public Task<PaginatedList<TypesEquipment>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams);
    public Task<TypesEquipment?> getByIdAsync(int id);
    public Task<TypesEquipment> addAsync(CreateTypesEquipmentDto dto);
    public Task<TypesEquipment?> updateAsync(int id, UpdateTypesEquipmentDto dto);
    public Task<bool> softDeleteAsync(int id);
}