﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Custom_Estadisticas_VentasMensualByUserResult
    {
        public DateTime? fecha { get; set; }
        [Column("total", TypeName = "decimal(38,2)")]
        public decimal? total { get; set; }
    }
}
