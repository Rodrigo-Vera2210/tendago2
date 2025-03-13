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
    internal class DivisionService
    {
        private DivisionEntity GetDivisionEntity(string id)
        {
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
            return GetDivisionEntity(idConverted);
        }

        private DivisionEntity GetDivisionEntity(int id)
        {
            var division = DivisionBussinesAction.LoadByPK(id);
            if (division == null || division.IdEmpresa != CurrentUser.IdEmpresa)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "División no existe", "La división solicitada no existe"));
            return division;
        }
    }
}
