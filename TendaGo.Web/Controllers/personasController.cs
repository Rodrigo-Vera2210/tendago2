using AutoMapper;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TendaGo.Web.Controllers
{
    public class personasController : Controller
    {
        // GET: personas
        public ActionResult Index(string id)
        {
            if (id == "proveedor")
            {
                ViewBag.Tipo = "Proveedores";
                ViewBag.Url = Url.Action("suppliers", new { uid = Tools.HashToken() });
            }
            else
            {
                ViewBag.Tipo = "Clientes";
                ViewBag.Url = Url.Action("customers", new { uid = Tools.HashToken() });
            }

            return View();
        }

        public async Task<JsonResult> suppliers()
        {
            var providers = ProveedorAppService.ConsultarProveedores(string.Empty);
            var model = providers.Select(x => new object[] { x.Id, x.Identificacion, x.RazonSocial, x.Correo, x.Telefono });

            return await Task.FromResult(Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet));
        }

        //[HttpGet]
        //public async Task<JsonResult> customers(string razonSocial)
        //{
        //    string filtro = razonSocial;
        //    var clientes = ClientesAppService.BuscarClientes(searchTerm: filtro, lite: true);
        //    var model = clientes.Select(x => new object[] { x.Id, x.Identificacion, x.RazonSocial, x.Correo, x.Telefono });

        //    return await Task.FromResult(Json(new
        //    {
        //        data = model
        //    }, JsonRequestBehavior.AllowGet));
        //}

        [HttpGet]
        public JsonResult customers(string id)
        {
            var clientes = ClientesAppService.BuscarClientes(searchTerm: id, lite: true);
            var model = clientes.Select(x => new object[] { x.Id, x.Identificacion, x.RazonSocial, x.Correo, x.Telefono });

            return (Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet));
        }

        public ActionResult ReporteClientes()
        {
            var model = new FacturaViewModels();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ConsultarClientesReporte(FacturaViewModels model, string format = "EXCEL")
        {
            var response = await ClientesAppService.DownloadClientesAsync(model, format);

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