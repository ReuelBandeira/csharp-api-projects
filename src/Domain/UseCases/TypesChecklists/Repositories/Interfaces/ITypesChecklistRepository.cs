
using Api.Domain.UseCases.TypesChecklists.Models;

namespace Api.Domain.UseCases.TypesChecklists.Repositories.Interfaces;

public interface ITypesChecklistRepository
{
    public IQueryable<TypesChecklist> getAll();
    public Task<TypesChecklist?> getByIdAsync(int id);
    public Task addAsync(TypesChecklist typesChecklist);
    public Task updateAsync(TypesChecklist typesChecklist);
    public Task softDeleteAsync(TypesChecklist typesChecklist);
    public Task<bool> existsByNameAsync(string name);
}