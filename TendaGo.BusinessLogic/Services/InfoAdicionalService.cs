using AutoMapper;
using ER.BE;
using ER.DA.Models;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class InfoAdicionalService : IInfoAdicionalService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public InfoAdicionalService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<InfoAdicionalEntityCollection> FindByAll(InfoAdicionalFindParameterEntity findParameter, int deepLoadLevel)
        {
            try
            {
                var rucR = await _procedimientos.InformacionAdicional_FindByAllAsync(findParameter.Id,findParameter.IdSalida);

                var result = _mapper.Map<InfoAdicionalEntityCollection>(rucR);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
