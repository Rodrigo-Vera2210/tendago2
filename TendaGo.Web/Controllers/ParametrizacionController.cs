using System;
using AutoMapper;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using TendaGo.Web.ApplicationServices;
//using TendaGo.Web.ProductoService;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class ParametrizacionController : Controller
    {
        #region Unidad de Medida
        public ActionResult IndexUnidadMedida()
        {
            //ViewBag.FiltrosBusqueda = ObtenerFiltrosBusquedaUnidadMedida();
            return View();
        }

        public ActionResult Unidades()
        {
            return View();
        }

        public ActionResult GetUnits()
        {
            var units = UnidadMedidaAppService.ObtenerUnidadesDeMedida();
            return PartialView(units);
        }

        public ActionResult MantUnit(int id)
        {
            if (id != 0)
            {
                var unit = UnidadMedidaAppService.ObtenerUnidad(id);
                ViewBag.IsEdit = true;
                var unitModel = new UnidadMedidaModel { Id = unit.Id, UnidadMedida = unit.UnidadMedida, IdEstado = unit.IdEstado.Equals(1) };
                return PartialView(unitModel);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult MantUnit(UnidadMedidaModel model)
        {
            var unidad = new MeasurementUnitDto
            {
                Id = model.Id,
                UnidadMedida = model.UnidadMedida,
                IdEstado = model.IdEstado ? (short)1 : (short)0,
                UsuarioModificacion = model.Id != 0 ? User.Identity.Name : string.Empty,
                IpModificacion = model.Id != 0 ? AppServiceBase.ObtenerIp() : string.Empty,
                UsuarioIngreso = model.Id == 0 ? User.Identity.Name : string.Empty,
                IpIngreso = model.Id == 0 ? AppServiceBase.ObtenerIp() : string.Empty,
                IdEmpresa= User.Identity.GetIdEmpresa()
        };
            return SaveUnit(unidad);
        }

        private JsonResult SaveUnit(MeasurementUnitDto unit)
        {
            try
            {
                var result = UnidadMedidaAppService.GuardarUnidad(unit);
                if (result != null)
                {
                    return Json(new { Success = true, Mensaje = "Registro Guardado", unit = result }, JsonRequestBehavior.AllowGet);
                }

                throw new Exception("Hubo un error al guardar la unidad");
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

        #region Metodos Privados Unidades

        //private List<MeasurementUnitDto> GetUnitsCatalog()
        //{
        //    var units = new List<MeasurementUnitDto>();
        //    var client = new RestClient(Tools.GetApiUrlBase());
        //    var request = new RestRequest("/measurementUnits", Method.GET);
        //    var restResponse = client.Execute<List<MeasurementUnitDto>>(request);
        //    if (restResponse.IsSuccessful)
        //    {
        //        units = restResponse.Data;
        //    }
        //    return units;
        //}

        //private MeasurementUnitDto GetUnit(int id)
        //{
        //    var unit = new MeasurementUnitDto();
        //    var client = new RestClient(Tools.GetApiUrlBase());
        //    var request = new RestRequest($"/measurementUnits/{id}", Method.GET);
        //    request.AddHeader("app_token", User.Identity.GetTokenUsuario());
        //    var restResponse = client.Execute<MeasurementUnitDto>(request);
        //    if (restResponse.IsSuccessful)
        //    {
        //        unit = restResponse.Data;
        //    }
        //    return unit;
        //}

        #endregion

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ConsultaUnidadMedida(string filtroBusqueda, string textoBusqueda)
        //{
        //    UnidadMedidaService.UnidadMedidaServiceClient servicio = new UnidadMedidaService.UnidadMedidaServiceClient();
        //    var unidadesMedidaDto = servicio.ConsultarUnidadesMedidaPorFiltro(filtroBusqueda, textoBusqueda, UnidadMedidaService.EstadoDto.TODOS);
        //    var unidadesMedidaModel = Mapper.Map<List<UnidadMedidaViewModel>>(unidadesMedidaDto);
        //    return PartialView(unidadesMedidaModel);
        //}

        [HttpGet]
        public ActionResult IngresoUnidadMedida()
        {
            return PartialView();
        }

        #endregion

        #region Producto
        #endregion

        #region Division

        public ActionResult Division()
        {
            return View();
        }

        private DivisionDto GetDivision(int id)
        {
            return DivisionesAppService.ObtenerDivision(id);
        }

        public ActionResult GetDivisions()
        {
            var divisions = GetDivisionsCatalog();
            return PartialView(divisions);
        }

        public JsonResult GetDivisionsList()
        {
            var divisions = GetDivisionsCatalog();
            return Json(divisions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MantDivision(int id)
        {
            if (id != 0)
            {
                var division = GetDivision(id);
                var divisionModel = new DivisionModel { Id = division.Id, Name = division.Division, IdEstado = division.IdEstado.Equals(1) };
                ViewBag.IsEdit = true;
                return PartialView(divisionModel);
            }
            return PartialView(new DivisionModel());
        }

        [HttpPost]
        public ActionResult MantDivision(DivisionModel model)
        {
            var div = new DivisionDto
            {
                Id = model.Id,
                Division = model.Name,
                IdEstado = model.IdEstado ? (short)1 : (short)0,
                UsuarioModificacion = model.Id != 0 ? User.Identity.Name : string.Empty,
                IpModificacion = model.Id != 0 ? AppServiceBase.ObtenerIp() : string.Empty,
                UsuarioIngreso = model.Id == 0 ? User.Identity.Name : string.Empty,
                IpIngreso = model.Id == 0 ? AppServiceBase.ObtenerIp() : string.Empty
            };
            return SaveDivision(div);
        }

        private JsonResult SaveDivision(DivisionDto division)
        {
            try
            {
                var result = DivisionesAppService.GuardarDivision(division);

                if (result != null)
                {
                    return Json(new RespuestaViewModel { Success = true, Mensaje = "Registro Guardado" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!" }, JsonRequestBehavior.AllowGet);

            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!", Error = ex.ToString()}, JsonRequestBehavior.AllowGet);
            }

        }
        
        private List<DivisionDto> GetDivisionsCatalog()
        {
            var divisions = DivisionesAppService.ObtenerDivisiones();
            return divisions;
        }

        #endregion

        #region Linea
        public ActionResult Lineas()
        {
            ViewBag.UrlBase = Tools.GetApiUrlBase();
            return View();
        }

        public ActionResult GetLines(int idDivision)
        {
            var lines = GetLinesCatalog(idDivision);
            return PartialView(lines);
        }

        public JsonResult GetLinesList(int idDivision)
        {
            var lines = GetLinesCatalog(idDivision);
            return Json(lines, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MantLine(int id)
        {
            var divisionList = this.GetDivisionsCatalog();
            ViewBag.DivisionList = new SelectList(divisionList, "Id", "Division");
            if (id != 0)
            {
                var line = GetLine(id);
                var lineModel = new LineModel { Id = line.Id, IdDivision = line.IdDivision, Name = line.Linea, IdEstado = line.IdEstado.Equals(1) };
                ViewBag.IsEdit = true;
                return PartialView(lineModel);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult MantLine(LineModel model)
        {
            var line = new LineDto
            {
                Id = model.Id,
                Linea = model.Name,
                IdDivision = model.IdDivision,
                IdEstado = model.IdEstado ? (short)1 : (short)0,
                UsuarioModificacion = model.Id != 0 ? User.Identity.Name : string.Empty,
                IpModificacion = model.Id != 0 ? AppServiceBase.ObtenerIp() : string.Empty,
                UsuarioIngreso = model.Id == 0 ? User.Identity.Name : string.Empty,
                IpIngreso = model.Id == 0 ? AppServiceBase.ObtenerIp() : string.Empty
            };

            return SaveLine(line);
        }

        #region Private Methods Linea

        private LineDto GetLine(int id)
        {
            return LineasAppService.ObtenerLinea(id);
        }

        private JsonResult SaveLine(LineDto line)
        {
            try
            {
                var result = LineasAppService.GuardarLinea(line);

                if (result != null)
                {
                    return Json(new { Success = true, Mensaje = "Registro Guardado", line = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!" }, JsonRequestBehavior.AllowGet);

            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!", Error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        private List<LineDto> GetLinesCatalog(int idDivision)
        {
            var lines = LineasAppService.ObtenerLineas(idDivision);
            return lines;
        }

        #endregion

        #endregion

        #region Categoria
        public ActionResult Categorias()
        {
            return View();
        }

        public ActionResult GetCategories(int idLine)
        {
            var cats = CategoriasAppService.ObtenerCategorias(idLine);
            return PartialView(cats);
        }

        public JsonResult GetCategoriesList(int idLine, bool all = true)
        {
            var cats = all ? CategoriasAppService.ObtenerCategorias(idLine)
                : CategoriasAppService.ObtenerCategoriasActivas(idLine);

            return Json(cats, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MantCategory(int id)
        {
            var divisionList = this.GetDivisionsCatalog();
            var lineList = this.GetLinesCatalog(divisionList.FirstOrDefault().Id);

            ViewBag.DivisionList = new SelectList(divisionList, "Id", "Division");

            if (id != 0)
            {
                var cat = GetCategory(id);
                var line = GetLine(cat.IdLinea);
                ViewBag.LineList = new SelectList(this.GetLinesCatalog(line.IdDivision), "Id", "Linea");
                var catModel = new CategoriaModel { Id = cat.Id, IdDivision = line.IdDivision, IdLinea = cat.IdLinea, Categoria = cat.Categoria, IdEstado = cat.IdEstado.Equals(1) };
                ViewBag.IsEdit = true;
                return PartialView(catModel);
            }
            ViewBag.LineList = new SelectList(lineList, "Id", "Linea");
            return PartialView();
        }

        [HttpPost]
        public ActionResult MantCategory(CategoriaModel model)
        {
            var catDto = new CategoryDto
            {
                Id = model.Id,
                Categoria = model.Categoria,
                IdLinea = model.IdLinea,
                IdEstado = model.IdEstado ? (short)1 : (short)0,
                UsuarioModificacion = model.Id != 0 ? User.Identity.Name : string.Empty,
                IpModificacion = model.Id != 0 ? AppServiceBase.ObtenerIp() : string.Empty,
                UsuarioIngreso = model.Id == 0 ? User.Identity.Name : string.Empty,
                IpIngreso = model.Id == 0 ? AppServiceBase.ObtenerIp() : string.Empty
            };
            return SaveCategory(catDto);
        }

        #region Private Methods Categorias
        private CategoryDto GetCategory(int id)
        {
            return CategoriasAppService.ObtenerCategoria(id);
        }
         
        private JsonResult SaveCategory(CategoryDto categoryDto)
        {
            try
            {
                var result = CategoriasAppService.GuardarCategoria(categoryDto);

                if (result != null)
                {
                    return Json(new { Success = true, Mensaje = "Registro Guardado", cat = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!" }, JsonRequestBehavior.AllowGet);

            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!", Error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #endregion

        #region Marcas

        public ActionResult Marcas()
        {
            ViewBag.UrlBase = Tools.GetApiUrlBase();
            return View();
        }

        public ActionResult GetBrands()
        {
            var brands = GetBrandsCatalog();
            return PartialView(brands);
        }

        public ActionResult MantBrand(int id)
        {
            if (id != 0)
            {
                var brand = GetBrand(id);
                ViewBag.IsEdit = true;
                var brandModel = new MarcaModel { Id = brand.Id, Marca = brand.Marca, IdEstado = brand.IdEstado.Equals(1) };
                return PartialView(brandModel);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult MantBrand(MarcaModel model)
        {
            var brandDto = new BrandDto
            {
                Id = model.Id,
                Marca = model.Marca,
                IdEstado = model.IdEstado ? (short)1 : (short)0,
                UsuarioModificacion = model.Id != 0 ? User.Identity.Name : string.Empty,
                IpModificacion = model.Id != 0 ? AppServiceBase.ObtenerIp() : string.Empty,
                UsuarioIngreso = model.Id == 0 ? User.Identity.Name : string.Empty,
                IpIngreso = model.Id == 0 ? AppServiceBase.ObtenerIp() : string.Empty
            };
            return SaveBrand(brandDto);
        }

        #region Private Methods Marcas

        private JsonResult SaveBrand(BrandDto brandDto)
        {
            try
            {
                var result = MarcasAppService.GuardarMarca(brandDto);

                if (result != null)
                {
                    return Json(new { Success = true, Mensaje = "Registro Guardado", brand = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!" }, JsonRequestBehavior.AllowGet);

            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = "Hubo un error al guardar el registro!", Error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        private BrandDto GetBrand(int id)
        {
            return MarcasAppService.ObtenerMarca(id);
        }

        private List<BrandDto> GetBrandsCatalog()
        {
            return MarcasAppService.ObtenerMarcas();
        }

        #endregion
        
        #endregion
        
        public ActionResult Locales()
        {
            return View();
        }
    }
}