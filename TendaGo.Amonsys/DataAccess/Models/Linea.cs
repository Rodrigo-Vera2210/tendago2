﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class Linea
{
    public int Id { get; set; }

    public int IdEmpresa { get; set; }

    public int IdDivision { get; set; }

    public string Linea1 { get; set; }

    public string IpIngreso { get; set; }

    public string UsuarioIngreso { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string IpModificacion { get; set; }

    public string UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public short IdEstado { get; set; }

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();

    public virtual Division IdDivisionNavigation { get; set; }

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}