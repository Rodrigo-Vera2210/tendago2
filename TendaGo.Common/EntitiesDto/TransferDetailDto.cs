using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class TransferDetailDto
    {
        public int IdProducto { get; set; }

        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int IdTipoUnidad { get; set; }
        
        public int IdLocal { get; set; }

        public short Estado { get; set; }

        public decimal CantidadIn0 { get; set; }
    }
}