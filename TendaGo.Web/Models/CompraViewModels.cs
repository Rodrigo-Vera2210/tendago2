using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class EntradaViewModel
    {
        public string Id { get; set; }

        public string TipoTransaccion { get; set; }

        public int IdProveedor { get; set; }
        public ProveedorViewModel Proveedor { get; set; }
        public DateTime Fecha { get; set; }  
        public decimal Total { get; set; }
        public string IdVendedor { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaHoraEntregaPropuesta { get; set; }

        [Required]
        public string FechaEntrega { get; set; }

        [Required]
        public string HoraEntrega { get; set; }

        public bool Facturar { get; set; }

        public string EstadoActual { get; set; }

        public int IdFormaPago { get; set; }

        public string TransaccionPadre { get; set; }
        public string TipoTransaccionPadre { get; set; }
        public string NumeroFacturaPedido { get; set; }
        public short IdMonedaOrigen { get; set; }
        public string IdMonedaOrigen_DisplayMember { get; set; }
        public decimal? Tasa { get; set; }

        public string FormaPago
        {
            get
            {

                return (IdFormaPago == 2) ? "CREDITO" : "CONTADO";
            }
        }

        public int IdLocal { get; set; }

        public string LocalSeleccionado { get; set; }

        public decimal ValorAdicional { get; set; }

        public List<DetalleEntradaViewModel> DetalleNotaPedido { get; set; }

        public List<CuentasPorPagarViewModel> CuentasPorPagar { get; set; }

        public NotaPedidoViewModel Salida { get; set; }
        public int IdLocalOrigen { get; set; }

        public List<EmpaquetadoViewModel> DetalleEmpaquetado { get; set; }
    }


    public class DetalleEntradaViewModel
    {
        public bool Recibido { get; set; }

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

        [Display(Name = "Precio Sugerido")]
        public decimal PrecioSugerido { get; set; }

        //public int IdPrecio { get; set; }

        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Display(Name = "Tipo de Unidad")]
        public int IdTipoUnidad { get; set; }
        public string DesTipoUnidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Nombre { get; set; }

        public decimal ValorFOB { get; set; }
        public decimal FactorDistribucion { get; set; }
        public decimal CostoUnitarioImportacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int IdLocal { get; set; }
        public string Local { get; set; }

        [Display(Name = "Cantidad Solicitada")]
        public decimal Cantidad { get; set; }
        public decimal CostoVenta { get; set; }

        public decimal Subtotal { get; set; }

    }


    public class CompraViewModel
    {
        
        [Display(Name = "Local Seleccionado")]
        [Required(ErrorMessage = "{0} es requerido")]
        public int? LocalSeleccionado { get; set; }

        [Display(Name = "RUC")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string Ruc { get; set; }

        [Display(Name = "Fecha Compra")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string FechaCompra { get; set; }

        [Display(Name = "Forma Pago")]
        [Required(ErrorMessage = "{0} es requerido")]
        public int IdFormaPago { get; set; }

        [Display(Name = "Nota Pedido/Factura")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string NumeroFacturaPedido { get; set; }

        [Display(Name = "Tasa de Cambio")]
        [Required(ErrorMessage = "{0} es requerido")]
        public decimal Tasa { get; set; }

        [Display(Name = "Valor Adicional")]
        public decimal ValorAdicional { get; set; }


        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "{0} es requerido")]
        public int IdProveedor { get; set; }

        [Display(Name = "Moneda Origen")]
        [Required(ErrorMessage = "{0} es requerido")]
        public short IdMonedaOrigen { get; set; }

        public decimal Subtotal0 { get; set; }

        public decimal Subtotal12 { get; set; }

        public decimal Total { get; set; }

        public decimal Saldo { get; set; }
        
        public List<DetalleCompraViewModel> DetalleCompra { get; set; }

        public List<CuentasPorPagarViewModel> CuentasPorPagar { get; set; }
    }

    public class DetalleCompraViewModel
    {
        public bool Seleccionado { get; set; }

        public int Id { get; set; }

        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }
        
        public decimal ValorFOB { get; set; }

        public decimal FactorDistribucion { get; set; }

        public decimal CostoUnitarioImportacion { get; set; }

        public decimal Cantidad { get; set; }

        public int IdLocal { get; set; }

        public int IdTipoUnidad { get; set; }

        public decimal CostoVenta { get; set; }

        public DateTime FechaCaducidad { get; set; }

        public decimal ValorAdicional { get; set; }

        public decimal Subtotal
        {
            get
            {
                return decimal.Round((this.CostoVenta * this.Cantidad) + this.ValorAdicional, 2);
            }
        }
        public string TipoProducto { get; set; }
        public List<SerieEntradaViewModel> Serie { get; set; }
    }

    
    public class CuentasPorPagarViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPendiente { get; set; }
        public decimal ValorPagado { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public decimal Saldo
        {
            get
            {
                return (this.ValorPendiente - this.ValorPagado);
            }
        }
    }

    public class SerieEntradaViewModel
    {
        public int Index { get; set; }
        public int IdProducto { get; set; }
        public string Serie { get; set; }
    }
}