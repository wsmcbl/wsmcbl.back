using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using wsmcbl.src.middleware.filter;

namespace wsmcbl.src.utilities;

public static class SwaggerServiceExtensions
{
    public static void SwaggerUIConfig(this SwaggerUIOptions options)
    {
        options.SwaggerEndpoint("/swagger/v6/swagger.json", "WSMCBL_API_V6");
        options.RoutePrefix = string.Empty;
    }
    
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(setupAction: options =>
        {
            options.SwaggerDoc(name: "v6",
                info: new OpenApiInfo
                {
                    Version = "v6", Title = "WSMCBL_API",
                    Description = "API of the Web System for Management of Colegio Bautista Libertad",
                    Contact = new OpenApiContact
                    {
                        Name = "Client",
                        Url = new Uri(uriString: $"https://cbl-edu.com")
                    }
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(path1: AppContext.BaseDirectory, path2: xmlFile);
            options.IncludeXmlComments(filePath: xmlPath);
            
            options.OperationFilter<RemoveDefaultSuccessResponseFilter>();
        });
    }
}