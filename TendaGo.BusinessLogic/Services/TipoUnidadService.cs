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
    public class TipoUnidadService : ITipoUnidadService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public TipoUnidadService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<TipoUnidadEntity> LoadByPK(int Id, int? deepLoadLevel = null)
        {
            try
            {
                var empresa = await _procedimientos.TipoUnidad_LoadByPKAsync(Id);

                var result = _mapper.Map<TipoUnidadEntity>(empresa);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
