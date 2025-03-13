﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Empaquetado_DeepFindByAllResult
    {
        public string Id { get; set; }
        public short IdEmpresa { get; set; }
        public string IdSalida { get; set; }
        public int Cantidad { get; set; }
        public int IdTipoPaquete { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public string Id_SalidaFromIdSalida { get; set; }
        public int IdEmpresa_SalidaFromIdSalida { get; set; }
        public int IdLocal_SalidaFromIdSalida { get; set; }
        public string IdVendedor_SalidaFromIdSalida { get; set; }
        public string Periodo_SalidaFromIdSalida { get; set; }
        public DateTime Fecha_SalidaFromIdSalida { get; set; }
        public string TipoTransaccion_SalidaFromIdSalida { get; set; }
        public int? IdCliente_SalidaFromIdSalida { get; set; }
        public bool Facturar_SalidaFromIdSalida { get; set; }
        public string Ruc_SalidaFromIdSalida { get; set; }
        [Column("Subtotal0_SalidaFromIdSalida", TypeName = "decimal(18,2)")]
        public decimal? Subtotal0_SalidaFromIdSalida { get; set; }
        [Column("SubtotalIva_SalidaFromIdSalida", TypeName = "decimal(18,2)")]
        public decimal? SubtotalIva_SalidaFromIdSalida { get; set; }
        [Column("Descuento_SalidaFromIdSalida", TypeName = "decimal(18,2)")]
        public decimal? Descuento_SalidaFromIdSalida { get; set; }
        [Column("Total_SalidaFromIdSalida", TypeName = "decimal(18,2)")]
        public decimal Total_SalidaFromIdSalida { get; set; }
        [Column("Saldo_SalidaFromIdSalida", TypeName = "decimal(18,2)")]
        public decimal Saldo_SalidaFromIdSalida { get; set; }
        public DateTime FechaHoraEntregaPropuesta_SalidaFromIdSalida { get; set; }
        public DateTime? FechaHoraEntregaReal_SalidaFromIdSalida { get; set; }
        public string EstadoActual_SalidaFromIdSalida { get; set; }
        public string Observaciones_SalidaFromIdSalida { get; set; }
        public int IdFormaPago_SalidaFromIdSalida { get; set; }
        public string IpIngreso_SalidaFromIdSalida { get; set; }
        public string UsuarioIngreso_SalidaFromIdSalida { get; set; }
        public DateTime FechaIngreso_SalidaFromIdSalida { get; set; }
        public string IpModificacion_SalidaFromIdSalida { get; set; }
        public string UsuarioModificacion_SalidaFromIdSalida { get; set; }
        public DateTime? FechaModificacion_SalidaFromIdSalida { get; set; }
        public short IdEstado_SalidaFromIdSalida { get; set; }
        public int Id_TipoPaqueteFromIdTipoPaquete { get; set; }
        public string TipoPaquete_TipoPaqueteFromIdTipoPaquete { get; set; }
    }
}
