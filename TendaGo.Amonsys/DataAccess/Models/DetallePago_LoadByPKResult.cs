﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class DetallePago_LoadByPKResult
    {
        public int Id { get; set; }
        public string IdPagoCredito { get; set; }
        public int IdCuentaPorPagar { get; set; }
        [Column("Valor", TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public int Id_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public string IdEntrada_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public int Numero_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public DateTime Periodo_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public DateTime Fecha_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        [Column("Valor_CuentaPorPagarFromIdCuentaPorPagar", TypeName = "decimal(18,2)")]
        public decimal Valor_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        [Column("Saldo_CuentaPorPagarFromIdCuentaPorPagar", TypeName = "decimal(18,2)")]
        public decimal? Saldo_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public string IpIngreso_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public string UsuarioIngreso_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public DateTime FechaIngreso_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public string IpModificacion_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public string UsuarioModificacion_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public DateTime? FechaModificacion_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public short IdEstado_CuentaPorPagarFromIdCuentaPorPagar { get; set; }
        public string Id_PagoCreditoFromIdPagoCredito { get; set; }
        public int IdEmpresa_PagoCreditoFromIdPagoCredito { get; set; }
        public DateTime Fecha_PagoCreditoFromIdPagoCredito { get; set; }
        public string Detalle_PagoCreditoFromIdPagoCredito { get; set; }
        public string IpIngreso_PagoCreditoFromIdPagoCredito { get; set; }
        public string UsuarioIngreso_PagoCreditoFromIdPagoCredito { get; set; }
        public DateTime FechaIngreso_PagoCreditoFromIdPagoCredito { get; set; }
        public string IpModificacion_PagoCreditoFromIdPagoCredito { get; set; }
        public string UsuarioModificacion_PagoCreditoFromIdPagoCredito { get; set; }
        public DateTime? FechaModificacion_PagoCreditoFromIdPagoCredito { get; set; }
        public short IdEstado_PagoCreditoFromIdPagoCredito { get; set; }
    }
}
