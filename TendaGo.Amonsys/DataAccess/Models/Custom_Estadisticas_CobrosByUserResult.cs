﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Custom_Estadisticas_CobrosByUserResult
    {
        public int? IdEmpresa { get; set; }
        public int? Anio { get; set; }
        public int? Mes { get; set; }
        public int? IdLocal { get; set; }
        public string Local { get; set; }
        [Column("Total", TypeName = "decimal(38,2)")]
        public decimal? Total { get; set; }
    }
}
