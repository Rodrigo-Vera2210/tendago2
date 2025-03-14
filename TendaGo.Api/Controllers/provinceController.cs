//using ER.BA;
//using ER.BE;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web.Http;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    public class provinceController : ApiControllerBase
//    {
//        [HttpGet, Route("provinces/{id}/Ciudades")]
//        public List<CityDto> GetCities(string id)
//        {
//            short idConverted;
//            bool isValidId = short.TryParse(id, out idConverted);
//            if (!isValidId)
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

//            var findParameter = new CiudadFindParameterEntity { IdProvincia = idConverted, IdEstado = 1 };
//            var cities = CiudadCollectionBussinesAction.FindByAll(findParameter);
//            var citiesDtoList = cities.Select(ln => ln.ToCityDto()).ToList();
//            return citiesDtoList;
//        }
//    }
//}