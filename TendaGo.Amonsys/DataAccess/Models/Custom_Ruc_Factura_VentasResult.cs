﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Custom_Ruc_Factura_VentasResult
    {
        public DateTime Fecha { get; set; }
        public string CodigoInterno { get; set; }
        public string Producto { get; set; }
        public string NumeroDocumento { get; set; }
        public string RUC { get; set; }
        public string RAZONSOCIAL { get; set; }
        public string ClienteRazonSocial { get; set; }
        public string ClienteIdentificacion { get; set; }
        [Column("Cantidad", TypeName = "decimal(10,2)")]
        public decimal? Cantidad { get; set; }
        [Column("CANTIDADUNIDAD", TypeName = "decimal(18,2)")]
        public decimal CANTIDADUNIDAD { get; set; }
        public string TIPOUNIDAD { get; set; }
        [Column("Precio", TypeName = "decimal(10,2)")]
        public decimal? Precio { get; set; }
        [Column("SubtotalSinImpuesto", TypeName = "decimal(10,2)")]
        public decimal? SubtotalSinImpuesto { get; set; }
        [Column("Iva", TypeName = "decimal(10,2)")]
        public decimal? Iva { get; set; }
        [Column("Total", TypeName = "decimal(10,2)")]
        public decimal? Total { get; set; }
    }
}
