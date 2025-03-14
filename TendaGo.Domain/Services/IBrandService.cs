using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.Domain.Services
{
    public interface IBrandService
    {
        Task<List<BrandDto>> GetBrands(MarcaFindParameterEntity findParameter);
        Task<BrandDto> PostBrand(BrandDto brand, int idEmpresa);
        Task<BrandDto> GetBrandEntity(int id, int idEmpresa);
    }
}
