using Api.Domain.UseCases.TypesEquipments.Dtos;
using Api.Domain.UseCases.TypesEquipments.Models;
using Api.Domain.UseCases.TypesEquipments.Repositories.Interfaces;
using Api.Domain.UseCases.TypesEquipments.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesEquipments.Services;

public class TypesEquipmentService(ITypesEquipmentRepository repository) : ITypesEquipmentService
{
    private readonly ITypesEquipmentRepository _repository = repository;

    public async Task<PaginatedList<TypesEquipment>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams)
    {
        var query = _repository.getAll();

        query = query.Where(x => x.deletedAt == null);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(filterParams.name))
        {
            query = query.Where(x => x.eqTypName.Contains(filterParams.name));
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
        return await PaginatedList<TypesEquipment>.CreateAsync(query, paginationParams.pageNumber, paginationParams.pageSize);
    }

    public async Task<TypesEquipment?> getByIdAsync(int id)
    {
        return await _repository.getByIdAsync(id);
    }


    public async Task<TypesEquipment> addAsync(CreateTypesEquipmentDto dto)
    {
        bool isNameTaken = await _repository.existsByNameAsync(dto.eqTypName);
        if (isNameTaken)
        {
            throw new EntityNotCreatedException("TypesEquipment name already exists.");
        }
        var typesEquipment = new TypesEquipment { eqTypName = dto.eqTypName, status = dto.status };
        await _repository.addAsync(typesEquipment);
        return typesEquipment;
    }

    public async Task<TypesEquipment?> updateAsync(int id, UpdateTypesEquipmentDto dto)
    {
        bool isNameTaken = await _repository.existsByNameAsync(dto.eqTypName);

        if (isNameTaken)
        {
            throw new EntityNotUpdatedException("TypesEquipment name already exists.");
        }
        var typesEquipment = await _repository.getByIdAsync(id);
        if (typesEquipment == null) return null;

        typesEquipment.eqTypName = dto.eqTypName;
        typesEquipment.status = dto.status;
        await _repository.updateAsync(typesEquipment);
        return typesEquipment;
    }

    public async Task<bool> softDeleteAsync(int id)
    {
        var typesEquipment = await _repository.getByIdAsync(id);
        if (typesEquipment == null) return false;

        await _repository.softDeleteAsync(typesEquipment);
        return true;
    }
}