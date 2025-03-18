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
        List<DivisionDto> GetAllDivisions();
        List<DivisionDto> GetDivisions();
        DivisionDto GetDivision(string id);
        DivisionDto PostDivision(DivisionDto division);
        void DeleteDivision(DivisionDto division);
        List<LineDto> GetAllLines(string id);
        List<LineDto> GetLines(string id);

    }
}
