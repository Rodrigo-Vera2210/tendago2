using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class OutputsReportDto
    {
        public string IdSalida { get; set; }
        public string IdVendedor { get; set; }
        public string Periodo { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTransaccion { get; set; }
        public bool Facturar { get; set; }
        public string RucVenta { get; set; }
        public decimal Total { get; set; }
        public decimal ValorCouta { get; set; }
        public decimal Cobro { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaHoraEntregaPropuesta { get; set; }
        public DateTime? FechaHoraEntregaReal { get; set; }
        public int Horas { get; set; }
        public string EstadoActual { get; set; }
        public string ObservacionesOrden { get; set; }
        public int IdCliente { get; set; }
        public string IdentificacionCliente { get; set; }
        public string RazonSocialCliente { get; set; }
        public string IdDetalleSalidad { get; set; }
        public int IdTipoUnidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoVenta { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubtotalCosto { get; set; }
        public decimal Venta { get; set; }
        public decimal MargenBruto { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string Lote { get; set; }
        public int IdProveedor { get; set; }
        public string RazonSocialProvedor { get; set; }
        public string IdentificacionProveedor { get; set; }
        public int IdLocal { get; set; }
        public string TipoLocal { get; set; }
        public string NombreLocal { get; set; }
        public int IdProducto { get; set; }
        public string CodigoProveedor { get; set; }
        public string CodigoInterno { get; set; }
        public string TipoProducto { get; set; }
        public string Producto { get; set; }
        public int IdProductoPadre { get; set; }
        public string ProductoPadre { get; set; }
        public int IdDivision { get; set; }
        public string Division { get; set; }
        public int Idlinea { get; set; }
        public string Linea { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdMarca { get; set; }
        public string Marca { get; set; }
        public int Plazo { get; set; }
    }
}