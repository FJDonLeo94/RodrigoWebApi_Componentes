using Microsoft.AspNetCore.Mvc;
using WebApi_Componentes.Models;
using WebApi_Componentes.Services;

namespace WebApi_Componentes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidosController(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Pedido>> GetAllPedidos()
    {
        return await _pedidoRepository.AllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Pedido> GetPedido(int id)
    {
        return await _pedidoRepository.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddPedido(Pedido pedido)
    {
        await _pedidoRepository.AddAsync(pedido);
    }

    [HttpPut]
    public async Task UpdatePedido(Pedido pedido)
    {
        await _pedidoRepository.UpdateAsync(pedido);
    }

    [HttpDelete("{id}")]
    public async Task DeletePedido(int id)
    {
        await _pedidoRepository.DeleteAsync(id);
    }
}
