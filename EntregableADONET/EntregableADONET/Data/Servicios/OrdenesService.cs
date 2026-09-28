using EntregableADONET.Data.Repositorios;
using EntregableADONET.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data.Servicios
{
    public class OrdenesService
    {
        private readonly OrdenesRepository _repository;

        public OrdenesService()
        {
            var factory = new SqlConnectionFactory();

            _repository = new OrdenesRepository(factory);
        }

        public void ListarOrdenes()
        {
            try
            {
                var ordenes = _repository.GetAll();

                Console.Clear();

                Console.WriteLine("======================================================================");
                Console.WriteLine("LISTADO DE ORDENES");
                Console.WriteLine("======================================================================");

                Console.WriteLine(
                $"{"ID",-8} {"CLIENTE",-15} {"FECHA",-25} {"TOTAL",-12}");

                Console.WriteLine(new string('-', 70));

                foreach (var orden in ordenes)
                {
                    Console.WriteLine(
                    $"{orden.OrdenId,-8}" +
                    $"{orden.ClienteId,-15}" +
                    $"{orden.FechaOrden,-25}" +
                    $"{orden.Total,-12:C}");
                }

                Console.WriteLine();
                Console.WriteLine($"Total de órdenes: {ordenes.Count}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public void AgregarOrden()
        {
            try
            {
                Console.Clear();

                Ordenes orden = new Ordenes();

                Console.Write("ClienteId: ");

                if (!int.TryParse(Console.ReadLine(), out int clienteId))
                {
                    Console.WriteLine("ClienteId inválido.");
                    Console.ReadKey();
                    return;
                }

                orden.ClienteId = clienteId;

                Console.Write("Total: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal total))
                {
                    Console.WriteLine("Total inválido.");
                    Console.ReadKey();
                    return;
                }

                if (total <= 0)
                {
                    Console.WriteLine("El total debe ser mayor a cero.");
                    Console.ReadKey();
                    return;
                }

                orden.Total = total;

                _repository.Insert(orden);

                Console.WriteLine();
                Console.WriteLine("Orden registrada correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public void ActualizarOrden()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese ID de la orden: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var orden = _repository.GetById(id);

                if (orden == null)
                {
                    Console.WriteLine("Orden no encontrada.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Cliente actual: {orden.ClienteId}");

                Console.Write("Nuevo ClienteId: ");

                if (!int.TryParse(Console.ReadLine(), out int clienteId))
                {
                    Console.WriteLine("ClienteId inválido.");
                    Console.ReadKey();
                    return;
                }

                orden.ClienteId = clienteId;

                Console.WriteLine($"Total actual: {orden.Total}");

                Console.Write("Nuevo Total: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal total))
                {
                    Console.WriteLine("Total inválido.");
                    Console.ReadKey();
                    return;
                }

                if (total <= 0)
                {
                    Console.WriteLine("El total debe ser mayor a cero.");
                    Console.ReadKey();
                    return;
                }

                orden.Total = total;

                _repository.Update(orden);

                Console.WriteLine();
                Console.WriteLine("Orden actualizada correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }


        public void EliminarOrden()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese ID de la orden: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var orden = _repository.GetById(id);

                if (orden == null)
                {
                    Console.WriteLine("Orden no encontrada.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Orden N° : {orden.OrdenId}");
                Console.WriteLine($"Cliente : {orden.ClienteId}");
                Console.WriteLine($"Total : {orden.Total:C}");
                Console.WriteLine();

                Console.Write("¿Desea eliminar la orden y todos sus detalles? (S/N): ");

                string respuesta = Console.ReadLine()?.Trim().ToUpper();

                if (respuesta == "S")
                {
                    var detalleRepository =
                    new OrdenDetalleRepository(
                    new SqlConnectionFactory());

                    detalleRepository.DeleteByOrdenId(id);

                    _repository.Delete(id);

                    Console.WriteLine();
                    Console.WriteLine("Orden y sus detalles eliminados correctamente.");
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

        public void BuscarOrdenPorId()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese ID de la orden: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var orden = _repository.GetById(id);

                Console.WriteLine();

                if (orden == null)
                {
                    Console.WriteLine("Orden no encontrada.");
                }
                else
                {
                    Console.WriteLine($"Orden Id : {orden.OrdenId}");
                    Console.WriteLine($"Cliente Id : {orden.ClienteId}");
                    Console.WriteLine($"Fecha : {orden.FechaOrden}");
                    Console.WriteLine($"Total : {orden.Total:C}");
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
