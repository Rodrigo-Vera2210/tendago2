using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.Domain.Services
{
    public interface ICountriesService
    {
        List<CountryDto> GetCountries();
        CountryDto GetCountry(string id);
        List<ProvinceDto> GetProvinces(string id);

    }
}
