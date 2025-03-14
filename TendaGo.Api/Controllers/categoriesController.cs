using ER.BA;
using ER.BE;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TendaGo.BusinessLogic.Services;
using TendaGo.Common;
using TendaGo.Domain.Models;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class categoriesController : ApiControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public categoriesController(ICategoriaService categoriaService, IUsuarioService usuarioService, IHttpContextAccessor httpContextAccessor) : base(usuarioService)
        {
            _categoriaService = categoriaService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet, Route("{id}")]
        public async Task<CategoriaDTO> GetCategory(int id)
        {
            try
            {
                var cat = await _categoriaService.GetCategoriaEntity(id, CurrentUser);

                return cat;
            }
            catch (System.Web.Http.HttpResponseException) { throw; }
            catch (Exception ex)
            {
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest){ Content = new StringContent($"{ex.GetAllMessages()} Ocurrio un error general, reintente" )});
            }
        }

        [HttpPost,Route("")]
        public async Task<CategoryDto> PostCategory(CategoryDto category)
        {
            try
            {
                var result = await _categoriaService.PostCategory(category);

                return result;
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Categoria"))
                {
                    UserError = "No puede ingresar una Categoria duplicada";
                }
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent($"{ex.GetAllMessages()} Ocurrio un error general, reintente") });
            }
        }
    }
}
