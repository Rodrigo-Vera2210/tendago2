using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
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
    public class LineService : ILineService
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public LineService(ITendaGOContextProcedures procedimientos, IMapper mapper, ICategoriaService categoriaService)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
            _categoriaService = categoriaService;
        }

        public async Task<List<CategoryDto>> GetAllCategories(string id, int idEmpresa)
        {
            try
            {
                int idConverted;
                bool isValidId = int.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El parametro id, es invalido, Id invalido") });

                var findParameter = new CategoriaFindParameterEntity { IdLinea = idConverted, IdEmpresa = idEmpresa };
                var categories = await _categoriaService.FindByAll(findParameter);

                var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories);

                return categoriesDtos;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public async Task<List<CategoryDto>> GetCategories(string id, int idEmpresa)
        {
            try
            {
                int idConverted;
                bool isValidId = int.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El parametro id, es invalido, Id invalido") });

                var findParameter = new CategoriaFindParameterEntity { IdLinea = idConverted, IdEmpresa = idEmpresa, IdEstado = 1 };
                var categories = await _categoriaService.FindByAll(findParameter);
                var categoriesDtoList = _mapper.Map<List<CategoryDto>>(categories);
                return categoriesDtoList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public async Task<LineDto> GetLine(string id, int idEmpresa)
        {
            try
            {
                var line = await GetLineaEntity(id, idEmpresa);

                var response = _mapper.Map<LineDto>(line);

                return response;
            }
            catch (HttpResponseException) { throw; }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message + "Ocurrio un error general, reintente") });
            }
        }
        
        public async Task<LineDto> PostLine(LineDto line, int idEmpresa)
        {
            try
            {
                LineaEntity lineaEntity;
                if (line.Id != 0)
                {
                    lineaEntity = await GetLineaEntity(line.Id,idEmpresa);
                    lineaEntity.Linea = line.Linea;
                    lineaEntity.IdDivision = line.IdDivision;
                    lineaEntity.IdEstado = line.IdEstado;
                    if (lineaEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        lineaEntity.UsuarioModificacion = line.UsuarioModificacion;
                        lineaEntity.IpModificacion = line.IpModificacion;
                        lineaEntity.FechaModificacion = DateTime.Today;
                    }
                }
                else
                {
                    lineaEntity = _mapper.Map<LineaEntity>(line);
                    lineaEntity.FechaIngreso = DateTime.Today;
                }
                lineaEntity.IdEmpresa = idEmpresa;
                lineaEntity = await Save(lineaEntity);
                var response = _mapper.Map<LineDto>(lineaEntity);
                return response;
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.Message.Contains("UQ_Linea"))
                {
                    UserError = "No puede ingresar una Linea duplicada";
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message + UserError) });
            }
        }

        private async Task<LineaEntity> GetLineaEntity(string id, int idEmpresa)
        {
            try
            {
                int idConverted;
                bool isValidId = int.TryParse(id, out idConverted);
                if (!isValidId)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("El parametro id, es invalido, Id invalido") });
                return await this.GetLineaEntity(idConverted,idEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private async Task<LineaEntity> GetLineaEntity(int id, int idEmpresa)
        {
            try
            {
                var lines = await _procedimientos.Linea_LoadByPKAsync(id);
                var line = lines.FirstOrDefault();
                if (line == null || line.IdEmpresa != idEmpresa)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Línea no existe, El registro solicitado no existe") });
                var response = _mapper.Map<LineaEntity>(line); 
                return response;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<LineaEntity> Save(LineaEntity linea)
        {

            try
            {
                switch (linea.CurrentState)
                {
                    case EntityStatesEnum.Deleted:
                        await Delete(linea);
                        break;
                    case EntityStatesEnum.Updated:
                        await Update(linea);
                        break;
                    case EntityStatesEnum.New:
                        linea = await Insert(linea);
                        break;
                    default:
                        break;
                }

                return linea;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private async Task<LineaEntity> Insert(LineaEntity linea)
        {
            try
            {
                OutputParameter<int?> idLinea = new();

                var result = await _procedimientos.Linea_InsertAsync(
                        linea.IdEmpresa,
                        linea.IdDivision,
                        linea.Linea,
                        linea.IpIngreso,
                        linea.UsuarioIngreso,
                        linea.FechaIngreso,
                        linea.IpModificacion,
                        linea.UsuarioModificacion,
                        linea.FechaModificacion,
                        linea.IdEstado,
                        idLinea
                    );

                var lineaE = await GetLineaEntity((int)idLinea.Value, linea.IdEmpresa);

                var response = _mapper.Map<LineaEntity>(lineaE);

                return response;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private async Task Update(LineaEntity linea)
        {
            try
            {
                OutputParameter<int> idLinea = new();


                var result = await _procedimientos.Linea_UpdateAsync(
                                                linea.Id,
                                                linea.IdEmpresa,
                                                linea.IdDivision,
                                                linea.Linea,
                                                linea.IpIngreso,
                                                linea.UsuarioIngreso,
                                                linea.FechaIngreso,
                                                linea.IpModificacion,
                                                linea.UsuarioModificacion,
                                                linea.FechaModificacion,
                                                linea.IdEstado,
                                                idLinea
                                            );


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private async Task Delete(LineaEntity linea)
        {
            try
            {
                var result = await _procedimientos.Linea_DeleteAsync(
                                                    linea.Id,
                                                    linea.FechaModificacion,
                                                    linea.UsuarioModificacion,
                                                    linea.IpModificacion
                                                   );


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
