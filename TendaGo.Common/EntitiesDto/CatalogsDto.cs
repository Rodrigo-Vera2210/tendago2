using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{

    public class CurrencyDto
    {
        public short Id { get; set; }
        public string CodigoISO { get; set; }
        public short Pais { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public string Moneda { get; set; }
        public string Descripcion => $"{CodigoISO} - {Moneda}";
    }
}