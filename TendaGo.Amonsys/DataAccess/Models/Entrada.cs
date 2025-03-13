﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class Entrada
{
    public string Id { get; set; }

    public int IdEmpresa { get; set; }

    public int IdLocal { get; set; }

    public string Periodo { get; set; }

    public DateTime Fecha { get; set; }

    public string TipoTransaccion { get; set; }

    public int? IdProveedor { get; set; }

    public decimal? Subtotal0 { get; set; }

    public decimal? Subtotal12 { get; set; }

    public decimal Total { get; set; }

    public decimal Saldo { get; set; }

    public decimal? ValorAdicional { get; set; }

    public string NumeroFacturaPedido { get; set; }

    public string TransaccionPadre { get; set; }

    public string TipoTransaccionPadre { get; set; }

    public string EstadoActual { get; set; }

    public string Observaciones { get; set; }

    public DateTime? FechaHoraEntrega { get; set; }

    public int IdFormaPago { get; set; }

    public short? IdMonedaOrigen { get; set; }

    public string Ruc { get; set; }

    public decimal? Tasa { get; set; }

    public string IpIngreso { get; set; }

    public string UsuarioIngreso { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string IpModificacion { get; set; }

    public string UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public short IdEstado { get; set; }

    public virtual ICollection<CuentaPorPagar> CuentaPorPagar { get; set; } = new List<CuentaPorPagar>();

    public virtual ICollection<DetalleEntrada> DetalleEntrada { get; set; } = new List<DetalleEntrada>();

    public virtual ICollection<EstadoEntrada> EstadoEntrada { get; set; } = new List<EstadoEntrada>();

    public virtual LocalBodega IdLocalNavigation { get; set; }

    public virtual Moneda IdMonedaOrigenNavigation { get; set; }

    public virtual Entidad IdProveedorNavigation { get; set; }

    public virtual Ruc RucNavigation { get; set; }
}