using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace wsmcbl.src.utilities;

public static class SwaggerServiceExtensions
{
    public static void SwaggerUIConfig(this SwaggerUIOptions options)
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WSMCBL API v1");
        options.RoutePrefix = string.Empty;
    }
    
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(setupAction: options =>
        {
            options.SwaggerDoc(name: "v1",
                info: new OpenApiInfo
                {
                    Version = "v1", Title = "WSMCBL API",
                    Description = "API for the Web System for Management of Colegio Bautista Libertad developed in .net 8",
                    Contact = new OpenApiContact
                    {
                        Name = "Client application",
                        Url = new Uri(uriString: $"https://cbl.somee.com")
                    }
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(path1: AppContext.BaseDirectory, path2: xmlFile);
            options.IncludeXmlComments(filePath: xmlPath);
        });
    }
}