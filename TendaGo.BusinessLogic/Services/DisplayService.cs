using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.BusinessLogic.Services
{
    public class DisplayService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public DisplayService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public List<DisplayDto> GetDisplays()
        {
            try
            {
                var parameters = new PantallaFindParameterEntity { IdEstado = 1 };

                var pantallas = _procedimientos.Pantalla_FindByAllAsync(
                                                    parameters.Id,
                                                    parameters.IdModulo,
                                                    parameters.IdGrupo,
                                                    parameters.Nombre,
                                                    parameters.Descripcion,
                                                    parameters.NombreAssembly,
                                                    parameters.Icono,
                                                    parameters.Ayuda,
                                                    parameters.IpIngreso,
                                                    parameters.UsuarioModificacion,
                                                    parameters.FechaIngreso,
                                                    parameters.IpModificacion,
                                                    parameters.UsuarioModificacion,
                                                    parameters.FechaModificacion,
                                                    parameters.IdEstado,
                                                    parameters.Orden
                                                );
                var displays =  _mapper.Map<List<DisplayDto>>(pantallas);
                return displays;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<DisplayDto> GetUserProfileDisplays(short idPerfil)
        {
            try
            {
                var parameters = new PantallaXPerfilFindParameterEntity { IdPerfil = idPerfil, IdEstado = 1 };
                var pantallasPerfil = _procedimientos.PantallaXPerfil_FindByAllAsync(
                                                            parameters.Id,
                                                            parameters.IdPerfil,
                                                            parameters.IdPantalla,
                                                            false,
                                                            false,
                                                            false,
                                                            false,
                                                            false,
                                                            false,
                                                            parameters.IpIngreso,
                                                            parameters.UsuarioIngreso,
                                                            parameters.FechaIngreso,
                                                            parameters.IpModificacion,
                                                            parameters.UsuarioModificacion,
                                                            parameters.FechaModificacion,
                                                            parameters.IdEstado
                                                        );

                var displays = _mapper.Map<List<DisplayDto>>(pantallasPerfil);
                return displays;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
