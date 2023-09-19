using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public class FakeOrdenadorRepository : IOrdenadorRepository
{
    private readonly List<Ordenador> _ordenadores = new()
    {
        new Ordenador { Id = 1, Descripcion = "OrdenadorPrueba", PedidoId = null, Pedido = null }
    };

    public Task<List<Ordenador>> AllAsync()
    {
        return Task.FromResult(_ordenadores);
    }

    public Task<Ordenador?> GetByIdAsync(int id)
    {
        return Task.FromResult(_ordenadores.Find(ordenador => ordenador.Id == id));
    }

    public Task AddAsync(Ordenador ordenador)
    {
        int nuevoId = _ordenadores.Count + 1;
        ordenador.Id = nuevoId;

        _ordenadores.Add(ordenador);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Ordenador ordenador)
    {
        var ordenadorActualizado = _ordenadores.Find(element => element.Id.Equals(ordenador.Id));

        if (ordenadorActualizado is null) return Task.CompletedTask;

        ordenadorActualizado.Id = ordenador.Id;
        ordenadorActualizado.Componentes = ordenador.Componentes;
        ordenadorActualizado.Descripcion = ordenador.Descripcion;
        ordenadorActualizado.PedidoId = ordenador.PedidoId;
        ordenadorActualizado.Pedido = ordenador.Pedido;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Ordenador? ordenador = _ordenadores.Find(ordenador => ordenador.Id == id);

        if (ordenador != null)
        {
            _ordenadores.Remove(ordenador);
        }

        return Task.CompletedTask;
    }
}
