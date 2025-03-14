//using ER.BA;
//using ER.BE;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using System.Web.Http;
//using TendaGo.Api.Reporting;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    [TokenAuthorize]
//    public class outputController : ApiControllerBase
//    {

//        [HttpPost, Route("output/report")]
//        public List<OutputsReportDto> GetReport(SearchOutputReport parametros)
//        {

//            SalidaFindParameterEntity ef = CatalogsExtensions.GlobalMapperConverter<SearchOutputReport, SalidaFindParameterEntity>(parametros);
//            DataSet outputs = SalidaCollectionBussinesAction.ReporteSalidas(ef);
//            return outputs.ConvertirToDto<OutputsReportDto>().ToList();
//        }

//        [HttpGet, Route("output;status={currenStatus}")]
//        public List<OutputDto> GetOutputsByCurrentStatus(TransactionStatus? currenStatus, TransactionType? transactionType = null)
//        {
//            //int statusConverted;
//            bool isValidId = (currenStatus != null);//int.TryParse(currenStatus, out statusConverted);

//            if (!isValidId)
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

//            var findParameter = new SalidaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa };
//            findParameter.EstadoActual = currenStatus.GetStatus();

//            if (transactionType != null)
//            {
//                findParameter.TipoTransaccion = transactionType.GetCode();
//            }

//            var salidasEntity = ER.BA.SalidaCollectionBussinesAction.FindByAll(findParameter, 1);
//            var outputs = salidasEntity.Select(x => x.ToOutputLiteDto()).ToList();
//            return outputs;
//        }

        

//        /// <summary>
//        /// Crea una salida de inventario
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost, Route("output/create")]
//        public OutputDto PostOutput(OutputCreateRequestModel model)
//        {
//            var salidaJson = JsonConvert.SerializeObject(model);

//            var salida = model.ToSalidaEntity();

//            SetCobroDebito(salida, model.CuentasPorCobrar);
//            SetInfoAdic(salida, model.InformacionAdicional);

//            var result = GuardarSalida(salida);

//            return result?.ToOutputDto();
//        }

//        /// <summary>
//        /// Devuelve salidas de pagetes por salida
//        /// </summary>
//        /// <param name="idSalida"></param>
//        /// <returns></returns>
//        [HttpGet, Route("output/{idSalida}/package")]
//        public List<EmpaquetadoEntity> GetPackage(string idSalida)
//        {
//            var packages = EmpaquetadoCollectionBussinesAction.FindByAll(new EmpaquetadoFindParameterEntity { IdSalida = idSalida, IdEstado = 1 }, 0); ;
//            List<EmpaquetadoEntity> pac = new List<EmpaquetadoEntity>();

//            if (packages != null && packages.Count() > 0)
//            {
//                foreach (var val in packages)
//                {
//                    pac.Add(val);
//                }
//            }

//            return pac;
//        }


//        /// <summary>
//        /// Crea una salida de inventario
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost, Route("output/detail/{id}")]
//        public void CheckDetail(string id, bool? empaquetado, bool? revisado)
//        {
//            DetalleSalidaBussinesAction.Check(id, empaquetado, revisado);
//        }

//        /// <summary>
//        /// Carga varias salidas de inventario
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost, Route("output/load")]
//        public List<OutputDto> PostOutputLoad(OutputLoadModel model)
//        {
//            try
//            {
//                if (model == null || model.ListaPedidos == null)
//                {
//                    throw new Exception("No se ha especificado las ordenes para guardar!");
//                }

//                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]))
//                {
//                    connection.Open();

//                    using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
//                    {
//                        try
//                        {
//                            var ordenes = new List<OutputDto>();

//                            foreach (var output in model.ListaPedidos)
//                            {
//                                output.TipoTransaccionPadre = model.TipoTransaccionPadre;
//                                output.TransaccionPadre = model.TransaccionPadre;
//                                output.FechaIngreso = model.FechaIngreso;
//                                output.UsuarioIngreso = model.UsuarioIngreso;
//                                output.IpIngreso = model.IpIngreso;

//                                var salida = output.ToSalidaEntity();

//                                SetCobroDebito(salida, output.CuentasPorCobrar);

//                                var result = GuardarSalida(salida, connection, transaction);

//                                if (result != null)
//                                {
//                                    ordenes.Add(result.ToOutputLiteDto());
//                                }
//                                else
//                                {
//                                    throw new Exception("Hubo un error al procesar los pedidos");
//                                }
//                            }

//                            transaction.Commit();

//                            return ordenes;
//                        }
//                        catch (Exception exc)
//                        {
//                            if (transaction != null)
//                            {
//                                transaction.Rollback();
//                            }

//                            throw exc;
//                        }
//                        finally
//                        {
//                            connection.Close();
//                        }
//                    }
//                }
//            }
//            catch (HttpResponseException ex)
//            {
//                throw ex;
//            }
//            catch (SqlException ex)
//            {
//                string userError = ex.GetMessage();

//                if (userError.Contains("PK_Salida"))
//                {
//                    userError = "No existe la salida especificada";
//                }
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                    ex.GetAllMessages(), userError));
//            }
//            catch (Exception ex)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                    ex.GetAllMessages(), ex.GetMessage()));
//            }

//        }


//        /// <summary>
//        /// Elimina una salida de inventario
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="dto"></param>
//        /// <returns></returns>
//        [HttpPost, Route("output/{id}/delete")]
//        public IHttpActionResult DeleteOutput(string id, OutputDeleteDto dto)
//        {
//            try
//            {
//                var salidaEntity = GetSalidaEntity(id);

//                if (salidaEntity == null)
//                {
//                    throw new Exception("PK_Salida");
//                }

//                salidaEntity.SetDeletedState();
//                salidaEntity.EstadoActual = "ANULADA";
//                salidaEntity.FechaModificacion = dto.FechaIngreso;
//                salidaEntity.UsuarioModificacion = dto.UsuarioIngreso;
//                salidaEntity.IpModificacion = dto.IpIngreso;
//                salidaEntity.Observaciones = $"ORDEN ANULADA: {dto.Observaciones} {dto.UsuarioIngreso} {dto.FechaIngreso} - {salidaEntity.Observaciones}";
//                salidaEntity.IdEstado = 0;

//                var newSalidaEntity = SalidaBussinesAction.Guardar(salidaEntity);

//                if (newSalidaEntity.CurrentState == EntityStatesEnum.SavedSuccessfully)
//                {
//                    return Ok(true);
//                }

//                throw new Exception("Hubo un error al eliminar el documento");
//            }
//            catch (HttpResponseException ex)
//            {
//                throw ex;
//            }
//            catch (SqlException ex)
//            {
//                string UserError = ex.GetMessage();

//                if (UserError.Contains("PK_Salida"))
//                {
//                    UserError = "No existe la salida especificada";
//                }
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                   ex.GetAllMessages(), UserError));
//            }
//            catch (Exception ex)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                    ex.GetAllMessages(), ex.GetMessage()));
//            }
//        }

//        [HttpPost, Route("output/{idSalida}/{IdLocalSelected}/changeLocal")]
//        public OutputDto PostOutputChange(string idSalida, int IdLocalSelected, OutputCreateRequestModel model)
//        {
//            var connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
//            connection.Open();
//            var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

//            try
//            {

//                //Transferencia
//                TransferRequest transfer = new TransferRequest();
//                transfer.Fecha = DateTime.Now;
//                transfer.EstadoActual = "EN PROCESO";
//                transfer.IdLocalDestino = model.IdLocal;
//                transfer.IdVendedor = User.Identity.Name;
//                transfer.IdEmpresa = CurrentUser.IdEmpresa;

//                //Valido el stock del producto            
//                var productos = model.DetalleNotaPedido.Select(x => x.IdProducto).JoinString(",");

//                var stocks = StockCollectionBussinesAction.SaldoInventario(CurrentUser.IdEmpresa, productos, model.IdLocal);

//                //Si no existe stock de produto lo añado en stock 0
//                foreach (var rec in model.DetalleNotaPedido)
//                {
//                    if (stocks == null)
//                        stocks = new ProductoEntityCollection();

//                    if (!stocks.Any(cus => cus.Id == rec.IdProducto))
//                    {
//                        var noStock = new ProductoEntity { Id = rec.IdProducto, Stock = 0 };
//                        stocks.Add(noStock);
//                    }
//                }

//                //if (stocks == null)
//                //    stocks = new ProductoEntityCollection();

//                transfer.Detalle = model.DetalleNotaPedido.Where(detalle => stocks == null || stocks.Any(x => x.Id == detalle.IdProducto && detalle.Cantidad > x.Stock))
//                                                .Select(prod => new TransferDetailDto
//                                                {
//                                                    Cantidad = prod.Cantidad - (stocks?.FirstOrDefault(t => t.Id == prod.IdProducto)?.Stock ?? 0),
//                                                    IdLocal = IdLocalSelected,
//                                                    IdProducto = prod.IdProducto,
//                                                    IdTipoUnidad = prod.IdTipoUnidad
//                                                }).ToList();


//                var salida = model.ToSalidaEntity();
//                salida.Id = idSalida;
//                salida.FechaModificacion = DateTime.Now;
//                salida.UsuarioModificacion = User.Identity.Name;
//                salida.IpModificacion = "1.1.1.1";
//                salida.TipoTransaccion = "NP";
//                salida.IdLocal = IdLocalSelected;
//                salida.IdEmpresa = CurrentUser.IdEmpresa;
//                salida.CuentaPorCobrarFromIdSalida = null;
//                salida.ReciboCobro = null;
//                salida.BorrarCobros = true;
//                salida.SetDeletedState();
//                salida.DetalleSalidaFromIdSalida
//                    .ToList()
//                    .ForEach(c => c.CurrentState = salida.CurrentState);

//                salida = SalidaBussinesAction.Guardar(salida, connection, transaction);

//                salida.IdLocal = model.IdLocal;
//                //if (transaction != null)
//                //    transaction.Commit();


//                ////Nuevo commit 
//                //connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
//                //connection.Open();
//                //transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

//                //Si no hay stock hago un pedido a bodega
//                if (transfer.Detalle.Any())
//                {
//                    transfer.FechaIngreso = DateTime.Now;
//                    transfer.UsuarioIngreso = User.Identity.Name;
//                    transfer.IpIngreso = "1.1.1";

//                    warehousesController.Transferir(transfer, connection, transaction);
//                }

//                var result = SalidaBussinesAction.Cambio(salida, connection, transaction);

//                if (transaction != null)
//                    transaction.Commit();

//                return result?.ToOutputDto();

//            }
//            catch (HttpResponseException ex)
//            {
//                throw ex;
//            }
//            catch (SqlException ex)
//            {
//                string userError = ex.GetMessage();

//                if (userError.Contains("PK_Salida"))
//                {
//                    userError = "No existe la salida especificada";
//                }
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                   ex.GetAllMessages(), userError));
//            }
//            catch (Exception ex)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                    ex.GetAllMessages(), ex.GetMessage()));
//            }
//            finally
//            {
//                connection.Close();
//            }

//        }

//        [HttpPost, Route("output/{id}/deleteChange")]
//        public IHttpActionResult DeleteOutputChange(string id, OutputDeleteDto dto)
//        {
//            try
//            {
//                var salidaEntity = GetSalidaEntity(id);

//                if (salidaEntity == null)
//                {
//                    throw new Exception("PK_Salida");
//                }

//                salidaEntity.SetDeletedState();
//                salidaEntity.EstadoActual = "ANULADA";
//                salidaEntity.FechaModificacion = dto.FechaIngreso;
//                salidaEntity.UsuarioModificacion = dto.UsuarioIngreso;
//                salidaEntity.IpModificacion = dto.IpIngreso;
//                salidaEntity.Observaciones = $"ORDEN ANULADA: {dto.Observaciones} {dto.UsuarioIngreso} {dto.FechaIngreso} - {salidaEntity.Observaciones}";
//                salidaEntity.IdEstado = 0;


//                var cuentasCobrar = CuentaPorCobrarCollectionBussinesAction.FindByAll(new CuentaPorCobrarFindParameterEntity
//                {
//                    IdSalida = salidaEntity.Id,
//                    IdEstado = 1
//                });

//                if (cuentasCobrar != null)
//                {
//                    salidaEntity.CuentaPorCobrarFromIdSalida = new CuentaPorCobrarEntityCollection();

//                    foreach (CuentaPorCobrarEntity cxc in cuentasCobrar)
//                    {
//                        cxc.CurrentState = EntityStatesEnum.Deleted;
//                        cxc.FechaModificacion = dto.FechaIngreso;
//                        cxc.UsuarioModificacion = dto.UsuarioIngreso;
//                        cxc.IpModificacion = dto.IpIngreso;
//                        var detalleCobro = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity
//                        {
//                            IdCuentaPorCobrar = cxc.Id,
//                            IdEstado = 1
//                        });

//                        if (cxc.DetalleCobroFromIdCuentaPorCobrar == null)
//                        {
//                            cxc.DetalleCobroFromIdCuentaPorCobrar = new DetalleCobroEntityCollection();
//                        }

//                        foreach (DetalleCobroEntity dp in detalleCobro)
//                        {
//                            dp.CurrentState = EntityStatesEnum.Deleted;
//                            dp.FechaModificacion = dto.FechaIngreso;
//                            dp.UsuarioModificacion = dto.UsuarioIngreso;
//                            dp.IpModificacion = dto.IpIngreso;
//                            cxc.DetalleCobroFromIdCuentaPorCobrar.Add(dp);

//                            CobroDebitoEntity rec = CobroDebitoBussinesAction.LoadByPK(dp.IdCobroDebito);

//                            if (rec != null)
//                            {
//                                if (salidaEntity.ReciboCobro == null)
//                                {
//                                    salidaEntity.ReciboCobro = new CobroDebitoEntity();
//                                }

//                                salidaEntity.ReciboCobro.Id = dp.IdCobroDebito;
//                                salidaEntity.ReciboCobro.CurrentState = EntityStatesEnum.Deleted;
//                                salidaEntity.ReciboCobro.FechaModificacion = dto.FechaIngreso;
//                                salidaEntity.ReciboCobro.UsuarioModificacion = dto.UsuarioIngreso;
//                                salidaEntity.ReciboCobro.IpModificacion = dto.IpIngreso;
//                            }

//                        }

//                        salidaEntity.CuentaPorCobrarFromIdSalida.Add(cxc);
//                    }
//                }

//                var newSalidaEntity = SalidaBussinesAction.Guardar(salidaEntity);

//                if (newSalidaEntity.CurrentState == EntityStatesEnum.SavedSuccessfully)
//                {
//                    return Ok(true);
//                }

//                throw new Exception("Hubo un error al eliminar el documento");
//            }
//            catch (HttpResponseException ex)
//            {
//                throw ex;
//            }
//            catch (SqlException ex)
//            {
//                string UserError = ex.GetMessage();

//                if (UserError.Contains("PK_Salida"))
//                {
//                    UserError = "No existe la salida especificada";
//                }
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                   ex.GetAllMessages(), UserError));
//            }
//            catch (Exception ex)
//            {
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
//                    ex.GetAllMessages(), ex.GetMessage()));
//            }
//        }

//        [HttpGet, Route("output/estadisticas")]
//        public StatisticsDto Estadisticas()
//        {

//            var estadistic = EstadisticaBussinesAction.LoadByPK(CurrentUser.IdEmpresa, 1);
//            var result = estadistic.ToStatisticsDto();

//            return result;
//        }

//        [HttpGet, Route("output/estadisticasMensual")]
//        public List<StatisticsMonthDto> EstadisticasMensual()
//        {
//            var outputs = EstadisticaBussinesAction.LoadMesByPK(CurrentUser.IdEmpresa, 1);
//            var outputsDtoList = outputs
//                .Select(br => br.ToStatisticsMonthDto())
//                .ToList();

//            return outputsDtoList;
//        }

//        [HttpGet, Route("output/estadisticasMensualResumida")]
//        public StatisticsMonthDto EstadisticasMensualResumida()
//        {
//            var outputs = EstadisticaBussinesAction.LoadMesByPK(CurrentUser.IdEmpresa, 1);
//            var totalMes = outputs.Sum(x => x.Total);

//            StatisticsMonthDto outputsDto = new StatisticsMonthDto
//            {
//                Fecha = DateTime.Now,
//                Total = totalMes
//            };

//            return outputsDto;
//        }

//        [HttpGet, Route("output/estadisticasCobro")]
//        public List<StatisticsMonthDto> EstadisticasCobro()
//        {

//            var outputs = EstadisticaBussinesAction.LoadCobroByPK(CurrentUser.IdEmpresa, 1);
//            List<StatisticsMonthDto> outp = new List<StatisticsMonthDto>();

//            if (outputs != null)
//            {
//                var outputsDtoList = outputs
//                .Select(br => br.ToStatisticsMonthDto())
//                .ToList();
//                return outputsDtoList;
//            }

//            return outp;
//        }

//        [HttpPost, Route("output/{id}/{email}/enviarCorreo")]
//        public async Task<IHttpActionResult> Correo(string id, string email)
//        {
//            // Obtengo el generador de reportes
//            var builder = new ReportBuilder("/tendago/Salida");

//            // Configuro los parametros del reporte
//            builder.AddParameter("IdSalida", id);

//            // Genero el reporte
//            var report = builder.Render(Convert.ToString(ReportFormatEnum.PDF));

//            var memStream = new MemoryStream(report.Content);
//            memStream.Position = 0;
//            var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
//            var reportAttachment = new Attachment(memStream, contentType);
//            reportAttachment.ContentDisposition.FileName = id.Trim() + ".pdf";

//            //mailMessage.Attachments.Add(reportAttachment);
//            //attach.ContentDisposition.FileName = id.Trim()+".pdf";


//            await Tools.SendMailAsync(email, "Nota de Pedido", "<h3> " + id + " </h3>", reportAttachment);
//            return Ok();
//        }
//    }
//}
