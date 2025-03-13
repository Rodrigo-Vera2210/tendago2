using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
using Microsoft.Identity.Client.Extensions.Msal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class EntidadService : IEntidadService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public EntidadService(ITendaGOContextProcedures procedimientos)
        {
            _procedimientos = procedimientos;
        }

        public async Task<EntidadEntity> LoadByPK(int id)
        {
            try
            {
                var usuario = await _procedimientos.Entidad_LoadByPKAsync(id);

                var result = _mapper.Map<EntidadEntity>(usuario);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async Task<string> UploadFileAsync(string imagen, string id)
        {
            var file = Convert.FromBase64String(imagen);
            var path = $"{CurrentUser.IdEmpresa}/clientes/{id}";

            var result = await Storage.FileHandler.UploadAsync(path, file);

            return $"{result.Uri}";
        }
        /// <summary>
        /// Obtiene la lista de clientes 
        /// </summary>
        /// <param name="searchTerm">texto de busqueda</param>
        /// <param name="identification"></param>
        /// <param name="lite"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private List<EntityDto> GetClients(string searchTerm = null, bool identification = false, bool lite = false, int? page = null)
        {
            var findParameter = new EntidadFindParameterEntity
            {
                IdEmpresa = CurrentUser.IdEmpresa,
                IdEstado = 1
            };

            //findParameter.EsCliente = 1;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (identification)
                {
                    findParameter.Identificacion = searchTerm;
                }
                else
                {
                    findParameter.RazonSocial = searchTerm;
                }
            }

            EntidadEntityCollection result = null;

            if ((page ?? 0) > 0)
            {
                result = EntidadCollectionBussinesAction.FindByAllPaged(findParameter, page.Value, 10, "", lite ? 0 : 1);
            }
            else
            {
                result = EntidadCollectionBussinesAction.FindByAll(findParameter, lite ? 0 : 1);
            }

            return result?.Select(pr => pr.ToEntityDto()).ToList();
        }


        /// <summary>
        /// Obtener todos los clientes
        /// </summary>
        /// <returns></returns>
        public List<EntityDto> GetAllClients()
        {
            return this.GetClients();
        }

        public EntityDto GetClient(string id)
        {
            try
            {
                var cat = GetEntidadEntity(id);
                return _mapper.Map<EntityDto>(cat);
            }
            catch (HttpResponseException) { throw; }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

        private EntidadEntity GetEntidadEntity(string id)
        {
            int idConverted;
            bool isValid = int.TryParse(id, out idConverted);
            if (!isValid)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));
            return GetEntidadEntity(idConverted);
        }

        private EntidadEntity GetEntidadEntity(int id)
        {
            var cat = LoadByPK(id);
            if (cat == null || cat.IdEmpresa != CurrentUser.IdEmpresa)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Entidad no existe", "El registro solicitado no existe"));
            return cat;

        }
    }
}
