using ER.BA;
using ER.BE;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    public class displayController : ApiControllerBase
    {
        private readonly IDisplayService _displayService;

        public displayController(IDisplayService displayService, IUsuarioService usuarioService) : base(usuarioService)
        {
            _displayService = displayService;
        }

        [HttpGet, Route("")]
        List<DisplayDto> GetDisplays()
        {
            try
            {
                var displays = _displayService.GetDisplays();

                return displays;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet, Route("profile")]
        public List<DisplayDto> GetUserProfileDisplays()
        {
            try
            {
                var displays = _displayService.GetUserProfileDisplays(CurrentUser.IdPerifl);

                return displays;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
