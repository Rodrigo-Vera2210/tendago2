using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Api.Reporting;
using TendaGo.Common;
using TendaGo.Common.EntitiesDto;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class reportsController : ApiControllerBase
    {
        private const string OUTPUT_REPORT_TICKET = "/tendago/Salida_Slim";
        private const string OUTPUT_REPORT = "/tendago/Salida";
        private const string INVENTORYB_REPORT = "/tendago/SalidaBodega";
        private const string INVENTORY_REPORT = "/tendago/EntradaBodega";
        private const string INVENTORYR_REPORT = "/tendago/InventarioValorizado";
        private const string CUENTAS_COBRO = "/tendago/EstadoDeCuenta";
        private const string STOCK_BODEGAS = "/tendago/Stock";
        private const string INPUT_REPORT = "/tendago/Entrada";
        private const string INVENTORY_KARDEX = "/tendago/Kardex";
        private const string INVOICE_REPORT = "/tendago/consultaFactura";
        private const string OUTPUTS_REPORT = "/tendago/consultaVentas";
        private const string OUTPUTSGENERAL_REPORT = "/tendago/consultaVentasGeneral";
        private const string PRODUCTS_REPORT = "/tendago/consultaProductos";
        private const string CLIENTS_REPORT = "/tendago/consultaClientes";
        private const string PURCHASE_REPORT = "/tendago/ConsultaCompras";
        private const string INVENTORYMOVEMENT_REPORT = "/tendago/ConsultarMovimientoInventario";
        private const string CASHCLOSURE_REPORT = "/tendago/ConsultarCierreCaja";

        /// <summary>
        /// Descarga el documento del pedido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Route("output/{id}/ticket")]
        public async Task<HttpResponseMessage> GetOutputReportSlim(string id, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(OUTPUT_REPORT_TICKET);

            // Configuro los parametros del reporte
            builder.AddParameter("IdSalida", id);

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.SLIM);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);

        }


        /// <summary>
        /// Descarga el documento del pedido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Route("output/{id}/download")]
        public async Task<HttpResponseMessage> GetOutputReport(string id, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(OUTPUT_REPORT);

            // Configuro los parametros del reporte
            builder.AddParameter("IdSalida", id);

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format));

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        [Route("inventory/{id}/download")]
        public async Task<HttpResponseMessage> GetInventoryReport(string id, ReportFormatEnum format = ReportFormatEnum.PDF, string view = default)
        {
            ReportBuilder builder;
            // Obtengo el generador de reportes
            if (view == "Inventario")
            {
                builder = new ReportBuilder(INVENTORY_REPORT);
            }
            else
            {
                builder = new ReportBuilder(INVENTORYB_REPORT);
            }


            // Configuro los parametros del reporte
            builder.AddParameter("IdSalida", id);

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format));

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Route("inventarioResumido/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> InventarioResumido(ProductDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(INVENTORYR_REPORT);

            #region Configuro los parametros del reporte
            builder.AddParameter("IdEmpresa", model.IdEmpresa);

            if (model.IdMarca != null)
            {
                builder.AddParameter("IdMarca", model.IdMarca);
            }
            else
            {
                builder.AddParameter("IdMarca", 0);
            }

            if (model.IdDivision != null)
            {
                builder.AddParameter("IdDivision", model.IdDivision);
            }
            else
            {
                builder.AddParameter("IdDivision", 0);
            }

            if (model.IdLinea != null)
            {
                builder.AddParameter("IdLinea", model.IdLinea);
            }
            else
            {
                builder.AddParameter("IdLinea", 0);
            }

            if (model.IdCategoria != null)
            {
                builder.AddParameter("IdCategoria", model.IdCategoria);
            }
            else
            {
                builder.AddParameter("IdCategoria", 0);
            }

            if (model.UnidadMedida != null)
            {
                builder.AddParameter("UnidadMedida", model.UnidadMedida);
            }
            else
            {
                builder.AddParameter("UnidadMedida", 0);
            }
            #endregion

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);

        }

        [Route("estadoDeCuentaClientes/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> EstadoDeCuentaClientes(EntityDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(CUENTAS_COBRO);

            // Configuro los parametros del reporte
            builder.AddParameter("IdEmpresa", model.IdEmpresa);
            builder.AddParameter("IdCliente", model.Id);
            builder.AddParameter("Status", 0);
            builder.AddParameter("Full", model.Full);

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format));

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

        [Route("stockPorBodegas/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> StockPorBodegas(StockBodegaDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(STOCK_BODEGAS);

            // Configuro los parametros del reporte
            builder.AddParameter("IdEmpresa", model.IdEmpresa);
            builder.AddParameter("IdProducto", model.IdProducto);
            builder.AddParameter("IdLocal", model.IdLocal);

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format));

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Descarga el documento del pedido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Route("input/{id}/download")]
        public async Task<HttpResponseMessage> GetInputReport(string id, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(INPUT_REPORT);

            // Configuro los parametros del reporte
            builder.AddParameter("IdEntrada", id);

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format));

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        [Route("inventarioKardex/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> InventarioKardex(ProductDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(INVENTORY_KARDEX);

            #region Configuro los parametros del reporte
            builder.AddParameter("IdEmpresa", model.IdEmpresa);
            builder.AddParameter("FechaDesde", model.FechaDesde);
            builder.AddParameter("FechaHasta", model.FechaHasta);

            if (model.Id >= 1)
            {
                builder.AddParameter("IdProducto", model.Id);
            }
            else
            {
                builder.AddParameter("IdProducto", 0);
            }

            if (model.IdLocal >= 1)
            {
                builder.AddParameter("IdLocal", model.IdLocal);
            }
            else
            {
                builder.AddParameter("IdLocal", DBNull.Value);
            }

            #endregion

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);

        }

        [Route("consultaFactura/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultaFactura(DocumentReportDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(INVOICE_REPORT);
            //var fechaInicio = Convert.ToDateTime(model.FechaDesde);
            // Configuro los parametros del reporte            
            builder.AddParameter("FechaInicio", DateTime.ParseExact(model.FechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("FechaCorte", DateTime.ParseExact(model.FechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("IdEmpresa", model.IdEmpresa);
            builder.AddParameter("Ruc", model.RUC);


            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

        [Route("consultaVentas/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultaVentas(DocumentReportDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(OUTPUTSGENERAL_REPORT);
            if (model.IdEmpresa == 1)//reporte para empresa Carmita
            {
                builder = new ReportBuilder(OUTPUTS_REPORT);
            }

            //var fechaInicio = Convert.ToDateTime(model.FechaDesde);
            // Configuro los parametros del reporte            
            builder.AddParameter("FechaInicio", DateTime.ParseExact(model.FechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("FechaCorte", DateTime.ParseExact(model.FechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("IdEmpresa", model.IdEmpresa);
            builder.AddParameter("Ruc", model.RUC);


            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

        [Route("consultaProductos/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultaProductos(DocumentReportDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(PRODUCTS_REPORT);
            //var fechaInicio = Convert.ToDateTime(model.FechaDesde);
            // Configuro los parametros del reporte            
            builder.AddParameter("IdEmpresa", model.IdEmpresa);


            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

        [Route("consultaClientes/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultaClientes(DocumentReportDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(CLIENTS_REPORT);
            //var fechaInicio = Convert.ToDateTime(model.FechaDesde);
            // Configuro los parametros del reporte            
            builder.AddParameter("IdEmpresa", model.IdEmpresa);


            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }


        [Route("consultaCierre/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultaCierre(int idEmpresa, int idLocal, string FechaDesde, string FechaHasta, string IdCajero, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(CASHCLOSURE_REPORT);
            // Configuro los parametros del reporte            
            builder.AddParameter("idEmpresa", idEmpresa);
            builder.AddParameter("idLocal", idLocal);
            builder.AddParameter("fechaDesde", DateTime.ParseExact(FechaDesde, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            builder.AddParameter("fechaHasta", DateTime.ParseExact(FechaHasta, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            builder.AddParameter("idVendedor", (string.IsNullOrEmpty(IdCajero) ? string.Empty : IdCajero));

            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

        [Route("consultaMovimientos/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultaMovimientoInventario(ProductReportDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(INVENTORYMOVEMENT_REPORT);
            //var fechaInicio = Convert.ToDateTime(model.FechaDesde);
            // Configuro los parametros del reporte            

            builder.AddParameter("FechaInicio", DateTime.ParseExact(model.FechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("FechaFin", DateTime.ParseExact(model.FechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("IdEmpresa", model.IdEmpresa);
            builder.AddParameter("IdProducto", model.IdProducto);
            builder.AddParameter("TipoTransaccion", model.TipoTransaccion);



            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

        [Route("consultaEntrada/download")]
        [HttpPost]
        public async Task<HttpResponseMessage> ConsultaEntrada(DocumentReportDto model, ReportFormatEnum format = ReportFormatEnum.PDF)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder(PURCHASE_REPORT);
            //var fechaInicio = Convert.ToDateTime(model.FechaDesde);
            // Configuro los parametros del reporte            
            builder.AddParameter("FechaInicio", DateTime.ParseExact(model.FechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("FechaCorte", DateTime.ParseExact(model.FechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            builder.AddParameter("IdEmpresa", model.IdEmpresa);
            builder.AddParameter("Ruc", model.RUC);
            builder.AddParameter("IdProveedor", model.IdProveedor);


            // Genero el reporte
            var report = builder.Render(Convert.ToString(format), ReportBuilder.ReportSizeEnum.HORIZONTAL);

            // Ahora configuramos el archivo que se retornara segun el tipo especificado:
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(report.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(report.MimeType);

            return await Task.FromResult(result);
        }

    }


}
