using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Web;
using System.Collections.Generic;

namespace TendaGo.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Usuario : IUser<string>
    {
        public string Id => UserName;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int IdEmpresa { get; set; }
        public int IdLocal { get; set; }
        public bool Estado { get; set; }
        public string Token { get; set; }
        public string AuthToken { get; set; }
        public int IdPerfil { get; set; }
        public string RaizArchivo { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Usuario FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Usuario>(json);
        }

        /// <summary>
        /// Determines if the user is logged in
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(this.Id) || string.IsNullOrEmpty(this.Nombre))
                return true;

            return false;
        }

        public static Usuario Actual
        {
            get
            {
                try
                {
                    var claimsIdentity = (HttpContext.Current.User.Identity as ClaimsIdentity);

                    var userData = claimsIdentity?.FindFirst("UserData")?.Value;

                    if (userData != null)
                    {
                        var data = Encoding.UTF8.GetString(Convert.FromBase64String(userData));
                        return FromJson(data);
                    }
                }
                catch (Exception) { }

                return default;
            }
        }

        public ClaimsIdentity ToClaims(string authenticationType)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, Id));
            claims.Add(new Claim(ClaimTypes.Name, UserName));
            claims.Add(new Claim("NombreUsuario", Nombre));
            claims.Add(new Claim("IdEmpresa", IdEmpresa.ToString()));
            claims.Add(new Claim("IdLocal", IdLocal.ToString()));
            claims.Add(new Claim("IdPerfil", IdPerfil.ToString()));
            claims.Add(new Claim("Token", Token?.ToString() ?? ""));
            claims.Add(new Claim("AuthToken", AuthToken?.ToString() ?? ""));
            claims.Add(new Claim("Estado", Convert.ToInt32(Estado).ToString()));
            claims.Add(new Claim("RaizArchivo", RaizArchivo));
            claims.Add(new Claim("UserData", Convert.ToBase64String(Encoding.UTF8.GetBytes(ToString()))));

            var claimsIdentity = new ClaimsIdentity(claims, authenticationType);

            return claimsIdentity;
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Usuario> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string RaizArchivo { get; set; }
        public bool Estado { get; set; }
    }

    public class LocalBodega
    {
        public int IdLocal { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }



    public static class IdentityExtensions
    {

        public static string GetClaim(this IIdentity identity, string claimName)
        {
            var claimIdentity = (identity as ClaimsIdentity);
            return claimIdentity?.FindFirst(claimName)?.Value;
        }

        public static string GetNombreUsuario(this IIdentity identity)
        {
            return identity.GetClaim("NombreUsuario");
        }

        public static int GetIdEmpresa(this IIdentity identity)
        {
            return identity.GetClaim("IdEmpresa").ToInt32();
        }

        public static int GetIdLocal(this IIdentity identity)
        {
            return identity.GetClaim("IdLocal").ToInt32();
        }

        public static string GetCarpetaRaizEmpresa(this IIdentity identity)
        {
            return identity.GetClaim("RaizArchivo");
        }

        public static string GetTokenUsuario(this IIdentity identity)
        {
            return identity.GetClaim("Token");
        }

        public static string GetAuthTokenUsuario(this IIdentity identity)
        {
            return identity.GetClaim("AuthToken");
        }

        public static Usuario ToUsuario(this IIdentity identity)
        {
            var userData = identity.GetClaim("UserData");

            return Usuario.FromJson(userData);
        }

    }
}