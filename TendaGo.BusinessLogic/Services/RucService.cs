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
    public class RucService : IRucService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public RucService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<RucEntity> LoadByPK(string ruc)
        {
            try
            {
                var rucR = await _procedimientos.Ruc_LoadByPKAsync(ruc.ToUpper());

                var result = _mapper.Map<RucEntity>(rucR);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
