using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class brandsController : ApiControllerBase
    {
        public List<BrandDto> GetBrands()
        {
            var brands = MarcaCollectionBussinesAction.FindByAll(new MarcaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa });
            var brandsDtoList = brands.Select(br => br.ToBrandDto()).ToList();
            return brandsDtoList;
        }

        [Route("brands/{id}")]
        public BrandDto GetBrand(string id)
        {
            try
            {
                var brand = GetBrandEntity(id);
                return brand.ToBrandDto();
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

        public BrandDto PostBrand(BrandDto brand)
        {
            try
            {
                MarcaEntity marcaEntity;
                if (brand.Id != 0)
                {
                    marcaEntity = MarcaBussinesAction.LoadByPK(brand.Id);
                    marcaEntity.Marca = brand.Marca;
                    marcaEntity.IdEstado = brand.IdEstado;
                    if (marcaEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        marcaEntity.UsuarioModificacion = brand.UsuarioModificacion;
                        marcaEntity.FechaModificacion = DateTime.Now;
                        marcaEntity.IpModificacion = brand.IpModificacion;
                    }
                }
                else
                {
                    marcaEntity = brand.ToMarcaEntity();
                    //var usuarioentity = brand.Id != 0 ? GetAuthenticatedUserByToken(brand.UsuarioModificacion) : GetAuthenticatedUserByToken(brand.UsuarioIngreso);
                    marcaEntity.IdEmpresa = CurrentUser.IdEmpresa;
                    marcaEntity.FechaIngreso = DateTime.Now;
                }
                marcaEntity = MarcaBussinesAction.Save(marcaEntity);
                return marcaEntity.ToBrandDto();
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Marca"))
                {
                    UserError = "No puede ingresar una Linea duplicada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }
        }

        private MarcaEntity GetBrandEntity(string id)
        {
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
            return GetBrandEntity(idConverted);
        }

        private MarcaEntity GetBrandEntity(int id)
        {
            var brand = MarcaBussinesAction.LoadByPK(id);

            if (brand == null || brand.IdEmpresa != CurrentUser.IdEmpresa)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Marca no existe", "La marca solicitada no existe"));

            return brand;
        }
    }
}
