﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class DetalleSalida_FindByAllResult
    {
        public string Id { get; set; }
        public string Periodo { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTransaccion { get; set; }
        public string IdSalida { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdLocal { get; set; }
        [Column("CostoUnitarioImportacion", TypeName = "decimal(18,4)")]
        public decimal? CostoUnitarioImportacion { get; set; }
        [Column("Cantidad", TypeName = "decimal(18,2)")]
        public decimal Cantidad { get; set; }
        public int IdTipoUnidad { get; set; }
        [Column("CostoVenta", TypeName = "decimal(18,4)")]
        public decimal CostoVenta { get; set; }
        [Column("Precio", TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Column("Descuento", TypeName = "decimal(18,2)")]
        public decimal? Descuento { get; set; }
        [Column("Subtotal", TypeName = "decimal(38,6)")]
        public decimal? Subtotal { get; set; }
        public DateTime? FechaFabricacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string Lote { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public bool Empaquetado { get; set; }
        public bool Revisado { get; set; }
        public int? IdEmpresa { get; set; }
        public bool? IncluyeIva { get; set; }
    }
}
