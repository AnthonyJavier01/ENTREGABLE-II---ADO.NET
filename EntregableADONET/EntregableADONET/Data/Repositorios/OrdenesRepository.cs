using EntregableADONET.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Repositorios
{
    public class OrdenesRepository : IRepository<Ordenes>
    {
        private readonly SqlConnectionFactory _factory;

        public OrdenesRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Ordenes> GetAll()
        {
            try
            {
                var lista = new List<Ordenes>();

                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT
                OrdenId,
                ClienteId,
                FechaOrden,
                Total
                FROM Ordenes
                WHERE Deleted IS NULL
                ORDER BY OrdenId";

                using var cmd = new SqlCommand(sql, cn);

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Ordenes
                    {
                        OrdenId = Convert.ToInt32(reader["OrdenId"]),
                        ClienteId = Convert.ToInt32(reader["ClienteId"]),
                        FechaOrden = Convert.ToDateTime(reader["FechaOrden"]),
                        Total = Convert.ToDecimal(reader["Total"])
                    });
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar órdenes: {ex.Message}", ex);
            }
        }

        public Ordenes GetById(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT
                OrdenId,
                ClienteId,
                FechaOrden,
                Total
                FROM Ordenes
                WHERE OrdenId = @OrdenId
                AND Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@OrdenId", id);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Ordenes
                    {
                        OrdenId = Convert.ToInt32(reader["OrdenId"]),
                        ClienteId = Convert.ToInt32(reader["ClienteId"]),
                        FechaOrden = Convert.ToDateTime(reader["FechaOrden"]),
                        Total = Convert.ToDecimal(reader["Total"])
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al buscar orden: {ex.Message}", ex);
            }
        }

        public void Insert(Ordenes orden)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                INSERT INTO Ordenes
                (
                ClienteId,
                Total
                )
                VALUES
                (
                @ClienteId,
                @Total
                )";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ClienteId", orden.ClienteId);
                cmd.Parameters.AddWithValue("@Total", orden.Total);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al registrar orden: {ex.Message}", ex);
            }
        }

        public void Update(Ordenes orden)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Ordenes
                SET ClienteId = @ClienteId,
                Total = @Total,
                Updated = GETDATE()
                WHERE OrdenId = @OrdenId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@OrdenId", orden.OrdenId);
                cmd.Parameters.AddWithValue("@ClienteId", orden.ClienteId);
                cmd.Parameters.AddWithValue("@Total", orden.Total);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al actualizar orden: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Ordenes
                SET Deleted = GETDATE(),
                Updated = GETDATE()
                WHERE OrdenId = @OrdenId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@OrdenId", id);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al eliminar orden: {ex.Message}", ex);
            }
        }

    }
}
