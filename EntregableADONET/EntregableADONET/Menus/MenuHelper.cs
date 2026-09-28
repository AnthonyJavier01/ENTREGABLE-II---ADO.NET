using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Menus
{
    public static class MenuHelper
    {
        public static int MostrarSubMenu(string titulo)
        {
            Console.Clear();

            Console.WriteLine($"===== {titulo} =====");

            Console.WriteLine("1. Categorias");
            Console.WriteLine("2. Productos");
            Console.WriteLine("3. Clientes");
            Console.WriteLine("4. Ordenes");
            Console.WriteLine("5. Detalle Orden");
            Console.WriteLine("0. Volver");

            Console.Write("Seleccione una opción: ");

            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
