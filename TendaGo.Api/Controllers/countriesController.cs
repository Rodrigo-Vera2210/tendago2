using ER.BA;
using ER.BE;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TendaGo.Common;
using TendaGo.Domain.Services;
using ER.DA.Models;

namespace TendaGo.Api.Controllers
{
    public class countriesController : ApiControllerBase
    {
        private readonly ICountriesService _countriesService;

        public countriesController(ICountriesService countriesService, IUsuarioService usuarioService) : base(usuarioService)
        {
            _countriesService = countriesService;
        }

        [HttpGet, Route("")]
        public List<CountryDto> GetCountries()
        {
            try
            {
                var countries = _countriesService.GetCountries();

                return countries;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpGet, Route("{id}")]
        public CountryDto GetCountry(string id)
        {
            try
            {
                var country = _countriesService.GetCountry(id);
                return country;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpGet, Route("{id}/Provincias")]
        public List<ProvinceDto> GetProvinces(string id)
        {
            try
            {
                var provincesDtoList = _countriesService.GetProvinces(id);
                return provincesDtoList;
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }
    }
}