using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public interface IOrdenadorRepository
{
    Task<List<Ordenador>> AllAsync();

    Task<Ordenador?> GetByIdAsync(int id);

    Task AddAsync(Ordenador ordenador);

    Task UpdateAsync(Ordenador ordenador);

    Task DeleteAsync(int id);
}
