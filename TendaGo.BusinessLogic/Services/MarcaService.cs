using AutoMapper;
using ER.BE;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public MarcaService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<MarcaEntityCollection> FindByAll(MarcaFindParameterEntity findParameter)
        {
            try
            {
                var marcas = await _procedimientos.Marca_FindByAllAsync(findParameter.Id, findParameter.IdEmpresa, findParameter.Marca, findParameter.IpIngreso, findParameter.UsuarioIngreso, findParameter.FechaIngreso, findParameter.IpModificacion, findParameter.UsuarioModificacion, findParameter.FechaModificacion, findParameter.IdEstado);

                var result = _mapper.Map<MarcaEntityCollection>(marcas);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
