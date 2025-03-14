//using ER.BA;
//using ER.BE;
//using System;
//using System.Configuration;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Xml;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    [TokenAuthorize]
//    public class documentsController : ApiControllerBase
//    {
//        private static string SriServiceUrl => ConfigurationManager.AppSettings["SRI:ServicioConsultaUrl"]
//                                                        ?? "https://cel.sri.gob.ec/comprobantes-electronicos-ws/AutorizacionComprobantesOffline?wsdl";

//        /// <summary>
//        /// Crea un documento electronico
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost, Route("documents/create")]
//        public DocumentDto PostDocument(DocumentDto model)
//        {
//            try
//            {
//                DocumentoEntity documento = model.ToDocumentoEntity();
//                documento.IdEmpresa = CurrentUser.IdEmpresa;

//                if (string.IsNullOrEmpty(documento.Id))
//                {
//                    documento.SetNewState();

//                    documento.Fecha = DateTime.Now;
//                    documento.FechaIngreso = DateTime.Now;
//                    documento.IdEstado = 1;
//                }
//                else
//                {
//                    documento.SetUpdatedState();
//                }

//                documento = documento.GenerateInvoice();
//                if (string.IsNullOrEmpty(documento.Id))
//                {
//                    throw new Exception("Hubo un error al guardar el documento");
//                }

//                var result = DocumentoBussinesAction
//                    .Save(documento)
//                    .ToDocumentDto();

//                return result;
//            }
//            catch (Exception ex)
//            {
//                string UserError = "Ocurrio un error general, reintente";
//                if (ex.GetMessage().Contains("PK_Salida"))
//                {
//                    UserError = "No puede ingresar un documento duplicado";
//                }

//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                    $"{ex.GetAllMessages()}", UserError));
//            }

//        }

//        [HttpGet, Route("documents/{id}")]
//        public DocumentDto GetInvoiceById(string id)
//        {
//            try
//            {
//                var output = DocumentoBussinesAction
//                                .LoadByPK(id)
//                                .ToDocumentDto();

//                output.Detalles = DetalleDocumentoCollectionBussinesAction
//                    .FindByAll(new DetalleDocumentoFindParameterEntity { IdDocumento = id })
//                    .Select(m => m.ToDocumentDetailDto())
//                    .ToList();

//                return output;
//            }
//            catch (HttpResponseException)
//            {
//                throw;
//            }
//            catch (Exception ex)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
//            }
//        }


//        [HttpGet, Route("documents/{id}/download")]
//        public async Task<HttpResponseMessage> DownloadInvoice(string id, DownloadTypeEnum type = DownloadTypeEnum.PDF)
//        {
//            try
//            {
//                var doc = DocumentoBussinesAction.LoadByPK(id, 0);
//                if (doc == null)
//                {
//                    throw Request.BuildHttpErrorException(HttpStatusCode.NotFound, "El documento especificado no existe!");
//                }

//                var ruc = RucBussinesAction.LoadByPK(doc.RUC);
//                //if (string.IsNullOrEmpty(ruc.TokenFactElectonica))
//                //{
//                //    throw Request.BuildHttpErrorException(HttpStatusCode.NotFound, "Su cuenta no esta conectada con facturación electrónica!");
//                //}

//                using (var client = new HttpClient())
//                {
//                    if (doc.IdFacturaGo > 0)
//                        return await client.GetAsync($"{CommonFunctions.UrlFacturago}/comprobantes/{doc.IdFacturaGo}/download");
//                    else
//                    {
//                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ruc.TokenFactElectonica);
//                        return await client.GetAsync($"{CommonFunctions.UrlEcuafact}/Documents/{doc.NumeroDocumento}/{type}");
//                    }
//                }
//            }
//            catch (HttpResponseException ex)
//            {
//                throw ex;
//            }
//            catch (Exception ex)
//            {
//                throw Request.BuildExceptionResponse(ex);
//            }
//        }


//        [HttpGet, Route("documents/sri/{claveacceso}/xml")]
//        public async Task<HttpResponseMessage> DescargarXMLSri(string claveacceso)
//        {
//            try
//            {
//                var client = new HttpClient();

//                client.DefaultRequestHeaders.Add("Accept", "*/*");

//                var content = new StringContent($"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ns1=\"http://ec.gob.sri.ws.autorizacion\"><SOAP-ENV:Body><ns1:autorizacionComprobante><claveAccesoComprobante>{claveacceso}</claveAccesoComprobante>\r\n    </ns1:autorizacionComprobante>\r\n  </SOAP-ENV:Body>\r\n</SOAP-ENV:Envelope>\r\n", Encoding.UTF8, "application/xml");
//                content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

//                var response = await client.PostAsync(SriServiceUrl, content);

//                try
//                {
//                    var stream = await response.Content.ReadAsStreamAsync();

//                    XmlDocument xmlDoc = new XmlDocument();
//                    xmlDoc.Load(stream);

//                    XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);

//                    xmlnsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
//                    xmlnsManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
//                    xmlnsManager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
//                    xmlnsManager.AddNamespace("ns2", "http://ec.gob.sri.ws.autorizacion");

//                    XmlNode node = xmlDoc.SelectSingleNode("/soap:Envelope/soap:Body/ns2:autorizacionComprobanteResponse", xmlnsManager);
//                    var filename = node.SelectSingleNode("/RespuestaAutorizacionComprobante/claveAccesoConsultada").InnerText;
//                    var bytes = Encoding.UTF8.GetBytes(node.InnerText);

//                    var result = Request.CreateResponse(HttpStatusCode.OK);
//                    result.Content = new ByteArrayContent(bytes);
//                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
//                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
//                    {
//                        FileName = filename + ".xml"
//                    };

//                    return result;
//                }
//                catch
//                {
//                    return response;
//                }
//            }
//            catch (Exception ex)
//            {
//                var response = Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", ex.Message);
//                return await Task.FromResult(response);
//            }
//        }

//    }

//    public enum DownloadTypeEnum
//    {
//        PDF, XML, Word, Excel
//    }
//}
