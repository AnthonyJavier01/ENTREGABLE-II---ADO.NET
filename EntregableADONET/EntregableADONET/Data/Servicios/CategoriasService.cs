using EntregableADONET.Data.Repositorios;
using EntregableADONET.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Servicios
{
    public class CategoriasService
    {
        private readonly CategoriasRepository _repository;

        public CategoriasService()
        {
            var factory = new SqlConnectionFactory();
            _repository = new CategoriasRepository(factory);
        }

        public void ListarCategorias()
        {
            try
            {
                var categorias = _repository.GetAll();

                Console.Clear();

                Console.WriteLine("==============================================================");
                Console.WriteLine(" LISTADO DE CATEGORIAS");
                Console.WriteLine("==============================================================");
                Console.WriteLine();

                Console.WriteLine(
                $"{"ID",-5} {"NOMBRE",-25} {"DESCRIPCION",-30}");

                Console.WriteLine(new string('-', 65));

                foreach (var categoria in categorias)
                {
                    Console.WriteLine(
                    $"{categoria.CategoriaId,-5} " +
                    $"{categoria.Nombre,-25} " +
                    $"{categoria.Descripcion,-30}");
                }

                Console.WriteLine();
                Console.WriteLine("==============================================================");
                Console.WriteLine($"Total de categorías: {categorias.Count}");
                Console.WriteLine("==============================================================");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void AgregarCategoria()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("========== NUEVA CATEGORIA ==========");
                Console.WriteLine();

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Descripción: ");
                string descripcion = Console.ReadLine();

                var categoria = new Categorias
                {
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                _repository.Insert(categoria);

                Console.WriteLine();
                Console.WriteLine("Categoría registrada correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void ActualizarCategoria()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("========== ACTUALIZAR CATEGORIA ==========");
                Console.WriteLine();

                Console.Write("Ingrese ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var categoria = _repository.GetById(id);

                if (categoria == null)
                {
                    Console.WriteLine("Categoría no encontrada.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Nombre actual: {categoria.Nombre}");

                Console.Write("Nuevo nombre: ");
                categoria.Nombre = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine($"Descripción actual: {categoria.Descripcion}");

                Console.Write("Nueva descripción: ");
                categoria.Descripcion = Console.ReadLine();

                _repository.Update(categoria);

                Console.WriteLine();
                Console.WriteLine("Categoría actualizada correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void EliminarCategoria()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("========== ELIMINAR CATEGORIA ==========");
                Console.WriteLine();

                Console.Write("Ingrese ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var categoria = _repository.GetById(id);

                if (categoria == null)
                {
                    Console.WriteLine("Categoría no encontrada.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Categoría: {categoria.Nombre}");
                Console.WriteLine();

                Console.Write("¿Desea eliminarla? (S/N): ");

                string respuesta = Console.ReadLine()?.ToUpper();

                if (respuesta == "S")
                {
                    _repository.Delete(id);

                    Console.WriteLine();
                    Console.WriteLine("Categoría eliminada correctamente.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Operación cancelada.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void BuscarCategoriaPorId()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var categoria = _repository.GetById(id);

                Console.WriteLine();

                if (categoria == null)
                {
                    Console.WriteLine("Categoría no encontrada.");
                }
                else
                {
                    Console.WriteLine($"ID: {categoria.CategoriaId}");
                    Console.WriteLine($"Nombre: {categoria.Nombre}");
                    Console.WriteLine($"Descripción: {categoria.Descripcion}");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }




    }
}
