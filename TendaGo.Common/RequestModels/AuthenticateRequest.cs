using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common.RequestModels
{
    public class AuthenticationRequest
    {
        [Required]
        public string AppId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }

    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public string Correo { get; set; }
        public string Alias { get; set; }
        public string Token { get; set; }

        //public AuthenticationResponse(IUserEntity user, string token)
        //{
        //    Id = user.Identificacion;
        //    Nombre = user.Nombre;
        //    Alias = user.InicioSesion;
        //    Correo = user.Correo;

        //    Token = token;
        //}
    }
}
