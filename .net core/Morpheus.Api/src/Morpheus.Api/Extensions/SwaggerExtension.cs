using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Morpheus.DataContracts.Base;
using System;
using System.IO;

namespace Morpheus.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Morpheus.Api", Version = "v1" });

                var xmlApiPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
                var xmlDataContractPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(OperationResponse).Assembly.GetName().Name}.xml");

                c.IncludeXmlComments(xmlApiPath);
                c.IncludeXmlComments(xmlDataContractPath);
            });
        }
    }
}
