namespace TendaGo.Api.Controllers
{
    /// <summary>
    /// Api controller base
    /// </summary>
    public class ApiControllerBase : ApiController
    {

        /// <summary>
        /// Usuario Actual
        /// </summary>
        protected UsuarioEntity CurrentUser => GetAuthenticatedUser();


        /// <summary>
        /// Valida al usuario autenticado
        /// </summary>
        /// <returns></returns>
        private UsuarioEntity GetAuthenticatedUser()
        {
            IEnumerable<string> header = null;

            if (Request.Headers.TryGetValues("app_token", out header))
            {
                var token = header.FirstOrDefault();

                if (!string.IsNullOrEmpty(token))
                {
                    if (string.IsNullOrEmpty(token))
                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El token de inicio de sesion no es válido"));

                    var usuarioEntity = UsuarioBussinesAction.LoadByToken(token);

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