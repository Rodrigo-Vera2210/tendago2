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
//    public class linesController : ApiControllerBase
//    {
//        [HttpGet, Route("lines/{id}/categories/all")]
//        public List<CategoryDto> GetAllCategories(string id)
//        {
//            int idConverted;
//            bool isValidId = int.TryParse(id, out idConverted);
//            if (!isValidId)
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

//            var findParameter = new CategoriaFindParameterEntity { IdLinea = idConverted, IdEmpresa = CurrentUser.IdEmpresa };
//            var categories = CategoriaCollectionBussinesAction.FindByAll(findParameter);
//            var categoriesDtoList = categories.Select(ct => ct.ToCategoryDto()).ToList();
//            return categoriesDtoList;
//        }

//        [HttpGet, Route("lines/{id}/categories")]
//        public List<CategoryDto> GetCategories(string id)
//        {
//            int idConverted;
//            bool isValidId = int.TryParse(id, out idConverted);
//            if (!isValidId)
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

//            var findParameter = new CategoriaFindParameterEntity { IdLinea = idConverted, IdEmpresa = CurrentUser.IdEmpresa, IdEstado = 1 };
//            var categories = CategoriaCollectionBussinesAction.FindByAll(findParameter);
//            var categoriesDtoList = categories.Select(ct => ct.ToCategoryDto()).ToList();
//            return categoriesDtoList;
//        }

//        [HttpGet, Route("lines/{id}")]
//        public LineDto GetLine(string id)
//        {
//            try
//            {
//                var line = GetLineaEntity(id);
//                return line.ToLineDto();
//            }
//            catch (HttpResponseException) { throw; }
//            catch (Exception ex)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
//            }
//        }

//        [HttpPost, Route("lines")]
//        public LineDto PostLine(LineDto line)
//        {
//            try
//            {
//                LineaEntity lineaEntity;
//                if (line.Id != 0)
//                {
//                    lineaEntity = LineaBussinesAction.LoadByPK(line.Id);
//                    lineaEntity.Linea = line.Linea;
//                    lineaEntity.IdDivision = line.IdDivision;
//                    lineaEntity.IdEstado = line.IdEstado;
//                    if (lineaEntity.CurrentState.Equals(EntityStatesEnum.Updated))
//                    {
//                        lineaEntity.UsuarioModificacion = line.UsuarioModificacion;
//                        lineaEntity.IpModificacion = line.IpModificacion;
//                        lineaEntity.FechaModificacion = DateTime.Today;
//                    }
//                }
//                else
//                {
//                    lineaEntity = line.ToLineEntity();
//                    lineaEntity.FechaIngreso = DateTime.Today;
//                }
//                lineaEntity.IdEmpresa = CurrentUser.IdEmpresa;
//                lineaEntity = LineaBussinesAction.Save(lineaEntity);
//                return lineaEntity.ToLineDto();
//            }
//            catch (Exception ex)
//            {
//                string UserError = "Ocurrio un error general, reintente";
//                if (ex.GetMessage().Contains("UQ_Linea"))
//                {
//                    UserError = "No puede ingresar una Linea duplicada";
//                }
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
//            }
//        }

//    }
//}
