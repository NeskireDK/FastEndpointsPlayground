﻿using FastEndpoints.Swagger;
using NSwag.Generation.AspNetCore;

namespace Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddClosureSwaggerDoc(this IServiceCollection services)
    {
        services
            .AddSwaggerDoc(maxEndpointVersion: 1, addJWTBearerAuth: true, shortSchemaNames:true, tagIndex: 2, settings: s => { SetDefaults(s, 1); })
            .AddSwaggerDoc(maxEndpointVersion: 2, addJWTBearerAuth: false, shortSchemaNames:true, tagIndex: 1, settings: s => { SetDefaults(s, 2); });
    }

    private static void SetDefaults(AspNetCoreOpenApiDocumentGeneratorSettings s, int version)
    {
        s.Title = $"Closure Management api v{version}";
        s.DocumentName = $"v{version}";
        s.Version = $"v{version}.0";
        s.GenerateEnumMappingDescription = true;
    }
}