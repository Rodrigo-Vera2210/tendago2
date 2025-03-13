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
    public class providersController : ApiControllerBase
    {

        [HttpGet, Route("providers/search/{searchterm?}")]
        public List<ProviderDto> GetProviders(string searchterm = null, bool identification = false)
        {
            var providers = this.GetProviders(1, searchterm, identification);
            //if (!string.IsNullOrEmpty(searchterm))
            //{
            //    providers = providers.Where(pr => pr.TextoBusqueda.Contains(searchterm)).ToList();
            //}
            return providers;
        }

        public List<ProviderDto> GetAllProviders()
        {
            return this.GetProviders(0, string.Empty);
        }

        private List<ProviderDto> GetProviders(int idEstado, string searchTerm, bool identification = false)
        {
            var findParameter = new EntidadFindParameterEntity
            {
                IdEmpresa = CurrentUser.IdEmpresa,
                //EsProveedor = 1,
                IdEstado = (short)idEstado,
            };

            if (identification)
            {
                findParameter.Identificacion = searchTerm;
            }
            else
            {
                findParameter.RazonSocial = searchTerm;
            }

            var providers = EntidadCollectionBussinesAction.FindByAll(findParameter);
            return providers.Select(pr => pr.ToProviderDto()).ToList();
        }

        public ProviderDto PostProvider(ProviderDto provider)
        {
            string UserError = "Ocurrio un error general, reintente";

            try
            {
                EntidadEntity proveedorEntity;
                if (provider.TipoIdentificacion == "C" || provider.TipoIdentificacion == "R")
                {
                    if (provider.TipoIdentificacion == "C" && provider.Identificacion.Length != 10)
                    {
                        UserError = "La longitud de la cédula es diferente a 10 dígitos";
                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, UserError, UserError));
                    }
                    if (provider.TipoIdentificacion == "R" && provider.Identificacion.Length != 13)
                    {
                        UserError = "La longitud del RUC es diferente a 13 dígitos";
                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, UserError, UserError));
                    }
                    if (!CommonFunctions.ValidarNumeroIdentificacion(provider.Identificacion))
                    {
                        UserError = "Numero de identificación no válido";
                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Número de identificación no válido", "Numero de identificación no válido"));
                    }
                }

                if (provider.Id != 0)
                {
                    proveedorEntity = EntidadBussinesAction.LoadByPK(provider.Id);
                    proveedorEntity.RazonSocial = provider.RazonSocial;
                    proveedorEntity.Direccion = provider.Direccion;
                    proveedorEntity.Correo = provider.Correo;
                    proveedorEntity.IdEstado = provider.IdEstado;
                    proveedorEntity.EsProveedor = true;
                    proveedorEntity.EsCliente = provider.EsCliente;

                    if (proveedorEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        proveedorEntity.UsuarioModificacion = provider.UsuarioModificacion;
                        proveedorEntity.IpModificacion = provider.IpModificacion;
                        proveedorEntity.FechaModificacion = DateTime.Today;
                    }
                }
                else
                {
                    //si es nuevo proveedor
                    var col = EntidadCollectionBussinesAction.FindByAll(new EntidadFindParameterEntity
                    {
                        IdEmpresa = CurrentUser.IdEmpresa,
                        Identificacion = provider.Identificacion.Substring(0, 10)
                    });

                    if (col.Count > 0)
                    {
                        UserError = "Numero de identificación ya existe en la base de datos";
                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Número de identificación ya existe", "Numero de identificación ya existe"));
                    }

                    proveedorEntity = provider.ToEntidadProveedorEntity();
                    proveedorEntity.FechaIngreso = DateTime.Now;
                    proveedorEntity.IdEmpresa = CurrentUser.IdEmpresa;
                    proveedorEntity.TipoEntidad = "PERSONA"; // "PROVEEDOR";
                    proveedorEntity.Foto = null;
                    proveedorEntity.EsProveedor = true;
                    proveedorEntity.EsCliente = false;
                }

                proveedorEntity = EntidadBussinesAction.Save(proveedorEntity);
                return proveedorEntity.ToProviderDto();
            }
            catch (Exception ex)
            {
                //string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Entidad"))
                {
                    UserError = "No puede ingresar un proveedor duplicado";
                }

                Log("PostProvider", ex);

                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }



        }

    }
}
