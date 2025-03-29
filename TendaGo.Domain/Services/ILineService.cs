using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.Domain.Services
{
    public interface ILineService
    {
        Task<List<CategoryDto>> GetAllCategories(string id, int idEmpresa);
        Task<List<CategoryDto>> GetCategories(string id, int idEmpresa);
        Task<LineDto> GetLine(string id, int idEmpresa);
        Task<LineDto> PostLine(LineDto line, int idEmpresa);

    }
}
