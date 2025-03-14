//using ER.BA;
//using ER.BE;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using System.Web.Http;
//using TendaGo.Common;

//namespace TendaGo.Api.Controllers
//{
//    [TokenAuthorize]
//    public class clientsController : ApiControllerBase
//    {
        
//        /// <summary>
//        /// Obtiene la lista de clientes 
//        /// </summary>
//        /// <param name="searchTerm">texto de busqueda</param>
//        /// <param name="identification"></param>
//        /// <param name="lite"></param>
//        /// <param name="page"></param>
//        /// <returns></returns>
//        [HttpGet, Route("clients/search/{searchTerm?}"), Route("clients/search")]
//        public List<EntityDto> SearchClients(string searchTerm = null, bool identification = false, bool lite = false, int? page = null)
//        {
//            if (searchTerm == null)
//                searchTerm = string.Empty;
//            if (searchTerm.Length <= 2)
//            { throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "La búsqueda  debe ser mayor a 4 caracteres", "búsqueda inválida")); }

//            return this.GetClients(searchTerm, identification, lite, page);
//        }


//        [HttpGet, Route("clients/consulta")]
//        public async Task<EntityDto> SearchClients(string id)
//        {
//            var findParameter = new EntidadFindParameterEntity
//            {
//                //IdEmpresa = CurrentUser.IdEmpresa,
//                IdEstado = 1,
//                Identificacion = id
//            };

//            EntidadEntityCollection result = EntidadCollectionBussinesAction.FindByAll(findParameter, 1);

//            if (result.Count > 0)
//            {
//                EntidadEntity clientEntity;
//                clientEntity = EntidadBussinesAction.LoadByPK(result[0].Id);
//                clientEntity.Id = 0;

//                return clientEntity.ToEntityDto();
//            }
//            else
//            {
//                var data = await DemographicService.GetDataAsync(id);
//                if (data.Data.Count > 0)
//                //if(true)
//                {
//                    EntityDto entidad = new EntityDto();

//                    entidad.RazonSocial = data.Data[0].Nombre;
//                    entidad.Identificacion = data.Data[0].Identificacion;
//                    entidad.FechaNacimiento = Convert.ToDateTime(data.Data[0].FechNac);
//                    entidad.Genero = data.Data[0].DesSexo;
//                    entidad.EstadoCivil = data.Data[0].DesEstadoCivil;
//                    entidad.Nacionalidad = data.Data[0].DesNacionalidad;
//                    entidad.NivelEstudio = data.Data[0].DescNivEst;
//                    entidad.Profesion = data.Data[0].DesProfesion;
//                    entidad.Direccion = data.Data[0].Calle;

//                    if (data.DataContact.Count > 0)
//                    {
//                        foreach (var contact in data.DataContact)
//                        {
//                            if (contact.IDENTIFICACION == id && contact.TipoDato == "CELULAR")
//                            {
//                                entidad.Celular = contact.Dato;
//                            }
//                            if (contact.IDENTIFICACION == id && contact.TipoDato == "FIJO")
//                            {
//                                entidad.Telefono = contact.Dato;
//                            }
//                            if (contact.IDENTIFICACION == id && contact.TipoDato == "CORREO")
//                            {
//                                entidad.Correo = contact.Dato;
//                            }
//                        }
//                    }

//                    //#region borrar esta parte
//                    //entidad.RazonSocial = "";
//                    //entidad.Identificacion = "";                    
//                    //entidad.EstadoCivil = "";
//                    //entidad.Nacionalidad = "";
//                    //entidad.NivelEstudio = "";
//                    //entidad.Profesion = "";
//                    //entidad.Direccion = "";
//                    //#endregion

//                    return entidad;
//                }
//                else
//                {
//                    return default;
//                }
//            }
//        }

        
//        /// <summary>
//        /// Guarda los datos para un cliente nuevo o existente
//        /// </summary>
//        /// <param name="client">modelo de datos de clientes</param>
//        /// <returns></returns>
//        [HttpPost]
//        public async Task<EntityDto> PostClient(EntityDto client)
//        {
//            string UserError = "Ocurrio un error general, reintente";
//            try
//            {

//                EntidadEntity clientEntity;

//                if (client.TipoIdentificacion == "C" || client.TipoIdentificacion == "R")
//                {
//                    if (client.TipoIdentificacion == "C" && client.Identificacion.Length != 10)
//                    {
//                        UserError = "La longitud de la cédula es diferente a 10 dígitos";
//                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, UserError, UserError));
//                    }
//                    if (client.TipoIdentificacion == "R" && client.Identificacion.Length != 13)
//                    {
//                        UserError = "La longitud del RUC es diferente a 13 dígitos";
//                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, UserError, UserError));
//                    }
//                    if (!CommonFunctions.ValidarNumeroIdentificacion(client.Identificacion))
//                    {
//                        UserError = "Numero de identificación no válido";
//                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, UserError, UserError));
//                    }
//                }

//                //if (!string.IsNullOrEmpty(client.Celular))
//                //{
//                //    if (client.Celular.Length != 10)
//                //    {
//                //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "El número de celular debe tener 10 digitos"));
//                //    }

//                //    if (!client.Celular.StartsWith("09"))
//                //    {
//                //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "El número de celular debe empezar con [09]"));
//                //    }
//                //}

//                if (client.Id > 0)
//                {
//                    clientEntity = EntidadBussinesAction.LoadByPK(client.Id);
//                    clientEntity.CurrentState = EntityStatesEnum.Updated;
//                    clientEntity.RazonSocial = client.RazonSocial;
//                    clientEntity.Identificacion = client.Identificacion;
//                    clientEntity.Direccion = client.Direccion;
//                    clientEntity.Correo = client.Correo;
//                    clientEntity.Telefono = client.Telefono;
//                    clientEntity.Celular = client.Celular;
//                    clientEntity.Observaciones = client.Observaciones;
//                    clientEntity.IdCiudad = client.IdCiudad.GetValueOrDefault();
//                    clientEntity.IdProvincia = client.IdProvincia.GetValueOrDefault();
//                    clientEntity.IdPais = client.IdPais.GetValueOrDefault();
//                    clientEntity.IdSector = client.IdSector.GetValueOrDefault();
//                    clientEntity.IdEstado = client.IdEstado;
//                    clientEntity.EsCliente = true;
//                    clientEntity.EsProveedor = client.EsProveedor;
//                    clientEntity.Latitud = client.Latitud;
//                    clientEntity.Longitud = client.Longitud;
//                    clientEntity.FechaNacimiento = client.FechaNacimiento;
//                    clientEntity.Genero = client.Genero;
//                    clientEntity.EstadoCivil = client.EstadoCivil;
//                    clientEntity.Nacionalidad = client.Nacionalidad;
//                    clientEntity.NivelEstudio = client.NivelEstudio;
//                    clientEntity.Profesion = client.Profesion;
//                    clientEntity.TipoIdentificacion = client.TipoIdentificacion;

//                    if (clientEntity.CurrentState.Equals(EntityStatesEnum.Updated))
//                    {
//                        clientEntity.UsuarioModificacion = CurrentUser.InicioSesion;
//                        clientEntity.IpModificacion = client.IpModificacion;
//                        clientEntity.FechaModificacion = DateTime.Today;
//                    }
//                    if (!string.IsNullOrEmpty(client.Foto))
//                    {
//                        clientEntity.Foto = await UploadFileAsync(client.Foto, client.Identificacion);
//                    }
//                    else
//                    {
//                        clientEntity.Foto = "";
//                    }
//                }
//                else
//                {
//                    // si el cliente es nuevo
//                    var col = EntidadCollectionBussinesAction.FindByAll(new EntidadFindParameterEntity
//                    {
//                        IdEmpresa = CurrentUser.IdEmpresa,
//                        Identificacion = client.Identificacion.Substring(0, client.Identificacion.Length < 10 ? client.Identificacion.Length : 10)
//                    });

//                    if (col.Count > 0)
//                    {
//                        UserError = "Numero de identificación ya existe en la base de datos";
//                        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Número de identificación ya existe", "Numero de identificación ya existe"));
//                    }

//                    clientEntity = client.ToEntidadEntity();

//                    clientEntity.IdEmpresa = CurrentUser.IdEmpresa;
//                    clientEntity.TipoEntidad = "PERSONA"; //"CLIENTE";
//                    clientEntity.IdEstado = 1;
//                    clientEntity.EsProveedor = false;
//                    clientEntity.EsCliente = true;
//                    clientEntity.Latitud = client.Latitud;
//                    clientEntity.Longitud = client.Longitud;
//                    clientEntity.FechaNacimiento = client.FechaNacimiento;
//                    clientEntity.Genero = client.Genero;
//                    clientEntity.EstadoCivil = client.EstadoCivil;
//                    clientEntity.Nacionalidad = client.Nacionalidad;
//                    clientEntity.NivelEstudio = client.NivelEstudio;
//                    clientEntity.Profesion = client.Profesion;

//                    clientEntity.UsuarioIngreso = CurrentUser.InicioSesion;
//                    clientEntity.IpIngreso = client.IpIngreso;
//                    clientEntity.FechaIngreso = DateTime.Now;

//                    clientEntity.UsuarioModificacion = null;
//                    clientEntity.IpModificacion = null;
//                    clientEntity.FechaModificacion = null;
//                    if (!string.IsNullOrEmpty(client.Foto))
//                    {
//                        clientEntity.Foto = await UploadFileAsync(client.Foto, client.Identificacion);
//                    }
//                    else
//                    {
//                        clientEntity.Foto = "";
//                    }
//                }

//                clientEntity = EntidadBussinesAction.Save(clientEntity);

//                return clientEntity.ToEntityDto();
//            }
//            catch (HttpResponseException ex)
//            {
//                Log("PostClient", ex);
//                throw ex;
//            }
//            catch (Exception ex)
//            {
//                Log("PostClient", ex);

//                if (ex.GetMessage().Contains("UQ_Entidad"))
//                {
//                    UserError = "No puede ingresar un proveedor duplicado";
//                }
//                else if (ex.GetMessage().ToUpper().Contains("PERSONA"))
//                {
//                    UserError = ex.GetMessage();
//                }

//                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
//            }
//        }


//        //private string UploadFile(string imagen,string id)
//        //{
//        //    Account account = new Account(
//        //    "binasystenda",
//        //    "922982135963177",
//        //    "SF9TToVTUiyRI2q4eLldIn7Ecf4");
//        //    Cloudinary cloudinary = new Cloudinary(account);

//        //    var file = Convert.FromBase64String(imagen);
//        //    var stream = new MemoryStream(file);

//        //    var uploadParams = new ImageUploadParams()
//        //    {
//        //        File = new FileDescription($"{id}_foto", stream),
//        //        PublicId = "TendaGo/Carmita/Clientes/"+id.Trim(),
//        //        Overwrite = true
//        //    };
//        //    var uploadResult = cloudinary.Upload(uploadParams);

//        //    return uploadResult.Url.OriginalString;
//        //}

//    }
//}