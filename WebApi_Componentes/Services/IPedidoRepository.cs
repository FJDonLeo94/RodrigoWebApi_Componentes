using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public interface IPedidoRepository
{
    Task<List<Pedido>> AllAsync();

    Task<Pedido?> GetByIdAsync(int id);

    Task AddAsync(Pedido pedido);

    Task UpdateAsync(Pedido pedido);

    Task DeleteAsync(int id);
}
