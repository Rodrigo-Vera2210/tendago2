﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Ruta_FaltanteResult
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CodigoInterno { get; set; }
        [Column("Cantidad", TypeName = "decimal(38,2)")]
        public decimal? Cantidad { get; set; }
        public int IdTipoUnidad { get; set; }
        [Column("Precio", TypeName = "decimal(18,4)")]
        public decimal? Precio { get; set; }
    }
}
