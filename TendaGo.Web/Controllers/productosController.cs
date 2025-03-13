using System;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
//using TendaGo.Web.ProductoService;
using TendaGo.Web.Models;
using AutoMapper;
using RestSharp;
using TendaGo.Common;
using Newtonsoft.Json;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Threading.Tasks;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class productosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult GetSearchItem(string id, int productId)
        {
            var products = Session[id] as List<ProductoViewModel>;
            
            var product = products?.FirstOrDefault(x => x.Id == productId);

            return PartialView("ProductosResultDetail", product ?? new ProductoViewModel { Producto = "Error al cargar el item" });
        }


        public ActionResult CrearProducto()
        {
            ViewBag.UrlBase = Tools.GetApiUrlBase();

            //var model = new ProductoViewModel { TipoProducto = "PRODUCTO" };
            var model = new ProductoViewModel();

            return PartialView("MantProducto", model);
        }

        [HttpPost]
        public ActionResult EditarProducto(int idProducto)
        {
            var productoDto = ProductosAppService.ObtenerProducto(idProducto);
            var productoViewModel = Mapper.Map<ProductoViewModel>(productoDto);
            productoViewModel.Activo = productoDto.IdEstado == 1;

            var divisions = DivisionesAppService.ObtenerDivisionesActivas();
            var selectedDivision = productoDto.IdDivision != 0 ? divisions.FirstOrDefault(d => d.Id == productoDto.IdDivision) : divisions.FirstOrDefault();

            var lines = LineasAppService.ObtenerLineasActivas(selectedDivision?.Id ?? 0);
            var selectedLine = productoDto.IdLinea != 0 ? lines.FirstOrDefault(l => l.Id == productoDto.IdLinea) : lines.FirstOrDefault();

            var cats = CategoriasAppService.ObtenerCategoriasActivas(selectedLine?.Id ?? 0);
            var selectedCat = productoDto.IdCategoria != 0 ? cats.FirstOrDefault(c => c.Id == productoDto.IdCategoria) : cats.FirstOrDefault();

            var brands = MarcasAppService.ObtenerMarcas();
            var unidadesMedida = UnidadMedidaAppService.ObtenerUnidadesDeMedidaActivas();
            var selectedBrand = productoDto.IdMarca != 0 ? brands.FirstOrDefault(m => m.Id == productoDto.IdMarca) : brands.FirstOrDefault();
                        
            productoViewModel.FotoDataUrl = productoViewModel.PathArchivo;

            //if (string.IsNullOrEmpty(productoViewModel.FotoDataUrl))
            //{
            //    if (!string.IsNullOrEmpty(productoViewModel.PathArchivo))
            //    {
            //        string realPath = Server.MapPath($"~{User.Identity.GetCarpetaRaizEmpresa()}{productoViewModel.PathArchivo}");
            //        //string pathArchivo = System.IO.File.Exists(realPath)
            //        //    ? realPath
            //        //    : Server.MapPath("~/Images/no_imagen.png");
            //        productoViewModel.FotoDataUrl = System.IO.File.Exists(realPath) ? Tools.ConvertirByteArrayAImagen(System.IO.File.ReadAllBytes(realPath)) : "";
            //    }
            //}

            ViewBag.ListaUnidadesMedida = new SelectList(unidadesMedida, "Id", "UnidadMedida");
            ViewBag.DivisionList = new SelectList(divisions, "Id", "Division", selectedCat);
            ViewBag.LineList = new SelectList(lines, "Id", "Linea", selectedLine);
            ViewBag.CategoryList = new SelectList(cats, "Id", "Categoria", selectedCat);
            ViewBag.BrandList = new SelectList(brands, "Id", "Marca", selectedBrand);
            ViewBag.UrlBase = Tools.GetApiUrlBase();

            return PartialView("MantProducto", productoViewModel);
        }

        [HttpPost]
        public JsonResult GuardarProducto(ProductoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)//isValid es falso cuando un campo requerido del model llegó null o 0
                {

                    model.CobraIva = model.PorcentajeTarifaImpuesto > 0;
                    var productoDto = Mapper.Map<ProductDto>(model);
                    HttpPostedFileBase poImgFile = null;
                    bool hasPhotoChanges = false;
                    if (Request.Files.Count > 0)
                    {
                        poImgFile = Request.Files["file"];
                        if (poImgFile?.ContentLength > 0)
                        {
                            hasPhotoChanges = true;
                            //string theFileName = $"{model.CodigoInterno}.jpg";
                            byte[] thePictureAsBytes = new byte[poImgFile.ContentLength];
                            using (BinaryReader theReader = new BinaryReader(poImgFile.InputStream))
                            {
                                thePictureAsBytes = theReader.ReadBytes(poImgFile.ContentLength);
                            }
                            productoDto.Foto = Convert.ToBase64String(thePictureAsBytes);
                        }
                    }
                    else
                        hasPhotoChanges = model.HasfotoChanges;


                    productoDto.FechaIngreso = DateTime.Now;
                    productoDto.UsuarioIngreso = model.Id == 0 ? User.Identity.Name : "";
                    productoDto.IpIngreso = model.Id == 0 ? AppServiceBase.ObtenerIp() : "";
                    productoDto.UsuarioModificacion = model.Id != 0 ? User.Identity.Name : "";
                    productoDto.IpModificacion = model.Id != 0 ? AppServiceBase.ObtenerIp() : "";
                    productoDto.IdEmpresa = User.Identity.GetIdEmpresa();

                    productoDto.HasFotoChanges = hasPhotoChanges;
                    if (hasPhotoChanges)
                    {
                        if (poImgFile != null)
                        {
                            //productoDto.PathArchivo = SaveProductImage(poImgFile, productoDto.CodigoInterno);
                            //productoDto.Foto = "";
                        }
                        else
                        {
                            //DeleteProductImage(productoDto.PathArchivo);//elminar archivo anterior
                            //productoDto.PathArchivo = null;
                        }
                    }

                    productoDto.Producto = model.Producto;
                    productoDto.IdEstado = model.Activo ? (short)1 : (short)0;
                    //ProductoServiceClient servicio = new ProductoServiceClient();
                    var producto = ProductosAppService.GuardarProducto(productoDto);

                    return Json(new RespuestaViewModel { Success = true, Mensaje = "El producto [" + producto.Id + "] ha sido guardado correctamente.", Data = producto.Id }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                string userError = $"El producto no ha sido guardado. {ex.Message}";
                
                if (ex.Message.Contains("UQ_Producto"))
                {
                    userError = "El codigo del producto ya existe, no puede duplicar productos";
                }

                DeleteProductImage(model?.PathArchivo);//elminar archivo
                
                return Json(new RespuestaViewModel { Success = false, Mensaje = userError }, JsonRequestBehavior.AllowGet);
            }
            return Json(new RespuestaViewModel { Success = false, Mensaje = "El producto no ha sido guardado." }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarProducto(int idProducto)
        {
            try
            {
                var productoDto = new ProductDto { Id = idProducto,UsuarioModificacion = User.Identity.Name,IpModificacion = AppServiceBase.ObtenerIp() };
                ProductosAppService.DeleteProduct(productoDto);
                //ProductoServiceClient servicio = new ProductoServiceClient();
                //servicio.EliminarProducto(productoDto);
                return Json(new RespuestaViewModel { Success = true, Mensaje = "El producto [" + idProducto + "] ha sido eliminado correctamente." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new RespuestaViewModel { Success = false, Mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult precios()
        {
            return View();
        }

        public ActionResult Buscar(int idOrigen)
        {
            ViewBag.Origen = idOrigen;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Buscar(string textoBusqueda, int idOrigen)
        {
            try
            {
                // Debido a la condicion de la busqueda, se va a limitar los datos extraidos.
                var productosModel = buscarProductos(textoBusqueda, idOrigen);
                
                ViewBag.Origen = idOrigen;
                return PartialView("ProductosResult", productosModel);
            }
            catch (ApplicationServicesException apex)
            {
                return Json(new { Success = false, Message = apex.Message, Error=apex.ApiErrorResponse}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {Success = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BuscarPorQrCode()
        {
            var image = Request.Files[0];
            var barcodeReader = new ZXing.BarcodeReader();
            var bmp = new System.Drawing.Bitmap(image.InputStream,true);
            //bmp.SetResolution(800, 800);
            var result = barcodeReader.Decode(bmp);
            if (result != null)
            {
                return Json(new { text = result.Text }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Mensaje = "Error en el BarCode" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductDetail(int idProduct)
        {
            var productoViewModel = GetProduct(idProduct);
            return PartialView(productoViewModel);
        }


        public ActionResult GetTiposUnidad(int idProduct)
        {
            try
            {
                var units = TipoUnidadAppService.ObtenerTiposUnidadPorProducto(idProduct, UnitTypeStatusEnum.All);
                ViewBag.IdProducto = idProduct;
                return PartialView(units);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = ex.Message, Error = ex }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MantUnitType(int id, int idProducto)
        {
            var units = UnidadMedidaAppService.ObtenerUnidadesDeMedida();

            ViewBag.UnidadMedidasList = new SelectList(units.Where(x => x.IdEstado == 1).ToList(), "Id", "UnidadMedida");

            var productoDto = ProductosAppService.ObtenerProducto(idProducto);
            ViewBag.CobraIva    = productoDto.CobraIva;
            ViewBag.TipoProducto = productoDto.TipoProducto;

            if (id != 0)
            {
                var unit = TipoUnidadAppService.GetUnitTypeById(id);

                ViewBag.IsEdit = true;                

                var unitTypeModel = new TipoUnidadModel
                {
                    Id = unit.Id,
                    Cantidad = unit.Cantidad,
                    CantidadMinima=unit.CantidadMinima,
                    Margen=unit.Margen,
                    IdProducto = unit.IdProducto,
                    IncluyeIva = unit.IncluyeIva ?? false,
                    Precio = unit.Precio ?? 0,
                    Costo = unit.Costo ?? 0,
                    UnidadMedidad = unit.UnidadMedidad,
                    TipoUnidad = unit.TipoUnidad,
                    IdEstado = unit.IdEstado.Equals(1),
                    Producto = unit.Producto,
                    UnidadMedida = unit.UnidadMedida
                };

                return PartialView(unitTypeModel);
            }

            var model = new TipoUnidadModel { IdProducto = idProducto, UnidadMedida = productoDto.UnidadMedida.ToString() };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult MantUnitType(TipoUnidadModel model)
        {
            try
            {

                var forcedCultureInfo = new System.Globalization.CultureInfo("en-us", true);
                //forcedCultureInfo.NumberFormat.CurrencyDecimalDigits = 4;
                forcedCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                forcedCultureInfo.NumberFormat.NumberDecimalDigits = 4;
                forcedCultureInfo.NumberFormat.CurrencyGroupSeparator = ",";
                Thread.CurrentThread.CurrentCulture = forcedCultureInfo;

                var unitType = new UnitTypeDto
                {
                    Id = model.Id,
                    Cantidad = model.Cantidad,
                    TipoUnidad = model.TipoUnidad,
                    IdProducto = model.IdProducto,
                    UnidadMedidad = model.UnidadMedidad,
                    CantidadMinima = model.CantidadMinima,
                    Margen = model.Margen,
                    Precio = model.Precio,
                    Costo = model.Costo,
                    IncluyeIva = model.IncluyeIva,
                    IdEstado = model.IdEstado ? (short)1 : (short)0,
                    UsuarioModificacion = model.Id != 0 ? User.Identity.Name : string.Empty,
                    IpModificacion = model.Id != 0 ? AppServiceBase.ObtenerIp() : string.Empty,
                    UsuarioIngreso = model.Id == 0 ? User.Identity.Name : string.Empty,
                    IpIngreso = model.Id == 0 ? AppServiceBase.ObtenerIp() : string.Empty
                };

                var result = TipoUnidadAppService.GuardarTipoUnidad(unitType);
                if (result != null)
                {
                    return Json(new { Success = true, Mensaje = "Registro Guardado", unitType = result }, JsonRequestBehavior.AllowGet);
                }

                throw new Exception("Hubo un error al guardar el registro");

            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse.UserMessage, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = ex.Message, Error = ex }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarParaModal(int idOrigen)
        {
            ViewBag.Origen = idOrigen;
            return PartialView();
        }
        [HttpPost]
        public ActionResult BuscarParaModal(string textoBusqueda, int idOrigen)
        {
            ViewBag.Origen = idOrigen;
            var productosModel = buscarProductos(textoBusqueda, idOrigen);
            return PartialView("ProductosResultModal", productosModel);
        }
         

        private ProductoViewModel GetProduct(int idProducto)
        {
            var productoDto = ProductosAppService.ObtenerProducto(idProducto);
            var productoViewModel = Mapper.Map<ProductoViewModel>(productoDto);
            //productoViewModel.FotoDataUrl = GetProductImage(productoViewModel.PathArchivo);
            productoViewModel.FotoDataUrl = productoViewModel.PathArchivo;
            return productoViewModel;
        }
        


        [HttpGet]
        public JsonResult SearchUnitTypes(int id)
        {
            var result = TipoUnidadAppService.ObtenerTiposUnidadPorProducto(id);
            result = result
                        .OrderBy(x => x.TipoUnidad)
                        .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SearchProducts(string searchTerm)
        {
            try
            {
                var result = ProductosAppService.SearchProductsByTerm(Url.Encode(searchTerm));
                  
                return Json(new
                {
                    results = result.Select(p => new
                    {
                        id = p.Id,
                        text = $"{p.CodigoInterno} {p.Producto} {p.Descripcion.Replace(p.Producto, "")}",
                        data = p,
                        photo = p.PathArchivo
                    }).ToArray(),
                    pagination = new { more = false }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Out?.WriteLine(ex.ToString());
                return Json(new {
                    results = new object[] { new { id = 0, text = ex.Message } },
                    pagination = new { more = false }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SearchProductsByLocal(int idLocal, string searchTerm)
        {
            try
            {
                var result = InventarioAppService.SearchProductsByLocal(idLocal, Url.Encode(searchTerm));

                return Json(new
                {
                    results = result.Select(p => new
                    {
                        id = p.Id,
                        text = $"{p.CodigoInterno} {p.Producto} {p.Descripcion.Replace(p.Producto, "")}",
                        data = p,
                        photo = p.PathArchivo
                    }).ToArray(),
                    pagination = new { more = false }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Out?.WriteLine(ex.ToString());
                return Json(new
                {
                    results = new object[] { new { id = 0, text = ex.Message } },
                    pagination = new { more = false }
                }, JsonRequestBehavior.AllowGet);
            }
        }


        private List<ProductoViewModel> buscarProductos(string searchTerm, int idOrigen)
        {
            OrigenesBusquedaProductos origen = (OrigenesBusquedaProductos)idOrigen;


            List<ProductoViewModel> productosModel;
            
            if (origen.Equals(OrigenesBusquedaProductos.MantProducto))
            {
                var searchModel = new SearchProductModel
                {
                    StateForSearch = ProductStatus.All,
                    SearchTerm = searchTerm
                };

                var productosDto = ProductosAppService.BuscarProductos(searchModel);
                productosModel = Mapper.Map<List<ProductoViewModel>>(productosDto);
            }
            else
            {
                var productosDto = ProductosAppService.SearchProductsByTerm(Url.Encode(searchTerm));
                productosModel = Mapper.Map<List<ProductoViewModel>>(productosDto);
            }
             
            productosModel.ForEach(pro =>
            {
                //pro.FotoDataUrl = GetProductImage(pro.PathArchivo);
                pro.FotoDataUrl = pro.PathArchivo;
            });

            return productosModel;
        }

        public ActionResult Photo(string path)
        {
            return File(Server.GetImageFrom($"~{AppServiceBase.Empresa.RaizArchivo}{path}"), "image/jpeg");
        }

        private string GetProductImage(string productImagePath)
        { 
            return Server.ImageToBase64String($"~{AppServiceBase.Empresa.RaizArchivo}{productImagePath}");
        }

        private void DeleteProductImage(string pathArchivo)
        {
            try
            {
                if (!string.IsNullOrEmpty(pathArchivo))
                {
                    string realPath = Server.MapPath($"~{AppServiceBase.Empresa.RaizArchivo}{pathArchivo}");
                    if (System.IO.File.Exists(realPath))
                    {
                        System.IO.File.Delete(realPath);
                    }
                }
            }
            catch
            {
            }
        }

        private string SaveProductImage(HttpPostedFileBase poImgFile, string codigoInternoProducto)
        {
            try
            {
                var directory = Server.MapPath("~"+Path.Combine(AppServiceBase.Empresa.RaizArchivo.Replace("/","\\"), "Productos"));

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                //todo obtener extension del archivo y nombrar con el codigo interno
                var filename = $"{codigoInternoProducto}.jpg";
                var productImagePath = $"{directory}\\{filename}";
                var productFilename = $"/Productos/{codigoInternoProducto}.jpg";
                
                //DeleteProductImage(productFilename);

                poImgFile.SaveAs(productImagePath);

                return productFilename;
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un problema al guardar la imagen del producto", ex);
            }
        }

        public ActionResult CargarProductos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadExcelFile()
        {
            int i = 1;
            try
            {
                HttpPostedFileBase files = Request.Files[0];
                ISheet sheet;
                bool isFileValid = true;
                string filename = Path.GetFileName(Server.MapPath(files.FileName));
                var fileExt = Path.GetExtension(filename);
                if (fileExt == ".xls")
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(files.InputStream);
                    sheet = hssfwb.GetSheetAt(0);
                }
                else
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(files.InputStream);
                    sheet = hssfwb.GetSheetAt(0);
                }
                var divisiones = DivisionesAppService.ObtenerDivisiones();
                var marcas = MarcasAppService.ObtenerMarcas();

                decimal totalCompra = 0;
                List<ProductoViewModel> rows = new List<ProductoViewModel>();
                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row != null) //null is when the row only contains empty cells   
                    {
                        i++;


                        var codigoproveedor = row.GetCell(0).GetString();
                        var codigobarra = row.GetCell(1).GetString();
                        var codigointerno = row.GetCell(2).GetString();
                        var tipoproducto = row.GetCell(3).GetString();
                        var producto = row.GetCell(4).GetString();
                        var descripcion = row.GetCell(5).GetString();
                        var stock = row.GetCell(6).GetNumber();
                        var stockminimo = row.GetCell(7).GetNumber();
                        var precio = row.GetCell(8).GetNumber();
                        var unidad = row.GetCell(9).GetString();
                        var descuento = row.GetCell(10).GetNumber();
                        var cobraiva = row.GetCell(11).GetString();
                        var division = row.GetCell(12).GetString();
                        var linea = row.GetCell(13).GetString();
                        var categoria = row.GetCell(14).GetString();
                        var marca = row.GetCell(15).GetString();
                        var costo = row.GetCell(16).GetNumber();



                        if (!string.IsNullOrEmpty(codigointerno))
                        {
                            var detalle = new ProductoViewModel();
                            var prod = ProductosAppService.GetProductByInternalCode(Url.Encode(codigointerno));
                            
                            if (prod == null)
                            {    
                                detalle.CodigoInterno = codigointerno;
                                detalle.Descripcion = $"{producto}";
                            }
                            else
                            {
                                detalle = Mapper.Map<ProductoViewModel>(prod); 
                            }


                            detalle.CodigoProveedor = codigoproveedor;
                            detalle.CodigoBarra = codigobarra;
                            detalle.TipoProducto = tipoproducto;
                            detalle.Producto = producto;
                            detalle.Descripcion = descripcion;
                            detalle.Stock = Convert.ToDecimal(stock);
                            detalle.StockMinimo = Convert.ToDecimal(stockminimo);
                            detalle.UnidadMedida = 1;
                            detalle.Descuento = Convert.ToDecimal(descuento);
                            detalle.CobraIva = cobraiva?.ToUpper() == "SI";
                            detalle.Costo = Convert.ToDecimal(costo);
                            detalle.IdEstado = 1;
                            detalle.IdEmpresa = AppServiceBase.Empresa.Id;  
                            
                            var div = divisiones.FirstOrDefault(x => x.Division?.ToUpper() == division?.ToUpper());

                            if (div == null)
                            {
                                div = DivisionesAppService.GuardarDivision(new DivisionDto
                                {
                                    Division = division,
                                    IdEstado = 1,
                                    IdEmpresa = AppServiceBase.Empresa.Id,
                                    IpIngreso = AppServiceBase.ObtenerIp(),
                                    FechaIngreso = DateTime.Now,
                                    UsuarioIngreso = AppServiceBase.AppUser.UserName
                                });

                                divisiones.Add(div);
                            }

                            detalle.IdDivision = div?.Id;
                            detalle.Division = division;

                            var lineas = LineasAppService.ObtenerLineas(div.Id);

                            if (!string.IsNullOrEmpty(linea))
                            {
                                var lin = lineas.FirstOrDefault(x => x.Linea?.ToUpper() == linea?.ToUpper());
                                if (lin == null)
                                {
                                    lin = LineasAppService.GuardarLinea(new LineDto
                                    {
                                        Linea = linea,
                                        IdDivision = div.Id,
                                        IdEstado = 1,
                                        IdEmpresa = AppServiceBase.Empresa.Id,
                                        IpIngreso = AppServiceBase.ObtenerIp(),
                                        FechaIngreso = DateTime.Now,
                                        UsuarioIngreso = AppServiceBase.AppUser.UserName
                                    });
                                }

                                detalle.IdLinea = lin?.Id;
                                detalle.Linea = linea;

                                if (!string.IsNullOrEmpty(categoria))
                                { 
                                    var categorias = CategoriasAppService.ObtenerCategorias(lin.Id);

                                    var cat = categorias.FirstOrDefault(x => x.Categoria?.ToUpper() == categoria?.ToUpper());
                                    if (cat == null)
                                    {
                                        cat = CategoriasAppService.GuardarCategoria(new CategoryDto
                                        {
                                            IdLinea = lin.Id,
                                            Categoria = categoria,
                                            
                                            IdEstado = 1,
                                            IdEmpresa = AppServiceBase.Empresa.Id,
                                            IpIngreso = AppServiceBase.ObtenerIp(),
                                            FechaIngreso = DateTime.Now,
                                            UsuarioIngreso = AppServiceBase.AppUser.UserName
                                        });
                                    }

                                    detalle.IdCategoria = cat?.Id;
                                    detalle.Categoria = categoria;
                                }
                            }
                             
                            if (!string.IsNullOrEmpty(marca))
                            {
                                var mar = marcas.FirstOrDefault(x => x.Marca?.ToUpper() == marca?.ToUpper());
                                if (mar == null)
                                {
                                    mar = MarcasAppService.GuardarMarca(new BrandDto
                                    {
                                        Marca = marca,
                                        IdEstado = 1,
                                        IdEmpresa = AppServiceBase.Empresa.Id,
                                        IpIngreso = AppServiceBase.ObtenerIp(),
                                        FechaIngreso = DateTime.Now,
                                        UsuarioIngreso = AppServiceBase.AppUser.UserName
                                    });
                                    marcas.Add(mar);
                                }

                                detalle.IdMarca = mar?.Id;
                                detalle.Marca = marca;
                            }

                            // Guardamos o actualizamos el nuevo producto
                            var result = GuardarProducto(detalle);
                            var data = result.Data as RespuestaViewModel;
                            
                            int id;
                            
                            if (int.TryParse($"{data?.Data}", out id))
                            {
                                detalle.Id = id;

                                // Entonces todo fue bien:
                                // Ahora agregarmos el precio del producto;
                                UnitTypeDto tipounidad = null;
                                try
                                {
                                    tipounidad = TipoUnidadAppService.ObtenerTipoUnidad(id, unidad);
                                }
                                catch { }
                                 
                                if (tipounidad==null)
                                {
                                    tipounidad = new UnitTypeDto
                                    {
                                        TipoUnidad = unidad,
                                        IdProducto = id,
                                        UnidadMedidad = 1,
                                        IncluyeIva = false,
                                        Producto = producto,
                                        IdEstado = 1,
                                        IdEmpresa = (short)AppServiceBase.Empresa.Id,
                                        IpIngreso = AppServiceBase.ObtenerIp(),
                                        FechaIngreso = DateTime.Now,
                                        UsuarioIngreso = AppServiceBase.AppUser.UserName
                                    };
                                    //agregamos el tipo de unidad;
                                }

                                tipounidad.Precio = Convert.ToDecimal(precio);
                                tipounidad.Margen = 100;
                                tipounidad.Cantidad = 1;
                                tipounidad.CantidadMinima = 0; 

                                TipoUnidadAppService.GuardarTipoUnidad(tipounidad);


                            }

                            detalle.Costo= Convert.ToDecimal(precio);
                            detalle.FotoDataUrl = unidad;


                            rows.Add(detalle);
                        }
                    }
                }

                ViewBag.IsFileValid = isFileValid;
                ViewBag.Total = totalCompra; 

                return View("UploadedExcelData", rows);
            }
            catch (Exception ex)
            {
                Console.WriteLine(i.ToString());
                return Json(new { success = false, error = $"Error en Linea {i}: {ex}" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ReporteProductos()
        {
            var model = new FacturaViewModels();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ConsultarProductosReporte(FacturaViewModels model, string format = "EXCEL")
        {
            var response = await ProductosAppService.DownloadProductosAsync(model, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

    }
}