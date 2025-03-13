using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using TendaGo.Common;
using TendaGo.Common.DtoExtensions;
using TendaGo.Common.EntitiesDto;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class productsController : ApiControllerBase
    {
        [HttpGet, Route("products;search={searchTerm}"), Route("products/find")]
        public List<LiteProductDto> GetProducts(string searchTerm, int? page = null)
        {
            if (searchTerm == null || searchTerm.Equals("all"))
                searchTerm = string.Empty;
            if (searchTerm.Length <= 2)
            { throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "La búsqueda debe ser mayor a 4 caracteres", "búsqueda inválida")); }


            var products = ProductoCollectionBussinesAction.SearchProducts(CurrentUser.IdEmpresa, searchTerm, false, null, null, page);
            var productsDtoList = products.Select(p => p.ToLiteProductDto()).ToList();

            return productsDtoList;
        }

        [HttpGet, Route("products/all;search={searchTerm}")]
        public List<ProductDto> GetAllProducts(string searchTerm)
        {
            if (searchTerm.Equals("all"))
                searchTerm = string.Empty;
            if (searchTerm.Length <= 2)
            { throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "La búsqueda debe ser mayor a 4 caracteres", "búsqueda inválida")); }
            try
            {
                var products = ProductoCollectionBussinesAction.FindByAll(new ProductoFindParameterEntity { DescipcionBusqueda = searchTerm, IdEmpresa = (short)CurrentUser.IdEmpresa });
                var productsDtoList = products.Select(p => p.ToProductDto()).ToList();
                return productsDtoList;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

        /// <summary>
        /// Obtiene una lista de tipo de unidad con su precio, unidades y cantidad minima para aplicar el precio
        /// </summary>
        /// <param name="idproducto">Codigo de producto</param>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        [HttpGet, Route("products/{idproducto}/unit-types")]
        public IEnumerable<UnitTypeDto> GetUnitTypes(string idproducto, UnitTypeStatusEnum? idEstado = UnitTypeStatusEnum.Active)
        {
            int idConverted;
            bool isValidId = int.TryParse(idproducto, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idproducto, es invalido", "Id invalido"));

            var findParameter = new TipoUnidadFindParameterEntity();
            findParameter.IdProducto = idConverted;
            findParameter.IdEmpresa = CurrentUser.IdEmpresa;

            if (idEstado != UnitTypeStatusEnum.All)
            {
                findParameter.IdEstado = (short)idEstado;
            }

            var unittypes = TipoUnidadCollectionBussinesAction.FindByAll(findParameter, 1);
            var unitTypesDtoList = unittypes.Select(ut => ut.ToUnitTypeDto()).ToList();
            return unitTypesDtoList;
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(ProductDto product)
        {
            try
            {
                var productEntity = ProductoBussinesAction.LoadByPK(product.Id);
                if (productEntity == null)
                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Producto no existe", "El Producto solicitado, no existe"));

                productEntity.UsuarioModificacion = product.UsuarioModificacion;
                productEntity.IpModificacion = product.IpModificacion;
                productEntity.FechaModificacion = DateTime.Now;
                productEntity.SetDeletedState();
                ProductoBussinesAction.Save(productEntity);
                return Ok();
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

        [HttpGet, Route("products;pcode={term}"), Route("products/provider-code")]
        public LiteProductDto GetProductByProviderCode(string term)
        {
            var products = ProductoCollectionBussinesAction.FindByAll(new ProductoFindParameterEntity { CodigoProveedor = term, IdEmpresa = CurrentUser.IdEmpresa }, 0);

            if (!products.Any())
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "Producto no existe", "No existe producto para el codigo proporcionado"));

            return products.FirstOrDefault().ToLiteProductDto();
        }

        [HttpGet, Route("products;code={term}"), Route("products/internal-code")]
        public LiteProductDto GetProductByCode(string term, int? idlocal = null)
        {
            var products = ProductoCollectionBussinesAction.FindByAll(new ProductoFindParameterEntity { CodigoInterno = term, IdEmpresa = CurrentUser.IdEmpresa }, 0);

            if (!products.Any())
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "Producto no existe", "No existe producto para el codigo proporcionado"));

            var product = products.FirstOrDefault();

            if (idlocal != null)
            {
                var productoEntity = ER.BA.StockCollectionBussinesAction.SaldoInventario(CurrentUser.IdEmpresa, product.Id, idlocal.Value)?.FirstOrDefault();
                product.Stock = productoEntity?.Stock ?? 0M;
            }

            return product.ToLiteProductDto();
        }

        /// <summary>
        /// Obtiene el stock disponible de un producto en un local o bodega
        /// </summary>
        /// <param name="id">codigo de producto</param>
        /// <param name="idLocal">codigo de local o bodega</param>
        /// <returns></returns>
        [HttpGet, Route("products/{id}/stock;local={idLocal}")]
        public ProductDto GetProductStock(string id, string idLocal)
        {
            int idProducto;
            bool isValidId = int.TryParse(id, out idProducto);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idproducto, es invalido", "Id invalido"));

            int idLocalConverted;
            bool isValidIdLocal = int.TryParse(idLocal, out idLocalConverted);
            if (!isValidIdLocal)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idLocal, es invalido", "Id invalido"));

            var productoEntity = ER.BA.StockCollectionBussinesAction.SaldoInventario(CurrentUser.IdEmpresa, idProducto, idLocalConverted)?.FirstOrDefault();

            return productoEntity?.ToProductDto();
        }

        /// <summary>
        /// Obtiene el carrito de una salida anterior, validando stock y precios actuales
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("products/{idSalida}/recreate-cart")]
        public List<CartDto> GetCarrito(string idSalida)
        {
            var param = new DetalleSalidaFindParameterEntity();
            param.IdSalida = idSalida;
            var detalles = ER.BA.DetalleSalidaCollectionBussinesAction.FindByAll(param);

            if (detalles == null)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idSalida, es invalido", "Id invalido"));
            }

            int idlocal = detalles.FirstOrDefault().IdLocal;
            string idsProducto = "";

            foreach (var item in detalles)
            {
                if (!idsProducto.Contains(item.IdProducto.ToString() + ','))
                {
                    idsProducto += item.IdProducto.ToString() + ','; ;
                }
            }

            var productoEntity = ER.BA.StockCollectionBussinesAction.CrearCarrito(CurrentUser.IdEmpresa, idsProducto, idSalida, idlocal, null, null)?
                                        .ConvertirToDto<CartDto>().ToList();

            return productoEntity;
        }

        /// <summary>
        /// Obtiene el stock disponible de un producto en todos los locales o bodegas
        /// </summary>
        /// <param name="id">codigo de producto</param>
        /// <returns></returns>
        [HttpGet, Route("products/{id}/stock")]
        public List<LiteStockDto> GetProductStockLocales(string id)
        {
            int idProducto;
            bool isValidId = int.TryParse(id, out idProducto);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idproducto, es invalido", "Id invalido"));

            var productoStocks = StockCollectionBussinesAction.SaldoInventario(CurrentUser.IdEmpresa, idProducto);

            return productoStocks?
                .Select(p => p.ToLiteStockDto())
                .ToList() ?? new List<LiteStockDto>();
        }

        /// <summary>
        /// Obtiene el stock disponible de una lista de productos
        /// </summary>
        /// <param name="productList">lista de Id de Productos separados por coma</param>
        /// <param name="idLocal"></param>
        /// <returns></returns>
        [Obsolete]
        [HttpGet, Route("products/stocks;productos={productList},local={idLocal}")]
        public IEnumerable<ProductDto> GetProductsStock(string productList, string idLocal)
        {
            //char[] separador = { ',', ' ' };
            //var strlist = productList.Split(separador, StringSplitOptions.RemoveEmptyEntries);
            //return GetProductsStocks(idLocal, strlist);
            return GetProductsStocks(idLocal, productList);
        }

        /// <summary>
        /// Obtiene el stock disponible de una lista de productos
        /// </summary>
        /// <param name="idLocal"></param>
        /// <param name="productList"></param>
        /// <returns></returns>
        [HttpPost, Route("products/stocks;local={idLocal}")]
        public IEnumerable<ProductDto> GetProductsStocks(string idLocal, [FromBody] string productList)
        {
            int idLocalConverted;
            bool isValidIdLocal = int.TryParse(idLocal, out idLocalConverted);
            if (!isValidIdLocal)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idLocal, es invalido", "Id invalido"));

            var products = Request.Content.GetContentAsString();

            // Si productList contiene la lista completa se usa esta.
            if (productList.Length > products.Length)
            {
                products = productList;
            }

            var stockProducts = ER.BA.StockCollectionBussinesAction.SaldoInventario(CurrentUser.IdEmpresa, products, idLocalConverted);

            return stockProducts.Select(x => x.ToProductDto());
        }

        [HttpGet, Route("products/{id}")]
        public ProductDto GetProduct(string id)
        {
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idproducto, es invalido", "Id invalido"));
            //var user = GetAuthUser(username);
            var productEntity = ProductoBussinesAction.LoadByPK(idConverted);
            if (productEntity == null)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Producto no existe", "El Producto solicitado, no existe"));
            return productEntity.ToProductDto();
        }

        /// <summary>
        /// Busqueda de producto por cualquier descripcion
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("products/search")]
        public List<ProductDto> SearchProducts(SearchProductModel searchModel)
        {
            var productoFindParameter = new ProductoFindParameterEntity();
            productoFindParameter.DescipcionBusqueda = searchModel.SearchTerm;
            if (searchModel.StateForSearch != ProductStatus.All)
                productoFindParameter.IdEstado = (short)searchModel.StateForSearch;

            productoFindParameter.IdEmpresa = CurrentUser.IdEmpresa;
            var products = ProductoCollectionBussinesAction.FindByAll(productoFindParameter);
            var productsDtoList = products.Select(p => p.ToProductDto()).ToList();
            return productsDtoList;
        }


        public async Task<ProductDto> PostProduct(ProductDto product)
        {
            var productoEntity = new ProductoEntity();
            if (product.Id > 0)
            {
                productoEntity = ProductoBussinesAction.LoadByPK(product.Id);
                productoEntity.IdEmpresa = CurrentUser.IdEmpresa;
                productoEntity.CodigoBarra = product.CodigoBarra;
                productoEntity.CodigoProveedor = product.CodigoProveedor;
                productoEntity.CodigoInterno = product.CodigoInterno;
                productoEntity.TipoProducto = product.TipoProducto;
                productoEntity.Producto = product.Producto;
                productoEntity.ProductoPadre = product.ProductoPadre;
                productoEntity.Descripcion = product.Descripcion;
                if (product.HasFotoChanges)
                {
                    //productoEntity.PathArchivo = product.PathArchivo;
                    if (!string.IsNullOrEmpty(product.Foto))
                    {
                        productoEntity.PathArchivo = await UploadFile(product.Foto, $"{product.CodigoInterno}");
                    }
                    else
                    {
                        productoEntity.PathArchivo = "https://tendagoeast.binasystem.com/empresas/assets/NoImage.jpeg";
                    }
                }


                //productoEntity.DescipcionBusqueda = product.DescripcionBusqueda;
                productoEntity.StockMinimo = product.StockMinimo ?? 0;
                productoEntity.Costo = product.Costo;
                productoEntity.UnidadMedida = product.UnidadMedida ?? 0;
                productoEntity.Descuento = product.Descuento ?? 0;
                productoEntity.CobraIva = product.CobraIva ?? false;
                productoEntity.IdCategoria = product.IdCategoria ?? 0;
                productoEntity.IdDivision = product.IdDivision ?? 0;
                productoEntity.IdLinea = product.IdLinea ?? 0;
                productoEntity.Volumen = product.Volumen ?? 0;
                productoEntity.RegistroSanitario = product.RegistroSanitario;
                productoEntity.Peso = product.Peso ?? 0;
                productoEntity.IdMarca = product.IdMarca ?? 0;
                productoEntity.IdEstado = product.IdEstado;

                if (productoEntity.IdEstado == 1)
                {
                    if (productoEntity.CurrentState == EntityStatesEnum.Deleted)
                        productoEntity.CurrentState = EntityStatesEnum.Updated;
                }

                if (productoEntity.CurrentState == EntityStatesEnum.Updated)
                {
                    productoEntity.IpModificacion = product.IpModificacion;
                    productoEntity.UsuarioModificacion = product.UsuarioModificacion;
                    productoEntity.FechaModificacion = DateTime.Today;
                }
            }
            else
            {
                productoEntity = product.GlobalMapperConverter<ProductDto, ProductoEntity>();
                productoEntity.IpIngreso = product.IpIngreso;
                productoEntity.UsuarioIngreso = product.UsuarioIngreso;
                productoEntity.FechaIngreso = DateTime.Today;
                productoEntity.IdEstado = 1;
                productoEntity.IdEmpresa = CurrentUser.IdEmpresa;
                if (!string.IsNullOrEmpty(product.Foto))
                {
                    productoEntity.PathArchivo = await UploadFile(product.Foto, $"{product.CodigoInterno}");
                }
                else
                {
                    productoEntity.PathArchivo = "https://tendagoeast.binasystem.com/empresas/assets/NoImage.jpeg";
                }
                productoEntity.SetNewState();
            }

            productoEntity = ProductoBussinesAction.Save(productoEntity);
            return productoEntity.ToProductDto();
        }
        private string GetProductPath(string id)
        {
            return $"{CurrentUser.IdEmpresa}/productos/{id}-v{DateTime.Now.ToFileTime()}.jpg".ToLower();
        }

        private async Task<string> UploadFile(Stream stream, string id)
        {
            var file = stream.ToByteArray();
            var path = GetProductPath(id);

            await Storage.FileHandler.UploadAsync(path, file);
            return await Storage.FileHandler.GetFileAsync(path);
        }

        private async Task<string> UploadFile(string imagen, string id)
        {
            var file = Convert.FromBase64String(imagen);
            var path = GetProductPath(id);

            var result = await Storage.FileHandler.UploadAsync(path, file);

            return $"{result.Uri}";
        }

        [HttpPost, Route("products/image-migration")]
        public async Task<string> SearchProducts(string ruta, int id)
        {
            CurrentUser.IdEmpresa = id;
            DirectoryInfo di = new DirectoryInfo(ruta);
            Console.WriteLine("No search pattern returns:");

            foreach (var fi in di.GetFiles())
            {
                var producto = ProductoCollectionBussinesAction.SearchProducts(CurrentUser.IdEmpresa, fi.Name.Replace(".jpg", ""), false, null, null, null);
                if (producto != null && producto.Count > 0)
                {
                    var file = fi.OpenRead();
                    var path = await UploadFile(file, fi.Name.Replace(".jpg", ""));

                    var productoEntity = new ProductoEntity();
                    productoEntity = ProductoBussinesAction.LoadByPK(producto[0].Id);
                    productoEntity.PathArchivo = path;

                    ProductoBussinesAction.Save(productoEntity);
                }
            }
            return "OK";
        }

        [HttpGet, Route("products/sales")]
        public List<SalesProductDto> SalesByProduct(int idLocal, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (startDate == null)
            {
                startDate = DateTime.Today;
            }

            if (endDate == null)
            {
                endDate = DateTime.Today.AddDays(1).AddMinutes(-1);
            }

            DataSet outputs = ProductoCollectionBussinesAction.SalesByProduct(CurrentUser.IdEmpresa, idLocal, startDate.Value, endDate.Value);

            var result = outputs.ConvertirToDto<SalesProductDto>().ToList();

            var products = new System.Text.StringBuilder();

            foreach (var item in result)
            {
                if (products.Length > 0)
                {
                    products.Append(",");
                }

                products.Append(item.IdProducto);
            }

            var stockProducts = ER.BA.StockCollectionBussinesAction
                .StockInventario(CurrentUser.IdEmpresa, products.ToString())?
                .ConvertirToDto<LiteStockDto>();

            var unidades = ER.BA.TipoUnidadCollectionBussinesAction.FindByAll(new TipoUnidadFindParameterEntity { IdProducto = products.ToString() }, 0)
                .Select(x => x.ToUnitTypeDto())
                .ToList();

            foreach (var item in result)
            {
                item.Unidades = unidades.Where(x => x.IdProducto == item.IdProducto).OrderBy(x => x.CantidadMinima).ToList();
                item.Stock = stockProducts.FirstOrDefault(x => x.IdProducto == item.IdProducto && x.IdLocal == item.IdLocal)?.StockInventario ?? 0M;
                item.StockLocales = stockProducts?.Where(x => x.IdProducto == item.IdProducto && x.IdLocal != item.IdLocal)?.OrderBy(x => x.Local)?.ToList();
                item.IdTipoUnidad = item.Unidades.FirstOrDefault()?.Id ?? 0;
                item.TipoUnidad = item.Unidades.FirstOrDefault()?.TipoUnidad ?? "No Disponible";
            }

            return result;
        }

        [HttpGet, Route("products/{id}/photo")]
        public string GetProductPhoto(int id)
        {
            var producto = ProductoBussinesAction.LoadByPK(id, 0);

            if (producto == null || producto.IdEmpresa != CurrentUser?.IdEmpresa)
            {
                throw Request.BuildHttpErrorException(HttpStatusCode.NotFound, "El producto especificado no existe");
            }

            var root = ConfigurationManager.AppSettings["TendaGo:Resources"];
            var path = Path.Combine(root, producto.IdEmpresa.ToString()) + producto.PathArchivo;

            if (!File.Exists(path))
            {
                path = HostingEnvironment.MapPath("~/no_imagen.png");
            }

            byte[] b = System.IO.File.ReadAllBytes(path);
            return "data:image/png;base64," + Convert.ToBase64String(b);

        }

    }
}
