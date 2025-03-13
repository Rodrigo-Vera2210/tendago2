using ER.BA;
using ER.BE;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using TendaGo.Common;

namespace TendaGo.Api
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!IsAnonymous(actionContext) && !IsApiKey(actionContext))
            {
                IEnumerable<string> header = null;

                if (actionContext.Request.Headers.TryGetValues("app_token", out header))
                {
                    var token = header.FirstOrDefault();

                    if (!string.IsNullOrEmpty(token))
                    {
                        if (HttpContext.Current.User.Identity.IsAuthenticated && token == HttpContext.Current.User.GetToken())
                        {
                            return; // OK
                        }

                        var user = UsuarioBussinesAction.LoadByToken(token);

                        if (user?.IdEstado == 1)
                        {
                            HttpContext.Current.User = Thread.CurrentPrincipal = user.ToPrincipal("token");
                            return; // OK;
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request.BuildHttpErrorResponse(HttpStatusCode.Forbidden, "Acceso no permitido");
                        }
                    }
                }

                actionContext.Response = actionContext.Request.BuildHttpErrorResponse(HttpStatusCode.Unauthorized, "Acceso no autorizado");
            }
        }

        private bool IsAnonymous(HttpActionContext actionContext)
        {
            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true);
            return attributes != null && attributes.Count > 0;
        }

        private bool IsApiKey(HttpActionContext actionContext)
        {
            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<ApiKeyAuthorizeAttribute>(true);
            return attributes != null && attributes.Count > 0;
        }
    }

    public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request?.Headers?.Authorization?.Parameter ?? actionContext.Request?.Headers?.Authorization?.Scheme;

            if (token != null)
            {
                var app = authorized_apps.FirstOrDefault(x => x.ApiKey?.ToLower() == token?.ToLower());

                if (app != null)
                {
                    return; // OK
                }
            }

            actionContext.Response = actionContext.Request.BuildHttpErrorResponse(HttpStatusCode.Unauthorized, "No Autorizado");

        }

        private static readonly List<ApplicationDto> authorized_apps =
            new List<ApplicationDto>
            {
                new ApplicationDto{ ApiKey="776125dc-4ab3-4f4a-877c-6e65baea8370", AppName= "Go Web" },
                new ApplicationDto{ ApiKey="23633afd-a0b3-4a88-b7a0-9ce1a5e4a00a", AppName= "Go Mobile" },
            };
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string FindFirstValue(this IPrincipal principal, string claim)
        {
            return (principal as ClaimsPrincipal)?.FindFirst(claim)?.Value;
        }

        public static string GetUserId(this IPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserName(this IPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static string GetIdEmpresa(this IPrincipal principal)
        {
            return principal.FindFirstValue("IdEmpresa");
        }

        public static string GetToken(this IPrincipal principal)
        {
            return principal.FindFirstValue("Token");
        }

        public static string GetEstado(this IPrincipal principal)
        {
            return principal.FindFirstValue("Estado");
        }


        public static UsuarioEntity ToUser(this IPrincipal principal)
        {
            var json = principal.FindFirstValue(ClaimTypes.UserData);

            if (!string.IsNullOrEmpty(json))
            {
                JsonConvert.DeserializeObject<UsuarioEntity>(json);
            }

            return default;
        }

        public static bool IsCurrentUser(this ClaimsPrincipal principal, string id)
        {
            var currentUserId = GetUserId(principal);

            return string.Equals(currentUserId, id, StringComparison.OrdinalIgnoreCase);
        }

        public static ClaimsPrincipal ToPrincipal(this UsuarioEntity user, string authenticationType)
        {
            return new ClaimsPrincipal(user.ToClaims(authenticationType));
        }

        public static ClaimsIdentity ToClaims(this UsuarioEntity user, string authenticationType)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Token));
            claims.Add(new Claim(ClaimTypes.Name, user.InicioSesion));
            claims.Add(new Claim(ClaimTypes.GivenName, user.Nombres));
            claims.Add(new Claim("IdEmpresa", user.IdEmpresa.ToString()));
            claims.Add(new Claim("Token", user.Token?.ToString() ?? ""));
            claims.Add(new Claim("Estado", user.IdEstado.ToString()));

            claims.Add(new Claim(ClaimTypes.UserData, Convert.ToBase64String(Encoding.UTF8.GetBytes(user.ToJson()))));

            var claimsIdentity = new ClaimsIdentity(claims, authenticationType);

            return claimsIdentity;
        }

    }
}