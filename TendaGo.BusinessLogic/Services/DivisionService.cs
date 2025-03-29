using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
using Microsoft.Data.SqlClient;
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
    public class DivisionService : IDivisionService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public DivisionService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<DivisionDto> GetDivisionDto(int id, int idEmpresa)
        {
            try
            {
                var procedimiento = await _procedimientos.Division_LoadByPKAsync(id);

                var division = _mapper.Map<DivisionDto>(procedimiento);

                if (division == null || division.IdEmpresa != idEmpresa)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("División no existe, La división solicitada no existe") });
                return division;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<DivisionEntity> GetDivisionEntity(int id, int idEmpresa)
        {
            try
            {
                var procedimiento = await _procedimientos.Division_LoadByPKAsync(id);

                var division = _mapper.Map<DivisionEntity>(procedimiento);

                if (division == null || division.IdEmpresa != idEmpresa)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("División no existe, La división solicitada no existe") });
                return division;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<List<DivisionDto>> GetAllDivisions(int idEmpresa)
        {
            try
            {
                var parameters = new DivisionFindParameterEntity { IdEmpresa = idEmpresa };
                var divisions = await _procedimientos.Division_FindByAllAsync(
                                                    parameters.Id,
                                                    parameters.IdEmpresa,
                                                    parameters.Division,
                                                    parameters.IpIngreso,
                                                    parameters.UsuarioIngreso,
                                                    parameters.FechaIngreso,
                                                    parameters.IpModificacion,
                                                    parameters.UsuarioModificacion,
                                                    parameters.FechaModificacion,
                                                    parameters.IdEstado
                                                );
                var divisionsDtoList = _mapper.Map<List<DivisionDto>>(divisions);
                return divisionsDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public async Task<List<DivisionDto>> GetDivisions(int idEmpresa)
        {
            try
            {
                var parameters = new DivisionFindParameterEntity { IdEmpresa = idEmpresa };
                var divisions = await _procedimientos.Division_FindByAllAsync(
                                                    parameters.Id,
                                                    parameters.IdEmpresa,
                                                    parameters.Division,
                                                    parameters.IpIngreso,
                                                    parameters.UsuarioIngreso,
                                                    parameters.FechaIngreso,
                                                    parameters.IpModificacion,
                                                    parameters.UsuarioModificacion,
                                                    parameters.FechaModificacion,
                                                    parameters.IdEstado
                                                );
                var divisionsDtoList = _mapper.Map<List<DivisionDto>>(divisions);
                return divisionsDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<DivisionDto> PostDivision(DivisionDto division, int idEmpresa)
        {
            try
            {
                DivisionEntity divisionEntity;
                if (division.Id > 0)
                {
                    var consulta = await _procedimientos.Division_LoadByPKAsync(division.Id);

                    divisionEntity = _mapper.Map<DivisionEntity>(consulta.FirstOrDefault());

                    divisionEntity.Division = division.Division;
                    divisionEntity.IdEstado = division.IdEstado;
                    if (divisionEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        divisionEntity.IpModificacion = division.IpModificacion;
                        divisionEntity.UsuarioModificacion = division.UsuarioModificacion;
                        divisionEntity.FechaModificacion = DateTime.Today;
                    }
                }
                else
                {
                    divisionEntity = _mapper.Map<DivisionEntity>(division);
                    divisionEntity.FechaIngreso = DateTime.Today;
                    divisionEntity.IdEmpresa = idEmpresa;
                }

                divisionEntity = await Save(divisionEntity);
                return _mapper.Map<DivisionDto>(divisionEntity);
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.Message.Contains("UQ_Division"))
                {
                    UserError = "No puede ingresar una division duplicada";
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message + UserError) });
            }
        }
        
        public async Task DeleteDivision(DivisionDto division, int idEmpresa)
        {
            try
            {
                var divisionEntity = await GetDivisionEntity(division.Id,idEmpresa);
                divisionEntity.UsuarioModificacion = division.UsuarioModificacion;
                divisionEntity.IpModificacion = division.IpModificacion;
                divisionEntity.FechaModificacion = DateTime.Today;
                divisionEntity.SetDeletedState();
                await Save(divisionEntity);
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
        
        public async Task<List<LineDto>> GetAllLines(string id, int idEmpresa)
        {
            try
            {
                int idConverted;
                bool isValidId = int.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El parametro id, es invalido" + "Ocurrio un error general, reintente") });

                var findParameter = new LineaFindParameterEntity { IdDivision = idConverted, IdEmpresa = idEmpresa, IdEstado = 1 };
                var lines = await _procedimientos.Linea_FindByAllAsync(
                                                    findParameter.Id,
                                                    findParameter.IdEmpresa,
                                                    findParameter.IdDivision,
                                                    findParameter.Linea,
                                                    findParameter.IpIngreso,
                                                    findParameter.UsuarioIngreso,
                                                    findParameter.FechaIngreso,
                                                    findParameter.IpModificacion,
                                                    findParameter.UsuarioModificacion,
                                                    findParameter.FechaModificacion,
                                                    findParameter.IdEstado
                                                 );
                var linesDtoList = _mapper.Map<List<LineDto>>(lines);
                return linesDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public async Task<List<LineDto>> GetLines(string id, int idEmpresa)
        {
            try
            {
                int idConverted;
                bool isValidId = int.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El parametro id, es invalido" + "Ocurrio un error general, reintente") });

                var findParameter = new LineaFindParameterEntity { IdDivision = idConverted, IdEmpresa = idEmpresa, IdEstado = 1 };
                var lines = await _procedimientos.Linea_FindByAllAsync(
                                                    findParameter.Id,
                                                    findParameter.IdEmpresa,
                                                    findParameter.IdDivision,
                                                    findParameter.Linea,
                                                    findParameter.IpIngreso,
                                                    findParameter.UsuarioIngreso,
                                                    findParameter.FechaIngreso,
                                                    findParameter.IpModificacion,
                                                    findParameter.UsuarioModificacion,
                                                    findParameter.FechaModificacion,
                                                    findParameter.IdEstado
                                                 );
                var linesDtoList = _mapper.Map<List<LineDto>>(lines);
                return linesDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<DivisionEntity> Save(DivisionEntity division)
        {

            try
            {
                switch (division.CurrentState)
                {
                    case EntityStatesEnum.Deleted:
                        await Delete(division);
                        break;
                    case EntityStatesEnum.Updated:
                        await Update(division);
                        break;
                    case EntityStatesEnum.New:
                        division = await Insert(division);
                        break;
                    default:
                        break;
                }

                return division;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private async Task<DivisionEntity> Insert(DivisionEntity division)
        {
            try
            {
                OutputParameter<int?> idDivision = new();

                var result = await _procedimientos.Division_InsertAsync(
                        division.IdEmpresa,
                        division.Division,
                        division.IpIngreso,
                        division.UsuarioIngreso,
                        division.FechaIngreso,
                        division.IpModificacion,
                        division.UsuarioModificacion,
                        division.FechaModificacion,
                        division.IdEstado,
                        idDivision
                    );

                var divisionE = await GetDivisionEntity((int)idDivision.Value, division.IdEmpresa);

                var response = _mapper.Map<DivisionEntity>(divisionE);

                return response;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private async Task Update(DivisionEntity division)
        {
            try
            {
                OutputParameter<int> idDivision = new();


                var result = await _procedimientos.Division_UpdateAsync(
                                                division.Id,
                                                division.IdEmpresa,
                                                division.Division,
                                                division.IpIngreso,
                                                division.UsuarioIngreso,
                                                division.FechaIngreso,
                                                division.IpModificacion,
                                                division.UsuarioModificacion,
                                                division.FechaModificacion,
                                                division.IdEstado,
                                                idDivision
                                            );


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private async Task Delete(DivisionEntity division)
        {
            try
            {
                var result = await _procedimientos.Division_DeleteAsync(
                                                    division.Id,
                                                    division.FechaModificacion,
                                                    division.UsuarioModificacion,
                                                    division.IpModificacion
                                                   );


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
}
