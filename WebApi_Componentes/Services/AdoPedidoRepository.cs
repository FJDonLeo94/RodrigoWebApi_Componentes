using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public class AdoPedidoRepository : IPedidoRepository
{
    private readonly string _connectionString;

    public AdoPedidoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<List<Pedido>> AllAsync()
    {
        var pedidos = new List<Pedido>();

        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Pedidos";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var pedido = new Pedido
                {
                    Id = reader.GetInt32("Id"),
                    Descripcion = reader.GetString("Descripcion"),
                    Fecha = reader.GetDateTime("Fecha")
                };

                pedido.Ordenadores = DatabaseQueryUtils.GetOrdenadoresByPedidoId(pedido.Id, connection);
                pedido.Coste = pedido.Ordenadores.Sum(ordenador => ordenador.Coste);

                pedidos.Add(pedido);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        return Task.FromResult(pedidos);
    }

    public Task<Pedido?> GetByIdAsync(int id)
    {
        var pedido = new Pedido();

        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM Pedidos WHERE Id = {id}";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                pedido.Id = reader.GetInt32("Id");
                pedido.Descripcion = reader.GetString("Descripcion");
                pedido.Fecha = reader.GetDateTime("Fecha");
                pedido.Ordenadores = DatabaseQueryUtils.GetOrdenadoresByPedidoId(pedido.Id, connection);
                pedido.Coste = pedido.Ordenadores.Sum(ordenador => ordenador.Coste);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        return Task.FromResult(pedido);
    }

    public Task AddAsync(Pedido pedido)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Pedidos (Id, Descripcion, Fecha) " +
                                  "VALUES (@Id, @Descripcion, @Fecha)";

            command.Parameters.AddWithValue("@Id", pedido.Id);
            command.Parameters.AddWithValue("@Descripcion", pedido.Descripcion);
            command.Parameters.AddWithValue("@Fecha", pedido.Fecha);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Pedido pedido)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Pedidos " +
                                  "SET " +
                                  "Descripcion = @Descripcion, " +
                                  "Fecha = @Fecha " +
                                  $"WHERE Id = {pedido.Id}";

            command.Parameters.AddWithValue("@Descripcion", pedido.Descripcion);
            command.Parameters.AddWithValue("@Fecha", pedido.Fecha);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM Pedidos WHERE Id = {id}";

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        return Task.CompletedTask;
    }

}
