using EntregableADONET.Data.Repositorios;
using EntregableADONET.Data.Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Menus
{
    public class MainMenu
    {
        private readonly CategoriasService _categoriasService;
        private readonly ProductosService _productosService;
        private readonly ClientesService _clientesService;
        private readonly OrdenesService _ordenesService;
        private readonly OrdenDetalleService _ordenDetalleService;

        public MainMenu()
        {
            _categoriasService = new CategoriasService();
            _productosService = new ProductosService();
            _clientesService = new ClientesService();
            _ordenesService = new OrdenesService();
            _ordenDetalleService = new OrdenDetalleService();
        }
        public void Mostrar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();

                Console.WriteLine("===== ENTREGABLE ADO.NET - VENTAS =====");

                Console.WriteLine("1. Listar");

                Console.WriteLine("2. Agregar");

                Console.WriteLine("3. Actualizar");

                Console.WriteLine("4. Eliminar");

                Console.WriteLine("0. Salir");

                int opcion =
                Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        MostrarMenuVer();
                        break;

                    case 2:
                        MostrarMenuAgregar();
                        break;

                    case 3:
                        MostrarMenuActualizar();
                        break;

                    case 4:
                        MostrarMenuEliminar();
                        break;

                    case 0:
                        salir = true;
                        break;
                }
            }
        }

        private void MostrarMenuVer()
        {

            bool volver = false;

            while (!volver)
            {
                int opcion = MenuHelper.MostrarSubMenu("MENU LISTAR");

                switch (opcion)
                {
                    case 1:
                        _categoriasService.ListarCategorias();
                        break;

                    case 2:
                        _productosService.ListarProductos();
                        break;

                    case 3:
                        _clientesService.ListarClientes();
                        break;

                    case 4:
                        _ordenesService.ListarOrdenes();
                        break;

                    case 5:
                        _ordenDetalleService.ListarDetalleOrden();
                        break;

                    case 0:
                        volver = true;
                        break;
                }
            }

        }

        private void MostrarMenuAgregar()
        {
            bool volver = false;

            while (!volver)
            {
                int opcion = MenuHelper.MostrarSubMenu("MENU AGREGAR");

                switch (opcion)
                {
                    case 1:
                        _categoriasService.AgregarCategoria();
                        break;

                    case 2:
                        _productosService.AgregarProducto();
                        break;

                    case 3:
                        _clientesService.AgregarCliente();
                        break;

                    case 4:
                        _ordenesService.AgregarOrden();
                        break;

                    case 5:
                        _ordenDetalleService.AgregarDetalleOrden();
                        break;

                    case 0:
                        volver = true;
                        break;
                }
            }
        }

           private void MostrarMenuActualizar(){
            bool volver = false;

            while (!volver)
            {
                int opcion = MenuHelper.MostrarSubMenu("MENU ACTUALIZAR");

                switch (opcion)
                {
                    case 1:
                        _categoriasService.ActualizarCategoria();
                        break;

                    case 2:
                        _productosService.ActualizarProducto();
                        break;

                    case 3:
                        _clientesService.ActualizarCliente();
                        break;

                    case 4:
                        _ordenesService.ActualizarOrden();
                        break;

                    case 5:
                        _ordenDetalleService.ActualizarDetalleOrden();
                        break;

                    case 0:
                        volver = true;
                        break;
                }
            }
        }
        

        private void MostrarMenuEliminar()
        {
            bool volver = false;

            while (!volver)
            {
                int opcion = MenuHelper.MostrarSubMenu("MENU ELIMINAR");

                switch (opcion)
                {
                    case 1:
                        _categoriasService.EliminarCategoria();
                        break;

                    case 2:
                        _productosService.EliminarProducto();
                        break;

                    case 3:
                        _clientesService.EliminarCliente();
                        break;

                    case 4:
                        _ordenesService.EliminarOrden();
                        break;

                    case 5:
                        _ordenDetalleService.EliminarDetalleOrden();
                        break;

                    case 0:
                        volver = true;
                        break;
                }
            }
        }



    }
}
