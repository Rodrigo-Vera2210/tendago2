﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class Modulo
{
    public short Id { get; set; }

    public string Modulo1 { get; set; }

    public string Icono { get; set; }

    public string IpIngreso { get; set; }

    public string UsuarioIngreso { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string IpModificacion { get; set; }

    public string UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public short IdEstado { get; set; }

    public int? Orden { get; set; }

    public virtual ICollection<Pantalla> Pantalla { get; set; } = new List<Pantalla>();
}