using ER.BA;
using ER.BE;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    public class countriesController : ApiControllerBase
    {
        [HttpGet, Route("countries/{id}")]
        public CountryDto GetCountry(string id)
        {
            short idConverted;
            bool isValidId = short.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
            var country = PaisBussinesAction.LoadByPK(idConverted);
            return country.GlobalMapperConverter<PaisEntity, CountryDto>();
        }
        /// <summary>
        /// Decuelve la lista de paises 
        /// </summary>
        /// <returns></returns>
        public List<CountryDto> GetCountries()
        {
            var brands = PaisCollectionBussinesAction.FindByAll(new PaisFindParameterEntity());
            var brandsDtoList = brands.Select(br => br.ToCountryDto()).ToList();
            return brandsDtoList;
        }

        [HttpGet, Route("countries/{id}/Provincias")]
        public List<ProvinceDto> GetProvinces(string id)
        {
            short idConverted;
            bool isValidId = short.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

            var findParameter = new ProvinciaFindParameterEntity { IdPais = idConverted, IdEstado = 1 };
            var provinces = ProvinciaCollectionBussinesAction.FindByAll(findParameter);
            var provincesDtoList = provinces.Select(ln => ln.ToProvinceDto()).ToList();
            return provincesDtoList;
        }
    }
}