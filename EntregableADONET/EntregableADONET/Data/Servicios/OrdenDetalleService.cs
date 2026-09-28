using EntregableADONET.Data.Repositorios;
using EntregableADONET.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Servicios
{
    public class OrdenDetalleService
    {
        private readonly OrdenDetalleRepository _repository;

        public OrdenDetalleService()
        {
            var factory = new SqlConnectionFactory();

            _repository =
            new OrdenDetalleRepository(factory);
        }

        public void ListarDetalleOrden()
        {
            try
            {
                var detalles = _repository.GetAll();

                Console.Clear();

                Console.WriteLine("================================================================================================");
                Console.WriteLine("DETALLE DE ORDENES");
                Console.WriteLine("================================================================================================");

                Console.WriteLine(
                $"{"ID",-5} {"ORDEN",-8} {"PRODUCTO",-10} {"CANTIDAD",-10} {"PRECIO",-12} {"SUBTOTAL",-12}");

                Console.WriteLine(new string('-', 100));

                foreach (var item in detalles)
                {
                    Console.WriteLine(
                    $"{item.DetalleId,-5}" +
                    $"{item.OrdenId,-8}" +
                    $"{item.ProductoId,-10}" +
                    $"{item.Cantidad,-10}" +
                    $"{item.PrecioUnitario,-12:C}" +
                    $"{item.SubTotal,-12:C}");
                }

                Console.WriteLine();
                Console.WriteLine($"Total de registros: {detalles.Count}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public void AgregarDetalleOrden()
        {
            try
            {
                Console.Clear();

                OrdenDetalle detalle = new OrdenDetalle();

                Console.Write("OrdenId: ");

                if (!int.TryParse(Console.ReadLine(), out int ordenId)
                || ordenId <= 0)
                {
                    Console.WriteLine("OrdenId inválido.");
                    Console.ReadKey();
                    return;
                }

                detalle.OrdenId = ordenId;

                Console.Write("ProductoId: ");

                if (!int.TryParse(Console.ReadLine(), out int productoId)
                || productoId <= 0)
                {
                    Console.WriteLine("ProductoId inválido.");
                    Console.ReadKey();
                    return;
                }

                detalle.ProductoId = productoId;

                Console.Write("Cantidad: ");

                if (!int.TryParse(Console.ReadLine(), out int cantidad)
                || cantidad <= 0)
                {
                    Console.WriteLine("La cantidad debe ser mayor a cero.");
                    Console.ReadKey();
                    return;
                }

                detalle.Cantidad = cantidad;

                Console.Write("Precio Unitario: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal precio)
                || precio <= 0)
                {
                    Console.WriteLine("Precio inválido.");
                    Console.ReadKey();
                    return;
                }

                detalle.PrecioUnitario = precio;

                detalle.SubTotal =
                detalle.Cantidad * detalle.PrecioUnitario;

                _repository.Insert(detalle);

                Console.WriteLine();
                Console.WriteLine("Detalle registrado correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public void ActualizarDetalleOrden()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese DetalleId: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Id inválido.");
                    Console.ReadKey();
                    return;
                }

                var detalle = _repository.GetById(id);

                if (detalle == null)
                {
                    Console.WriteLine("Detalle no encontrado.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Cantidad actual: {detalle.Cantidad}");
                Console.Write("Nueva cantidad: ");

                if (!int.TryParse(Console.ReadLine(), out int cantidad)
                || cantidad <= 0)
                {
                    Console.WriteLine("Cantidad inválida.");
                    Console.ReadKey();
                    return;
                }

                detalle.Cantidad = cantidad;

                Console.WriteLine($"Precio actual: {detalle.PrecioUnitario}");
                Console.Write("Nuevo precio: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal precio)
                || precio <= 0)
                {
                    Console.WriteLine("Precio inválido.");
                    Console.ReadKey();
                    return;
                }

                detalle.PrecioUnitario = precio;

                detalle.SubTotal =
                detalle.Cantidad * detalle.PrecioUnitario;

                _repository.Update(detalle);

                Console.WriteLine();
                Console.WriteLine("Detalle actualizado correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public void EliminarDetalleOrden()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese DetalleId: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Id inválido.");
                    Console.ReadKey();
                    return;
                }

                var detalle = _repository.GetById(id);

                if (detalle == null)
                {
                    Console.WriteLine("Detalle no encontrado.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("¿Desea eliminar el detalle? (S/N): ");

                string respuesta =
                Console.ReadLine()?.ToUpper();

                if (respuesta == "S")
                {
                    _repository.Delete(id);

                    Console.WriteLine("Detalle eliminado correctamente.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
