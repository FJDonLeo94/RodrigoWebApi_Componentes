using System.Data;
using System.Data.SqlClient;
using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public static class DatabaseQueryUtils
{
    public static void PopulateOrdenadorComponente(Componente componente, SqlConnection connection)
    {
        if (componente.OrdenadorId is null) return;

        var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT * FROM Ordenadores WHERE Id = {componente.OrdenadorId}";

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            componente.Ordenador = new Ordenador()
            {
                Id = reader.GetInt32("Id"),
                Descripcion = reader.GetString("Descripcion"),
                PedidoId = reader.IsDBNull("PedidoId") ? null : reader.GetInt32("PedidoId")
            };
        }
    }

    public static void PopulatePedidoOrdenador(Ordenador ordenador, SqlConnection connection)
    {
        if (ordenador.PedidoId is null) return;

        var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT * FROM Pedidos WHERE Id = {ordenador.PedidoId}";

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            ordenador.Pedido = new Pedido()
            {
                Id = reader.GetInt32("Id"),
                Descripcion = reader.GetString("Descripcion"),
                Fecha = reader.GetDateTime("Fecha")
            };
        }
    }

    public static List<Componente> GetCompontesByOrdenadorId(int ordenadorId, SqlConnection connection)
    {
        var componentes = new List<Componente>();

        var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT * FROM Componentes WHERE OrdenadorId = {ordenadorId}";

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            var componente = new Componente
            {
                Id = reader.GetInt32("Id"),
                NumeroDeSerie = reader.GetString("NumeroDeSerie"),
                Descripcion = reader.GetString("Descripcion"),
                Calor = reader.GetInt32("Calor"),
                Megas = reader.GetInt64("Megas"),
                Cores = reader.GetInt32("Cores"),
                Coste = reader.GetInt32("Coste"),
                Tipo = (TipoComponente)reader.GetInt32("Tipo"),
                OrdenadorId = reader.IsDBNull("OrdenadorId") ? null : reader.GetInt32("OrdenadorId")
            };

            PopulateOrdenadorComponente(componente, connection);

            componentes.Add(componente);
        }

        return componentes;
    }

    public static List<Ordenador> GetOrdenadoresByPedidoId(int pedidoId, SqlConnection connection)
    {
        var ordenadores = new List<Ordenador>();

        var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT * FROM Ordenadores WHERE PedidoId = {pedidoId}";

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            var ordenador = new Ordenador
            {
                Id = reader.GetInt32("Id"),
                Descripcion = reader.GetString("Descripcion"),
                PedidoId = reader.IsDBNull("PedidoId") ? null : reader.GetInt32("PedidoId")
            };

            PopulatePedidoOrdenador(ordenador, connection);

            ordenador.Componentes = GetCompontesByOrdenadorId(ordenador.Id, connection);
            ordenador.Coste = ordenador.Componentes.Sum(componente => componente.Coste);

            ordenadores.Add(ordenador);
        }

        return ordenadores;
    }

}
