using EntregableADONET.Data.Repositorios;
using EntregableADONET.Modelos;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace EntregableADONET.Data.Servicios
{
    public class ClientesService
    {
        private readonly ClientesRepository _repository;

        public ClientesService()
        {
            var factory = new SqlConnectionFactory();

            _repository = new ClientesRepository(factory);
        }

        private bool EmailValido(string email)
        {
            try
            {
                var mail = new MailAddress(email);

                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void ListarClientes()
        {
            try
            {
                var clientes = _repository.GetAll();

                Console.Clear();

                Console.WriteLine("========================================================================================================");
                Console.WriteLine("LISTADO DE CLIENTES");
                Console.WriteLine("========================================================================================================");

                Console.WriteLine(
                $"{"ID",-5} {"NOMBRE",-15} {"APELLIDO",-15} {"DNI",-10} {"EMAIL",-30} {"TELEFONO",-15}");

                Console.WriteLine(new string('-', 110));

                foreach (var cliente in clientes)
                {
                    Console.WriteLine(
                    $"{cliente.ClienteId,-5}" +
                    $"{cliente.Nombre,-15}" +
                    $"{cliente.Apellido,-15}" +
                    $"{cliente.DNI,-10}" +
                    $"{cliente.Email,-30}" +
                    $"{cliente.Telefono,-15}");
                }

                Console.WriteLine();
                Console.WriteLine($"Total de clientes: {clientes.Count}");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void AgregarCliente()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("NUEVO CLIENTE");
                Console.WriteLine("======================================");
                Console.WriteLine();

                Clientes cliente = new Clientes();

                Console.Write("Nombre: ");
                cliente.Nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                cliente.Apellido = Console.ReadLine();

                Console.Write("DNI (8 dígitos): ");
                cliente.DNI = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(cliente.DNI) ||
                cliente.DNI.Length != 8 ||
                !cliente.DNI.All(char.IsDigit))
                {
                    Console.WriteLine("El DNI debe contener exactamente 8 dígitos.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Email: ");
                cliente.Email = Console.ReadLine();

                if (!EmailValido(cliente.Email))
                {
                    Console.WriteLine("El email ingresado no es válido.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Teléfono: ");
                cliente.Telefono = Console.ReadLine();

                _repository.Insert(cliente);

                Console.WriteLine();
                Console.WriteLine("Cliente registrado correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void ActualizarCliente()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("ACTUALIZAR CLIENTE");
                Console.WriteLine("======================================");
                Console.WriteLine();

                Console.Write("Ingrese ID del cliente: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var cliente = _repository.GetById(id);

                if (cliente == null)
                {
                    Console.WriteLine("Cliente no encontrado.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Nombre actual: {cliente.Nombre}");
                Console.Write("Nuevo nombre: ");
                cliente.Nombre = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine($"Apellido actual: {cliente.Apellido}");
                Console.Write("Nuevo apellido: ");
                cliente.Apellido = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine($"DNI actual: {cliente.DNI}");
                Console.Write("Nuevo DNI: ");

                string nuevoDni = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nuevoDni) ||
                nuevoDni.Length != 8 ||
                !nuevoDni.All(char.IsDigit))
                {
                    Console.WriteLine("El DNI debe contener exactamente 8 dígitos.");
                    Console.ReadKey();
                    return;
                }

                cliente.DNI = nuevoDni;

                Console.WriteLine();
                Console.WriteLine($"Email actual: {cliente.Email}");
                Console.Write("Nuevo Email: ");

                string nuevoEmail = Console.ReadLine();

                if (!EmailValido(nuevoEmail))
                {
                    Console.WriteLine("El email ingresado no es válido.");
                    Console.ReadKey();
                    return;
                }

                cliente.Email = nuevoEmail;

                Console.WriteLine();
                Console.WriteLine($"Teléfono actual: {cliente.Telefono}");
                Console.Write("Nuevo teléfono: ");
                cliente.Telefono = Console.ReadLine();

                _repository.Update(cliente);

                Console.WriteLine();
                Console.WriteLine("Cliente actualizado correctamente.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void EliminarCliente()
        {
            try
            {
                Console.Clear();

                Console.Write("Ingrese ID del cliente: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    Console.ReadKey();
                    return;
                }

                var cliente = _repository.GetById(id);

                if (cliente == null)
                {
                    Console.WriteLine("Cliente no encontrado.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"{cliente.Nombre} {cliente.Apellido}");

                Console.Write("¿Desea eliminar el cliente? (S/N): ");

                string respuesta = Console.ReadLine()?.ToUpper();

                if (respuesta == "S")
                {
                    _repository.Delete(id);

                    Console.WriteLine("Cliente eliminado correctamente.");
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