﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Empresa_LoadByPKResult
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Logo { get; set; }
        public string RaizArchivo { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public bool FlujoInventario { get; set; }
        public bool FacturaPOS { get; set; }
        public bool Importacion { get; set; }
        public bool IncluyeIVA { get; set; }
        [Column("IVA", TypeName = "decimal(10,2)")]
        public decimal IVA { get; set; }
        public bool FacturaGo { get; set; }
    }
}
