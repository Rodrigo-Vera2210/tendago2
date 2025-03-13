using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Domain.Services
{
    public interface IStockService
    {
        Task<DataSet> StockInventario(int idEmpresa, object idProducto, int idLocal = 0);
    }
}
