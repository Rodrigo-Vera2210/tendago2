﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Stock_LoadAllResult
    {
        public string ID { get; set; }
        public int IDEMPRESA { get; set; }
        public string TIPO { get; set; }
        public string IDSALIDAENTRADA { get; set; }
        public string IDDETALLE { get; set; }
        public int IDPRODUCTO { get; set; }
        public int IDLOCAL { get; set; }
        public DateTime FECHA { get; set; }
        [Column("CANTIDAD", TypeName = "decimal(18,2)")]
        public decimal CANTIDAD { get; set; }
        [Column("VALORUNITARIO", TypeName = "decimal(18,4)")]
        public decimal VALORUNITARIO { get; set; }
        [Column("VALORTOTAL", TypeName = "decimal(37,6)")]
        public decimal? VALORTOTAL { get; set; }
        public int? IDTIPOUNIDAD { get; set; }
        [Column("CANTIDADTIPOUNIDAD", TypeName = "decimal(18,2)")]
        public decimal? CANTIDADTIPOUNIDAD { get; set; }
        [Column("STOCKINVENTARIO", TypeName = "decimal(18,2)")]
        public decimal? STOCKINVENTARIO { get; set; }
        [Column("COSTOPROMEDIOPONDERADO", TypeName = "decimal(18,4)")]
        public decimal? COSTOPROMEDIOPONDERADO { get; set; }
        [Column("SALDOINVENTARIO", TypeName = "decimal(37,6)")]
        public decimal? SALDOINVENTARIO { get; set; }
        public short? IDESTADO { get; set; }
    }
}
