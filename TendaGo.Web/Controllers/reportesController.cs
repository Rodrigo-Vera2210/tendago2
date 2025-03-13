
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json.Linq;
using TendaGo.Web.Models;
using TendaGo.Web.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class reportesController : Controller
    {
        public int? IdLocalSelected => Session.GetIdLocal();
        private readonly IEmbedService m_embedService;

        public reportesController()
        {
            m_embedService = new EmbedService();
        }

        
        // GET: reportes
        public ActionResult Dashboard()
        {
            var report = new ReportViewModel("/tendago/Ventas");
            report.Parameters.Add("IdEmpresa", User.Identity.GetIdEmpresa());
            report.Parameters.Add("IdLocal", IdLocalSelected.Value);
            report.Parameters.Add("Desde", DateTime.Today.AddDays(-30));
            report.Parameters.Add("Hasta", DateTime.Today);
            report.Parameters.Add("TipoTransaccion", 1);
            return View(report);
        }


        public async Task<ActionResult> Index(string username, string roles)
        {
            var filter = $"Ventas/IdEmpresa eq {User?.Identity?.GetIdEmpresa()}";

            await m_embedService.EmbedReport(username, roles, filter);
            
            return View(m_embedService.EmbedConfig);
            
        }


        // GET: inventario
        public ActionResult Inventario()
        {
            var report = new ReportViewModel("/tendago/Inventario");
            report.Parameters.Add("IdEmpresa", User.Identity.GetIdEmpresa());
            report.Parameters.Add("IdLocal", IdLocalSelected.Value);
            report.Parameters.Add("Desde", DateTime.Today.AddDays(-30));
            report.Parameters.Add("Hasta", DateTime.Today);
            report.Parameters.Add("TipoTransaccion", 1);
            return View(report);
        }


    }
}