//using ER.BA;
//using ER.BE;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Http;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    [TokenAuthorize]
//    public class rucController : ApiControllerBase
//    {
//        public List<RucDtoLite> GetRucLites()
//        {
//            var rucFindParameter = new RucFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa, IdEstado = 1 };
//            var rucs = RucCollectionBussinesAction.FindByAll(rucFindParameter, 0);
//            var rucsDtoList = rucs.Select(r => r.ToRucDtoLite()).ToList();
//            return rucsDtoList;
//        }

//        [HttpGet, Route("ruc/{ruc}")]
//        public RucDto GetRUC(string ruc)
//        {
//            return RucBussinesAction.LoadByPK(ruc)?.ToRucDto();
//        }
//    }
//}
