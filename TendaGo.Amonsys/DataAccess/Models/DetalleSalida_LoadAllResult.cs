﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class DetalleSalida_LoadAllResult
    {
        public string ID { get; set; }
        public string PERIODO { get; set; }
        public DateTime FECHA { get; set; }
        public string TIPOTRANSACCION { get; set; }
        public string IDSALIDA { get; set; }
        public int IDPRODUCTO { get; set; }
        public int IDPROVEEDOR { get; set; }
        public int IDLOCAL { get; set; }
        [Column("COSTOUNITARIOIMPORTACION", TypeName = "decimal(18,4)")]
        public decimal? COSTOUNITARIOIMPORTACION { get; set; }
        [Column("CANTIDAD", TypeName = "decimal(18,2)")]
        public decimal CANTIDAD { get; set; }
        public int IDTIPOUNIDAD { get; set; }
        [Column("COSTOVENTA", TypeName = "decimal(18,4)")]
        public decimal COSTOVENTA { get; set; }
        [Column("PRECIO", TypeName = "decimal(18,4)")]
        public decimal PRECIO { get; set; }
        [Column("DESCUENTO", TypeName = "decimal(18,2)")]
        public decimal? DESCUENTO { get; set; }
        [Column("SUBTOTAL", TypeName = "decimal(38,6)")]
        public decimal? SUBTOTAL { get; set; }
        public DateTime? FECHAFABRICACION { get; set; }
        public DateTime? FECHAEXPIRACION { get; set; }
        public string LOTE { get; set; }
        public string IPINGRESO { get; set; }
        public string USUARIOINGRESO { get; set; }
        public DateTime FECHAINGRESO { get; set; }
        public string IPMODIFICACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public short IDESTADO { get; set; }
        public bool EMPAQUETADO { get; set; }
        public bool REVISADO { get; set; }
        public int? IDEMPRESA { get; set; }
    }
}
