using Microsoft.AspNetCore.Mvc;
using WebApi_Componentes.Models;
using WebApi_Componentes.Services;

namespace WebApi_Componentes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComponentesController : ControllerBase
{

    private readonly IComponenteRepository _componenteRepository;

    public ComponentesController(IComponenteRepository componenteRepository)
    {
        _componenteRepository = componenteRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Componente>> GetAllComponentes()
    {
        return await _componenteRepository.AllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Componente> GetComponente(int id)
    {
        return await _componenteRepository.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddComponente(Componente componente)
    {
        await _componenteRepository.AddAsync(componente);
    }

    [HttpPut]
    public async Task UpdateComponente(Componente componente)
    {
        await _componenteRepository.UpdateAsync(componente);
    }

    [HttpDelete("{id}")]
    public async Task DeleteComponent(int id)
    {
        await _componenteRepository.DeleteAsync(id);
    }
}
