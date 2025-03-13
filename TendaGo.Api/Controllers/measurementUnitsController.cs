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
    public class measurementUnitsController : ApiController
    {
        [HttpGet, Route("measurementUnits/all")]
        public List<MeasurementUnitDto> GetAllMeasurementUnits()
        {
            var units = UnidadMedidaCollectionBussinesAction.FindByAll(new UnidadMedidaFindParameterEntity());
            var unitsDtoList = units.Select(un => un.ToMeasurementUnitDto()).ToList();
            return unitsDtoList;
        }

        [HttpGet, Route("measurementUnits")]
        public List<MeasurementUnitDto> GetMeasurementUnits()
        {
            var units = UnidadMedidaCollectionBussinesAction.FindByAll(new UnidadMedidaFindParameterEntity { IdEstado = 1 });
            var unitsDtoList = units.Select(un => un.ToMeasurementUnitDto()).ToList();
            return unitsDtoList;
        }

        [TokenAuthorize]
        [HttpGet, Route("measurementUnits/{id}")]
        public MeasurementUnitDto GetMeasurementUnit(string id)
        {
            try
            {
                var unit = GetMeasurementUnitEntity(id);
                return unit.ToMeasurementUnitDto();
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

        [TokenAuthorize]
        [HttpPost, Route("measurementUnits")]
        public MeasurementUnitDto PostMeasurementUnit(MeasurementUnitDto measurementUnit)
        {
            try
            {
                UnidadMedidaEntity unidadMedidaEntity;
                if (measurementUnit.Id != 0)
                {
                    unidadMedidaEntity = UnidadMedidaBussinesAction.LoadByPK(measurementUnit.Id);
                    unidadMedidaEntity.UnidadMedida = measurementUnit.UnidadMedida;
                    unidadMedidaEntity.IdEstado = measurementUnit.IdEstado;
                    if (unidadMedidaEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        unidadMedidaEntity.UsuarioModificacion = measurementUnit.UsuarioModificacion;
                        unidadMedidaEntity.FechaModificacion = DateTime.Now;
                        unidadMedidaEntity.IpModificacion = measurementUnit.IpModificacion;
                    }
                }
                else
                {
                    unidadMedidaEntity = measurementUnit.ToUnidadMedidaEntity();
                    unidadMedidaEntity.FechaIngreso = DateTime.Now;
                }

                unidadMedidaEntity = UnidadMedidaBussinesAction.Save(unidadMedidaEntity);
                return unidadMedidaEntity.ToMeasurementUnitDto();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }
        [TokenAuthorize]
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
