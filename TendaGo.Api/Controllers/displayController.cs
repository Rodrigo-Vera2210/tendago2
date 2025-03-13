using ER.BA;
using ER.BE;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    public class displayController : ApiControllerBase
    {
        public List<DisplayDto> GetDisplays()
        {
            var pantallas = PantallaCollectionBussinesAction.FindByAll(new PantallaFindParameterEntity { IdEstado = 1 });
            var displays = pantallas.Select(pa => pa.GlobalMapperConverter<PantallaEntity, DisplayDto>()).ToList();
            return displays;
        }

        [HttpGet, Route("display/profile")]
        public List<DisplayDto> GetUserProfileDisplays()
        {
            var pantallasPerfil = PantallaXPerfilCollectionBussinesAction.FindByAll(new PantallaXPerfilFindParameterEntity { IdPerfil = CurrentUser.IdPerifl, IdEstado = 1 });
            var pantallas = pantallasPerfil.Select(pa => pa.IdPantallaAsPantalla).OrderBy(x => x.Orden).ToList();
            var displays = pantallas.Select(pa => pa.GlobalMapperConverter<PantallaEntity, DisplayDto>()).ToList();
            return displays;
            //var displays = PantallaCollectionBussinesAction.ObtenerPantallasPorPerfil(user.InicioSesion);
            //return displays.Select(x => x.GlobalMapperConverter<PantallaEntity, DisplayDto>()).ToList();
        }

    }
}
