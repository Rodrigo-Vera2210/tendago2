﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ER.DA.Models;

public partial class EstadoEntrada
{
    public long Id { get; set; }

    public string Entrada { get; set; }

    public string Usuario { get; set; }

    public string Estado { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Entrada EntradaNavigation { get; set; }
}