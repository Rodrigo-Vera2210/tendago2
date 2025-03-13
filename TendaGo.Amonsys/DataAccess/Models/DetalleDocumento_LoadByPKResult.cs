﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class DetalleDocumento_LoadByPKResult
    {
        public long Id { get; set; }
        public string IdDocumento { get; set; }
        public int IdProducto { get; set; }
        [Column("Precio", TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Column("Cantidad", TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; }
        [Column("Descuento", TypeName = "decimal(18,2)")]
        public decimal Descuento { get; set; }
        [Column("SubtotalSinImpuesto", TypeName = "decimal(38,8)")]
        public decimal? SubtotalSinImpuesto { get; set; }
        public string TipoIva { get; set; }
        public string TipoIce { get; set; }
        [Column("Iva", TypeName = "decimal(18,2)")]
        public decimal Iva { get; set; }
        [Column("Ice", TypeName = "decimal(18,2)")]
        public decimal Ice { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public string Id_DocumentoFromIdDocumento { get; set; }
        public string IdTipoDocumento_DocumentoFromIdDocumento { get; set; }
        public int IdEmpresa_DocumentoFromIdDocumento { get; set; }
        public string IdSalida_DocumentoFromIdDocumento { get; set; }
        public int IdEntidad_DocumentoFromIdDocumento { get; set; }
        public short IdMoneda_DocumentoFromIdDocumento { get; set; }
        public string Notas_DocumentoFromIdDocumento { get; set; }
        public string GuiaRemision_DocumentoFromIdDocumento { get; set; }
        public string NumeroDocumento_DocumentoFromIdDocumento { get; set; }
        [Column("Base0_DocumentoFromIdDocumento", TypeName = "decimal(18,2)")]
        public decimal Base0_DocumentoFromIdDocumento { get; set; }
        [Column("BaseIva_DocumentoFromIdDocumento", TypeName = "decimal(18,2)")]
        public decimal BaseIva_DocumentoFromIdDocumento { get; set; }
        [Column("TotalDescuento_DocumentoFromIdDocumento", TypeName = "decimal(18,2)")]
        public decimal TotalDescuento_DocumentoFromIdDocumento { get; set; }
        [Column("TotalSinImpuesto_DocumentoFromIdDocumento", TypeName = "decimal(20,2)")]
        public decimal? TotalSinImpuesto_DocumentoFromIdDocumento { get; set; }
        [Column("TotalIce_DocumentoFromIdDocumento", TypeName = "decimal(18,2)")]
        public decimal TotalIce_DocumentoFromIdDocumento { get; set; }
        [Column("TotalIva_DocumentoFromIdDocumento", TypeName = "decimal(18,2)")]
        public decimal TotalIva_DocumentoFromIdDocumento { get; set; }
        [Column("Total_DocumentoFromIdDocumento", TypeName = "decimal(22,2)")]
        public decimal? Total_DocumentoFromIdDocumento { get; set; }
        public string FormaPago_DocumentoFromIdDocumento { get; set; }
        public int Plazo_DocumentoFromIdDocumento { get; set; }
        public string UnidadTiempo_DocumentoFromIdDocumento { get; set; }
        public string IpIngreso_DocumentoFromIdDocumento { get; set; }
        public string UsuarioIngreso_DocumentoFromIdDocumento { get; set; }
        public DateTime FechaIngreso_DocumentoFromIdDocumento { get; set; }
        public string IpModificacion_DocumentoFromIdDocumento { get; set; }
        public string UsuarioModificacion_DocumentoFromIdDocumento { get; set; }
        public DateTime? FechaModificacion_DocumentoFromIdDocumento { get; set; }
        public short IdEstado_DocumentoFromIdDocumento { get; set; }
        public int Id_ProductoFromIdProducto { get; set; }
        public int IdEmpresa_ProductoFromIdProducto { get; set; }
        public string CodigoProveedor_ProductoFromIdProducto { get; set; }
        public string CodigoInterno_ProductoFromIdProducto { get; set; }
        public string TipoProducto_ProductoFromIdProducto { get; set; }
        public string Producto_ProductoFromIdProducto { get; set; }
        public string Descripcion_ProductoFromIdProducto { get; set; }
        public string DescipcionBusqueda_ProductoFromIdProducto { get; set; }
        public int Stock_ProductoFromIdProducto { get; set; }
        public int? StockMinimo_ProductoFromIdProducto { get; set; }
        [Column("Costo_ProductoFromIdProducto", TypeName = "decimal(18,4)")]
        public decimal Costo_ProductoFromIdProducto { get; set; }
        public int? UnidadMedida_ProductoFromIdProducto { get; set; }
        [Column("Descuento_ProductoFromIdProducto", TypeName = "decimal(18,2)")]
        public decimal? Descuento_ProductoFromIdProducto { get; set; }
        public bool? CobraIva_ProductoFromIdProducto { get; set; }
        public int? IdDivision_ProductoFromIdProducto { get; set; }
        public int? IdLinea_ProductoFromIdProducto { get; set; }
        public int? IdCategoria_ProductoFromIdProducto { get; set; }
        public int? IdMarca_ProductoFromIdProducto { get; set; }
        [Column("Peso_ProductoFromIdProducto", TypeName = "decimal(18,4)")]
        public decimal? Peso_ProductoFromIdProducto { get; set; }
        [Column("Volumen_ProductoFromIdProducto", TypeName = "decimal(18,4)")]
        public decimal? Volumen_ProductoFromIdProducto { get; set; }
        public string RegistroSanitario_ProductoFromIdProducto { get; set; }
        public string PathArchivo_ProductoFromIdProducto { get; set; }
        public string CodigoBarra_ProductoFromIdProducto { get; set; }
        public string IpIngreso_ProductoFromIdProducto { get; set; }
        public string UsuarioIngreso_ProductoFromIdProducto { get; set; }
        public DateTime FechaIngreso_ProductoFromIdProducto { get; set; }
        public string IpModificacion_ProductoFromIdProducto { get; set; }
        public string UsuarioModificacion_ProductoFromIdProducto { get; set; }
        public DateTime? FechaModificacion_ProductoFromIdProducto { get; set; }
        public short IdEstado_ProductoFromIdProducto { get; set; }
    }
}
