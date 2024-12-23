using Api.Domain.UseCases.EquipmentFamilys.Dtos;
using Api.Domain.UseCases.EquipmentFamilys.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.EquipmentFamilys.Services.Interfaces;

public interface IEquipmentFamilyService
{
    public Task<PaginatedList<EquipmentFamily>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams);
    public Task<EquipmentFamily?> getByIdAsync(int id);
    public Task<EquipmentFamily> addAsync(CreateEquipmentFamilyDto dto);
    public Task<EquipmentFamily?> updateAsync(int id, UpdateEquipmentFamilyDto dto);
    public Task<bool> softDeleteAsync(int id);
}