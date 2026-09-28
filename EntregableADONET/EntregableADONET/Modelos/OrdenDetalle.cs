using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Modelos
{
    public class OrdenDetalle
    {
        public int DetalleId { get; set; }

        public int OrdenId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal SubTotal { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
