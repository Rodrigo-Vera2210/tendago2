using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Models;
using TendaGo.Api.Models.EntitiesDto;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private const int IdModuloNotaPedido = 3;
        private CustomApplicationSignInManager _signInManager;

        public CustomApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<CustomApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || returnUrl.ToLower().Contains("login"))
            {
                returnUrl = Url.Content("~/");
            }

            //AppServiceBase.AppToken = User.Identity.GetTokenUsuario();
            
            Session.Clear();
            Session.Abandon();

            if (User.Identity.IsAuthenticated)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }

            Session["ReturnUrl"] = ViewBag.ReturnUrl = Url.Content(Url.IsLocalUrl(returnUrl) ? returnUrl : "~/");

            return View(new LoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Index(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Json(new { redirect = false, modelError = messages }, JsonRequestBehavior.AllowGet);
                }

                var result = await SignInManager.PasswordSignInAsync(model.Usuario, model.Password, model.CierraSesion, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        //AppServiceBase.AppToken = User.Identity.GetTokenUsuario();
                        //AppServiceBase.Permisos = PantallasAppService.ObtenerPantallasPerfil();
                        
                        if (AppServiceBase.Permisos.Any())
                        {
                            var modules = ModulosAppServices.ObtenerModulos();
                            Session.Add("Modules", modules);
                            var locales = UserAppService.ObtenerLocalesUsuario();
                            Session.Add("Locales", locales);

                            if (string.IsNullOrEmpty(returnUrl))
                            {
                                if (AppServiceBase.Permisos.Any(x => x.IdModulo == 3 && x.NombreAssembly.Contains("Index")))
                                {
                                    returnUrl = Url.Action("Index", "NotaPedido");
                                }
                                else
                                {
                                    returnUrl = Url.Action("Index", "Productos");
                                }
                            }
                            else if (returnUrl.Contains("NotaPedido") && !AppServiceBase.Permisos.Any(x => x.IdModulo == 3 && x.NombreAssembly.Contains("Index")))
                            {
                                returnUrl = Url.Action("Index", "Productos");
                            } 


                            var urlSuccess = GetRedirectToLocal(returnUrl);
                            return Json(new { redirect = true, redirectUrl = urlSuccess }, JsonRequestBehavior.AllowGet);
                        }

                        throw new Exception("Ocurrio un error al recuperar los permisos del usuario.");
                        
                    case SignInStatus.LockedOut:
                        var urlLocked = Url.Action("Lockout");
                        return Json(new { redirect = true, redirectUrl = urlLocked }, JsonRequestBehavior.AllowGet);
                    case SignInStatus.Failure:
                    default:
                        return Json(new { redirect = false, modelError = "Intento de login fallido" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Login");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private string GetRedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }
            return Url.Action("Index", "Productos");
        }
    }
}