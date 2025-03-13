using ER.BE;
using System.Collections.Generic;
using System.Linq;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class packageTypesController : ApiControllerBase
    {
        public List<PackageTypeDto> GetPaymentMethods()
        {
            var findParameter = new ER.BE.TipoPaqueteFindParameterEntity { IdEstado = 1 };
            var packageTypes = ER.BA.TipoPaqueteCollectionBussinesAction.FindByAll(findParameter);
            var packageTypesDto = packageTypes.Select(pkg => pkg.GlobalMapperConverter<TipoPaqueteEntity, PackageTypeDto>()).ToList();
            return packageTypesDto;
        }



    }
}
