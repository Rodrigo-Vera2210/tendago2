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
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public TipoDocumentoService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<SecuencialEntity> GetDocumentSecuential(string ruc, string idTipoDocumento)
        {
            try
            {
                var documentoSecuencial = await _procedimientos.Custom_Obtener_SecuencialAsync(ruc,idTipoDocumento);

                var result = _mapper.Map<SecuencialEntity>(documentoSecuencial);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
