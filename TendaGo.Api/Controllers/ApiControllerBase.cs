using ER.BA;
using ER.BE;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Domain.Models;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    /// <summary>
    /// Api controller base
    /// </summary>
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]/")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public ApiControllerBase(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        protected UsuarioDTO CurrentUser => GetAuthenticatedUser(); 

        private UsuarioDTO GetAuthenticatedUser()
        {

            if (Request.Headers.TryGetValue("app_token", out var tokenValues))
            {
                var token = tokenValues.FirstOrDefault();

                if (!string.IsNullOrEmpty(token))
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        var usuarioEntity = _usuarioService.LoadByToken(token).Result;

                        if (usuarioEntity != null)
                            return usuarioEntity;

                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El usuario solicitado no existe o el token no es válido") });
                    }
                }
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Acceso no Autorizado") });
        }

        private ObjectResult UnauthorizedResponse(string message)
        {
            return StatusCode((int)HttpStatusCode.Unauthorized, new { error = message });
        }

        protected void Log(string source, params object[] messages)
        {
            CommonFunctions.Log(source, messages);
        }

    }
}