using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public CountriesService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public List<CountryDto> GetCountries()
        {
            try
            {
                var parameters = new PaisFindParameterEntity();
                var brands = _procedimientos.Pais_FindByAllAsync(
                                                parameters.Id,
                                                parameters.Codigo,
                                                parameters.Pais,
                                                parameters.Nacionalidad,
                                                parameters.CodigoNacionalidad,
                                                parameters.IpIngreso,
                                                parameters.UsuarioIngreso,
                                                parameters.FechaIngreso,
                                                parameters.IpModificacion,
                                                parameters.UsuarioModificacion,
                                                parameters.FechaModificacion,
                                                parameters.IdEstado
                                            );

                var brandsDtoList = _mapper.Map<List<CountryDto>>(brands);

                return brandsDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public CountryDto GetCountry(string id)
        {
            try
            {
                short idConverted;
                bool isValidId = short.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El parametro id, es invalido, Id invalido") });

                var country = _procedimientos.Pais_LoadByPKAsync(idConverted);
                return _mapper.Map<CountryDto>(country);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProvinceDto> GetProvinces(string id)
        {
            try
            {
                short idConverted;
                bool isValidId = short.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El parametro id, es invalido, Id invalido") });

                var findParameter = new ProvinciaFindParameterEntity { IdPais = idConverted, IdEstado = 1 };
                var provinces = _procedimientos.Provincia_FindByAllAsync(
                                                    findParameter.Id,
                                                    findParameter.IdPais,
                                                    findParameter.Provincia,
                                                    findParameter.Codigo,
                                                    findParameter.IpIngreso,
                                                    findParameter.UsuarioIngreso,
                                                    findParameter.FechaIngreso,
                                                    findParameter.IpModificacion,
                                                    findParameter.UsuarioModificacion,
                                                    findParameter.FechaModificacion,
                                                    findParameter.IdEstado
                                                );

                var provincesDtoList = _mapper.Map<List<ProvinceDto>>(provinces);

                return provincesDtoList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
