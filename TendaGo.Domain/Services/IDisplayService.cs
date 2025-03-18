using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.Domain.Services
{
    public interface IDisplayService
    {
        public List<DisplayDto> GetDisplays();
        public List<DisplayDto> GetUserProfileDisplays(short idPerfil);

    }
}
