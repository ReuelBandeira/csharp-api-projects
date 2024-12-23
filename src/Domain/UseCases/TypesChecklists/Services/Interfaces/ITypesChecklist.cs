using Api.Domain.UseCases.TypesChecklists.Dtos;
using Api.Domain.UseCases.TypesChecklists.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesChecklists.Services.Interfaces;

public interface ITypesChecklistService
{
    public Task<PaginatedList<TypesChecklist>> getAllAsync(PaginationParams paginationParams, FilterParams filterParams);
    public Task<TypesChecklist?> getByIdAsync(int id);
    public Task<TypesChecklist> addAsync(CreateTypesChecklistDto dto);
    public Task<TypesChecklist?> updateAsync(int id, UpdateTypesChecklistDto dto);
    public Task<bool> softDeleteAsync(int id);
}