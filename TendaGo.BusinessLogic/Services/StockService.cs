using AutoMapper;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class StockService : IStockService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public StockService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public async Task<DataSet> StockInventario(int idEmpresa, object idProducto, int idLocal = 0)
        {
            try
            {
                var stock = await _procedimientos.Custom_Stock_SaldoInventarioAsync(idEmpresa,idProducto.ToString(),idLocal);

                DataSet result = new DataSet();
                
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
