using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TendaGo.BusinessLogic.Services;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class linesController : ApiControllerBase
    {
        private readonly ILineService _lineService;

        public linesController(ILineService lineService, IUsuarioService usuarioService):base(usuarioService)
        {
            _lineService = lineService;
        }

        [HttpGet, Route("{id}/categories/all")]
        public async Task<List<CategoryDto>> GetAllCategories(string id)
        {
            try
            {
                var lines = await _lineService.GetAllCategories(id, CurrentUser.IdEmpresa);

                return lines;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

        [HttpGet, Route("{id}/categories")]
        public async Task<List<CategoryDto>> GetCategories(string id)
        {
            try
            {
                var cat = await _lineService.GetCategories(id, CurrentUser.IdEmpresa);

                return cat;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

        [HttpGet, Route("{id}")]
        public async Task<LineDto> GetLine(string id)
        {
            try
            {
                var cat = await _lineService.GetLine(id, CurrentUser.IdEmpresa);

                return cat;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

        [HttpPost, Route("")]
        public async Task<LineDto> PostLine(LineDto line)
        {
            try
            {
                var cat = await _lineService.PostLine(line, CurrentUser.IdEmpresa);

                return cat;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
        }

    }
}
