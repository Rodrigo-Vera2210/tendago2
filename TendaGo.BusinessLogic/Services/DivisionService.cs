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
    public class DivisionService : IDivisionService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public DivisionService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        private async Task<DivisionEntity> GetDivisionEntity(int id, int idEmpresa)
        {
            try
            {
                var procedimiento = await _procedimientos.Division_LoadByPKAsync(id);

                var division = _mapper.Map<DivisionEntity>(procedimiento);

                if (division == null || division.IdEmpresa != idEmpresa)
                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "División no existe", "La división solicitada no existe"));
                return division;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<DivisionDto> GetAllDivisions(int idEmpresa)
        {
            try
            {
                var parameters = new DivisionFindParameterEntity { IdEmpresa = idEmpresa };
                var divisions = _procedimientos.Division_FindByAllAsync(
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
        public List<DivisionDto> GetDivisions(int idEmpresa)
        {
            try
            {
                var parameters = new DivisionFindParameterEntity { IdEmpresa = idEmpresa };
                var divisions = _procedimientos.Division_FindByAllAsync(
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

        public DivisionDto PostDivision(DivisionDto division)
        {
            try
            {
                DivisionEntity divisionEntity;
                if (division.Id > 0)
                {
                    divisionEntity = DivisionBussinesAction.LoadByPK(division.Id);
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
                    divisionEntity = division.ToDivisionEntity();
                    divisionEntity.FechaIngreso = DateTime.Today;
                    divisionEntity.IdEmpresa = CurrentUser.IdEmpresa;
                }
                divisionEntity = DivisionBussinesAction.Save(divisionEntity);
                return divisionEntity.ToDivisionDto();
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Division"))
                {
                    UserError = "No puede ingresar una division duplicada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }
        }
        public void DeleteDivision(DivisionDto division)
        {
            try
            {
                var divisionEntity = GetDivisionEntity(division.Id);
                divisionEntity.UsuarioModificacion = division.UsuarioModificacion;
                divisionEntity.IpModificacion = division.IpModificacion;
                divisionEntity.FechaModificacion = DateTime.Today;
                divisionEntity.SetDeletedState();
                DivisionBussinesAction.Save(divisionEntity);
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
        public List<LineDto> GetAllLines(string id)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

            var findParameter = new LineaFindParameterEntity { IdDivision = idConverted, IdEmpresa = CurrentUser.IdEmpresa, IdEstado = 1 };
            var lines = LineaCollectionBussinesAction.FindByAll(findParameter);
            var linesDtoList = lines.Select(ln => ln.ToLineDto()).ToList();
            return linesDtoList;
        }
        public List<LineDto> GetLines(string id)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

            var findParameter = new LineaFindParameterEntity { IdDivision = idConverted, IdEmpresa = CurrentUser.IdEmpresa };
            var lines = LineaCollectionBussinesAction.FindByAll(findParameter);
            var linesDtoList = lines.Select(ln => ln.ToLineDto()).ToList();
            return linesDtoList;
        }

    }
}
