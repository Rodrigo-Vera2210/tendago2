using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace TendaGo.Common
{
    public class LiteProductDto
    {
        public int Id { get; set; }

        [Display(Name = "Código Interno")]
        public string CodigoInterno { get; set; }
        public string CodigoBarra { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal Stock { get; set; }
        [Display(Name = "Stock Mínimo")]
        public decimal? StockMinimo { get; set; }
        [Display(Name = "Estado")]
        public short IdEstado { get; set; }
        [Display(Name = "Código Padre")]
        public string CodigoProductoPadre { get; set; }
        public string PathArchivo { get; set; }

        public string Marca { get; set; }
        public int? IdMarca { get; set; }
        public string Division { get; set; }
        public int? IdDivision { get; set; }
        public string Linea { get; set; }
        public int? IdLinea { get; set; }
        public string Categoria { get; set; }
        public int? IdCategoria { get; set; }
        public int? ProductoPadre { get; set; }
        public int IdEmpresa { get; set; }
        public string PhotoUrl { get; set; }
        public int porcentajeTarifaImpuesto { get; set; }
        public int IdTarifaImpuesto { get; set; }
        public bool? CobraIva { get; set; }
        public string TipoProducto { get; set; }
        public decimal Descuento { get; set; }
        public string NombreUnidadMedida { get; set; }
    }

    public class ProductDto : LiteProductDto
    {
        //public int Id { get; set; }
        public string CodigoProveedor { get; set; }        
        
        //public string Producto { get; set; }
        //public string Descripcion { get; set; }
        public string DescipcionBusqueda { get; set; }
        //public int Stock { get; set; }
        //public int? StockMinimo { get; set; }
        public decimal Costo { get; set; }
        public int? UnidadMedida { get; set; }
        public decimal? Descuento { get; set; }               
        public decimal? Peso { get; set; }
        public decimal? Volumen { get; set; }
        public string RegistroSanitario { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        //public short IdEstado { get; set; }
        public bool HasFotoChanges { get; set; }
        public string Foto { get; set; }
        public int IdLocal { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
    }

    public enum ProductType
    {
        Producto = 1, Servicio=2
    }

}