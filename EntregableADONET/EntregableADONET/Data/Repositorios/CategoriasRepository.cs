using EntregableADONET.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Repositorios
{
    public class CategoriasRepository : IRepository<Categorias>
    {
        private readonly SqlConnectionFactory _factory;

        public CategoriasRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Categorias> GetAll()
        {
            try
            {
                var lista = new List<Categorias>();

                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT CategoriaId,
                Nombre,
                Descripcion
                FROM Categorias
                WHERE Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Categorias
                    {
                        CategoriaId = Convert.ToInt32(reader["CategoriaId"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString()
                    });
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al listar categorías: {ex.Message}", ex);
            }
        }

        public void Insert(Categorias categoria)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                INSERT INTO Categorias
                (
                Nombre,
                Descripcion
                )
                VALUES
                (
                @Nombre,
                @Descripcion
                )";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al registrar categoría: {ex.Message}", ex);
            }
        }

        public void Update(Categorias categoria)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Categorias
                SET Nombre = @Nombre,
                Descripcion = @Descripcion,
                Updated = GETDATE()
                WHERE CategoriaId = @CategoriaId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@CategoriaId", categoria.CategoriaId);
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al actualizar categoría: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                UPDATE Categorias
                SET Deleted = GETDATE(),
                Updated = GETDATE()
                WHERE CategoriaId = @CategoriaId";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@CategoriaId", id);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al eliminar categoría: {ex.Message}", ex);
            }
        }

        public Categorias GetById(int id)
        {
            try
            {
                using var cn = _factory.Create();

                cn.Open();

                string sql = @"
                SELECT CategoriaId,
                Nombre,
                Descripcion
                FROM Categorias
                WHERE CategoriaId = @CategoriaId
                AND Deleted IS NULL";

                using var cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@CategoriaId", id);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Categorias
                    {
                        CategoriaId = Convert.ToInt32(reader["CategoriaId"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString()
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al buscar categoría por Id: {ex.Message}", ex);
            }
        }
    }
}
