﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class DisplayDto
    {
        public short Id { get; set; }
        public short IdModulo { get; set; }
        public short? IdGrupo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreAssembly { get; set; }
        public string Icono { get; set; }
        public string Ayuda { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public int Orden { get; set; }
    }
}