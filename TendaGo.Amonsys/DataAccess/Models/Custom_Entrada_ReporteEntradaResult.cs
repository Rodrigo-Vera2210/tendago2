﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class Custom_Entrada_ReporteEntradaResult
    {
        public string Logo { get; set; }
        public string Ruc { get; set; }
        public string NombreComercial { get; set; }
        public string NombreEmpresa { get; set; }
        public string ActividadEconomica { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NumeroEntrada { get; set; }
        public string Local { get; set; }
        public string Vendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTransaccion { get; set; }
        [Column("Total", TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        [Column("Subtotal0", TypeName = "decimal(18,2)")]
        public decimal Subtotal0 { get; set; }
        [Column("SubtotalIVA", TypeName = "decimal(18,2)")]
        public decimal SubtotalIVA { get; set; }
        [Column("IVA", TypeName = "decimal(20,2)")]
        public decimal? IVA { get; set; }
        [Column("Saldo", TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }
        public string Observaciones { get; set; }
        public string EstadoActual { get; set; }
        public DateTime? FechaHoraEntregaPropuesta { get; set; }
        public string FormaPago { get; set; }
        public string Cliente { get; set; }
        public string Identificacion { get; set; }
        public string DireccionCliente { get; set; }
        public string Correo { get; set; }
        public string TelefonoCliente { get; set; }
        public string CelularCliente { get; set; }
        public string CiudadCliente { get; set; }
        public string Id { get; set; }
        public string Producto { get; set; }
        public string Proveedor { get; set; }
        [Column("Cantidad", TypeName = "decimal(18,2)")]
        public decimal Cantidad { get; set; }
        public string TipoUnidad { get; set; }
        [Column("CantidadTipoUnidad", TypeName = "decimal(18,2)")]
        public decimal CantidadTipoUnidad { get; set; }
        [Column("Precio", TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Column("PrecioFinal", TypeName = "decimal(38,6)")]
        public decimal? PrecioFinal { get; set; }
        [Column("Subtotal", TypeName = "decimal(38,6)")]
        public decimal? Subtotal { get; set; }
        public bool IncluyeIVA { get; set; }
        [Column("ValorMonedaOrigen", TypeName = "decimal(18,4)")]
        public decimal? ValorMonedaOrigen { get; set; }
        [Column("ValorFOB", TypeName = "decimal(18,2)")]
        public decimal? ValorFOB { get; set; }
        [Column("TasaCambio", TypeName = "decimal(18,4)")]
        public decimal? TasaCambio { get; set; }
        public string MonedaOrigen { get; set; }
        public string NotaPedidoFactura { get; set; }
        [Column("ValorAdicional", TypeName = "decimal(18,2)")]
        public decimal ValorAdicional { get; set; }
    }
}
