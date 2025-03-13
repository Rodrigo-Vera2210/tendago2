using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class providersController : ApiControllerBase
    {

        [HttpGet, Route("providers/search/{searchterm?}")]
        public List<ProviderDto> GetProviders(string searchterm = null, bool identification = false)
        {
            var providers = this.GetProviders(1, searchterm, identification);
            //if (!string.IsNullOrEmpty(searchterm))
            //{
            //    providers = providers.Where(pr => pr.TextoBusqueda.Contains(searchterm)).ToList();
            //}
            return providers;
        }


    }
}
