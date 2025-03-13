using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Web.Infraestructura
{
    public interface IServicioUsuario
    {
        Usuario ConsultarUsuarioPorNombre(string nombreUsuario);
    }
}
