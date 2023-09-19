using WebApi_Componentes.Controllers;
using WebApi_Componentes.Models;
using WebApi_Componentes.Services;

namespace WebApi_Componentes.Tests;

[TestClass]
public class UnitTestOrdenador
{
    private readonly OrdenadoresController _controlador = new(new FakeOrdenadorRepository());

    [TestMethod]
    public async Task TestGetAllOrdenadores()
    {
        var ordenadores = await _controlador.GetAllOrdenadores();
        Assert.IsNotNull(ordenadores);
        Assert.AreEqual(1, ordenadores.Count());
    }

    [TestMethod]
    public async Task TestGetOrdenador()
    {
        var ordenador = await _controlador.GetOrdenador(1);
        Assert.IsNotNull(ordenador);
        Assert.AreEqual(1, ordenador.Id);
        Assert.AreEqual("OrdenadorPrueba", ordenador.Descripcion);
        Assert.IsNotNull(ordenador.Componentes);
        Assert.AreEqual(0, ordenador.Componentes.Count);
        Assert.IsNull(ordenador.PedidoId);
        Assert.IsNull(ordenador.Pedido);
    }

    [TestMethod]
    public async Task TestAddOrdenador()
    {
        var nuevoOrdenador = new Ordenador()
        {
            Id = 2,
            Descripcion = "OrdenadorNuevo"
        };

        await _controlador.AddOrdenador(nuevoOrdenador);

        var ordenadores = await _controlador.GetAllOrdenadores();
        Assert.AreEqual(2, ordenadores.Count());

        var ordenador = await _controlador.GetOrdenador(2);
        Assert.IsNotNull(ordenador);
        Assert.AreEqual(2, ordenador.Id);
        Assert.AreEqual("OrdenadorNuevo", ordenador.Descripcion);
        Assert.IsNotNull(ordenador.Componentes);
        Assert.AreEqual(0, ordenador.Componentes.Count);
        Assert.IsNull(ordenador.PedidoId);
        Assert.IsNull(ordenador.Pedido);
    }

    [TestMethod]
    public async Task TestUpdateOrdenador()
    {
        var ordenador = await _controlador.GetOrdenador(1);
        Assert.IsNotNull(ordenador);

        ordenador.Descripcion = "OrdenadorPrueba actualizado";

        await _controlador.UpdateOrdenador(ordenador);

        var ordenadorActualizado = await _controlador.GetOrdenador(1);
        Assert.IsNotNull(ordenadorActualizado);
        Assert.AreEqual(1, ordenadorActualizado.Id);
        Assert.AreEqual("OrdenadorPrueba actualizado", ordenadorActualizado.Descripcion);
    }

    [TestMethod]
    public async Task TestDeleteOrdenador()
    {
        var ordenador = await _controlador.GetOrdenador(1);
        Assert.IsNotNull(ordenador);

        await _controlador.DeleteOrdenador(1);

        var ordenadorEliminado = await _controlador.GetOrdenador(1);
        Assert.IsNull(ordenadorEliminado);
    }

}
