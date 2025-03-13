using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.Domain.Services
{
    public interface ICategoriaService
    {
        Task<CategoriaEntity> GetCategoriaEntity(int id);
        Task<CategoryDto> PostCategory(CategoryDto category);
        Task<CategoriaEntityCollection> FindByAll(CategoriaFindParameterEntity findParameter);
        Task<CategoriaEntityCollection> FindByAllPaged(CategoriaFindParameterEntity findParameter, int pageNumber, int pageSize);
    }
}
