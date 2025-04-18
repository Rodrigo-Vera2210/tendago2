﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Entrada_LoadAllResult
    {
        public string ID { get; set; }
        public int IDEMPRESA { get; set; }
        public int IDLOCAL { get; set; }
        public string PERIODO { get; set; }
        public DateTime FECHA { get; set; }
        public string TIPOTRANSACCION { get; set; }
        public int? IDPROVEEDOR { get; set; }
        [Column("SUBTOTAL0", TypeName = "decimal(18,2)")]
        public decimal? SUBTOTAL0 { get; set; }
        [Column("SUBTOTAL12", TypeName = "decimal(18,2)")]
        public decimal? SUBTOTAL12 { get; set; }
        [Column("TOTAL", TypeName = "decimal(18,2)")]
        public decimal TOTAL { get; set; }
        [Column("SALDO", TypeName = "decimal(18,2)")]
        public decimal SALDO { get; set; }
        public string NUMEROFACTURAPEDIDO { get; set; }
        public int IDFORMAPAGO { get; set; }
        [Column("Tasa", TypeName = "decimal(18,4)")]
        public decimal? Tasa { get; set; }
        public short? IdMonedaOrigen { get; set; }
        public string Ruc { get; set; }
        public string IPINGRESO { get; set; }
        public string USUARIOINGRESO { get; set; }
        public DateTime FECHAINGRESO { get; set; }
        public string IPMODIFICACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public short IDESTADO { get; set; }
    }
}
