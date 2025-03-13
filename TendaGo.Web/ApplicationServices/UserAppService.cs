using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using TendaGo.Common;
using RestSharp;
using TendaGo.Web.Infraestructura;
using Newtonsoft.Json;
using TendaGo.Web.Models;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text;
using TendaGo.Common.RequestModels;
using TendaGo.Common.EntitiesDto;

namespace TendaGo.Web.ApplicationServices
{
    public class UserAppService : AppServiceBase
    {

        //public const string baseAuthUrl = "https://api.binasystem.com/auth/v1/";


        public static WarehouseDto ObtenerLocal(int id)
        {
            return ObtenerLocalesUsuario().FirstOrDefault(x=>x.Id == id);
        }
        
        public static List<WarehouseDto> ObtenerLocalesUsuario()
        {
            var locales = new List<WarehouseDto>();
            var request = new RestRequest("/user/warehouses", Method.GET);
            var restResponse = DefaultClient.Execute<List<WarehouseDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                locales = restResponse.Data;
            }
            return locales;
        }

        public static CompanyDto ObtenerEmpresa()
        { 
            var request = new RestRequest("/user/company", Method.GET);
            var restResponse = DefaultClient.Execute<CompanyDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }
            return default;
        }

        public static List<SellerDto> BuscarVendedores(string searchTerm)
        {
            var vendedores = new List<SellerDto>();
            var request = new RestRequest($"/sellers/search/{searchTerm}", Method.GET);
            var restResponse = DefaultClient.Execute<List<SellerDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                vendedores = restResponse.Data;
            }
            return vendedores;
        }



        public static UserDto LoginUser(string username, string password)
        {
            UserDto user = null;

            var loginRequest = new LoginModel() { User = username, Password = password };
            var request = new RestRequest("/user/login", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(loginRequest);


            var loginResponse = DefaultClient.Execute<UserDto>(request);
            var authResponse = LoginApiAuth(username, password);

            if (loginResponse.StatusCode.Equals(HttpStatusCode.OK) && authResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var userAuth = JsonConvert.DeserializeObject<ResultDto<AuthDto>>(authResponse.Content);
                user = JsonConvert.DeserializeObject<UserDto>(loginResponse.Content);
                user.AuthToken = userAuth.Result.Token;
                return user;
            }

            var errorMsg = loginResponse?.ErrorMessage ?? authResponse?.ErrorMessage;

            if (!string.IsNullOrEmpty(loginResponse?.Content) || !string.IsNullOrEmpty(authResponse?.Content))
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(loginResponse?.Content);
                if (string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
                {
                    var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(loginResponse?.Content);
                    errorMsg = genericApiResponse.Message;
                }
            }

            throw new ApplicationServicesException(errorMsg);
        }


        public static IRestResponse<ResultDto<AuthDto>> LoginApiAuth(string username, string password)
        {
            var appId = "601194F8-132C-4A4A-806A-EE986A1324B6";
            //var client = new RestClient(baseAuthUrl);
            var loginRequest = new AuthenticationRequest() { AppId = appId, DeviceId = "string", Latitud = "string", Longitud = "string" };
            var request = new RestRequest("/login", Method.POST);

            var credentials = $"{username}:{password}";
            var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            request.AddHeader("Authorization", $"Basic {base64Credentials}");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(loginRequest);

            var result = AuthClient.Execute<ResultDto<AuthDto>>(request);
            return result;
        }



        internal static UserDto BuscarUsuarioAsync(string username)
        { 
            var request = new RestRequest($"/profile/user/{username}", Method.GET);
            request.AddHeader("Authorization", $"ApiKey {Tools.GetApiKey()}");

            var restResponse = new RestClient().Execute<UserDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }

            return default;
        }



        internal static List<ProfileDto> ObtenerPerfiles()
        {
            var profiles = new List<ProfileDto>();
            var request = new RestRequest("/profile", Method.GET);

            var restResponse = DefaultClient.Execute<List<ProfileDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                profiles = restResponse.Data;
            }
            return profiles;
        }

        internal static List<DisplayDto> ObtenerPantallasPorModulo(short idModulo)
        {
            var displays = new List<DisplayDto>();
            var request = new RestRequest($"/profile/{idModulo}/displays", Method.GET);

            var restResponse = DefaultClient.Execute<List<DisplayDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                displays = restResponse.Data;
            }
            return displays;
        }

        internal static List<PaymentMethodDto> ObtenerMetodosPago()
        {
            var profiles = new List<PaymentMethodDto>();
            var request = new RestRequest("/paymentMethods", Method.GET);

            var restResponse = DefaultClient.Execute<List<PaymentMethodDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                profiles = restResponse.Data;
            }
            return profiles;
        }

        internal static PaymentMethodDto ObtenerMetodosPagoByPK(int id)
        {
            var profiles = new PaymentMethodDto();
            var request = new RestRequest("/getPaymentMethodsByPK/" + id.ToString().Trim(), Method.GET);

            var restResponse = DefaultClient.Execute<PaymentMethodDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                profiles = restResponse.Data;
            }
            return profiles;
        }

        internal static PaymentMethodDto GuardarMetodosPago(PaymentMethodDto model)
        { 
            var request = new RestRequest("/paymentMethods", Method.POST);
            request.AddJsonBody(model);
            var restResponse = DefaultClient.Execute<PaymentMethodDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }
            return null;
        }
        internal static PaymentMethodDto BorrarMetodosPago(PaymentMethodDto model)
        {
            var request = new RestRequest("/paymentMethods", Method.DELETE);
            request.AddJsonBody(model);
            var restResponse = DefaultClient.Execute<PaymentMethodDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }
            return null;
        }

        internal static UserDto RegistroAsync(UserDto user)
        {
            throw new NotImplementedException();
        }
         
        internal static UserDto ActualizarPerfilAsync(UserDto user)
        {
            throw new NotImplementedException();
        }
        internal static UserDto ActualizarContraseña(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}