﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class Vehiculo
{
    public int Id { get; set; }

    public string Placa { get; set; }

    public string Marca { get; set; }

    public string Modelo { get; set; }

    public string Observacion { get; set; }

    public bool IdEstado { get; set; }

    public int IdEmpresa { get; set; }

    public virtual ICollection<GuiaRemision> GuiaRemision { get; set; } = new List<GuiaRemision>();
}