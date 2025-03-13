using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.BusinessLogic.Services
{
    public class CountriesService
    {
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
    }
}
