using ER.BA;
using ER.BE;
using System;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class categoriesController : ApiControllerBase
    {
        [Route("categories/{id}")]
        public CategoryDto GetCategory(string id)
        {
            try
            {
                var cat = GetCategoriaEntity(id);
                return cat.ToCategoryDto();
            }
            catch (HttpResponseException) { throw; }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

        public CategoryDto PostCategory(CategoryDto category)
        {
            try
            {
                CategoriaEntity catEntity;
                if (category.Id != 0)
                {
                    catEntity = CategoriaBussinesAction.LoadByPK(category.Id);
                    catEntity.IdLinea = category.IdLinea;
                    catEntity.Categoria = category.Categoria;
                    catEntity.IdEstado = category.IdEstado;
                    if (catEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        catEntity.UsuarioModificacion = category.UsuarioModificacion;
                        catEntity.IpModificacion = category.IpModificacion;
                        catEntity.FechaModificacion = DateTime.Today;
                    }
                }
                else
                {
                    catEntity = category.ToCategoryEntity();
                    catEntity.FechaIngreso = DateTime.Today;
                }
                //var usuarioentity = category.Id != 0 ? GetAuthenticatedUserByToken(category.UsuarioModificacion) : GetAuthenticatedUserByToken(category.UsuarioIngreso);

                catEntity.IdEmpresa = CurrentUser.IdEmpresa;
                catEntity = CategoriaBussinesAction.Save(catEntity);
                return catEntity.ToCategoryDto();
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Categoria"))
                {
                    UserError = "No puede ingresar una Categoria duplicada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }
        }

        private CategoriaEntity GetCategoriaEntity(string id)
        {
            int idConverted;
            bool isValid = int.TryParse(id, out idConverted);
            if (!isValid)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
            return GetCategoriaEntity(idConverted);
        }

        private CategoriaEntity GetCategoriaEntity(int id)
        {
            var cat = CategoriaBussinesAction.LoadByPK(id);
            if (cat == null || cat.IdEmpresa != CurrentUser.IdEmpresa)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Categoría no existe", "El registro solicitado no existe"));
            return cat;

        }
    }
}
