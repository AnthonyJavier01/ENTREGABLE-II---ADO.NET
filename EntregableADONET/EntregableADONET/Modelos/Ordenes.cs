using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Modelos
{
    public class Ordenes
    {
        public int OrdenId { get; set; }

        public int ClienteId { get; set; }

        public DateTime FechaOrden { get; set; }

        public decimal Total { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
