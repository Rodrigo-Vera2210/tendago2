﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Custom_Ruc_Factura_ComprasResult
    {
        public DateTime Fecha { get; set; }
        public string CodigoInterno { get; set; }
        public string Producto { get; set; }
        public string NumeroDocumento { get; set; }
        public int idproveedor { get; set; }
        [Column("Cantidad", TypeName = "decimal(18,2)")]
        public decimal Cantidad { get; set; }
        [Column("CantidadUnidad", TypeName = "decimal(18,2)")]
        public decimal CantidadUnidad { get; set; }
        [Column("Precio", TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Column("Total", TypeName = "decimal(38,6)")]
        public decimal? Total { get; set; }
        public string Proveedor { get; set; }
        public string Identificacion { get; set; }
        public string Usuario { get; set; }
        public string Ruc { get; set; }
        [Column("TasaCambio", TypeName = "decimal(18,4)")]
        public decimal? TasaCambio { get; set; }
        public string MonedaOrigen { get; set; }
        public string NumeroFacturaPedido { get; set; }
        public string TipoUnidad { get; set; }
        [Column("ValorFOB", TypeName = "decimal(18,2)")]
        public decimal? ValorFOB { get; set; }
        [Column("ValMonedaOrigen", TypeName = "decimal(18,4)")]
        public decimal? ValMonedaOrigen { get; set; }
        [Column("ImpuestoGasto", TypeName = "decimal(18,2)")]
        public decimal? ImpuestoGasto { get; set; }
    }
}
