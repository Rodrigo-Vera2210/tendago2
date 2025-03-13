using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TendaGo.Web.Infraestructura
{
    public class SSRSService: IReportingService
    {
        public async Task<Stream> ObtenerReporteSalidaEnPDF(int? idEmpresa, string idSalida)
        {
            try
            {
                CredentialCache credentialCache = new CredentialCache();
                credentialCache.Add(new Uri(ConfigurationManager.AppSettings["URLServidor"]), "NTLM",
                    new NetworkCredential(
                    ConfigurationManager.AppSettings["UsuarioReporte"],
                   ConfigurationManager.AppSettings["ContrasenaReporte"]
                ));

                Stream report = null;
                using (var httpClient = new HttpClient(
                    new HttpClientHandler { Credentials = credentialCache }))
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                    report = await httpClient.GetStreamAsync(
                        ConfigurationManager.AppSettings["URLReporte"] + "&IdEmpresa=" + Convert.ToString(idEmpresa)+ "&IdSalida="+idSalida);
                }

                return report;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
    }
}