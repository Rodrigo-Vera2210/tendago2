﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Producto_SearchProductsResult
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string CodigoProveedor { get; set; }
        public string CodigoInterno { get; set; }
        public string CodigoBarra { get; set; }
        public string TipoProducto { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public string DescipcionBusqueda { get; set; }
        public int Stock { get; set; }
        public int? StockMinimo { get; set; }
        [Column("Costo", TypeName = "decimal(18,4)")]
        public decimal Costo { get; set; }
        public int? UnidadMedida { get; set; }
        [Column("Descuento", TypeName = "decimal(18,2)")]
        public decimal? Descuento { get; set; }
        public bool? CobraIva { get; set; }
        public int? IdDivision { get; set; }
        public int? IdLinea { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdMarca { get; set; }
        [Column("Peso", TypeName = "decimal(18,4)")]
        public decimal? Peso { get; set; }
        [Column("Volumen", TypeName = "decimal(18,4)")]
        public decimal? Volumen { get; set; }
        public string RegistroSanitario { get; set; }
        public string PathArchivo { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public int? ProductoPadre { get; set; }
        public int? Id_ProductoFromProductoPadre { get; set; }
        public string CodigoProveedor_ProductoFromProductoPadre { get; set; }
        public string CodigoInterno_ProductoFromProductoPadre { get; set; }
        public string Producto_ProductoFromProductoPadre { get; set; }
        public int? Id_MarcaFromIdMarca { get; set; }
        public string Marca_MarcaFromIdMarca { get; set; }
    }
}
