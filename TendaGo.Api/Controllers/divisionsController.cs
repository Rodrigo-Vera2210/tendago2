using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TendaGo.Common;
using TendaGo.Domain.Services;
using TendaGo.BusinessLogic.Services;
using System.Threading.Tasks;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class divisionsController : ApiControllerBase
    {
        private readonly IDivisionService _divisionService;

        public divisionsController(IDivisionService divisionService,IUsuarioService usuarioService):base(usuarioService)
        {
            _divisionService = divisionService;
        }

        [HttpGet, Route("all")]
        public async Task<List<DivisionDto>> GetAllDivisions()
        {
            try
            {
                var divisions = await _divisionService.GetAllDivisions(CurrentUser.IdEmpresa);

                return divisions;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

        [HttpGet, Route("")]
        public async Task<List<DivisionDto>> GetDivisions()
        {
            try
            {
                var divisions = await _divisionService.GetDivisions(CurrentUser.IdEmpresa);

                return divisions;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

        [HttpGet, Route("{id}")]
        public async Task<DivisionDto> GetDivision(string id)
        {
            try
            {
                var division = await _divisionService.GetDivisionDto(int.Parse(id), CurrentUser.IdEmpresa);
                
                return division;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost, Route("")]
        public async Task<DivisionDto> PostDivision(DivisionDto division)
        {
            try
            {
                var divisionR = await _divisionService.PostDivision(division, CurrentUser.IdEmpresa);

                return divisionR;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete, Route("")]
        public async Task DeleteDivision(DivisionDto division)
        {
            try
            {
                await _divisionService.DeleteDivision(division,CurrentUser.IdEmpresa);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet, Route("{id}/lines/all")]
        public async Task<List<LineDto>> GetAllLines(string id)
        {
            try
            {
                var divisions = await _divisionService.GetAllLines(id,CurrentUser.IdEmpresa);

                return divisions;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

        [HttpGet, Route("{id}/lines")]
        public async Task<List<LineDto>> GetLines(string id)
        {
            try
            {
                var divisions = await _divisionService.GetLines(id,CurrentUser.IdEmpresa);

                return divisions;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

    }
}
