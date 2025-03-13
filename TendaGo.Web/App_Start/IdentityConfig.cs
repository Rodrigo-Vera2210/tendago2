using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TendaGo.Web.Models;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.ApplicationServices;
using TendaGo.Common;

namespace TendaGo.Web
{
    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your email service here to send an email.
    //        return Task.FromResult(0);
    //    }
    //}

    //public class SmsService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your SMS service here to send a text message.
    //        return Task.FromResult(0);
    //    }
    //}

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class CustomApplicationUserManager : UserManager<Usuario, string>
    {
        public CustomApplicationUserManager(IUserStore<Usuario, string> store)
            : base(store)
        {
        }

        public static CustomApplicationUserManager Create(IdentityFactoryOptions<CustomApplicationUserManager> options, IOwinContext context)
        {
            var manager = new CustomApplicationUserManager(new CustomUserStore());
            //// Configure validation logic for usernames
            //manager.UserValidator = new UserValidator<CustomApplicationUser>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};

            //// Configure validation logic for passwords
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};

            //// Configure user lockout defaults
            //manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            //// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            //// You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<CustomApplicationUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<CustomApplicationUser>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<Usuario>(dataProtectionProvider.Create("ASP.NET Identity"))
            //        {
            //            TokenLifespan = TimeSpan.FromDays(15)
            //        };
            //}
            return manager;
        }

        public override Task<bool> CheckPasswordAsync(Usuario user, string password)
        {
            //Usuario usuario = this.FindByName(user.UserName);
            //if (usuario == null) Task.FromResult(false);
            return Task.FromResult<bool>(user.Estado);
        }

        public override Task<ClaimsIdentity> CreateIdentityAsync(Usuario user, string authenticationType)
        {
            var identity = user.ToClaims(DefaultAuthenticationTypes.ApplicationCookie);

            return Task.FromResult(identity);
        }

    }

    // Configure the application sign-in manager which is used in this application.
    public class CustomApplicationSignInManager : SignInManager<Usuario, string>
    {
        public CustomApplicationSignInManager(CustomApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static CustomApplicationSignInManager Create(IdentityFactoryOptions<CustomApplicationSignInManager> options, IOwinContext context)
        {
            return new CustomApplicationSignInManager(context.GetUserManager<CustomApplicationUserManager>(), context.Authentication);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (UserManager == null)
                return SignInStatus.Failure;

            //var usuario = await UserManager.FindByNameAsync(userName);
            UserDto usuarioDto = null;

            try
            {
               usuarioDto = UserAppService.LoginUser(userName, password);
            }
            catch
            {
                return SignInStatus.Failure;
            }

            if (usuarioDto == null)
                return SignInStatus.Failure;

            var usuario = usuarioDto.ToUsuario();
              
            if (usuario.Estado == false)
                return SignInStatus.RequiresVerification;

            if (await UserManager.CheckPasswordAsync(usuario, password))
            {
                await SignInAsync(usuario, isPersistent, false);

                // Asigno el token del usuario autenticado
                AppServiceBase.AppToken = usuario.Token;
                AppServiceBase.ApiToken = usuario.AuthToken;

                // Lo movi a este lugar por seguridad:
                AppServiceBase.AppUser = usuario;
                return SignInStatus.Success;
            }
            return SignInStatus.Failure;
        } 
    }

    public class CustomUserStore : IUserStore<Usuario, string>
    { 

        public Task CreateAsync(Usuario user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Usuario user)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindByIdAsync(string userId)
        {
            return FindByNameAsync(userId);
        }

        public Task<Usuario> FindByNameAsync(string userName)
        {
            return Task.FromResult(UserAppService.BuscarUsuarioAsync(userName).ToUsuario());
        }

        public Task UpdateAsync(Usuario user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
