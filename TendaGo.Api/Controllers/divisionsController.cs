using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class divisionsController : ApiControllerBase
    {
        [HttpGet, Route("divisions/all")]
        public List<DivisionDto> GetAllDivisions()
        {
            var divisions = DivisionCollectionBussinesAction.FindByAll(new DivisionFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa });
            var divisionsDtoList = divisions.Select(dv => dv.ToDivisionDto()).ToList();
            return divisionsDtoList;
        }

        [HttpGet, Route("divisions")]
        public List<DivisionDto> GetDivisions()
        {
            var divisions = DivisionCollectionBussinesAction.FindByAll(new DivisionFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa });
            var divisionsDtoList = divisions.Select(dv => dv.ToDivisionDto()).ToList();
            return divisionsDtoList;
        }

        [HttpGet, Route("divisions/{id}")]
        public DivisionDto GetDivision(string id)
        {
            try
            {
                var division = GetDivisionEntity(id);
                return division.ToDivisionDto();
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

        [HttpPost, Route("divisions")]
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

        [HttpDelete, Route("divisions")]
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

        [HttpGet, Route("divisions/{id}/lines/all")]
        public List<LineDto> GetAllLines(string id)
        {
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

            var findParameter = new LineaFindParameterEntity { IdDivision = idConverted, IdEmpresa = CurrentUser.IdEmpresa, IdEstado = 1 };
            var lines = LineaCollectionBussinesAction.FindByAll(findParameter);
            var linesDtoList = lines.Select(ln => ln.ToLineDto()).ToList();
            return linesDtoList;
        }

        [HttpGet, Route("divisions/{id}/lines")]
        public List<LineDto> GetLines(string id)
        {
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
