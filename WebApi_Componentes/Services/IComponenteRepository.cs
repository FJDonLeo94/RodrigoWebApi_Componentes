using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public interface IComponenteRepository
{
    Task<List<Componente>> AllAsync();

    Task<Componente?> GetByIdAsync(int id);

    Task AddAsync(Componente componente);

    Task UpdateAsync(Componente componente);

    Task DeleteAsync(int id);
}