﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Custom_Inventario_Valorizado2Result
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string TipoUnidad { get; set; }
        [Column("Entradas", TypeName = "decimal(38,4)")]
        public decimal Entradas { get; set; }
        [Column("Salidas", TypeName = "decimal(38,4)")]
        public decimal Salidas { get; set; }
        [Column("Stock", TypeName = "decimal(38,4)")]
        public decimal? Stock { get; set; }
        [Column("Precio", TypeName = "decimal(18,4)")]
        public decimal? Precio { get; set; }
        public int Inventario { get; set; }
        [Column("PrecioCompra", TypeName = "decimal(38,6)")]
        public decimal PrecioCompra { get; set; }
        [Column("PrecioVenta", TypeName = "decimal(38,6)")]
        public decimal? PrecioVenta { get; set; }
        [Column("CantidadPaquete", TypeName = "decimal(18,2)")]
        public decimal? CantidadPaquete { get; set; }
        [Column("Valorizado", TypeName = "decimal(38,6)")]
        public decimal? Valorizado { get; set; }
    }
}
