﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class SerieSalida
{
    public int Id { get; set; }

    public string IdDetalleSalida { get; set; }

    public int IdProducto { get; set; }

    public string Serie { get; set; }

    public string IpIngreso { get; set; }

    public string UsuarioIngreso { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string IpModificacion { get; set; }

    public string UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public short IdEstado { get; set; }

    public int IdEmpresa { get; set; }
}