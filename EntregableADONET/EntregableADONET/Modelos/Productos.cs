using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Modelos
{
    public class Productos
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public int CategoriaId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
