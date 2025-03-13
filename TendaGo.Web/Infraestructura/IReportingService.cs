using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TendaGo.Web.Infraestructura
{
    public interface IReportingService
    {
        Task<Stream> ObtenerReporteSalidaEnPDF(int? idEmpresa,string idSalida);
    }
}