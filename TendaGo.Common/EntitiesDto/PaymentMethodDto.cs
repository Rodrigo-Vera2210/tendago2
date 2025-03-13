using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class PaymentMethodDto
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string MedioPago { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
    }
}