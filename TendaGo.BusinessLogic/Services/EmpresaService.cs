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
    public class EmpresaService : IEmpresaService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public EmpresaService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<EmpresaEntity> LoadByPK(int Id)
        {
            try
            {
                var empresa = await _procedimientos.Empresa_LoadByPKAsync(Id);

                var result = _mapper.Map<EmpresaEntity>(empresa);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
