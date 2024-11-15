using FluentValidation;
using FluentValidation.AspNetCore;
using wsmcbl.src.middleware.filter;
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
        Services.AddControllers(options => options.Conventions.Add(new RoutePrefixConvention("v2")));
    }
    
    public static void AddFluentValidationConfig(this IServiceCollection Services)
    {
        Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        
        Services.AddValidatorsFromAssemblyContaining<CreateStudentProfileDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollmentToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollmentToUpdateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollStudentDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<SchoolyearToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<StudentFullDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<StudentParentDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<TransactionToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<UserToCreateDtoValidator>();
    }
}