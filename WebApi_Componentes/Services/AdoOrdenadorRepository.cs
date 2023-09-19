using System.Data;
using System.Data.SqlClient;
using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public class AdoOrdenadorRepository : IOrdenadorRepository
{
    private readonly string _connectionString;

    public AdoOrdenadorRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<List<Ordenador>> AllAsync()
    {
        var ordenadores = new List<Ordenador>();

        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Ordenadores";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var ordenador = new Ordenador
                {
                    Id = reader.GetInt32("Id"),
                    Descripcion = reader.GetString("Descripcion"),
                    PedidoId = reader.IsDBNull("PedidoId") ? null : reader.GetInt32("PedidoId")
                };

                ordenador.Componentes = DatabaseQueryUtils.GetCompontesByOrdenadorId(ordenador.Id, connection);
                ordenador.Coste = ordenador.Componentes.Sum(componente => componente.Coste);

                DatabaseQueryUtils.PopulatePedidoOrdenador(ordenador, connection);

                ordenadores.Add(ordenador);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        return Task.FromResult(ordenadores);
    }

    public Task<Ordenador?> GetByIdAsync(int id)
    {
        var ordenador = new Ordenador();

        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM Ordenadores WHERE Id = {id}";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ordenador.Id = reader.GetInt32("Id");
                ordenador.Descripcion = reader.GetString("Descripcion");
                ordenador.PedidoId = reader.IsDBNull("PedidoId") ? null : reader.GetInt32("PedidoId");
                ordenador.Componentes = DatabaseQueryUtils.GetCompontesByOrdenadorId(ordenador.Id, connection);
                ordenador.Coste = ordenador.Componentes.Sum(componente => componente.Coste);

                DatabaseQueryUtils.PopulatePedidoOrdenador(ordenador, connection);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        return Task.FromResult(ordenador);
    }

    public Task AddAsync(Ordenador ordenador)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Ordenadores (Id, Descripcion, PedidoId) " +
                                  "VALUES (@Id, @Descripcion, @PedidoId)";

            command.Parameters.AddWithValue("@Id", ordenador.Id);
            command.Parameters.AddWithValue("@Descripcion", ordenador.Descripcion);
            command.Parameters.AddWithValue("@PedidoId", ordenador.PedidoId is null ? DBNull.Value : ordenador.PedidoId);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Ordenador ordenador)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Ordenadores " +
                                  "SET " +
                                  "Descripcion = @Descripcion, " +
                                  "PedidoId = @PedidoId " +
                                  $"WHERE Id = {ordenador.Id}";

            command.Parameters.AddWithValue("@Descripcion", ordenador.Descripcion);
            command.Parameters.AddWithValue("@PedidoId", ordenador.PedidoId is null ? DBNull.Value : ordenador.PedidoId);

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
            command.CommandText = $"DELETE FROM Ordenadores WHERE Id = {id}";

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        return Task.CompletedTask;
    }

}
