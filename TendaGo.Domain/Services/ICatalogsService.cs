using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.Domain.Services
{
    public interface ICatalogsService
    {
        public Task<List<CurrencyDto>> GetCurrencies();
        public Task<CurrencyDto> GetCurrencie(short idMoneda);
        public Task<List<SectorDto>> GetSector();
        public List<IdentificationTypesDto> GetIdentificationTypes();

    }
}
