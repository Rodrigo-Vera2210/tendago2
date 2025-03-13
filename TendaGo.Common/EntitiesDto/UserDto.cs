using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class UserDto
    {
        public string InicioSesion { get; set; }

        public int IdEmpresa { get; set; }

        public short IdPerfil { get; set; }

        public string Nombres { get; set; }

        public string Identificacion { get; set; }

        public bool? Sexo { get; set; }

        public string Direccion { get; set; }

        public string Correo { get; set; }

        //public string Contraseña { get; set; }

        public string Telefono { get; set; }

        public byte[] Foto { get; set; }

        public string IpIngreso { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string IpModificacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public short IdEstado { get; set; }

        public string Token { get; set; }
        public string AuthToken { get; set; }

        public string RaizArchivo { get; set; }

        public int DefaultLocal { get; set; }

    }
}