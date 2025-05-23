﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class PresupuestoFacturacion_LoadByPKResult
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Ruc { get; set; }
        public string Mes { get; set; }
        public string Fecha { get; set; }
        [Column("Presupuesto", TypeName = "decimal(18,2)")]
        public decimal Presupuesto { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public int Id_EmpresaFromIdEmpresa { get; set; }
        public string NombreEmpresa_EmpresaFromIdEmpresa { get; set; }
        public string Direccion_EmpresaFromIdEmpresa { get; set; }
        public string Telefono_EmpresaFromIdEmpresa { get; set; }
        public string IpIngreso_EmpresaFromIdEmpresa { get; set; }
        public string UsuarioIngreso_EmpresaFromIdEmpresa { get; set; }
        public DateTime FechaIngreso_EmpresaFromIdEmpresa { get; set; }
        public string IpModificacion_EmpresaFromIdEmpresa { get; set; }
        public string UsuarioModificacion_EmpresaFromIdEmpresa { get; set; }
        public DateTime? FechaModificacion_EmpresaFromIdEmpresa { get; set; }
        public short IdEstado_EmpresaFromIdEmpresa { get; set; }
        public string Ruc_RucFromRuc { get; set; }
        public string TokenFactElectonica_RucFromRuc { get; set; }
        public int IdEmpresa_RucFromRuc { get; set; }
        [Column("LimiteFacturacion_RucFromRuc", TypeName = "decimal(18,2)")]
        public decimal LimiteFacturacion_RucFromRuc { get; set; }
        [Column("TotalFacturado_RucFromRuc", TypeName = "decimal(18,2)")]
        public decimal? TotalFacturado_RucFromRuc { get; set; }
        public string IpIngreso_RucFromRuc { get; set; }
        public string UsuarioIngreso_RucFromRuc { get; set; }
        public DateTime FechaIngreso_RucFromRuc { get; set; }
        public string IpModificacion_RucFromRuc { get; set; }
        public string UsuarioModificacion_RucFromRuc { get; set; }
        public DateTime? FechaModificacion_RucFromRuc { get; set; }
        public short IdEstado_RucFromRuc { get; set; }
    }
}
