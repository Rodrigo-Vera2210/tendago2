using ER.BA;
using ER.BE;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    /// <summary>
    /// Api controller base
    /// </summary>
    public class ApiControllerBase : ApiController
    {
        private readonly IUsuarioService _usuarioService;

        public ApiControllerBase(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Usuario Actual
        /// </summary>
        protected async Task<UsuarioEntity> CurrentUser () { return await GetAuthenticatedUser(); }


        /// <summary>
        /// Valida al usuario autenticado
        /// </summary>
        /// <returns></returns>
        private async Task<UsuarioEntity> GetAuthenticatedUser()
        {
            IEnumerable<string> header = null;

            if (Request.Headers.TryGetValues("app_token", out header))
            {
                var token = header.FirstOrDefault();

                if (!string.IsNullOrEmpty(token))
                {
                    if (string.IsNullOrEmpty(token))
                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El token de inicio de sesion no es válido"));

                    var usuarioEntity = await _usuarioService.LoadByToken(token);

                    if (usuarioEntity != null)
                        return usuarioEntity;

                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El usuario solicitado no existe o el token no es válido"));
                }
            }

            throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.Unauthorized, "Acceso no Autorizado"));
        }

        protected void Log(string source, params object[] messages)
        {
            CommonFunctions.Log(source, messages);
        }

    }
}