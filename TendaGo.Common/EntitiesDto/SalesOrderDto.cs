using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{ 
    public abstract class SalesOrderDto
    {
        public string Id { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public string Ip { get; set; }
        public string Usuario { get; set; }
        public decimal ValorAdicional { get; set; }
        public bool GeneraNc { get; set; }
    }

    public class SalesOrderApprovalDto : SalesOrderDto
    { 
        public int IdCliente { get; set; } 
        public bool Facturar { get; set; } 
        public decimal Subtotal0 { get; set; } 
        public decimal SubtotalIva { get; set; }  
        public int IdFormaPago { get; set; }
        public int Cuotas { get; set; }
        public int Plazo { get; set; }
        public string Ruc { get; set; }
        public bool EntregaInmediata { get; set; }
        public string Entrega { get; set; }
        public List<SalesOrderDetailDto> Detalles { get; set; } 
        public List<ReceivableDto> CuentasPorCobrar { get; set; }
        
    }

    public class SalesOrderPackingDto : SalesOrderDto
    { 
        public List<PackingDto> DetalleEmpaquetado { get; set; }
    }

    public class SalesOrderReviewDto : SalesOrderPackingDto
    {
        public int IdFormaPago { get; set; }
        public int Cuotas { get; set; }
        public int Plazo { get; set; }
        public List<SalesOrderDetailDto> Detalles { get; set; }
        public List<ReceivableDto> CuentasPorCobrar { get; set; }
    }

    public class SalesOrderDeliverDto : SalesOrderApprovalDto
    { 

    }

    public class SalesOrderInvoiceDto : SalesOrderDto
    {
        public DocumentDto Factura { get; set; }
        public short Porcentaje { get; set; }
        public string Ruc { get; set; }
        public int IdCliente { get; set; }
        public bool ConsumidorFinal { get; set; }

    }

    public class SalesOrderDetailDto
    {
        public string CodigoInterno { get; set; }
        public string DescripcionProducto { get; set; }
        public string Id { get; set; }
        public string Periodo { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTransaccion { get; set; }
        public string IdSalida { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdLocal { get; set; }
        public decimal Cantidad { get; set; }
        public int IdTipoUnidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }
        public short IdEstado { get; set; }
        public bool Revisado { get; set; }
        public bool Empaquetado { get; set; }
        public bool Entregado { get; set; }
    }

    public class SalesProductDto
    {
        public int IdProducto { get; set; }

        public string CodigoInterno { get; set; }

        public string DescripcionProducto { get; set; }

        public decimal CantidadMinima { get; set; }

        public decimal Cantidad { get; set; }

        public decimal Stock { get; set; }

        public int? IdLocal { get; set; }
        public int IdTipoUnidad { get; set; }
        public string TipoUnidad { get; set; }

        public List<LiteStockDto> StockLocales { get; set; }

        public List<UnitTypeDto> Unidades { get; set; }
    }

}