﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class LocalBodega_DeepFindByAllResult
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; }
        public string Local { get; set; }
        public string Direccion { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public string DefaultRUC { get; set; }
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
    }
}
