using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SqlClient;
using WebApi_Componentes.Models;

namespace WebApi_Componentes.Services;

public class AdoComponenteRepository : IComponenteRepository
{
    private readonly string _connectionString;

    public AdoComponenteRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<List<Componente>> AllAsync()
    {
        var componentes = new List<Componente>();

        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Componentes";

            connection.Open();

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

                DatabaseQueryUtils.PopulateOrdenadorComponente(componente, connection);

                componentes.Add(componente);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        return Task.FromResult(componentes);
    }

    public Task<Componente?> GetByIdAsync(int id)
    {
        var componente = new Componente();

        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM Componentes WHERE Id = {id}";

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                componente.Id = reader.GetInt32("Id");
                componente.NumeroDeSerie = reader.GetString("NumeroDeSerie");
                componente.Descripcion = reader.GetString("Descripcion");
                componente.Calor = reader.GetInt32("Calor");
                componente.Megas = reader.GetInt64("Megas");
                componente.Cores = reader.GetInt32("Cores");
                componente.Coste = reader.GetInt32("Coste");
                componente.Tipo = (TipoComponente)reader.GetInt32("Tipo");
                componente.OrdenadorId = reader.IsDBNull("OrdenadorId") ? null : reader.GetInt32("OrdenadorId");

                DatabaseQueryUtils.PopulateOrdenadorComponente(componente, connection);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        return Task.FromResult(componente);
    }

    public Task AddAsync(Componente componente)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Componentes (Id, NumeroDeSerie, Descripcion, Calor, Megas, Cores, Coste, Tipo, OrdenadorId) " +
                                  "VALUES (@Id, @NumeroDeSerie, @Descripcion, @Calor, @Megas, @Cores, @Coste, @Tipo, @OrdenadorId)";

            command.Parameters.AddWithValue("@Id", componente.Id);
            command.Parameters.AddWithValue("@NumeroDeSerie", componente.NumeroDeSerie);
            command.Parameters.AddWithValue("@Descripcion", componente.Descripcion);
            command.Parameters.AddWithValue("@Calor", componente.Calor);
            command.Parameters.AddWithValue("@Megas", componente.Megas);
            command.Parameters.AddWithValue("@Cores", componente.Cores);
            command.Parameters.AddWithValue("@Coste", componente.Coste);
            command.Parameters.AddWithValue("@Tipo", componente.Tipo);
            command.Parameters.AddWithValue("@OrdenadorId", componente.OrdenadorId is null ? DBNull.Value : componente.OrdenadorId);

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Componente componente)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Componentes " +
                                  "SET " +
                                  "NumeroDeSerie = @NumeroDeSerie, " +
                                  "Descripcion = @Descripcion, " +
                                  "Calor = @Calor, " +
                                  "Megas = @Megas, " +
                                  "Cores = @Cores, " +
                                  "Coste = @Coste, " +
                                  "Tipo = @Tipo, " +
                                  "OrdenadorId = @OrdenadorId " +
                                  $"WHERE Id = {componente.Id}";

            command.Parameters.AddWithValue("@NumeroDeSerie", componente.NumeroDeSerie);
            command.Parameters.AddWithValue("@Descripcion", componente.Descripcion);
            command.Parameters.AddWithValue("@Calor", componente.Calor);
            command.Parameters.AddWithValue("@Megas", componente.Megas);
            command.Parameters.AddWithValue("@Cores", componente.Cores);
            command.Parameters.AddWithValue("@Coste", componente.Coste);
            command.Parameters.AddWithValue("@Tipo", componente.Tipo);
            command.Parameters.AddWithValue("@OrdenadorId", componente.OrdenadorId is null ? DBNull.Value : componente.OrdenadorId);

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
            command.CommandText = $"DELETE FROM Componentes WHERE Id = {id}";

            connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        return Task.CompletedTask;
    }

}
