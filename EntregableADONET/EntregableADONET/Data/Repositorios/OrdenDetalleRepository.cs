using EntregableADONET.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Repositorios
{
    public class OrdenDetalleRepository : IRepository<OrdenDetalle>
    {
        private readonly SqlConnectionFactory _factory;

        public OrdenDetalleRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<OrdenDetalle> GetAll()
        {
            try
            {
                var lista = new List<OrdenDetalle>();

                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT
                DetalleId,
                OrdenId,
                ProductoId,
                Cantidad,
                PrecioUnitario,
                SubTotal
                FROM OrdenDetalle
                WHERE Deleted IS NULL
                ORDER BY DetalleId";

                using var cmd = new SqlCommand(sql, cn);

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new OrdenDetalle
                    {
                        DetalleId = Convert.ToInt32(reader["DetalleId"]),
                        OrdenId = Convert.ToInt32(reader["OrdenId"]),
                        ProductoId = Convert.ToInt32(reader["ProductoId"]),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"])
                    });
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception(
                $"Error al listar detalles de orden: {ex.Message}", ex);
            }
        }

        public OrdenDetalle GetById(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT
                DetalleId,
                OrdenId,
                ProductoId,
                Cantidad,
                PrecioUnitario,
                SubTotal
                FROM OrdenDetalle
                WHERE DetalleId = @DetalleId
                AND Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@DetalleId", id);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new OrdenDetalle
                    {
                        DetalleId = Convert.ToInt32(reader["DetalleId"]),
                        OrdenId = Convert.ToInt32(reader["OrdenId"]),
                        ProductoId = Convert.ToInt32(reader["ProductoId"]),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"])
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception(
                $"Error al buscar detalle: {ex.Message}", ex);
            }
        }

        public void Insert(OrdenDetalle detalle)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                INSERT INTO OrdenDetalle
                (
                OrdenId,
                ProductoId,
                Cantidad,
                PrecioUnitario,
                SubTotal
                )
                VALUES
                (
                @OrdenId,
                @ProductoId,
                @Cantidad,
                @PrecioUnitario,
                @SubTotal
                )";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@OrdenId", detalle.OrdenId);
                cmd.Parameters.AddWithValue("@ProductoId", detalle.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@SubTotal", detalle.SubTotal);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(
                $"Error al registrar detalle: {ex.Message}", ex);
            }
        }

        public void Update(OrdenDetalle detalle)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE OrdenDetalle
                SET OrdenId = @OrdenId,
                ProductoId = @ProductoId,
                Cantidad = @Cantidad,
                PrecioUnitario = @PrecioUnitario,
                SubTotal = @SubTotal,
                Updated = GETDATE()
                WHERE DetalleId = @DetalleId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@DetalleId", detalle.DetalleId);
                cmd.Parameters.AddWithValue("@OrdenId", detalle.OrdenId);
                cmd.Parameters.AddWithValue("@ProductoId", detalle.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@SubTotal", detalle.SubTotal);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(
                $"Error al actualizar detalle: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE OrdenDetalle
                SET Deleted = GETDATE(),
                Updated = GETDATE()
                WHERE DetalleId = @DetalleId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@DetalleId", id);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(
                $"Error al eliminar detalle: {ex.Message}", ex);
            }
        }

        public void DeleteByOrdenId(int ordenId)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE OrdenDetalle
                SET Deleted = GETDATE(),
                Updated = GETDATE()
                WHERE OrdenId = @OrdenId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@OrdenId", ordenId);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(
                $"Error al eliminar detalles de la orden: {ex.Message}",
                ex);
            }
        }


    }
}
