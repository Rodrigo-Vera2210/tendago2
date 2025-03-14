//using ER.BA;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Http;
//using TendaGo.Common;

//namespace TendaGo.Api.Areas.Marketplace.Controllers
//{
//    [RoutePrefix("market-place")]
//    public class marketplaceController : ApiController
//    {
//        // GET: Marketplace/products
//        [Route("products")]
//        public List<ProductDto> GetProducts(int page, int pageSize = 10)
//        {
//            return ProductoCollectionBussinesAction
//                .FindByAllPaged(new ER.BE.ProductoFindParameterEntity { }, page, pageSize, "Producto.Id")
//                .Select(x => x.ToProductDto())
//                .ToList();
//        }
//    }
//}