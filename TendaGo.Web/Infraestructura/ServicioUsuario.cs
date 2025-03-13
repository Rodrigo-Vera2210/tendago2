using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TendaGo.Web.ApplicationServices;

namespace TendaGo.Web.Infraestructura
{
    public class ServicioUsuario : IServicioUsuario
    {
        public Usuario ConsultarUsuarioPorNombre(string nombreUsuario)
        {
            UsuarioService.UsuarioServiceClient servicio = new UsuarioService.UsuarioServiceClient();
            var usuarioDto = servicio.ConsultarUsuarioPorInicioSesion(nombreUsuario);

            return new Usuario
            {
                Id = usuarioDto.InicioSesion,
                UserName = usuarioDto.InicioSesion,
                Password = usuarioDto.Contraseña,
                Nombre = usuarioDto.Nombres,
                Empresa = new Empresa { IdEmpresa = usuarioDto.Empresa.Id, NombreEmpresa = usuarioDto.Empresa.Nombre, RaizArchivo = usuarioDto.Empresa.RaizArchivo,Estado = true },
                LocalBodega = new LocalBodega { IdLocal = 1, Nombre = "PRINCIPAL", Estado = true },
                Estado = true,
                Token = usuarioDto.Token,
                Tipo = usuarioDto.Perfil.Nombre
            };
        }
    }
}