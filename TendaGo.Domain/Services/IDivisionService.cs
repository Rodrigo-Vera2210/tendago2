using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.Domain.Services
{
    public interface IDivisionService
    {
        Task<List<DivisionDto>> GetAllDivisions(int idEmpresa);
        Task<List<DivisionDto>> GetDivisions(int idEmpresa);
        Task<DivisionDto> GetDivisionDto(int id, int idEmpresa);
        Task<DivisionEntity> GetDivisionEntity(int id, int idEmpresa);
        Task<DivisionDto> PostDivision(DivisionDto division, int idEmpresa);
        Task DeleteDivision(DivisionDto division, int idEmpresa);
        Task<List<LineDto>> GetAllLines(string id, int idEmpresa);
        Task<List<LineDto>> GetLines(string id, int idEmpresa);

    }
}
