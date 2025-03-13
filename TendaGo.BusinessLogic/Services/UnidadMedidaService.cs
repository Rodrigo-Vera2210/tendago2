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
    internal class UnidadMedidaService
    {
        private UnidadMedidaEntity GetMeasurementUnitEntity(string id)
        {
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
            return GetMeasurementUnitEntity(idConverted);
        }

        private UnidadMedidaEntity GetMeasurementUnitEntity(int id)
        {
            var unit = UnidadMedidaBussinesAction.LoadByPK(id);
            if (unit == null)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Unidad de Medida no existe", "La unidad solicitada no existe"));
            return unit;
        }
    }
}
