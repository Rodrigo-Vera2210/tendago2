using System;
using System.Collections.Generic;

namespace TendaGo.Common 
{
    public class DocumentDto
    {
		public string Id{ get; set; }
		public string IdTipoDocumento { get; set; }
		public DateTime Fecha { get; set; }
		public int IdEmpresa{ get; set; }
		public string RUC { get; set; }
		public string IdSalida{ get; set; }
		public int IdEntidad{ get; set; }
		public short IdMoneda{ get; set; }
		public string Notas{ get; set; }
		public string GuiaRemision{ get; set; }
		public string Establecimiento { get; set; }
		public string PuntoEmision { get; set; }
		public string NumeroDocumento{ get; set; }
		public decimal Base0{ get; set; }
		public decimal BaseIva{ get; set; }
		public decimal TotalDescuento{ get; set; }
		public decimal TotalSinImpuesto{ get; set; }
		public decimal TotalIce{ get; set; }
		public decimal TotalIva{ get; set; }
		public decimal Total{ get; set; }
		public string FormaPago{ get; set; }
        public int IdFormaPagoSri { get; set; }
        public int Plazo{ get; set; }
		public string UnidadTiempo{ get; set; }
		public bool ConsumidorFinal { get; set; }

		public string IpIngreso { get; set; }
		public string UsuarioIngreso { get; set; }
		public DateTime FechaIngreso { get; set; }

		public string IpModificacion { get; set; }
		public string UsuarioModificacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		
		public short IdEstado { get; set; }
		public List<DocumentDetailDto> Detalles { get; set; }

	}

    public class DocumentReportDto
    {
        public int IdEmpresa { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string RUC { get; set; }
        public string IdProveedor { get; set; }
    }
}