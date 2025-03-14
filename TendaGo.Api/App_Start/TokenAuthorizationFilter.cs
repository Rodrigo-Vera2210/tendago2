using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TendaGo.Domain.Services;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;

namespace TendaGo.Api
{
    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioService _usuario;

        public TokenAuthorizationFilter(IHttpContextAccessor httpContextAccessor, IUsuarioService usuario)
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

                            var user = _usuario.LoadByToken(token).Result;

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
}
