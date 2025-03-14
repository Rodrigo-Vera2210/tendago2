using ER.BE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.Api
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioService _usuario;

        public TokenAuthorizeAttribute(IHttpContextAccessor httpContextAccessor, IUsuarioService usuario)
        {
            _httpContextAccessor = httpContextAccessor;
            _usuario = usuario;
        }

        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            if (!IsAnonymous(actionContext) && !IsApiKey(actionContext))
            {
                try
                {
                     var header = actionContext.HttpContext.Request.Headers["app_token"];

                    if (!string.IsNullOrEmpty(header))
                    {
                        var token = header.FirstOrDefault();

                        if (!string.IsNullOrEmpty(token))
                        {
                            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated && token == _httpContextAccessor.HttpContext.User.GetToken())
                            {
                                return; // OK
                            }

                            var user =  _usuario.LoadByToken(token).Result;

                            if (user?.IdEstado == 1)
                            {
                                _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(user.ToPrincipal("token"));
                                return; // OK;
                            }
                            else
                            {
                                actionContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult("Acceso no authorizado");
                            }
                        }
                    }

                    actionContext.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                }
                catch (Exception ex)
                {
                    actionContext.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                }
               
            }

            actionContext.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
        }

        private bool IsAnonymous(AuthorizationFilterContext actionContext)
        {
            var attributes = actionContext.Filters.OfType<AllowAnonymousAttribute>();
            return attributes.Any();
        }

        private bool IsApiKey(AuthorizationFilterContext actionContext)
        {
            var attributes = actionContext.Filters.OfType<ApiKeyAuthorizeAttribute>();
            return attributes.Any();
        }
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