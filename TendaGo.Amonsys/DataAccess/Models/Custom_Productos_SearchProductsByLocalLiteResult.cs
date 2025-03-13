﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Custom_Productos_SearchProductsByLocalLiteResult
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string CodigoProveedor { get; set; }
        public string CodigoInterno { get; set; }
        public string CodigoBarra { get; set; }
        public string TipoProducto { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public int? StockMinimo { get; set; }
        public int? UnidadMedida { get; set; }
        [Column("Descuento", TypeName = "decimal(18,2)")]
        public decimal? Descuento { get; set; }
        public bool? CobraIva { get; set; }
        public string PathArchivo { get; set; }
        [Column("StockInventario", TypeName = "decimal(18,2)")]
        public decimal StockInventario { get; set; }
        public int IdLocal { get; set; }
        public string TipoUnidad { get; set; }
        [Column("Precio", TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
    }
}
