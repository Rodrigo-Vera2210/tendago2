//using ER.BA;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web.Http;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    public class userController : ApiControllerBase
//    {
//        /// <summary>
//        /// Obtiene las bodegas o locales a las que tiene acceso un usuario
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet, Route("user/warehouses")]
//        [TokenAuthorize]
//        public List<WarehouseDto> GetUserWarehouses()
//        {
//            var findParam = new ER.BE.UsuarioLocalBodegaFindParameterEntity { InicioSesion = CurrentUser.InicioSesion, IdEstado = 1 };
//            var userWarehouses = UsuarioLocalBodegaCollectionBussinesAction.FindByAll(findParam, 2);
//            var warehouses = userWarehouses.Select(x => x.IdLocalBodegaAsLocalBodega).ToList();
//            return warehouses.Select(x => x.ToWarehouseDto()).ToList();
//        }

//        /// <summary>
//        /// Obtiene la empresa actual del usuario especificado
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet, Route("user/company")]
//        [TokenAuthorize]
//        public CompanyDto GetCompany()
//        {
//            var empresaId = CurrentUser.IdEmpresa;
//            var empresa = EmpresaBussinesAction.LoadByPK(empresaId);

//            return empresa.ToCompanyDto();
//        }


//        /// <summary>
//        /// Metodo para iniciar sesion   
//        /// </summary>
//        /// <param name="login">credenciales para la autenticacion</param>
//        /// <returns></returns>
//        [AllowAnonymous]
//        [HttpPost, Route("user/login")]
//        public UserDto PostLogin(LoginModel login)
//        {
//            var user = UsuarioBussinesAction.LoadByPK(login.User);

//            if (user == null || user.IdEstado <= 0)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El usuario no es valido", "El usuario no es valido"));
//            }

//            if (user.Contraseña != login.Password)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El usuario o contrasena incorrectos", "El usuario o contrasena incorrectos"));
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
//            userDto.IdPerfil = user.IdPerifl;

//            return userDto;
//        }

//        //[AllowAnonymous]
//        //[HttpPost, Route("user/login")]
//        //public UserDto ResetPassword(string usuario)
//        //{
//        //    try
//        //    {
//        //        var user = UsuarioBussinesAction.LoadByPK(usuario);

//        //        var contrato = _contrato.Search(x => x.IdEntidad == user.IdEntidad).FirstOrDefault();
//        //        if (user != null && contrato.IdEntidad > 0)
//        //        {
//        //            var userDto = CargarRoles(_mapper.Map<UsuarioDto>(user));

//        //            var token = _seguridad.FirstOrDefault(x => x.IdUsuario == user.IdUsuario)?.Codebase;

//        //            userDto.Token = $"{token}:{DateTime.Now.AddDays(1).ToBinary()}".ToBase64String();

//        //            return await userDto.ToResultAsync();
//        //        }

//        //        return new OperationResult<UsuarioDto>(System.Net.HttpStatusCode.NotFound, "El usuario especificado no existe!");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return new OperationResult<UsuarioDto>(ex);
//        //    }
//        //}

//    }
//}
