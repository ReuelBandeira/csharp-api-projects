using Api.Domain.UseCases.TypesEquipments.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesEquipments.Repositories.Interfaces;

public interface ITypesEquipmentRepository
{
    public IQueryable<TypesEquipment> getAll();
    public Task<TypesEquipment?> getByIdAsync(int id);
    public Task addAsync(TypesEquipment typesEquipment);
    public Task updateAsync(TypesEquipment typesEquipment);
    public Task softDeleteAsync(TypesEquipment typesEquipment);
    public Task<bool> existsByNameAsync(string name);
}