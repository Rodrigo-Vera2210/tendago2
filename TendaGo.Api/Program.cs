﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using TendaGo.BusinessLogic.Services;
using TendaGo.Domain.Services;
using System.Net.Http;
using Azure;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using TendaGo.Api;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ER.DA.Repositories;
using AutoMapper;
using TendaGo.BusinessLogic.Mapping;
using NPOI.SS.Formula.Functions;

namespace TendaGo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();


            builder.Services.AddDbContext<TendaGOContext>(options =>
                options.UseSqlServer("Data Source=thanos.binasystem.com,5000;Initial Catalog=TendaGO;User Id=TendaGO;Password=Vqm5R4vCPVAFm;", sql => sql.EnableRetryOnFailure())
            );
            
            ConfigureMapper(builder);

            builder.Services.AddScoped<TendaGOContext>();
            builder.Services.AddScoped<DbContext, TendaGOContext>();
            builder.Services.AddScoped<TendaGOContext>();
            builder.Services.AddScoped<ITendaGOContextProcedures, TendaGOContextProcedures>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            // Configura otros servicios según sea necesario
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TendaGo.Api",
                    Version = "v1",
                    Description = "Recursos y documentacion",
                    Contact = new OpenApiContact
                    {
                        Name = "Edison Romero",
                        Url = new Uri("https://binasystem.com/nosotros/contactanos"),
                        Email = "eromero@binasystem.com"
                    }
                });

                // Incluir comentarios XML
                //var xmlFile = Path.Combine(AppContext.BaseDirectory, "TendaGo.Api.xml");
                //options.IncludeXmlComments(xmlFile);

                // Filtrar operaciones con seguridad
                options.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();

                // Configurar Enum para que sea descripto como cadena en lugar de números
                //options.DescribeAllEnumsAsStrings();
            });

            var app = builder.Build();

            // Configuración de middleware
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Rutas de los controladores
            app.MapControllers();

            // Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "API REST TendaGo";
            });

            // Ejecutar la aplicación
            app.Run();
        }

        protected static void ConfigureMapper(WebApplicationBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            var mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
        }

        private static string GetXmlCommentsPath()
        {
            return Path.Combine(AppContext.BaseDirectory, "TendaGo.Api.xml");
        }

        private static string GetRootUrlFromAppConfig(HttpRequestMessage req)
        {
            return req.RequestUri.Scheme + "://" + req.RequestUri.Authority;
        }

        public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor != null)
                {
                    var attributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(TokenAuthorizeAttribute), true);

                    if (attributes.Any())
                    {
                        if (operation.Parameters == null)
                        {
                            operation.Parameters = new List<OpenApiParameter>();
                        }

                        operation.Parameters.Add(new OpenApiParameter
                        {
                            Name = "app_token",
                            In = ParameterLocation.Header,
                            Description = "Token",
                            Required = true,
                            Schema = new OpenApiSchema { Type = "string" }
                        });
                    }
                }
            }

        }
    }

    
}
