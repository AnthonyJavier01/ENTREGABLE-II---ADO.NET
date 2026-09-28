using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Modelos
{
    public class Categorias
    {
        public int CategoriaId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
