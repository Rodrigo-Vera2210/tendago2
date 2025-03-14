using AutoMapper;
using ER.BE;
using ER.DA.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Domain.Models;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public UsuarioService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<UsuarioEntity> LoadByPK(string inicioSesion)
        {
            try
            {
                OutputParameter<int> idUsuario = new();

                var usuario = await _procedimientos.Usuario_LoadByPKAsync(inicioSesion.ToUpper());

                var result = _mapper.Map<UsuarioEntity>(usuario);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> LoadByToken(string token)
        {
            try
            {
                OutputParameter<int> idUsuario = new();
                
                var usuario = await _procedimientos.Custom_Usuario_LoadByTokenAsync(token,idUsuario);

                var result = _mapper.Map<UsuarioDTO>(usuario.FirstOrDefault());

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
