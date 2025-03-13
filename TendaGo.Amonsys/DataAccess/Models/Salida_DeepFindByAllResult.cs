﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Salida_DeepFindByAllResult
    {
        public string Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdLocal { get; set; }
        public string IdVendedor { get; set; }
        public string Periodo { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTransaccion { get; set; }
        public int? IdCliente { get; set; }
        public bool Facturar { get; set; }
        public string Ruc { get; set; }
        [Column("Subtotal0", TypeName = "decimal(18,2)")]
        public decimal? Subtotal0 { get; set; }
        [Column("SubtotalIva", TypeName = "decimal(18,2)")]
        public decimal? SubtotalIva { get; set; }
        [Column("Descuento", TypeName = "decimal(18,2)")]
        public decimal? Descuento { get; set; }
        [Column("Total", TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        [Column("Saldo", TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }
        public DateTime FechaHoraEntregaPropuesta { get; set; }
        public DateTime? FechaHoraEntregaReal { get; set; }
        public string EstadoActual { get; set; }
        public string Observaciones { get; set; }
        public int IdFormaPago { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public string TipoTransaccionPadre { get; set; }
        public string TransaccionPadre { get; set; }
        public int Plazo { get; set; }
        public int Cuotas { get; set; }
        public int Id_EntidadFromIdCliente { get; set; }
        public int IdEmpresa_EntidadFromIdCliente { get; set; }
        public string TipoEntidad_EntidadFromIdCliente { get; set; }
        public string RazonSocial_EntidadFromIdCliente { get; set; }
        public string Identificacion_EntidadFromIdCliente { get; set; }
        public short? IdPais_EntidadFromIdCliente { get; set; }
        public int? IdProvincia_EntidadFromIdCliente { get; set; }
        public int? IdCiudad_EntidadFromIdCliente { get; set; }
        public string Direccion_EntidadFromIdCliente { get; set; }
        public int? IdSector_EntidadFromIdCliente { get; set; }
        public string Telefono_EntidadFromIdCliente { get; set; }
        public string Celular_EntidadFromIdCliente { get; set; }
        public string Correo_EntidadFromIdCliente { get; set; }
        public string Observaciones_EntidadFromIdCliente { get; set; }
        public string Foto_EntidadFromIdCliente { get; set; }
        public string IpIngreso_EntidadFromIdCliente { get; set; }
        public string UsuarioIngreso_EntidadFromIdCliente { get; set; }
        public DateTime FechaIngreso_EntidadFromIdCliente { get; set; }
        public string IpModificacion_EntidadFromIdCliente { get; set; }
        public string UsuarioModificacion_EntidadFromIdCliente { get; set; }
        public DateTime? FechaModificacion_EntidadFromIdCliente { get; set; }
        public short IdEstado_EntidadFromIdCliente { get; set; }
        public int Id_LocalBodegaFromIdLocal { get; set; }
        public int IdEmpresa_LocalBodegaFromIdLocal { get; set; }
        public string Tipo_LocalBodegaFromIdLocal { get; set; }
        public string Local_LocalBodegaFromIdLocal { get; set; }
        public string Direccion_LocalBodegaFromIdLocal { get; set; }
        public string IpIngreso_LocalBodegaFromIdLocal { get; set; }
        public string UsuarioIngreso_LocalBodegaFromIdLocal { get; set; }
        public DateTime FechaIngreso_LocalBodegaFromIdLocal { get; set; }
        public string IpModificacion_LocalBodegaFromIdLocal { get; set; }
        public string UsuarioModificacion_LocalBodegaFromIdLocal { get; set; }
        public DateTime? FechaModificacion_LocalBodegaFromIdLocal { get; set; }
        public short IdEstado_LocalBodegaFromIdLocal { get; set; }
        public string Ruc_RucFromRuc { get; set; }
        public string TokenFactElectonica_RucFromRuc { get; set; }
        public int? IdEmpresa_RucFromRuc { get; set; }
        [Column("LimiteFacturacion_RucFromRuc", TypeName = "decimal(18,2)")]
        public decimal? LimiteFacturacion_RucFromRuc { get; set; }
        [Column("TotalFacturado_RucFromRuc", TypeName = "decimal(18,2)")]
        public decimal? TotalFacturado_RucFromRuc { get; set; }
        public string IpIngreso_RucFromRuc { get; set; }
        public string UsuarioIngreso_RucFromRuc { get; set; }
        public DateTime? FechaIngreso_RucFromRuc { get; set; }
        public string IpModificacion_RucFromRuc { get; set; }
        public string UsuarioModificacion_RucFromRuc { get; set; }
        public DateTime? FechaModificacion_RucFromRuc { get; set; }
        public short? IdEstado_RucFromRuc { get; set; }
    }
}
