using ER.BA;
using ER.BE;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class modulesController : ApiController
    {
        public List<ModuleDto> GetModules()
        {
            var modules = ModuloCollectionBussinesAction.FindByAll(new ER.BE.ModuloFindParameterEntity { IdEstado = 1 });
            var modulesDto = modules.Select(x => x.GlobalMapperConverter<ModuloEntity, ModuleDto>()).ToList();
            return modulesDto;
        }
    }
}
