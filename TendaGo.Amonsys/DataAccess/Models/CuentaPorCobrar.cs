﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class CuentaPorCobrar
{
    public int Id { get; set; }

    public string IdSalida { get; set; }

    public int Numero { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public decimal Valor { get; set; }

    public decimal? Saldo { get; set; }

    public string IpIngreso { get; set; }

    public string UsuarioIngreso { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string IpModificacion { get; set; }

    public string UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public short IdEstado { get; set; }

    public virtual ICollection<DetalleCobro> DetalleCobro { get; set; } = new List<DetalleCobro>();

    public virtual Salida IdSalidaNavigation { get; set; }
}