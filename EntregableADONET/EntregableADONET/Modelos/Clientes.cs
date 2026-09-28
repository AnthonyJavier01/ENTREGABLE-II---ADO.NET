using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Modelos
{
    public class Clientes
    {
        public int ClienteId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string DNI { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
