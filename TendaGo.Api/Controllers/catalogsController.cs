using ER.BA;
using ER.BE;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class catalogsController : ApiController
    {

        /// <summary>
        /// Monedas
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("catalogs/currencies")]
        public List<CurrencyDto> GetCurrencies()
        {
            var monedas = MonedaCollectionBussinesAction.FindByAll(new MonedaFindParameterEntity());
            var currencies = monedas.Select(mo => mo.GlobalMapperConverter<MonedaEntity, CurrencyDto>()).ToList();
            return currencies;
        }

        /// <summary>
        /// Moneda
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("catalogs/currencie/{idMoneda}")]
        public CurrencyDto GetCurrencie(short idMoneda)
        {
            var moneda = MonedaBussinesAction.LoadByPK(idMoneda);
            var currencie = moneda.GlobalMapperConverter<MonedaEntity, CurrencyDto>();
            return currencie;
        }

        /// <summary>
        /// Sector
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("catalogs/sectors")]
        public List<SectorDto> GetSector()
        {
            SectorFindParameterEntity sf = new SectorFindParameterEntity();
            sf.IdEstado = 1;
            var sectores = SectorCollectionBussinesAction.FindByAll(sf);
            var sectorDto = sectores.Select(mo => mo.GlobalMapperConverter<SectorEntity, SectorDto>()).ToList();
            return sectorDto;
        }

        /// <summary>
        /// Tipos de Identificacion
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("catalogs/identification-types")]
        public List<IdentificationTypesDto> GetIdentificationTypes()
        {
            return new List<IdentificationTypesDto>
            {
                new IdentificationTypesDto{ Codigo="R", TipoIdentificacion="RUC"},
                new IdentificationTypesDto{ Codigo="C", TipoIdentificacion="Cedula"},
                new IdentificationTypesDto{ Codigo="P", TipoIdentificacion="Pasaporte"},
            };
        }
    }
}