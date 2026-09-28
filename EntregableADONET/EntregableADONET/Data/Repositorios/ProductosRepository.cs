using EntregableADONET.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Repositorios
{
    public class ProductosRepository : IRepository<Productos>
    {
        private readonly SqlConnectionFactory _factory;

        public ProductosRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Productos> GetAll()
        {
            try
            {
                var lista = new List<Productos>();

                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT
                ProductoId,
                Nombre,
                Precio,
                Stock,
                CategoriaId
                FROM Productos
                WHERE Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Productos
                    {
                        ProductoId = Convert.ToInt32(reader["ProductoId"]),
                        Nombre = reader["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        CategoriaId = Convert.ToInt32(reader["CategoriaId"])
                    });
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar productos: {ex.Message}", ex);
            }
        }

        public Productos GetById(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT
                ProductoId,
                Nombre,
                Precio,
                Stock,
                CategoriaId
                FROM Productos
                WHERE ProductoId = @ProductoId
                AND Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ProductoId", id);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Productos
                    {
                        ProductoId = Convert.ToInt32(reader["ProductoId"]),
                        Nombre = reader["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        CategoriaId = Convert.ToInt32(reader["CategoriaId"])
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al buscar producto: {ex.Message}", ex);
            }
        }

        public void Insert(Productos producto)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                INSERT INTO Productos
                (
                Nombre,
                Precio,
                Stock,
                CategoriaId
                )
                VALUES
                (
                @Nombre,
                @Precio,
                @Stock,
                @CategoriaId
                )";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@CategoriaId", producto.CategoriaId);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al registrar producto: {ex.Message}", ex);
            }
        }

        public void Update(Productos producto)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Productos
                SET Nombre = @Nombre,
                Precio = @Precio,
                Stock = @Stock,
                CategoriaId = @CategoriaId,
                Updated = GETDATE()
                WHERE ProductoId = @ProductoId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ProductoId", producto.ProductoId);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@CategoriaId", producto.CategoriaId);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al actualizar producto: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Productos
                SET Deleted = GETDATE(),
                Updated = GETDATE()
                WHERE ProductoId = @ProductoId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ProductoId", id);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al eliminar producto: {ex.Message}", ex);
            }
        }
    }
}
