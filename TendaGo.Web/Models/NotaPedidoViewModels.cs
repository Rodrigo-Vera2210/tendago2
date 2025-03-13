using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TendaGo.Web.Anotaciones;
using System.Globalization;
using TendaGo.Common;
using TendaGo.Common.EntitiesDto;

namespace TendaGo.Web.Models
{


    public class CategoriaViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class MarcaViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class ModeloViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class DetalleNotaPedidoViewModel
    {
        public string Id { get; set; }
        public int IdProducto { get; set; }

        [Display(Name = "Código")]
        public string CodigoInterno { get; set; }

        [Display(Name = "Nombre Producto")]
        public string NombreProducto { get; set; }

        [Display(Name = "Descripción")]
        public string DescripcionProducto { get; set; }

        public string FotoDataUrl { get; set; }

        //public string Categoria { get; set; }
        //public string Marca { get; set; }
        //public string Modelo { get; set; }

        [Display(Name = "Stock Total")]
        public decimal CantidadTotalDisponible { get; set; }

        [Display(Name = "Tipo de Unidad")]
        public int IdTipoUnidad { get; set; }
        public string DesTipoUnidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Precio Sugerido")]
        public decimal PrecioSugerido { get; set; }
        
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        public decimal PrecioFinal => decimal.Round(Total / Cantidad, 4);

        [Display(Name = "Cantidad Solicitada")]
        public decimal Cantidad { get; set; }
        public decimal Stock { get; set; }
        private decimal _subtotal => Cantidad * Precio;

        public decimal Subtotal => decimal.Round(_subtotal, 4);

        public decimal Total => (IncluyeIva || CobraIva) ? decimal.Round(Subtotal + IVA, 4) : Subtotal;

        public decimal IVA => (!Rise && CobraIva) ? decimal.Round(_subtotal * (Convert.ToDecimal(PorcentajeTarifaImpuesto) / 100), 4) : 0M;

        public decimal BaseIVA => (CobraIva) ? Subtotal : 0M;

        public decimal BaseCero => (!CobraIva) ? Subtotal : 0M;
        
        [Display(Name = "Mínimo")]
        public decimal CantidadMinima { get; set; }
         
        public decimal CostoVenta { get; set; }
        public int IdTarifaImpuesto { get; set; }
        public int PorcentajeTarifaImpuesto { get; set; }

        public int IdNotaPedidoTemp { get; set; }

        public List<NotaPedidoProductoTempModel> NotasTemp { get; set; }

        public bool Revisado { get; set; }

        public bool Empaquetado { get; set; }

        public bool Entregado { get; set; }

        public bool CobraIva { get; set; }

        public bool IncluyeIva { get; set; }
        public bool Rise { get; set; }
        public List<UnitTypeDto> TipoUnidades { get; set; }
        public decimal Descuento { get; set; }

        public decimal ValorDescuento { get; set; }

    }


    public class PedidoViewModel : NotaPedidoViewModel
    {
        public int Row { get; set; }
        public bool Actual { get; set; }

        public RucDtoLite Ruc { get; set; }

        public bool Rise => Ruc?.Rise ?? false;
        public bool slim { get; set; }
        public decimal Descuento { get; set; }
        public decimal ValorDescuento { get; set; }
    }



    public class NotaPedidoViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente.")]
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreCliente { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public decimal CantidadProductos { get; set; }
        public decimal Total { get; set; }
        public string IdVendedor { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaHoraEntregaPropuesta { get; set; }

        [Required]
        public string FechaEntrega { get; set; }

        [Required]
        public string HoraEntrega { get; set; }

        public bool EntregaInmediata { get; set; }

        public bool Facturar { get; set; }

        public bool ConsumidorFinal { get; set; }

        public string EstadoActual { get; set; }

        public int IdFormaPago { get; set; }
        public int IdFormaPagoSri { get; set; }

        public string TransaccionPadre { get; set; }
        
        public string TipoTransaccionPadre { get; set; }
        
        public string FormaPago
        {
            get
            {

                return (IdFormaPago == 2) ? "CREDITO" : "CONTADO";
            }
        }

        public int IdLocal { get; set; }

        public string LocalSeleccionado { get; set; }

        public List<DetalleNotaPedidoViewModel> DetalleNotaPedido { get; set; }

        public decimal Saldo { get; set; }
        public decimal ValorAdicional { get; set; }
        public bool GeneraNc { get; set; }

    }

    public class AprobacionNotaPedidoViewModel
    {
        public string Id { get; set; }
        public int IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public bool Facturar { get; set; }
        public string EstadoActual { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la forma de pago.")]
        public int IdFormaPago { get; set; }
        public decimal Total { get; set; }
        public short IdEstado { get; set; }
        public string IdVendedor { get; set; }
        public string Observaciones { get; set; }
        public int Plazo { get; set; }
        public int Cuotas { get; set; }
        public bool EntregaInmediata { get; set; }
        public string Entrega { get; set; }

        public CuentasPorCobrarViewModel[] CuentasPorCobrar { get; set; }
        public AditionalInfoDto[] InformacionAdicional { get; set; }

        public DetalleNotaPedidoViewModel[] DetalleNotaPedido { get; set; }

        [Display(Name = "RUC")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string Ruc { get; set; }
        public decimal ValorAdicional { get; set; }
    }

    public class DocumentViewModel
    { 
        public string Id { get; set; }
        public string IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int IdEmpresa { get; set; }
        public string RUC { get; set; }
        public string IdSalida { get; set; }
        public int IdEntidad { get; set; }
        public DateTime Fecha { get; set; } 
        public string Notas { get; set; }
        public string GuiaRemision { get; set; } 
        public decimal Base0 { get; set; }
        public decimal BaseIva { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal TotalSinImpuesto { get; set; }
        public decimal TotalIce { get; set; }
        public decimal TotalIva { get; set; }
        public decimal Total { get; set; }
        public string FormaPago { get; set; }
        public int IdFormaPagoSri { get; set; }
        public int Plazo { get; set; }
        public string UnidadTiempo { get; set; }
        public bool ConsumidorFinal { get; set; }
        public List<DocumentDetailViewModel> Detalles { get; set; }
        public EntityDto Entidad { get; set; }

    }

    public class DocumentDetailViewModel
    {
        public string Id { get; set; }
        public string IdDocumento { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Descuento { get; set; }
        private decimal ValorDescuento => (Descuento > 0 ? decimal.Round(Valor * (Descuento / 100M), 4) : 0M);
        private decimal Valor => decimal.Round(Cantidad * Precio, 2);
        public decimal SubtotalSinImpuesto => (Valor - ValorDescuento);
        public decimal Subtotal => (SubtotalSinImpuesto + Iva);
        public string TipoIva { get; set; }
        public string TipoIce { get; set; }
        public int IdTipoUnidad { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Iva => (decimal.Round(Valor * (Convert.ToDecimal(PorcentajeTarifaImpuesto) / 100), 2));
        public decimal Ice { get; set; }
        public int IdTarifaImpuesto { get; set; }
        public int PorcentajeTarifaImpuesto { get; set; }

        public ProductDto Producto { get; set; }
        
        public string CodigoInterno { get; set; }

        public string Nombre { get; set; }

        public string NombreProducto { get; set; }

        public string DescripcionProducto { get; set; }

    }


    public class EmpaquetadoViewModel
    {
        public string Id { get; set; }
        public int IdTipoPaquete { get; set; }
        public string TipoPaquete { get; set; }
        public int Cantidad { get; set; }
        public short IdEstado { get; set; }
    }

    public class RevisadoNotaPedidoViewModel : EmpaquetadoNotaPedidoViewModel
    {

        public int IdFormaPago { get; set; }
        public int Plazo { get; set; }
        public int Cuotas { get; set; }
        public List<CuentasPorCobrarViewModel> CuentasPorCobrar { get; set; }
        public List<AditionalInfoDto> InformacionAdicional { get; set; }

    }

    public class EmpaquetadoNotaPedidoViewModel
    {
        public string Id { get; set; }
        public int IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public string Observaciones { get; set; }
        public decimal Total { get; set; }
        public short IdEstado { get; set; }
        public List<DetalleNotaPedidoViewModel> DetalleNotaPedido { get; set; }
        public List<EmpaquetadoViewModel> DetalleEmpaquetado { get; set; }
        public decimal ValorAdicional { get; set; }
    }

    public class EntregaNotaPedidoViewModel
    {
        public string Id { get; set; }
        public int IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public string Observaciones { get; set; }
        public bool Facturar { get; set; }
        public int Porcentaje { get; set; }
        public object Factura { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la forma de pago.")]
        public int IdFormaPago { get; set; }
        public decimal Total { get; set; }
        public short IdEstado { get; set; }
        public string Ruc { get; set; }
        public List<DetalleNotaPedidoViewModel> DetalleNotaPedido { get; set; }

        public int Plazo { get; set; }
        public int Cuotas { get; set; }
        public List<CuentasPorCobrarViewModel> CuentasPorCobrar { get; set; }
        public List<AditionalInfoDto> InformacionAdicional { get; set; }
        public List<EmpaquetadoViewModel> DetalleEmpaquetado { get; set; }
        public decimal ValorAdicional { get; set; }
        public bool GeneraNc { get; set; }

    }



    public class FacturaNotaPedidoViewModel
    {
        public string Id { get; set; }
        public DateTime FechaFactura { get; set; }
        public int IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public string Observaciones { get; set; }
        public int PorcentajeFactura { get; set; }
        public bool ConsumidorFinal { get; set; }
        public int IdFormaPagoSri { get; set; }
        public string Factura { get; set; }
        public decimal Total { get; set; }
        public short IdEstado { get; set; }
        public string Ruc { get; set; }
        public string FacturaJson { get; set; }
    }

    public class ExistenciaProductoViewModel
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int IdBodega { get; set; }
        public string DescripcionBodega { get; set; }
        public decimal StockDisponible { get; set; }
    }

    public class CuentasPorCobrarViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPendiente { get; set; }
        public decimal ValorPagado { get; set; }
        public DateTime FechaVencimiento { get; set; }

        [Display(Name = "Medio Pago")]
        public int IdMedioPago { get; set; }
        public List<ReceivablePayMethodDto> MetodosPago { get; set; }

        public decimal Saldo
        {
            get
            {
                return (this.Valor - this.ValorPagado);
            }
        }
    }

    public class NotaPedidoTempModel
    {
        public int Id { get; set; }
        //public List<NotaPedidoProductoTempModel> NotaPedidoProductosTempModel { get; set; }
    }

    public class NotaPedidoProductoTempModel
    {
        public int IdNotaPedidoTemp { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }



    public class NotaPedidoModel : NotaPedidoViewModel
    {

        public string RazonSocial { get; set; }
         
        public string Ruc { get; set; }

        public string NumeroFacturaPedido { get; set; }

        public string Periodo { get; set; }

        public int PorcentajeFactura { get; set; } = 100;

        public int Plazo { get; set; }
        public int Cuotas { get; set; }
        public string Entrega { get; set; }
        public List<CuentasPorCobrarViewModel> CuentasPorCobrar { get; set; }
        public List<AditionalInfoDto> InformacionAdicional { get; set; }

        public List<EmpaquetadoViewModel> DetalleEmpaquetado { get; set; }

        public DocumentViewModel Factura { get; set; }

        public EntradaViewModel Entrada { get; set; }
        public List<string> Recibo { get; set; } = new List<string>();
        public List<decimal> ReciboValor { get; set; } = new List<decimal>();
        public decimal ValorNc { get; set; }
        public string FacturaJson { get; set; }
    }


    public class OrderSearchModel
    {
        public string accion { get; set; }
        public TransactionType? tipoTransaccion { get; set; }
        public string filtro { get; set; }
        public string idVendedor { get; set; }
        public int? idPersona { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }

        public TransactionStatus? status { get; set; }

        public int idLocal { get; set; }

        internal TransactionStatus? getStatus()
        {
            switch (accion)
            {
                case "Aprobar":
                    return TransactionStatus.EnProceso;
                case "Empaquetar":
                    return TransactionStatus.Aprobada;
                case "Revisar":
                    return TransactionStatus.Empaquetada;
                case "Entregar":
                    return TransactionStatus.Revisada;
                case "Facturar":
                    return TransactionStatus.Entregada;
            }

            return default;
        }
         
        internal DateTime? getFechaFin()
        {
            DateTime date;
            //Busco el ultimo segundo del día
            return DateTime.TryParse(fechaFin, CultureInfo.GetCultureInfo("es-EC"), DateTimeStyles.None, out date) ? ((DateTime?)(date.AddDays(1).AddSeconds(-1))) : default;
        }

        internal DateTime? getFechaInicio()
        {
            DateTime date;
            return DateTime.TryParse(fechaInicio, CultureInfo.GetCultureInfo("es-EC"), DateTimeStyles.None, out date) ? (DateTime?)date : null;
        }
        public string transaccionPadre { get; set; }
    }

}