using EntregableADONET.Data.Repositorios;
using EntregableADONET.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Servicios
{
    public class ProductosService
    {
        private readonly ProductosRepository _repository;

        public ProductosService()
        {
            var factory = new SqlConnectionFactory();

            _repository = new ProductosRepository(factory);
        }

        public void ListarProductos()
        {
            try
            {
                var productos = _repository.GetAll();

                Console.Clear();

                Console.WriteLine("==================================================================================");
                Console.WriteLine("LISTADO DE PRODUCTOS");
                Console.WriteLine("==================================================================================");

                Console.WriteLine(
                $"{"ID",-5} {"NOMBRE",-25} {"PRECIO",-12} {"STOCK",-10} {"CAT.ID",-10}");

                Console.WriteLine(new string('-', 85));

                foreach (var producto in productos)
                {
                    Console.WriteLine(
                    $"{producto.ProductoId,-5}" +
                    $"{producto.Nombre,-25}" +
                    $"{producto.Precio,-12:C}" +
                    $"{producto.Stock,-10}" +
                    $"{producto.CategoriaId,-10}");
                }

                Console.WriteLine();
                Console.WriteLine($"Total de productos: {productos.Count}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void AgregarProducto()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("===== NUEVO PRODUCTO =====");

                Productos producto = new Productos();

                Console.Write("Nombre: ");
                producto.Nombre = Console.ReadLine();

                Console.Write("Precio: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
                {
                    Console.WriteLine("Precio inválido.");
                    Console.ReadKey();
                    return;
                }
                if (precio <= 0)
                {
                    Console.WriteLine("El precio debe ser mayor a cero.");
                    Console.ReadKey();
                    return;
                }

                producto.Precio = precio;

                Console.Write("Stock: ");

                if (!int.TryParse(Console.ReadLine(), out int stock))
                {
                    Console.WriteLine("Stock inválido.");
                    Console.ReadKey();
                    return;
                }

                if (stock < 0)
                {
                    Console.WriteLine("El stock no puede ser negativo.");
                    Console.ReadKey();
                    return;
                }

                producto.Stock = stock;

                Console.Write("Categoria Id: ");

                if (!int.TryParse(Console.ReadLine(), out int categoriaId))
                {
                    Console.WriteLine("Categoría inválida.");
                    Console.ReadKey();
                    return;
                }

                producto.CategoriaId = categoriaId;

                _repository.Insert(producto);

                Console.WriteLine();
                Console.WriteLine("Producto registrado correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void ActualizarProducto()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese ID del producto: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var producto = _repository.GetById(id);

                if (producto == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Nombre actual: {producto.Nombre}");
                Console.Write("Nuevo nombre: ");
                producto.Nombre = Console.ReadLine();

                Console.WriteLine($"Precio actual: {producto.Precio}");
                Console.Write("Nuevo precio: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
                {
                    Console.WriteLine("Precio inválido.");
                    Console.ReadKey();
                    return;
                }
                if (precio <= 0)
                {
                    Console.WriteLine("El precio debe ser mayor a cero.");
                    Console.ReadKey();
                    return;
                }

                producto.Precio = precio;

                Console.WriteLine($"Stock actual: {producto.Stock}");
                Console.Write("Nuevo stock: ");

                if (!int.TryParse(Console.ReadLine(), out int stock))
                {
                    Console.WriteLine("Stock inválido.");
                    Console.ReadKey();
                    return;
                }

                if (stock < 0)
                {
                    Console.WriteLine("El stock no puede ser negativo.");
                    Console.ReadKey();
                    return;
                }

                producto.Stock = stock;

                Console.WriteLine($"Categoria actual: {producto.CategoriaId}");
                Console.Write("Nueva categoria: ");

                if (!int.TryParse(Console.ReadLine(), out int categoriaId))
                {
                    Console.WriteLine("Categoría inválida.");
                    Console.ReadKey();
                    return;
                }

                producto.CategoriaId = categoriaId;

                _repository.Update(producto);

                Console.WriteLine();
                Console.WriteLine("Producto actualizado correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void EliminarProducto()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese ID del producto: ");

                int id = Convert.ToInt32(Console.ReadLine());

                var producto = _repository.GetById(id);

                if (producto == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Producto: {producto.Nombre}");

                Console.Write("¿Desea eliminarlo? (S/N): ");

                string respuesta = Console.ReadLine()?.ToUpper();

                if (respuesta == "S")
                {
                    _repository.Delete(id);

                    Console.WriteLine();
                    Console.WriteLine("Producto eliminado correctamente.");
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
