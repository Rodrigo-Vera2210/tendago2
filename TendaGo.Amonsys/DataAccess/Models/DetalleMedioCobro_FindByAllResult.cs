﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class DetalleMedioCobro_FindByAllResult
    {
        public int Id { get; set; }
        public string IdCobroDebito { get; set; }
        public int IdMedioPago { get; set; }
        [Column("Valor", TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
    }
}
