using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public UnidadMedidaService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<List<MeasurementUnitDto>> GetAllMeasurementUnits()
        {
            try
            {
                var parameters = new UnidadMedidaFindParameterEntity();
                var units = await _procedimientos.UnidadMedida_FindByAllAsync(
                                                    parameters.Id,
                                                    parameters.UnidadMedida,
                                                    parameters.IpIngreso,
                                                    parameters.UsuarioIngreso,
                                                    parameters.FechaIngreso,
                                                    parameters.IpModificacion,
                                                    parameters.UsuarioModificacion,
                                                    parameters.FechaModificacion,
                                                    parameters.IdEstado
                                                 );
                var unitsDtoList = _mapper.Map<List<MeasurementUnitDto>>(units);
                return unitsDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public async Task<List<MeasurementUnitDto>> GetMeasurementUnits()
        {
            try
            {
                var parameters = new UnidadMedidaFindParameterEntity { IdEstado = 1 };
                var units = await _procedimientos.UnidadMedida_FindByAllAsync(
                                                    parameters.Id,
                                                    parameters.UnidadMedida,
                                                    parameters.IpIngreso,
                                                    parameters.UsuarioIngreso,
                                                    parameters.FechaIngreso,
                                                    parameters.IpModificacion,
                                                    parameters.UsuarioModificacion,
                                                    parameters.FechaModificacion,
                                                    parameters.IdEstado
                                                 );
                var unitsDtoList = _mapper.Map<List<MeasurementUnitDto>>(units);
                return unitsDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public MeasurementUnitDto GetMeasurementUnit(string id)
        {
            try
            {
                var unit = GetMeasurementUnitEntity(id);
                
                var response = _mapper.Map<MeasurementUnitDto>(unit);
                return response;
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message + "Ocurrio un error general, reintente") });
            }
        }
        
        public async Task<MeasurementUnitDto> PostMeasurementUnit(MeasurementUnitDto measurementUnit)
        {
            try
            {
                UnidadMedidaEntity unidadMedidaEntity;
                if (measurementUnit.Id != 0)
                {
                    var unidadMedida = await _procedimientos.UnidadMedida_LoadByPKAsync(measurementUnit.Id);
                    unidadMedidaEntity = _mapper.Map<UnidadMedidaEntity>(unidadMedida);
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
                    unidadMedidaEntity = _mapper.Map<UnidadMedidaEntity>(measurementUnit);
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
        
        private UnidadMedidaEntity GetMeasurementUnitEntity(string id)
        {
            try
            {
                int idConverted;
                bool isValidId = int.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
                return GetMeasurementUnitEntity(idConverted);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private UnidadMedidaEntity GetMeasurementUnitEntity(int id)
        {
            try
            {
                var unit = UnidadMedidaBussinesAction.LoadByPK(id);
                if (unit == null)
                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Unidad de Medida no existe", "La unidad solicitada no existe"));
                return unit;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
