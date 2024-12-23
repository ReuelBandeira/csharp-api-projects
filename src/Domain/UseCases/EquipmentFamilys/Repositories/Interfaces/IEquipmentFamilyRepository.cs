using Api.Domain.UseCases.EquipmentFamilys.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.EquipmentFamilys.Repositories.Interfaces;

public interface IEquipmentFamilyRepository
{
    public IQueryable<EquipmentFamily> getAll();
    public Task<EquipmentFamily?> getByIdAsync(int id);
    public Task addAsync(EquipmentFamily equipmentFamily);
    public Task updateAsync(EquipmentFamily equipmentFamily);
    public Task softDeleteAsync(EquipmentFamily equipmentFamily);
    public Task<bool> existsByNameAsync(string name);
}