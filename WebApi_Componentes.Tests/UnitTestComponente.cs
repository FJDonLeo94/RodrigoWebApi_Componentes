using System.Drawing;
using WebApi_Componentes.Controllers;
using WebApi_Componentes.Models;
using WebApi_Componentes.Services;

namespace WebApi_Componentes.Tests;

[TestClass]
public class UnitTestComponente
{
    private readonly ComponentesController _controlador = new(new FakeComponenteRepository());

    [TestMethod]
    public async Task TestGetAllComponentes()
    {
        var componentes = await _controlador.GetAllComponentes();
        Assert.IsNotNull(componentes);
        Assert.AreEqual(15, componentes.Count());
    }

    [TestMethod]
    public async Task TestGetComponente()
    {
        var componente = await _controlador.GetComponente(2);
        Assert.IsNotNull(componente);
        Assert.AreEqual(2, componente.Id);
        Assert.AreEqual(12, componente.Calor);
        Assert.AreEqual(10, componente.Cores);
        Assert.AreEqual(138, componente.Coste);
        Assert.AreEqual("Procesador Intel i7", componente.Descripcion);
        Assert.AreEqual(0, componente.Megas);
        Assert.AreEqual("789-XCD", componente.NumeroDeSerie);
        Assert.AreEqual(TipoComponente.Procesador, componente.Tipo);
    }

    [TestMethod]
    public async Task TestAddComponente()
    {
        var nuevoComponente = new Componente()
        {
            Id = 16,
            Calor = 45,
            Cores = 0,
            Coste = 101,
            Descripcion = "Disco Mec�nico Patatin v2",
            Megas = 500,
            NumeroDeSerie = "788-fg-4",
            Tipo = TipoComponente.DiscoDuro
        };

        await _controlador.AddComponente(nuevoComponente);

        var componentes = await _controlador.GetAllComponentes();
        Assert.AreEqual(16, componentes.Count());

        var componente = await _controlador.GetComponente(16);
        Assert.IsNotNull(componente);
        Assert.AreEqual(16, componente.Id);
        Assert.AreEqual(45, componente.Calor);
        Assert.AreEqual(0, componente.Cores);
        Assert.AreEqual(101, componente.Coste);
        Assert.AreEqual("Disco Mec�nico Patatin v2", componente.Descripcion);
        Assert.AreEqual(500, componente.Megas);
        Assert.AreEqual("788-fg-4", componente.NumeroDeSerie);
        Assert.AreEqual(TipoComponente.DiscoDuro, componente.Tipo);
    }

    [TestMethod]
    public async Task TestUpdateComponente()
    {
        var componente = await _controlador.GetComponente(6);
        Assert.IsNotNull(componente);

        componente.Calor = 24 * 2;
        componente.Cores = 0 + 1;
        componente.Coste = 150 * 2;
        componente.Descripcion = "Banco de Memoria SSD actualizado";
        componente.Megas = 1024 * 2;
        componente.NumeroDeSerie = "879FH-T-2";
        componente.Tipo = TipoComponente.DiscoDuro;

        await _controlador.UpdateComponente(componente);

        var componenteActualizado = await _controlador.GetComponente(6);
        Assert.IsNotNull(componenteActualizado);
        Assert.AreEqual(6, componenteActualizado.Id);
        Assert.AreEqual(48, componenteActualizado.Calor);
        Assert.AreEqual(1, componenteActualizado.Cores);
        Assert.AreEqual(300, componenteActualizado.Coste);
        Assert.AreEqual("Banco de Memoria SSD actualizado", componenteActualizado.Descripcion);
        Assert.AreEqual(2048, componenteActualizado.Megas);
        Assert.AreEqual("879FH-T-2", componenteActualizado.NumeroDeSerie);
        Assert.AreEqual(TipoComponente.DiscoDuro, componenteActualizado.Tipo);
    }

    [TestMethod]
    public async Task TestDeleteComponente()
    {
        var componente = await _controlador.GetComponente(7);
        Assert.IsNotNull(componente);

        await _controlador.DeleteComponent(7);

        var componenteEliminado = await _controlador.GetComponente(7);
        Assert.IsNull(componenteEliminado);
    }
}
