using WebApi_Componentes.Controllers;
using WebApi_Componentes.Models;
using WebApi_Componentes.Services;

namespace WebApi_Componentes.Tests;

[TestClass]
public class UnitTestPedido
{
    private readonly PedidosController _controlador = new(new FakePedidoRepository());

    [TestMethod]
    public async Task TestGetAllPedidos()
    {
        var pedidos = await _controlador.GetAllPedidos();
        Assert.IsNotNull(pedidos);
        Assert.AreEqual(1, pedidos.Count());
    }

    [TestMethod]
    public async Task TestGetPedido()
    {
        var pedido = await _controlador.GetPedido(1);
        Assert.IsNotNull(pedido);
        Assert.AreEqual(1, pedido.Id);
        Assert.AreEqual("Pedido de prueba 1", pedido.Descripcion);
        Assert.AreEqual(new DateTime(2023, 9, 8), pedido.Fecha);
        Assert.IsNotNull(pedido.Ordenadores);
        Assert.AreEqual(0, pedido.Ordenadores.Count);
    }

    [TestMethod]
    public async Task TestAddPedido()
    {
        var nuevoPedido = new Pedido()
        {
            Id = 2,
            Descripcion = "Pedido nuevo",
            Fecha = new DateTime(2023, 9, 10)
        };

        await _controlador.AddPedido(nuevoPedido);

        var pedidos = await _controlador.GetAllPedidos();
        Assert.AreEqual(2, pedidos.Count());

        var pedido = await _controlador.GetPedido(2);
        Assert.IsNotNull(pedido);
        Assert.AreEqual(2, pedido.Id);
        Assert.AreEqual("Pedido nuevo", pedido.Descripcion);
        Assert.AreEqual(new DateTime(2023, 9, 10), pedido.Fecha);
        Assert.IsNotNull(pedido.Ordenadores);
        Assert.AreEqual(0, pedido.Ordenadores.Count);
    }

    [TestMethod]
    public async Task TestUpdatePedido()
    {
        var pedido = await _controlador.GetPedido(1);
        Assert.IsNotNull(pedido);

        pedido.Descripcion = "Pedido de prueba 1 actualizado";

        await _controlador.UpdatePedido(pedido);

        var pedidoActualizado = await _controlador.GetPedido(1);
        Assert.IsNotNull(pedidoActualizado);
        Assert.AreEqual(1, pedidoActualizado.Id);
        Assert.AreEqual("Pedido de prueba 1 actualizado", pedidoActualizado.Descripcion);
    }

    [TestMethod]
    public async Task TestDeletePedido()
    {
        var pedido = await _controlador.GetPedido(1);
        Assert.IsNotNull(pedido);

        await _controlador.DeletePedido(1);

        var pedidoEliminado = await _controlador.GetPedido(1);
        Assert.IsNull(pedidoEliminado);
    }

}
