﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ER.DA.Models
{
    public partial class PantallaXPerfil_DeepFindByAllResult
    {
        public short Id { get; set; }
        public short IdPerfil { get; set; }
        public short IdPantalla { get; set; }
        public bool Guardar { get; set; }
        public bool Modificar { get; set; }
        public bool Eliminar { get; set; }
        public bool Consultar { get; set; }
        public bool VistaPreliminar { get; set; }
        public bool Imprimir { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public short Id_PantallaFromIdPantalla { get; set; }
        public short IdModulo_PantallaFromIdPantalla { get; set; }
        public short? IdGrupo_PantallaFromIdPantalla { get; set; }
        public string Nombre_PantallaFromIdPantalla { get; set; }
        public string Descripcion_PantallaFromIdPantalla { get; set; }
        public string NombreAssembly_PantallaFromIdPantalla { get; set; }
        public string Icono_PantallaFromIdPantalla { get; set; }
        public string Ayuda_PantallaFromIdPantalla { get; set; }
        public string IpIngreso_PantallaFromIdPantalla { get; set; }
        public string UsuarioIngreso_PantallaFromIdPantalla { get; set; }
        public DateTime FechaIngreso_PantallaFromIdPantalla { get; set; }
        public string IpModificacion_PantallaFromIdPantalla { get; set; }
        public string UsuarioModificacion_PantallaFromIdPantalla { get; set; }
        public DateTime? FechaModificacion_PantallaFromIdPantalla { get; set; }
        public short IdEstado_PantallaFromIdPantalla { get; set; }
        public int Orden_PantallaFromIdPantalla { get; set; }
        public short Id_PerfilFromIdPerfil { get; set; }
        public string Perfil_PerfilFromIdPerfil { get; set; }
        public string IpIngreso_PerfilFromIdPerfil { get; set; }
        public string UsuarioIngreso_PerfilFromIdPerfil { get; set; }
        public DateTime FechaIngreso_PerfilFromIdPerfil { get; set; }
        public string IpModificacion_PerfilFromIdPerfil { get; set; }
        public string UsuarioModificacion_PerfilFromIdPerfil { get; set; }
        public DateTime? FechaModificacion_PerfilFromIdPerfil { get; set; }
        public short IdEstado_PerfilFromIdPerfil { get; set; }
    }
}
