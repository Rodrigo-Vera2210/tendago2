using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Domain.Models;

namespace TendaGo.Domain.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> LoadByToken(string token);
        Task<UsuarioEntity> LoadByPK(string inicioSesion);
    }
}
