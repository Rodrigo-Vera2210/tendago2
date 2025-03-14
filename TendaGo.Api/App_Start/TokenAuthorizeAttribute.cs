using ER.BE;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TendaGo.BusinessLogic.Services;
using TendaGo.Common;
using TendaGo.Domain.Models;
using TendaGo.Domain.Services;

namespace TendaGo.Api
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        

        
    }

    public class ApiKeyAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (token != null)
            {
                var app = authorized_apps.FirstOrDefault(x => x.ApiKey?.ToLower() == token?.ToLower());

                if (app != null)
                {
                    return; // OK
                }
            }

            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
        }

        private static readonly List<ApplicationDto> authorized_apps =
            new List<ApplicationDto>
            {
            new ApplicationDto{ ApiKey="776125dc-4ab3-4f4a-877c-6e65baea8370", AppName= "Go Web" },
            new ApplicationDto{ ApiKey="23633afd-a0b3-4a88-b7a0-9ce1a5e4a00a", AppName= "Go Mobile" },
            };
    }

    public class AppTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUsuarioService _usuario;

        public AppTokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUsuarioService usuario)
            : base(options, logger, encoder, clock)
        {
            _usuario = usuario;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("app_token"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token no encontrado"));
            }

            var token = Request.Headers["app_token"].ToString();

            if (string.IsNullOrEmpty(token) || !IsValidToken(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token inválido"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "User")
            };

            var identity = new ClaimsIdentity(claims, "AppToken");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "AppToken");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private bool IsValidToken(string token)
        {

            var user = _usuario.LoadByToken(token).Result;

            if (user == null || user.IdEstado != 1) return false;

            return true;  // Ejemplo de validación
        }
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

        public static ClaimsPrincipal ToPrincipal(this UsuarioDTO user, string authenticationType)
        {
            return new ClaimsPrincipal(user.ToClaims(authenticationType));
        }

        public static ClaimsIdentity ToClaims(this UsuarioDTO user, string authenticationType)
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