using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace TendaGo.Web.Controllers
{
    [AllowAnonymous]
    public class authController : Controller
    {
        public ActionResult login(string returnUrl)
        {
            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult Index(string returnUrl = null, string message = null)
        {
            if (string.IsNullOrEmpty(returnUrl) || returnUrl.ToLower().Contains("login") || returnUrl.ToLower().Contains("auth"))
            {
                returnUrl = Url.Content("~/");
            }

            if (Request.IsAuthenticated)
                IdentitySignOut();

            if (!string.IsNullOrEmpty(message))
                ModelState.AddModelError("", message);

            Session["ReturnUrl"] = ViewBag.ReturnUrl = Url.Content(Url.IsLocalUrl(returnUrl) ? returnUrl : "~/");

            var model = new LoginViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Index(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return await Task.FromResult(Json(new { redirect = false, modelError = messages }, JsonRequestBehavior.AllowGet));
                }

                var user = UserAppService.LoginUser(model.Usuario, model.Password);

                if (Request.Url.OriginalString.Contains("grupogiraldo") && user.IdEmpresa != 1)
                {
                    user = null;
                }

                if (user != null)
                {
                    await IdentitySignIn(user.ToUsuario(), model.CierraSesion);
                     
                    if (AppServiceBase.Permisos.Any())
                    {
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
                        
                        Session.Timeout = 80000;
                        
                        var urlSuccess = GetRedirectToLocal(returnUrl);
                        return await Task.FromResult(Json(new { redirect = true, redirectUrl = urlSuccess }, JsonRequestBehavior.AllowGet));
                    }

                    throw new Exception("Ocurrio un error al recuperar los permisos del usuario.");

                }

                return await Task.FromResult(Json(new { redirect = false, modelError = "Intento de login fallido" }, JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Json(new { redirect = false, modelError = ex.Message }, JsonRequestBehavior.AllowGet));
            }
        }


        public ActionResult logout()
        {
            IdentitySignOut();
            return RedirectToAction("index");

        }


        private string GetRedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }
            return Url.Action("Index", "Productos");
        }


        // **** Helpers 

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "5c4r13t3_$31!.2*#";

        public class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                    properties.Dictionary[XsrfKey] = UserId;

                var owin = context.HttpContext.GetOwinContext();
                owin.Authentication.Challenge(properties, LoginProvider);
            }
        }





        #region "Helper Methods"
        private Task IdentitySignIn(Usuario user, bool isPersistent = false)
        {
            var identity = user?.ToClaims(DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = false,
                IsPersistent = isPersistent,
                ExpiresUtc = isPersistent ? DateTime.UtcNow.AddDays(5) : DateTime.UtcNow.AddMinutes(59)
            }, identity);
             
            Thread.CurrentPrincipal = HttpContext.User = new ClaimsPrincipal(identity);

            AppServiceBase.AppToken = user.Token;
            AppServiceBase.ApiToken = user.AuthToken;
            AppServiceBase.AppUser = user;

            return Task.CompletedTask;
        }

        private void IdentitySignOut()
        {
            Session.Clear();
            Session.Abandon();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Thread.CurrentPrincipal = HttpContext.User = null;
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        #endregion





        #region External Providers [Google, Facebook, Twitter, etc]


        // POST: /Account/ExternalLogin
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "~/";

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
                return RedirectToAction("LogOn");

            // AUTHENTICATED!
            var providerKey = loginInfo.Login.ProviderKey;


            // Aplication specific code goes here.

            //var user = userBus.ValidateUserWithExternalLogin(providerKey);
            var user = UserAppService.BuscarUsuarioAsync(providerKey);

            if (user == null)
            {
                return RedirectToAction("LogOn", new
                {
                    message = $"No es posible iniciar sesión con {loginInfo.Login.LoginProvider}. "
                });
            }

            // write the authentication cookie
            await IdentitySignIn(user.ToUsuario(), isPersistent: true);

            return Redirect(returnUrl);
        }



        // Initiate oAuth call for external Login
        // GET: /Account/ExternalLinkLogin
        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLinkLogin(string provider)
        //{
        //    var id = Request.Form["Id"];

        //    // create an empty AppUser with a new generated id
        //    AppUserState.UserId = id;
        //    AppUserState.Name = "";
        //    IdentitySignin(AppUserState, null);

        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new ChallengeResult(provider, Url.Action("ExternalLinkLoginCallback"), AppUserState.UserId);
        //}

        // oAuth Callback for external login
        // GET: /Manage/LinkLogin
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ExternalLinkLoginCallback()
        {
            // Handle external Login Callback
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, "1");
            if (loginInfo == null)
            {
                IdentitySignOut(); // to be safe we log out
                return RedirectToAction("Register", new { message = "Unable to authenticate with external login." });
            }

            // Authenticated!
            string providerKey = loginInfo.Login.ProviderKey;
            string providerName = loginInfo.Login.LoginProvider;

            var user = UserAppService.BuscarUsuarioAsync(loginInfo.Login.ProviderKey);

            // Now load, create or update our custom user

            //// normalize email and username if available
            //if (string.IsNullOrEmpty(AppUserState.Email))
            //    AppUserState.Email = loginInfo.Email;
            //if (string.IsNullOrEmpty(AppUserState.Name))
            //    AppUserState.Name = loginInfo.DefaultUserName;

            //var userBus = new busUser();
            //User user = null;

            //if (!string.IsNullOrEmpty(AppUserState.UserId))
            //    user = userBus.Load(AppUserState.UserId);

            //if (user == null && !string.IsNullOrEmpty(providerKey))
            //    user = userBus.LoadUserByProviderKey(providerKey);

            //if (user == null && !string.IsNullOrEmpty(loginInfo.Email))
            //    user = userBus.LoadUserByEmail(loginInfo.Email);

            //if (user == null)
            //{
            //    user = userBus.NewEntity();
            //    userBus.SetUserForEmailValidation(user);
            //}

            //if (string.IsNullOrEmpty(user.Email))
            //    user.Email = AppUserState.Email;

            //if (string.IsNullOrEmpty(user.Name))
            //    user.Name = AppUserState.Name ?? "Unknown (" + providerName + ")";


            //Update the database with Provider key and login provider.
            //if (loginInfo.Login != null)
            //{
            //    user.OpenIdClaim = loginInfo.Login.ProviderKey;
            //    user.OpenId = loginInfo.Login.LoginProvider;
            //}
            //else
            //{
            //    user.OpenId = null;
            //    user.OpenIdClaim = null;
            //}

            // finally save user inf
            // bool result = userBus.Save(user);

            // update the actual identity cookie
            //  AppUserState.FromUser(user);
            await IdentitySignIn(user.ToUsuario(), true);

            return RedirectToAction("Register");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalUnlinkLogin()
        //{
        //    var userId = AppUserState.UserId;
        //    var user = busUser.Load(userId);
        //    if (user == null)
        //    {
        //        ErrorDisplay.ShowError("Couldn't find associated User: " + busUser.ErrorMessage);
        //        return RedirectToAction("Register", new { id = userId });
        //    }
        //    user.OpenId = string.Empty;
        //    user.OpenIdClaim = string.Empty;

        //    if (busUser.Save())
        //        return RedirectToAction("Register", new { id = userId });

        //    return RedirectToAction("Register", new { message = "Unable to unlink OpenId. " + busUser.ErrorMessage });
        //}

        #endregion


    }
}