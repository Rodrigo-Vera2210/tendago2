using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class CompraModel
    {
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

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "{0} es requerido")]
        public int IdProveedor { get; set; }

        [Display(Name = "Moneda Origen")]
        [Required(ErrorMessage = "{0} es requerido")]
        public short IdMonedaOrigen { get; set; }

        [Display(Name = "Valor Adicional")]
        public decimal ValorAdicional { get; set; }

        public decimal Total { get; set; }
        public decimal Saldo { get; set; }

        public List<DetalleArchivoCompra> ItemsArchivo { get; set; }
        public List<CuentasPorPagarViewModel> CuentasPorPagar { get; set; }
    }

    public class DetalleArchivoCompra
    {
        [Display(Name = "Estado")]
        public bool ItemValido { get; set; }

        [Display(Name = "Cod. Producto")]
        public string CodigoProducto { get; set; }

        [Display(Name = "Producto")]
        public string Producto { get; set; }

        [Display(Name = "Local/Bodega")]
        public string Local { get; set; }
        
        [Display(Name = "Tipo Unidad")]
        public string TipoUnidad { get; set; }
        
        [Display(Name = "Valor FOB")]
        public decimal ValorFob { get; set; }

        [Display(Name = "Impuesto Gasto")]
        public decimal CostoUnitarioImportacion { get; set; }

        [Display(Name = "Val. Moneda Origen")]
        public decimal FactorDistribucion { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }

        [Display(Name = "Costo Venta")]
        public decimal CostoVenta { get; set; }

        [Display(Name = "Valor Adicional")]
        public decimal ValorAdicional { get; set; }


        public decimal Subtotal
        {
            get
            {
                if (Cantidad != 0 && FactorDistribucion != 0)
                    return Cantidad * FactorDistribucion;
                return 0;
            }
        }

        [Display(Name = "Fecha Caducidad")]
        public string FechaCaducidad { get; set; }
        
        public string Observacion { get; set; }

        public int IdProducto { get; set; } 
        public int IdLocal { get; set; }
        public int IdTipoUnidad { get; set; }
    }
}