using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class CatalogsService : ICatalogsService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public CatalogsService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<List<CurrencyDto>> GetCurrencies()
        {
            try
            {
                var monedaParameters = new MonedaFindParameterEntity();

                var monedas = await _procedimientos.Moneda_FindByAllAsync(
                                                        monedaParameters.Id,
                                                        monedaParameters.CodigoISO,
                                                        monedaParameters.Pais,
                                                        monedaParameters.IpIngreso,
                                                        monedaParameters.UsuarioIngreso,
                                                        monedaParameters.FechaIngreso,
                                                        monedaParameters.IpModificacion,
                                                        monedaParameters.UsuarioModificacion,
                                                        monedaParameters.FechaModificacion,
                                                        monedaParameters.IdEstado
                                                    );

                var currencies = _mapper.Map<List<CurrencyDto>>(monedas);
                
                return currencies;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<CurrencyDto> GetCurrencie(short idMoneda)
        {
            try
            {
                var moneda = await _procedimientos.Moneda_LoadByPKAsync(idMoneda);

                var currencie = _mapper.Map<CurrencyDto>(moneda);

                return currencie;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<List<SectorDto>> GetSector()
        {
            try
            {
                SectorFindParameterEntity sf = new SectorFindParameterEntity();
                sf.IdEstado = 1;
                var sectores = await _procedimientos.Sector_FindByAllAsync(
                                                        sf.Id,
                                                        sf.Sector,
                                                        sf.IpIngreso,
                                                        sf.UsuarioIngreso,
                                                        sf.FechaIngreso,
                                                        sf.IpModificacion,
                                                        sf.UsuarioModificacion,
                                                        sf.FechaModificacion,
                                                        sf.IdEstado
                                                    );
                var sectorDto = _mapper.Map<List<SectorDto>>(sectores);

                return sectorDto;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public List<IdentificationTypesDto> GetIdentificationTypes()
        {
            try
            {
                return new List<IdentificationTypesDto>
                {
                    new IdentificationTypesDto{ Codigo="R", TipoIdentificacion="RUC"},
                    new IdentificationTypesDto{ Codigo="C", TipoIdentificacion="Cedula"},
                    new IdentificationTypesDto{ Codigo="P", TipoIdentificacion="Pasaporte"},
                };
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
