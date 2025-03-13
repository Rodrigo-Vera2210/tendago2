using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class PriceDto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdTipoUnidad { get; set; }
        public decimal Precio { get; set; }
        public int Moneda { get; set; }
        public bool IncluyeIva { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }

    }
    
}