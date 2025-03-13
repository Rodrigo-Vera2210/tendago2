using ER.BA;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class sellersController : ApiControllerBase
    {
        /// <summary>
        /// Obtiene la lista de clientes 
        /// </summary>
        /// <param name="searchterm">texto de busqueda</param>
        /// <returns></returns>
        [HttpGet, Route("sellers/search/{searchterm?}")]
        public List<SellerDto> GetSellers(string searchterm = "all")
        {
            if (searchterm.Equals("all"))
            {
                searchterm = "";
            }

            var vendedores = UsuarioCollectionBussinesAction
                    .SearchAll(CurrentUser.IdEmpresa, searchterm)
                    .Select(x => x.ToSellerDto());

            return vendedores.ToList();
        }
    }
}
