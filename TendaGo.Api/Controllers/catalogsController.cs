using ER.BA;
using ER.BE;
using ER.DA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TendaGo.BusinessLogic.Services;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    public class catalogsController : ApiControllerBase
    {
        private readonly ICatalogsService _catalogsService;

        public catalogsController(ICatalogsService catalogsService, IUsuarioService usuarioService) : base(usuarioService)
        {
            _catalogsService = catalogsService;
        }

        [HttpGet, Route("currencies")]
        public async Task<List<CurrencyDto>> GetCurrencies()
        {
            try
            {
                var monedas = await _catalogsService.GetCurrencies();

                return monedas;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet, Route("currencie/{idMoneda}")]
        public async Task<CurrencyDto> GetCurrencie(short idMoneda)
        {
            try
            {
                var currencie = await _catalogsService.GetCurrencie(idMoneda);

                return currencie;
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        [HttpGet, Route("sectors")]
        public async Task<List<SectorDto>> GetSector()
        {
            try
            {
                var sectorDto = await _catalogsService.GetSector();
                return sectorDto;
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        [HttpGet, Route("identification-types")]
        public List<IdentificationTypesDto> GetIdentificationTypes()
        {
            try
            {
                var result = _catalogsService.GetIdentificationTypes();

                return result;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}