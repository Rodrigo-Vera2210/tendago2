﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class Ciudad
{
    public int Id { get; set; }

    public int IdProvincia { get; set; }

    public string Ciudad1 { get; set; }

    public string Codigo { get; set; }

    public string IpIngreso { get; set; }

    public string UsuarioIngreso { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string IpModificacion { get; set; }

    public string UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public short IdEstado { get; set; }

    public decimal? Longitud { get; set; }

    public decimal? Latitud { get; set; }

    public virtual ICollection<Entidad> Entidad { get; set; } = new List<Entidad>();

    public virtual Provincia IdProvinciaNavigation { get; set; }
}