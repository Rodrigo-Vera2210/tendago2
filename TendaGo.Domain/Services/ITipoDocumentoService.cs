﻿using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Domain.Services
{
    public interface ITipoDocumentoService
    {
        Task<SecuencialEntity> GetDocumentSecuential(string ruc, string idTipoDocumento);
    }
}
