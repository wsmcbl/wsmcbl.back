using FluentValidation;
using FluentValidation.AspNetCore;
using wsmcbl.src.exception;
using wsmcbl.src.middleware.validator;

namespace wsmcbl.src.utilities;

public static class BuilderService
{
    public static string? getConnectionString(this WebApplicationBuilder builder)
    {
        return builder.Configuration.GetConnectionString("DefaultConnection");
    }

    public static void AddControllersOptions(this IServiceCollection Services)
    {
        Services.AddControllers(options =>
        {
            options.Conventions.Add(new RoutePrefixConvention("v6"));
        });
    }
    
    public static void AddFluentValidationConfig(this IServiceCollection Services)
    {
        Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        Services.AddValidatorsFromAssemblyContaining<ChangeStudentDiscountDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<CreateStudentProfileDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollmentToUpdateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollStudentDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<MediaEntityValidator>();
        Services.AddValidatorsFromAssemblyContaining<PagedRequestValidator>();
        Services.AddValidatorsFromAssemblyContaining<SchoolyearToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<SubjectDataEntityValidator>();
        Services.AddValidatorsFromAssemblyContaining<TariffDataDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<TariffDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<TransactionToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<UserToCreateDtoValidator>();
    }

    public static void AddDefaultCors(this IServiceCollection Services)
    {
        string[] origins = ["http://localhost:4200", "http://localhost:4002"];

        if (Utility.isInProductionEnvironment())
        {
            var url = getAppUrl();
            origins = [$"https://{url}", $"https://wwww.{url}"];
        }
        
        Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader();
            });
        });
    }
    
    private static string getAppUrl()
    {
        var value = Environment.GetEnvironmentVariable("APP_URL");
        if (value == null)
        {
            throw new InternalException("APP_URL environment not found.");
        }

        return value;
    }
}
