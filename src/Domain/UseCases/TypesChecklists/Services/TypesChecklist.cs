using Api.Domain.UseCases.TypesChecklists.Dtos;
using Api.Domain.UseCases.TypesChecklists.Models;
using Api.Domain.UseCases.TypesChecklists.Repositories.Interfaces;
using Api.Domain.UseCases.TypesChecklists.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesChecklists.Services;

public class TypesChecklistService(ITypesChecklistRepository repository) : ITypesChecklistService
{
    private readonly ITypesChecklistRepository _repository = repository;

    public async Task<PaginatedList<TypesChecklist>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams)
    {
        var query = _repository.getAll();

        query = query.Where(x => x.deletedAt == null);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(filterParams.name))
        {
            query = query.Where(x => x.chTypName.Contains(filterParams.name));
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
            query = query.Where(x => x.chTypStatus == filterParams.status);
        }

        // Retornar resultados paginados
        return await PaginatedList<TypesChecklist>.CreateAsync(query, paginationParams.pageNumber, paginationParams.pageSize);
    }

    public async Task<TypesChecklist?> getByIdAsync(int id)
    {
        return await _repository.getByIdAsync(id);
    }

    public async Task<TypesChecklist> addAsync(CreateTypesChecklistDto dto)
    {
        bool isNameTaken = await _repository.existsByNameAsync(dto.chTypName);
        if (isNameTaken)
        {
            throw new EntityNotCreatedException("TypesChecklist name already exists.");
        }
        var typesChecklist = new TypesChecklist { chTypName = dto.chTypName, chTypStatus = dto.chTypStatus };
        await _repository.addAsync(typesChecklist);
        return typesChecklist;
    }

    public async Task<TypesChecklist?> updateAsync(int id, UpdateTypesChecklistDto dto)
    {
        bool isNameTaken = await _repository.existsByNameAsync(dto.chTypName);

        if (isNameTaken)
        {
            throw new EntityNotUpdatedException("TypesChecklist name already exists.");
        }
        var typesChecklist = await _repository.getByIdAsync(id);
        if (typesChecklist == null) return null;

        typesChecklist.chTypName = dto.chTypName;
        typesChecklist.chTypStatus = dto.chTypStatus;
        await _repository.updateAsync(typesChecklist);
        return typesChecklist;
    }

    public async Task<bool> softDeleteAsync(int id)
    {
        var typesEquipment = await _repository.getByIdAsync(id);
        if (typesEquipment == null) return false;

        await _repository.softDeleteAsync(typesEquipment);
        return true;
    }
}