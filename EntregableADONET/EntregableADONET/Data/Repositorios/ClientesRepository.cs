using EntregableADONET.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Repositorios
{
    public class ClientesRepository : IRepository<Clientes>
    {
        private readonly SqlConnectionFactory _factory;

        public ClientesRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Clientes> GetAll()
        {
            try
            {
                var lista = new List<Clientes>();

                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT ClienteId,
                Nombre,
                Apellido,
                DNI,
                Email,
                Telefono
                FROM Clientes
                WHERE Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Clientes
                    {
                        ClienteId = Convert.ToInt32(reader["ClienteId"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        DNI = reader["DNI"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    });
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar clientes: {ex.Message}", ex);
            }
        }

        public Clientes GetById(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT ClienteId,
                Nombre,
                Apellido,
                DNI,
                Email,
                Telefono
                FROM Clientes
                WHERE ClienteId = @ClienteId
                AND Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ClienteId", id);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Clientes
                    {
                        ClienteId = Convert.ToInt32(reader["ClienteId"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        DNI = reader["DNI"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al buscar cliente: {ex.Message}", ex);
            }
        }

        public void Insert(Clientes cliente)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                INSERT INTO Clientes
                (
                Nombre,
                Apellido,
                DNI,
                Email,
                Telefono
                )
                VALUES
                (
                @Nombre,
                @Apellido,
                @DNI,
                @Email,
                @Telefono
                )";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al registrar cliente: {ex.Message}", ex);
            }
        }

        public void Update(Clientes cliente)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Clientes
                SET Nombre = @Nombre,
                Apellido = @Apellido,
                DNI = @DNI,
                Email = @Email,
                Telefono = @Telefono,
                Updated = GETDATE()
                WHERE ClienteId = @ClienteId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al actualizar cliente: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Clientes
                SET Deleted = GETDATE(),
                Updated = GETDATE()
                WHERE ClienteId = @ClienteId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ClienteId", id);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al eliminar cliente: {ex.Message}", ex);
            }
        }
    }
}
