using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class OutputDetailDto
    {
        public string Id { get; set; }
        public string Periodo { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTransaccion { get; set; }
        public string IdSalida { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdLocal { get; set; }
        public decimal? CostoUnitarioImportacion { get; set; }
        public decimal Cantidad { get; set; }
        public int IdTipoUnidad { get; set; }
        public decimal CostoVenta { get; set; }
        public decimal Precio { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Subtotal { get; set; } 
        public DateTime? FechaFabricacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string Lote { get; set; }

        public int? IdEmpresa { get; set; }
        public int IdNotaTemp { get; set; }

        public bool Revisado { get; set; }

        public bool Empaquetado { get; set; }

        public bool Rise { get; set; }
        public string CodigoInterno { get; set; }
        public string NombreProducto { get; set; }
        public string DesTipoUnidad { get; set; }
        public string UnidadMedida { get; set; }
        public string FotoDataUrl { get; set; }
        
        public bool CobraIva { get; set; }

        public bool IncluyeIva { get; set; }

        public int IdTarifaImpuesto { get; set; }
        public int PorcentajeTarifaImpuesto { get; set; }

        public decimal IVA { get; set; }
        
        public decimal Total { get; set; }

        //public string IpIngreso { get; set; }
        //public string UsuarioIngreso { get; set; }
        //public DateTime FechaIngreso { get; set; }
        //public string IpModificacion { get; set; }
        //public string UsuarioModificacion { get; set; }
        //public DateTime? FechaModificacion { get; set; }
        //public short IdEstado { get; set; }
        public List<SerieSalidaDto> Serie { get; set; }
    }

     
}