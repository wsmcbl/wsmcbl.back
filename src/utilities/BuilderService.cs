using FluentValidation;
using FluentValidation.AspNetCore;
using wsmcbl.src.middleware.filter;

namespace wsmcbl.src.utilities;

public static class BuilderService
{
    public static void AddEnvironmentConfig(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();

        var environment = builder.Environment.EnvironmentName;
        
        if(environment.Equals("Test"))
            builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
    }
    
    public static string? getConnectionString(this WebApplicationBuilder builder)
    {
        return builder.Configuration.GetConnectionString("DefaultConnection");
    }

    public static void AddFluentValidationConfig(this IServiceCollection Services)
    {
        Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        
        Services.AddValidatorsFromAssemblyContaining<TransactionToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollmentToCreateDtoValidator>();
    }
}