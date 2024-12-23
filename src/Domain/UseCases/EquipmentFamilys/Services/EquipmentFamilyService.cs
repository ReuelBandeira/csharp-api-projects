using Api.Domain.UseCases.EquipmentFamilys.Dtos;
using Api.Domain.UseCases.EquipmentFamilys.Models;
using Api.Domain.UseCases.EquipmentFamilys.Repositories.Interfaces;
using Api.Domain.UseCases.EquipmentFamilys.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.EquipmentFamilys.Services;

public class EquipmentFamilyService(IEquipmentFamilyRepository repository) : IEquipmentFamilyService
{
    private readonly IEquipmentFamilyRepository _repository = repository;

    public async Task<PaginatedList<EquipmentFamily>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams)
    {
        var query = _repository.getAll();

        query = query.Where(x => x.deletedAt == null);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(filterParams.name))
        {
            query = query.Where(x => x.eqFamName.Contains(filterParams.name));
        }

        if (filterParams.createdAfter.HasValue)
        {
            query = query.Where(x => x.createdAt >= filterParams.createdAfter.Value);
        }

        if (filterParams.createdBefore.HasValue)
        {
            query = query.Where(x => x.createdAt <= filterParams.createdBefore.Value);
        }

        if (!string.IsNullOrEmpty(filterParams.status))
        {
            query = query.Where(x => x.status == filterParams.status);
        }

        // Retornar resultados paginados
        return await PaginatedList<EquipmentFamily>.CreateAsync(query, paginationParams.pageNumber, paginationParams.pageSize);
    }

    public async Task<EquipmentFamily?> getByIdAsync(int id)
    {
        return await _repository.getByIdAsync(id);
    }


    public async Task<EquipmentFamily> addAsync(CreateEquipmentFamilyDto dto)
    {
        bool isNameTaken = await _repository.existsByNameAsync(dto.eqFamilyName);
        if (isNameTaken)
        {
            throw new EntityNotCreatedException("Equipment family name already exists.");
        }
        var equipmentFamily = new EquipmentFamily { eqFamName = dto.eqFamilyName, status = dto.status };
        await _repository.addAsync(equipmentFamily);
        return equipmentFamily;
    }

    public async Task<EquipmentFamily?> updateAsync(int id, UpdateEquipmentFamilyDto dto)
    {
        bool isNameTaken = await _repository.existsByNameAsync(dto.eqFamilyName);

        if (isNameTaken)
        {
            throw new EntityNotUpdatedException("Equipment family name already exists.");
        }
        var equipmentFamily = await _repository.getByIdAsync(id);
        if (equipmentFamily == null) return null;

        equipmentFamily.eqFamName = dto.eqFamilyName;
        equipmentFamily.status = dto.status;
        await _repository.updateAsync(equipmentFamily);
        return equipmentFamily;
    }

    public async Task<bool> softDeleteAsync(int id)
    {
        var equipmentFamily = await _repository.getByIdAsync(id);
        if (equipmentFamily == null) return false;

        await _repository.softDeleteAsync(equipmentFamily);
        return true;
    }
}