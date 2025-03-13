using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class RucDto : RucDtoLite
    {
        //public string Ruc { get; set; }
        public string TokenFactElectonica { get; set; }
        public int IdEmpresa { get; set; }
        public decimal LimiteFacturacion { get; set; }
        public decimal? TotalFacturado { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        //public short IdEstado { get; set; }

        public string ActividadEconomica { get; set; }

    }

    public class RucDtoLite
    {
        public string Ruc { get; set; }
        
        public string RazonSocial { get; set; }
        
        public int? LocalPorDefecto { get; set; }

        public short IdEstado { get; set; }
        public bool Rise { get; set; }

        public string Descripcion
        {
            get { return $"{Ruc:D13} - {RazonSocial}"; }
        }

        public override string ToString()
        {
            return Ruc;
        }
    }
}