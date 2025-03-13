using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TendaGo.BusinessLogic.Services
{
    internal class LineService
    {
        private LineaEntity GetLineaEntity(string id)
        {
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
            return this.GetLineaEntity(idConverted);
        }

        private LineaEntity GetLineaEntity(int id)
        {
            var line = LineaBussinesAction.LoadByPK(id);
            if (line == null || line.IdEmpresa != CurrentUser.IdEmpresa)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Línea no existe", "El registro solicitado no existe"));
            return line;
        }
    }
}
