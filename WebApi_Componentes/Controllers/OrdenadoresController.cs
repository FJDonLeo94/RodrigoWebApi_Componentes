using Microsoft.AspNetCore.Mvc;
using WebApi_Componentes.Models;
using WebApi_Componentes.Services;

namespace WebApi_Componentes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdenadoresController : ControllerBase
{
    private readonly IOrdenadorRepository _ordenadorRepository;

    public OrdenadoresController(IOrdenadorRepository ordenadorRepository)
    {
        _ordenadorRepository = ordenadorRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Ordenador>> GetAllOrdenadores()
    {
        return await _ordenadorRepository.AllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Ordenador> GetOrdenador(int id)
    {
        return await _ordenadorRepository.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddOrdenador(Ordenador ordenador)
    {
        await _ordenadorRepository.AddAsync(ordenador);
    }

    [HttpPut]
    public async Task UpdateOrdenador(Ordenador ordenador)
    {
        await _ordenadorRepository.UpdateAsync(ordenador);
    }

    [HttpDelete("{id}")]
    public async Task DeleteOrdenador(int id)
    {
        await _ordenadorRepository.DeleteAsync(id);
    }
}
