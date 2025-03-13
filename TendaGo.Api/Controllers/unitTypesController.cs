using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class unitTypesController : ApiControllerBase
    {
        public IEnumerable<UnitTypeDto> GetUnitTypes(string idProduct, int idEstado = 1)
        {
            int idConverted;
            bool isValidId = int.TryParse(idProduct, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro idproducto, es invalido", "Id invalido"));
            var findParameter = new TipoUnidadFindParameterEntity();
            findParameter.IdProducto = idConverted;
            findParameter.IdEmpresa = CurrentUser.IdEmpresa;
            if (idEstado != -1)
            {
                findParameter.IdEstado = (short)idEstado;
            }
            var unittypes = TipoUnidadCollectionBussinesAction.FindByAll(findParameter);
            var unitTypesDtoList = unittypes.Select(ut => ut.ToUnitTypeDto()).ToList();
            return unitTypesDtoList;
        }

        public UnitTypeDto GetUnitType(string id)
        {
            try
            {
                int idConverted;
                bool isValidId = int.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
                var unitType = TipoUnidadBussinesAction.LoadByPK(idConverted);
                if (unitType == null)
                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Tipo de Unidad no existe", "El Tipo de Unidad solicitado, no existe"));
                return unitType.ToUnitTypeDto();
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

        [HttpGet, Route("products/{idProduct}/unitTypes;name={unitType}"), Route("products/{idProduct}/unitTypes")]
        public UnitTypeDto GetUnitTypeByName(string idProduct, string unitType)
        {
            int idConverted;
            bool isValidId = int.TryParse(idProduct, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

            var tiposUnidad = TipoUnidadCollectionBussinesAction.FindByAll(new TipoUnidadFindParameterEntity { IdProducto = idConverted, TipoUnidad = unitType, IdEmpresa = (short)CurrentUser.IdEmpresa }, 0);
            if (!tiposUnidad.Any())
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "Producto no existe", "No existe producto para el codigo proporcionado"));

            return tiposUnidad.FirstOrDefault().ToUnitTypeDto();
        }

        public UnitTypeDto PostUnitType(UnitTypeDto unit)
        {
            try
            {
                TipoUnidadEntity tipoUnidadEntity;

                if (unit.Id != 0)
                {
                    tipoUnidadEntity = TipoUnidadBussinesAction.LoadByPK(unit.Id);
                    tipoUnidadEntity.TipoUnidad = unit.TipoUnidad;
                    tipoUnidadEntity.Cantidad = unit.Cantidad;
                    tipoUnidadEntity.CantidadMinima = unit.CantidadMinima;
                    tipoUnidadEntity.UnidadMedidad = unit.UnidadMedidad;
                    tipoUnidadEntity.Precio = unit.Precio ?? 0;
                    tipoUnidadEntity.Costo = unit.Costo ?? 0;
                    tipoUnidadEntity.Plantilla = unit.Plantilla ?? false;
                    tipoUnidadEntity.IncluyeIva = unit.IncluyeIva ?? false;
                    tipoUnidadEntity.IdEstado = unit.IdEstado;

                    if (tipoUnidadEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        tipoUnidadEntity.UsuarioModificacion = unit.UsuarioModificacion;
                        tipoUnidadEntity.FechaModificacion = DateTime.Now;
                        tipoUnidadEntity.IpModificacion = unit.IpModificacion;
                    }
                }
                else
                {
                    var prod = ProductoBussinesAction.LoadByPK(unit.IdProducto);
                    tipoUnidadEntity = unit.ToUnitTypeEntity();
                    tipoUnidadEntity.IdEmpresa = (short)CurrentUser.IdEmpresa;
                    tipoUnidadEntity.FechaIngreso = DateTime.Now;
                    tipoUnidadEntity.UnidadMedidad = prod.UnidadMedida;
                }

                tipoUnidadEntity = TipoUnidadBussinesAction.Save(tipoUnidadEntity);
                return tipoUnidadEntity.ToUnitTypeDto();
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_TipoUnidad"))
                {
                    UserError = "No puede ingresar tipo de unidad duplicada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }
        }

    }
}
