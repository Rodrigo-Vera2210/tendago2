using AutoMapper;
using ER.BA;
using ER.BE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace TendaGo.Common
{
    public static class DocumentExtensions
    {
        internal static readonly MapperConfiguration DefaultMapper = new MapperConfiguration(config =>
        {
            config.CreateMap<DetalleDocumentoEntity, DocumentDetailDto>();
            config.CreateMap<DocumentDetailDto, DetalleDocumentoEntity>();

            config.CreateMap<DocumentoEntity, DocumentDto>()
                .ForMember(m => m.Detalles, x => x.MapFrom(model => model.DetalleDocumentoFromIdDocumento));

            config.CreateMap<DocumentDto, DocumentoEntity>()
                .ForMember(m => m.DetalleDocumentoFromIdDocumento, x => x.MapFrom(model => model.Detalles));

        });

        internal static DocumentDto ToDocumentDto(this DocumentoEntity entity)
        {
            var mapper = DefaultMapper.CreateMapper();
            var dto = mapper.Map<DocumentDto>(entity);
            return dto;
        }

        internal static DocumentDetailDto ToDocumentDetailDto(this DetalleDocumentoEntity detalle)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<DetalleDocumentoEntity, DocumentDetailDto>());
            var mapper = conf.CreateMapper();
            var result = mapper.Map<DocumentDetailDto>(detalle);
            return result;
        }
        internal static DetalleDocumentoEntity ToDetalleDocumentoEntity(this DocumentDetailDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<DocumentDetailDto, DetalleDocumentoEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<DetalleDocumentoEntity>(dto);
            return entity;
        }

        internal static DocumentoEntity ToDocumentoEntity(this DocumentDto dto)
        {
            var mapper = DefaultMapper.CreateMapper();
            var entity = mapper.Map<DocumentoEntity>(dto);
            if (dto.Detalles.Any())
            {
                entity.DetalleDocumentoFromIdDocumento = new DetalleDocumentoEntityCollection();

                foreach (var DocumentDetailDto in dto.Detalles)
                {
                    entity.DetalleDocumentoFromIdDocumento.Add(DocumentDetailDto.ToDetalleDocumentoEntity());
                }
            }

            return entity;
        }

        internal static InvoiceRequestModel ToInvoiceRequest(this DocumentoEntity documento, SqlConnection connection = null, SqlTransaction transaccion = null)
        {
            var cliente = EntidadBussinesAction.LoadByPK(documento.IdEntidad, connection, transaccion);

            return new InvoiceRequestModel
            {
                Identification = documento.ConsumidorFinal ? "9999999999" : cliente.Identificacion,
                IdentificationType = documento.ConsumidorFinal ? "07" : (
                    cliente.TipoIdentificacion == "R" ? "04" :
                    cliente.TipoIdentificacion == "C" ? "05" : "06"
                ),

                ContributorName = documento.ConsumidorFinal ? "Consumidor Final" : cliente.RazonSocial,
                Currency = "DOLAR",
                Address = cliente.Direccion.Left(300),
                EmailAddresses = cliente.Correo,
                IssuedOn = documento.Fecha.ToString("dd/MM/yyyy"),
                Phone = cliente.Telefono,
                Reason = documento.Notas.Left(300),
                ValueAddedTax = documento.TotalIva,
                Subtotal = documento.TotalSinImpuesto,
                SubtotalVat = documento.BaseIva,
                SubtotalVatZero = documento.Base0,
                Total = documento.Total,
                Status = true,

                EstablishmentCode = documento.Establecimiento,
                IssuePointCode = documento.PuntoEmision,

                Details = documento.DetalleDocumentoFromIdDocumento.Select(m =>
                {
                    var producto = ProductoBussinesAction.LoadByPK(m.IdProducto, connection, transaccion);
                    var unidad = TipoUnidadBussinesAction.LoadByPK(m.IdTipoUnidad, connection, transaccion, 0);

                    return new InvoiceDetailModel
                    {
                        MainCode = producto.CodigoInterno,
                        Description = producto.Producto.Left(300),
                        Name1 = "Medida",
                        Value1 = GetUnidad(unidad?.TipoUnidad),
                        Name2 = "Marca",
                        Value2 = producto.IdMarcaAsMarca?.Marca?.Left(300),
                        Amount = m.Cantidad,
                        UnitPrice = m.Precio,
                        SubTotal = m.SubtotalSinImpuesto,
                        Discount = m.Descuento,
                        ValueAddedTaxCode = producto.IdTarifaImpuesto.ToString(),
                        ValueAddedTaxValue = producto.PorcentajeTarifaImpuesto
                    };

                }).ToList(),
                AdditionalFields = GetDetallesAdicionales(documento, cliente),
                Payments = new List<PaymentModel>
                    {
                        new PaymentModel
                        {
                            Name = "EFECTIVO",
                            PaymentMethodCode = documento.FormaPago,
                            Term = documento.Plazo,
                            TimeUnit = documento.UnidadTiempo,
                            Total = documento.Total
                        }
                    }
            };
        }

        internal static ComprobanteRequestModel ToInvoiceRequestGo(this DocumentoEntity documento, SqlConnection connection = null, SqlTransaction transaccion = null)
        {
            var cliente = EntidadBussinesAction.LoadByPK(documento.IdEntidad, connection, transaccion);
            var empresa = EmpresaBussinesAction.LoadByPK(documento.IdEmpresa, connection, transaccion);
            decimal totalDesc = 0;

            if (documento.TotalDescuento > 0)
            {
                totalDesc = documento.DetalleDocumentoFromIdDocumento.Sum(x => x.Cantidad * x.Precio);
            }

            return new ComprobanteRequestModel
            {
                TipoComprobante = "01",
                Ruc = documento.RUC, //"0927031732001",
                DireccionEstablecimiento = empresa.Direccion,
                Establecimiento = documento.Establecimiento,
                PuntoEmision = documento.PuntoEmision,
                FechaEmision = documento.Fecha.ToString("yyyy-MM-dd"),
                TipoIdentificacion = documento.ConsumidorFinal ? "07" : (
                    cliente.TipoIdentificacion == "R" ? "04" :
                    cliente.TipoIdentificacion == "C" ? "05" : "06"
                ),
                Identificacion = documento.ConsumidorFinal ? "9999999999" : cliente.Identificacion,
                Nombre = documento.ConsumidorFinal ? "Consumidor Final" : cliente.RazonSocial,
                Direccion = cliente.Direccion.Left(300),
                Correo = cliente.Correo,
                ValorTotal = documento.Total - (documento.TotalDescuento > 0 ? (Math.Round((documento.Total * documento.TotalDescuento) / 100, 2)) : 0),
                Factura = new FacturaRequestModel
                {
                    Moneda = "DOLAR",
                    Placa = "",
                    TotalSinImpuestos = documento.TotalSinImpuesto - (documento.TotalDescuento > 0 ? (Math.Round((documento.TotalSinImpuesto * documento.TotalDescuento) / 100, 2)) : 0),
                    TotalSubsidio = 0,
                    TotalDescuento = (documento.TotalDescuento > 0 ? (Math.Round((totalDesc * documento.TotalDescuento) / 100, 2)) : 0), //documento.TotalDescuento,
                    ImporteTotal = documento.Total - (documento.TotalDescuento > 0 ? (Math.Round((documento.Total * documento.TotalDescuento) / 100, 2)) : 0),
                    Propina = 0,
                    PorcentajeIva = 15,
                    ValorRetIva = 0,
                    ValorRetRenta = 0
                },
                DetalleComprobante = documento.DetalleDocumentoFromIdDocumento.Select(m =>
                {
                    var producto = ProductoBussinesAction.LoadByPK(m.IdProducto, connection, transaccion);
                    var unidad = TipoUnidadBussinesAction.LoadByPK(m.IdTipoUnidad, connection, transaccion, 0);
                    var totalSinIMpuesto = m.SubtotalSinImpuesto - (documento.TotalDescuento > 0 ? (Math.Round((m.SubtotalSinImpuesto * documento.TotalDescuento) / 100, 2)) : 0);

                    ////valido si el string que manda el front no da error al convertir en int
                    //if (int.TryParse(m.TipoIva, out int idTarifa))
                    //{
                    //    Console.WriteLine("Conversión exitosa");
                    //}
                    //else
                    //{
                    //    idTarifa = producto.IdTarifaImpuesto;
                    //}

                    return new DetalleComprobanteRequestModel
                    {
                        CodigoPrincipal = producto.CodigoInterno,
                        CodigoAuxiliar = producto.CodigoInterno,
                        Descripcion = producto.Producto.Left(300),
                        UnidadMedida = GetUnidad(unidad?.TipoUnidad),
                        Cantidad = m.Cantidad,
                        PrecioUnitario = m.Precio,
                        PrecioSinSubsidio = 0,
                        Descuento = (documento.TotalDescuento > 0 ? (Math.Round((m.SubtotalSinImpuesto * documento.TotalDescuento) / 100, 2)) : 0),//m.Descuento,
                        PrecioTotalSinImpuesto = m.SubtotalSinImpuesto - (documento.TotalDescuento > 0 ? (Math.Round((m.SubtotalSinImpuesto * documento.TotalDescuento) / 100, 2)) : 0),

                        DetalleImpuesto = new List<DetalleImpuestoRequestModel>
                                {
                                    new DetalleImpuestoRequestModel
                                    {
                                        IdImpuesto = 2,
                                        IdTarifa = producto.IdTarifaImpuesto,
                                        BaseImponible = totalSinIMpuesto,
                                        Valor = (documento.TotalDescuento > 0 && m.Iva > 0 ? (Math.Round((totalSinIMpuesto * producto.PorcentajeTarifaImpuesto) / 100, 2)) : m.Iva) //m.Iva
                                    }
                                }
                        //ValueAddedTaxCode = m.TipoIva,
                    };

                }).ToList(),
                CampoAdicional = GetDetallesAdicionalesGo(documento, cliente),
                DetallePago = new List<PagoRequestModel>
                    {
                        new PagoRequestModel
                        {
                            IdFormaPago = documento.IdFormaPagoSri == 0 ? 7 : documento.IdFormaPagoSri, //guardar forma pago del nuevo endpoint 
                            Total=documento.Total - (documento.TotalDescuento > 0 ? (Math.Round((documento.Total * documento.TotalDescuento) / 100, 2)) : 0),
                            Plazo=documento.Plazo,
                            IdUnidadTiempo=1
                        }
                    }
            };
        }

        internal static ComprobanteRequestModel ToCreditRequestGo(this DocumentoEntity documento, SqlConnection connection = null, SqlTransaction transaccion = null)
        {
            var cliente = EntidadBussinesAction.LoadByPK(documento.IdEntidad, connection, transaccion);
            var empresa = EmpresaBussinesAction.LoadByPK(documento.IdEmpresa, connection, transaccion);

            return new NotaCreditoRequestModel
            {
                TipoComprobante = "04",
                Ruc = documento.RUC, //"0927031732001",
                DireccionEstablecimiento = empresa.Direccion,
                Establecimiento = documento.Establecimiento,
                PuntoEmision = documento.PuntoEmision,
                FechaEmision = DateTime.Today.ToString("yyyy-MM-dd"),
                TipoIdentificacion = documento.ConsumidorFinal ? "07" : (
                    cliente.TipoIdentificacion == "R" ? "04" :
                    cliente.TipoIdentificacion == "C" ? "05" : "06"
                ),
                Identificacion = documento.ConsumidorFinal ? "9999999999" : cliente.Identificacion,
                Nombre = documento.ConsumidorFinal ? "Consumidor Final" : cliente.RazonSocial,
                Direccion = cliente.Direccion.Left(300),
                Correo = cliente.Correo,
                ValorTotal = documento.Total,
                CodDocModificado = "01",
                NumDocModificado = documento.NumeroDocumento,
                FechaEmisionDocSustento = documento.Fecha.ToString("yyyy-MM-dd"),
                ValorModificacion = documento.Total,
                Motivo = "Devolucion",
                TotalSinImpuestos = documento.TotalSinImpuesto,
                Moneda = "DOLAR",
                PorcentajeIva = 15,
                ValorRetIva = 0,
                ValorRetRenta = 0,
                DetalleComprobante = documento.DetalleDocumentoFromIdDocumento.Select(m =>
                {
                    var producto = ProductoBussinesAction.LoadByPK(m.IdProducto, connection, transaccion);
                    var unidad = TipoUnidadBussinesAction.LoadByPK(m.IdTipoUnidad, connection, transaccion, 0);
                    var totalSinIMpuesto = m.SubtotalSinImpuesto - (documento.TotalDescuento > 0 ? (Math.Round((m.SubtotalSinImpuesto * documento.TotalDescuento) / 100, 2)) : 0);

                    return new DetalleComprobanteRequestModel
                    {
                        CodigoPrincipal = producto.CodigoInterno,
                        CodigoAuxiliar = producto.CodigoInterno,
                        Descripcion = producto.Producto.Left(300),
                        UnidadMedida = GetUnidad(unidad?.TipoUnidad),
                        Cantidad = m.Cantidad,
                        PrecioUnitario = m.Precio,
                        PrecioSinSubsidio = 0,
                        Descuento = m.Descuento,
                        PrecioTotalSinImpuesto = m.SubtotalSinImpuesto,
                        DetalleImpuesto = new List<DetalleImpuestoRequestModel>
                                {
                                    new DetalleImpuestoRequestModel
                                    {
                                        IdImpuesto = 2,
                                        IdTarifa = producto.IdTarifaImpuesto,
                                        BaseImponible = totalSinIMpuesto,
                                        Valor = (documento.TotalDescuento > 0 && m.Iva > 0 ? (Math.Round((totalSinIMpuesto * producto.PorcentajeTarifaImpuesto) / 100, 2)) : m.Iva) //m.Iva
                                    }
                                }
                        //ValueAddedTaxCode = m.TipoIva,
                    };

                }).ToList(),
                CampoAdicional = GetDetallesAdicionalesGo(documento, cliente),
                DetallePago = new List<PagoRequestModel>
                    {
                        new PagoRequestModel
                        {
                            IdFormaPago=1,
                            Total=documento.Total,
                            Plazo=documento.Plazo,
                            IdUnidadTiempo=1
                        }
                    }
            };
        }

        /// <summary>
        /// Convierto los "A UNIDAD" con "UNIDAD"
        /// </summary>
        /// <param name="tipoUnidad"></param>
        /// <returns></returns>
        private static string GetUnidad(string tipoUnidad)
        {
            if (!string.IsNullOrEmpty(tipoUnidad))
            {
                if (tipoUnidad.Length > 1 && tipoUnidad[1] == ' ')
                {
                    tipoUnidad = tipoUnidad.Substring(2, tipoUnidad.Length - 2);
                }

                return tipoUnidad;
            }

            return "UNIDAD";
        }

        internal static DocumentoEntity GenerateInvoice(this DocumentoEntity documento, SqlConnection connection = null, SqlTransaction transaccion = null)
        {

            try
            {
                var ruc = RucBussinesAction.LoadByPK(documento.RUC, connection, transaccion);
                var request = documento.ToInvoiceRequest(connection, transaccion);

                if (documento.IdTipoDocumento == "01")
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ruc.TokenFactElectonica?.Trim());

                        var response = client.PostAsync($"{CommonFunctions.UrlEcuafact}/Invoice", request, new JsonMediaTypeFormatter()).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;

                            var result = JsonConvert.DeserializeObject<InvoiceModel>(json);

                            if (result != null)
                            {
                                documento.IdMoneda = 1;
                                documento.Establecimiento = result.EstablishmentCode;
                                documento.PuntoEmision = result.IssuePointCode;
                                documento.NumeroDocumento = result.DocumentNumber;

                                return documento;
                            }

                            throw new Exception($"Hubo un problema con el servicio de facturación electrónica!", new Exception(json));
                        }

                        throw new HttpResponseException(response);
                    }
                }
                else
                {
                    SecuencialEntity secuencial = TipoDocumentoBussinesAction.GetDocumentSecuential(documento.RUC, documento.IdTipoDocumento);

                    documento.IdMoneda = 1;
                    documento.Establecimiento = secuencial.Establecimiento;
                    documento.PuntoEmision = secuencial.PuntoVenta;
                    documento.NumeroDocumento = secuencial.Secuencial.ToString("D9");

                    return documento;
                }
            }
            catch (HttpResponseException ex)
            {
                Logger.Error($"Hubo un error al enviar la factura electronica : {ex.Message}", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Error($"Hubo un error al enviar la factura electronica : {ex.Message}", ex);
                throw new Exception($"Hubo un error al enviar la factura electrónica. {ex.GetMessage()}", ex);
            }

        }

        internal static DocumentoEntity GenerateInvoiceGo(this DocumentoEntity documento, SqlConnection connection = null, SqlTransaction transaccion = null)
        {
            try
            {
                var ruc = RucBussinesAction.LoadByPK(documento.RUC, connection, transaccion);
                var request = documento.ToInvoiceRequestGo(connection, transaccion);

                //var resulttt = JsonConvert.SerializeObject(request);

                if (documento.IdTipoDocumento == "01")
                {
                    using (var client = new HttpClient())
                    {
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ruc.TokenFactElectonica?.Trim());

                        var response = client.PostAsync($"{CommonFunctions.UrlFacturago}/comprobantes/guardar", request, new JsonMediaTypeFormatter()).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;

                            //var result = JsonConvert.DeserializeObject<DocumentResult>(json);

                            var result = JsonConvert.DeserializeObject<OperationResult<DocumentResult>>(json);

                            if (result != null)
                            {
                                documento.IdMoneda = 1;
                                documento.Establecimiento = result.Result.Establecimiento;
                                documento.PuntoEmision = result.Result.PuntoEmision;
                                documento.NumeroDocumento = $"{result.Result.Establecimiento}-{result.Result.PuntoEmision}-{result.Result.Secuencial.ToString().PadLeft(9, '0')}";
                                documento.IdFacturaGo = result.Result.Id;

                                return documento;
                            }

                            throw new Exception($"Hubo un problema con el servicio de facturación electrónica!", new Exception(json));
                        }

                        throw new HttpResponseException(response);
                    }
                }
                else
                {
                    SecuencialEntity secuencial = TipoDocumentoBussinesAction.GetDocumentSecuential(documento.RUC, documento.IdTipoDocumento);

                    documento.IdMoneda = 1;
                    //documento.Establecimiento = secuencial.Establecimiento;
                    //documento.PuntoEmision = secuencial.PuntoVenta;
                    documento.NumeroDocumento = secuencial.Secuencial.ToString("D9");

                    return documento;
                }
            }
            catch (HttpResponseException ex)
            {
                Logger.Error($"Hubo un error al enviar la factura electronica : {ex.Message}", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Error($"Hubo un error al enviar la factura electronica : {ex.Message}", ex);
                throw new Exception($"Hubo un error al enviar la factura electrónica. {ex.GetMessage()}", ex);
            }

        }

        internal static DocumentoEntity GenerateCreditGo(this DocumentoEntity documento, SqlConnection connection = null, SqlTransaction transaccion = null)
        {
            try
            {
                var ruc = RucBussinesAction.LoadByPK(documento.RUC, connection, transaccion);
                var request = documento.ToCreditRequestGo(connection, transaccion);

                //var resulttt = JsonConvert.SerializeObject(request);

                if (documento.IdTipoDocumento == "04")
                {
                    using (var client = new HttpClient())
                    {
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ruc.TokenFactElectonica?.Trim());

                        var response = client.PostAsync($"{CommonFunctions.UrlFacturago}/comprobantes/nota-credito", request, new JsonMediaTypeFormatter()).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;

                            //var result = JsonConvert.DeserializeObject<DocumentResult>(json);

                            var result = JsonConvert.DeserializeObject<OperationResult<DocumentResult>>(json);

                            if (result != null)
                            {
                                documento.IdMoneda = 1;
                                documento.Establecimiento = result.Result.Establecimiento;
                                documento.PuntoEmision = result.Result.PuntoEmision;
                                documento.NumeroDocumento = $"{result.Result.Establecimiento}-{result.Result.PuntoEmision}-{result.Result.Secuencial.ToString().PadLeft(9, '0')}";
                                documento.IdFacturaGo = result.Result.Id;

                                return documento;
                            }

                            throw new Exception($"Hubo un problema con el servicio de facturación electrónica!", new Exception(json));
                        }

                        throw new HttpResponseException(response);
                    }
                }
                else
                {
                    SecuencialEntity secuencial = TipoDocumentoBussinesAction.GetDocumentSecuential(documento.RUC, documento.IdTipoDocumento);

                    documento.IdMoneda = 1;
                    //documento.Establecimiento = secuencial.Establecimiento;
                    //documento.PuntoEmision = secuencial.PuntoVenta;
                    documento.NumeroDocumento = secuencial.Secuencial.ToString("D9");

                    return documento;
                }
            }
            catch (HttpResponseException ex)
            {
                Logger.Error($"Hubo un error al enviar la factura electronica : {ex.Message}", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Error($"Hubo un error al enviar la factura electronica : {ex.Message}", ex);
                throw new Exception($"Hubo un error al enviar la factura electrónica. {ex.GetMessage()}", ex);
            }

        }

        private static List<AdditionalFieldModel> GetDetallesAdicionales(DocumentoEntity documento, EntidadEntity cliente)
        {
            var list = new List<AdditionalFieldModel>();
            var i = 1;

            // Agregamos la informacion Adicional del pedido.
            if (documento.IdSalida != null)
            {
                list.Add(new AdditionalFieldModel { LineNumber = i, Name = "Orden de Compra", Value = documento.IdSalida });

                var infoAdd = InfoAdicionalCollectionBussinesAction.FindByAll(new InfoAdicionalFindParameterEntity { IdSalida = documento.IdSalida }, 0);
                if (infoAdd != null && infoAdd.Count > 0)
                {
                    foreach (var inf in infoAdd)
                    {
                        list.Add(new AdditionalFieldModel { LineNumber = i++, Name = inf.TituloInfoAdicional, Value = inf.InfoAdicional });
                    }
                }
            }

            // Agrego esta información en el caso de los consumidores finales:
            if (documento.ConsumidorFinal)
            {
                list.Add(new AdditionalFieldModel { LineNumber = i++, Name = "Identificacion", Value = cliente.Identificacion });
                list.Add(new AdditionalFieldModel { LineNumber = i++, Name = "Cliente", Value = cliente.RazonSocial });
            }

            list.AddRange(new[]{
                        new AdditionalFieldModel{ LineNumber = i++, Name="Direccion", Value=cliente.Direccion },
                        new AdditionalFieldModel{ LineNumber = i++, Name="Email", Value=cliente.Correo },
                        new AdditionalFieldModel{ LineNumber = i++, Name="Telefono", Value=cliente.Telefono },
                        new AdditionalFieldModel{ LineNumber = i++, Name="Observaciones", Value= documento.Notas }
                });

            return list;
        }

        private static List<CampoAdicionalRequestModel> GetDetallesAdicionalesGo(DocumentoEntity documento, EntidadEntity cliente)
        {
            var list = new List<CampoAdicionalRequestModel>();
            //var i = 1;

            // Agregamos la informacion Adicional del pedido.
            if (documento.IdSalida != null)
            {
                list.Add(new CampoAdicionalRequestModel { Nombre = "Orden de Compra", Valor = documento.IdSalida });

                var infoAdd = InfoAdicionalCollectionBussinesAction.FindByAll(new InfoAdicionalFindParameterEntity { IdSalida = documento.IdSalida }, 0);
                if (infoAdd != null && infoAdd.Count > 0)
                {
                    foreach (var inf in infoAdd)
                    {
                        list.Add(new CampoAdicionalRequestModel { Nombre = inf.TituloInfoAdicional, Valor = inf.InfoAdicional });
                    }
                }
            }

            // Agrego esta información en el caso de los consumidores finales:
            if (documento.ConsumidorFinal)
            {
                list.Add(new CampoAdicionalRequestModel { Nombre = "Identificacion", Valor = cliente.Identificacion });
                list.Add(new CampoAdicionalRequestModel { Nombre = "Cliente", Valor = cliente.RazonSocial });
            }

            list.AddRange(new[]{
                        new CampoAdicionalRequestModel{Nombre="Direccion", Valor=cliente.Direccion },
                        new CampoAdicionalRequestModel{Nombre="Email", Valor=cliente.Correo }
                });

            if (!string.IsNullOrEmpty(cliente.Telefono))
            {
                list.Add(new CampoAdicionalRequestModel { Nombre = "Telefono", Valor = cliente.Telefono });
            }
            if (!string.IsNullOrEmpty(documento.Notas))
            {
                list.Add(new CampoAdicionalRequestModel { Nombre = "Observaciones", Valor = documento.Notas });
            }

            return list;
        }
    }
}