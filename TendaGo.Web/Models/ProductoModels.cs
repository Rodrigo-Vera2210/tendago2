using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public enum OrigenesBusquedaProductos
    {
        MantProducto=1, MantPrecio, NotaPedido, Compra
    }

    public class ProductoModels
    {
    }

    public class ProductoViewModel
    {
        private int id = 0;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdEmpresa { get; set; }

        [Display(Name = "Cod. Proveedor")]
        [Required(ErrorMessage = "Dato requerido")]
        public string CodigoProveedor { get; set; }

        [Display(Name = "Cod.Interno")]
        [Required(ErrorMessage = "Dato requerido")]
        public string CodigoInterno { get; set; }

        [Display(Name = "Tipo Producto")]
        [Required(ErrorMessage = "Dato requerido")]
        public string TipoProducto { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string Producto { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Foto")]
        public string FotoDataUrl { get; set; }

        public bool HasfotoChanges { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public decimal Stock { get; set; }

        [Display(Name = "Stock Minimo")]
        [Required(ErrorMessage = "Dato requerido")]
        public decimal StockMinimo { get; set; }

        public decimal Costo { get; set; }

        [Display(Name = "Unidad de Medida")]
        [Required(ErrorMessage = "Debe seleccionar la unidad de medida")]
        public int UnidadMedida { get; set; }

        [Display(Name = "I.V.A.")]
        [Required(ErrorMessage = "Debe seleccionar la clase de I.V.A.")]
        public int PorcentajeTarifaImpuesto { get; set; }
        public int IdTarifaImpuesto { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public decimal Descuento { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [Display(Name = "Cobra IVA")]
        public bool CobraIva { get; set; }

        [Display(Name = "Categoría")]
        public int? IdCategoria { get; set; }

        public string Categoria { get; set; }

        public int IdClasificacion { get; set; }

        public short IdEstado { get; set; }

        [Display(Name = "División")]
        public int? IdDivision { get; set; }

        public string Division { get; set; }

        [Display(Name = "Línea")]
        public int? IdLinea { get; set; }

        public string Linea { get; set; }

        public decimal? Volumen { get; set; }

        [Display(Name = "Registro Sanitario")] 
        public string RegistroSanitario { get; set; }

        public decimal? Peso { get; set; }

        [Display(Name = "Marca")]
        public int? IdMarca { get; set; }

        public string PathArchivo { get; set; }

        public string Marca { get; set; }

        public bool Activo { get; set; }

        [StringLength(50)]
        [Display(Name = "Codigo de Barra")]
        public string CodigoBarra { get; set; }

        public int? ProductoPadre { get; set; }

        public OrigenesBusquedaProductos? Origen { get; set; }
        
        [Display(Name = "Local")]
        public int IdLocal { get; set; }

        [Display(Name = "Fecha Desde")]
        public string FechaDesde { get; set; }

        [Display(Name = "Fecha Hasta")]
        public string FechaHasta { get; set; }
    }

    public class StockLocalViewModel
    {
        public int IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public int IdLocal { get; set; }
    }
}