﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class UsuarioLocalBodega_LoadAllResult
    {
        public int ID { get; set; }
        public string INICIOSESION { get; set; }
        public int IDLOCALBODEGA { get; set; }
        public string IPINGRESO { get; set; }
        public string USUARIOINGRESO { get; set; }
        public DateTime FECHAINGRESO { get; set; }
        public string IPMODIFICACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public short IDESTADO { get; set; }
    }
}
