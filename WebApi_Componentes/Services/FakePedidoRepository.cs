using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public class FakePedidoRepository : IPedidoRepository
{
    private readonly List<Pedido> _pedidos = new()
    {
        new Pedido()
        {
            Id = 1,
            Descripcion = "Pedido de prueba 1",
            Fecha = new DateTime(2023, 9, 8),
            Ordenadores = new List<Ordenador>()
        }
    };

    public Task<List<Pedido>> AllAsync()
    {
        return Task.FromResult(_pedidos);
    }

    public Task<Pedido?> GetByIdAsync(int id)
    {
        return Task.FromResult(_pedidos.Find(pedido => pedido.Id == id));
    }

    public Task AddAsync(Pedido pedido)
    {
        int nuevoId = _pedidos.Count + 1;
        pedido.Id = nuevoId;

        _pedidos.Add(pedido);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Pedido pedido)
    {
        var pedidoActualizado = _pedidos.Find(element => element.Id.Equals(pedido.Id));

        if (pedidoActualizado is null) return Task.CompletedTask;

        pedidoActualizado.Id = pedido.Id;
        pedidoActualizado.Descripcion = pedido.Descripcion;
        pedidoActualizado.Fecha = pedido.Fecha;
        pedidoActualizado.Ordenadores = pedido.Ordenadores;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Pedido? pedido = _pedidos.Find(pedido => pedido.Id == id);

        if (pedido != null)
        {
            _pedidos.Remove(pedido);
        }

        return Task.CompletedTask;
    }
}
