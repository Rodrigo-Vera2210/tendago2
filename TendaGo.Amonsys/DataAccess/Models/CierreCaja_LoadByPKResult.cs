﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class CierreCaja_LoadByPKResult
    {
        public string Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdLocal { get; set; }
        public string IdCajero { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string Observaciones { get; set; }
        [Column("SaldoInicial", TypeName = "decimal(18,2)")]
        public decimal SaldoInicial { get; set; }
        [Column("TotalIngresos", TypeName = "decimal(18,2)")]
        public decimal TotalIngresos { get; set; }
        [Column("TotalEgresos", TypeName = "decimal(18,2)")]
        public decimal TotalEgresos { get; set; }
        [Column("SaldoFinal", TypeName = "decimal(18,2)")]
        public decimal SaldoFinal { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short? IdEstado { get; set; }
        public int Id_LocalBodegaFromIdLocal { get; set; }
        public int IdEmpresa_LocalBodegaFromIdLocal { get; set; }
        public string Local_LocalBodegaFromIdLocal { get; set; }
        public string IpIngreso_LocalBodegaFromIdLocal { get; set; }
        public string UsuarioIngreso_LocalBodegaFromIdLocal { get; set; }
        public DateTime FechaIngreso_LocalBodegaFromIdLocal { get; set; }
        public string IpModificacion_LocalBodegaFromIdLocal { get; set; }
        public string UsuarioModificacion_LocalBodegaFromIdLocal { get; set; }
        public DateTime? FechaModificacion_LocalBodegaFromIdLocal { get; set; }
        public short IdEstado_LocalBodegaFromIdLocal { get; set; }
    }
}
