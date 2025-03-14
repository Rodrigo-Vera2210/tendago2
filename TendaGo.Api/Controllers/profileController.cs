//using ER.BA;
//using ER.BE;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web.Http;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    [TokenAuthorize]
//    public class profileController : ApiControllerBase
//    {
        

//        [HttpGet, Route("profile/{id}/displays")]
//        public List<DisplayDto> GetDisplaysByProfile(string id)
//        {
//            short idConverted;
//            bool isValidId = short.TryParse(id, out idConverted);
//            if (!isValidId)
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

//            var pantallas = PantallaCollectionBussinesAction.FindByAll(new PantallaFindParameterEntity { IdModulo = idConverted, IdEstado = 1 });
//            var displays = pantallas.Select(pa => pa.GlobalMapperConverter<PantallaEntity, DisplayDto>()).ToList();
//            return displays;
//        }


//        [HttpPost, Route("profile/displays")]
//        public List<DisplayDto> PostProfileDisplays(ProfileDisplaysModel profileDisplaysModel)
//        {
//            var displays = new List<DisplayDto>();

//            var pantallasDelPerfil = new PantallaXPerfilEntityCollection();
//            foreach (var idPantalla in profileDisplaysModel.ProfileDisplays)
//            {
//                var pantallaXPerfil = new PantallaXPerfilEntity();
//                pantallaXPerfil.IdPantalla = idPantalla;
//                pantallaXPerfil.IdPerfil = profileDisplaysModel.IdProfile;
//                pantallaXPerfil.IdEstado = 1;
//                pantallaXPerfil.FechaIngreso = DateTime.Now;
//                pantallaXPerfil.UsuarioIngreso = "";
//                pantallasDelPerfil.Add(pantallaXPerfil);
//            }
//            pantallasDelPerfil = PantallaXPerfilCollectionBussinesAction.Save(pantallasDelPerfil);
//            var pantallas = pantallasDelPerfil.Select(pa => pa.IdPantallaAsPantalla).ToList();
//            displays = pantallas.Select(pa => pa.GlobalMapperConverter<PantallaEntity, DisplayDto>()).ToList();
//            return displays;
//        }


//        /// <summary>
//        /// Metodo para buscar un perfil por username   
//        /// </summary>
//        /// <param name="username">credenciales para la autenticacion</param>
//        /// <returns></returns>
//        [HttpPost, Route("profile/user/{username}")]
//        [ApiKeyAuthorize]
//        public UserDto FindUserById(string username)
//        {
//            var user = UsuarioBussinesAction.LoadByPK(username);

//            if (user == null)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El usuario no es valido", "El usuario no es valido"));
//            }

//            var defaultLocal = UsuarioLocalBodegaCollectionBussinesAction.
//                FindByAll(new ER.BE.UsuarioLocalBodegaFindParameterEntity
//                {
//                    InicioSesion = user.InicioSesion
//                }, 0)?
//                .FirstOrDefault()?
//                .IdLocalBodega ?? 0;

//            UserDto userDto = user.ToUserDto();
//            userDto.DefaultLocal = defaultLocal;

//            return userDto;
//        }

//        /// <summary>
//        /// Valida la validez del token especificado   
//        /// </summary>
//        /// <param name="token">credenciales para la autenticacion</param>
//        /// <returns></returns>
//        [HttpPost, Route("profile/token/{token}")]
//        [ApiKeyAuthorize]
//        public UserDto ValidateToken(string token)
//        {
//            var user = UsuarioBussinesAction.LoadByToken(token);

//            if (user == null)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El token no es valido", "El token no es valido"));
//            }

//            var defaultLocal = UsuarioLocalBodegaCollectionBussinesAction.
//                FindByAll(new ER.BE.UsuarioLocalBodegaFindParameterEntity
//                {
//                    InicioSesion = user.InicioSesion
//                }, 0)?
//                .FirstOrDefault()?
//                .IdLocalBodega ?? 0;

//            UserDto userDto = user.ToUserDto();
//            userDto.DefaultLocal = defaultLocal;

//            return userDto;
//        }
//    }
//}
