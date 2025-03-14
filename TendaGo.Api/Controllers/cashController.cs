//using ER.BA;
//using System;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Net;
//using System.Web.Http;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    [RoutePrefix("cash")]
//    public class cashController : ApiControllerBase
//    {
//        [HttpPost, Route("balance")]
//        public CashBalanceDto PostBalance(CashBalanceRequestModel request)
//        {
//            var connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
//            connection.Open();
//            var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

//            try
//            {

//                var cierreCaja = request.ToEntity();

//                var result = CierreCajaBussinesAction.Save(cierreCaja, connection, transaction);

//                if (!string.IsNullOrEmpty(result.Id))
//                {
//                    foreach (var item in request.Detalles)
//                    {
//                        var cobroDebito = CobroDebitoBussinesAction.LoadByPK(item.IdCobroDebito, connection, transaction, 0);

//                        cobroDebito.IdCierreCaja = result.Id;
//                        cobroDebito.FechaModificacion = result.FechaIngreso;
//                        cobroDebito.IpModificacion = result.IpIngreso;
//                        cobroDebito.UsuarioIngreso = result.UsuarioIngreso;

//                        cobroDebito = CobroDebitoBussinesAction.Save(cobroDebito, connection, transaction);


//                    }
//                }


//                //End of Transaction
//                if (transaction != null)
//                {
//                    transaction.Commit();
//                }

//                return result.ToDto();

//                // Implementation
//            }
//            catch (Exception ex)
//            {
//                if (transaction != null)
//                {
//                    transaction.Rollback();
//                }

//                string UserError = "Ocurrio un error general, reintente";
//                if (ex.GetMessage().Contains("UQ_Recibo"))
//                {
//                    UserError = "No puede generar un recibo duplicado";
//                }
//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
//            }
//            finally
//            {
//                connection.Close();
//            }
//        }
//    }
//}