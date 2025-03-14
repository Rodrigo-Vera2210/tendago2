using ER.BA;
using ER.BE;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.BusinessLogic.Services;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    //[TokenAuthorize]
    //public class categoriesController : ApiControllerBase
    //{
    //    private readonly ICategoriaService _categoriaService;

    //    public categoriesController(CategoriaService categoriaService)
    //    {
    //        _categoriaService = categoriaService;
    //    }

    //    [HttpGet, Route("categories/{id}")]
    //    public async Task<CategoryDto> GetCategory(int id)
    //    {
    //        try
    //        {
    //            var cat = await _categoriaService.GetCategoriaEntity(id);

    //            return cat.ToCategoryDto();
    //        }
    //        catch (HttpResponseException) { throw; }
    //        catch (Exception ex)
    //        {
    //            throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<CategoryDto> PostCategory(CategoryDto category)
    //    {
    //        try
    //        {
    //            var result = await _categoriaService.PostCategory(category);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            string UserError = "Ocurrio un error general, reintente";
    //            if (ex.GetMessage().Contains("UQ_Categoria"))
    //            {
    //                UserError = "No puede ingresar una Categoria duplicada";
    //            }
    //            throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
    //        }
    //    }
    //}
}
