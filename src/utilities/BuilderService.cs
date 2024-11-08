using FluentValidation;
using FluentValidation.AspNetCore;
using wsmcbl.src.middleware.filter;

namespace wsmcbl.src.utilities;

public static class BuilderService
{
    public static string? getConnectionString(this WebApplicationBuilder builder)
    {
        return builder.Configuration.GetConnectionString("DefaultConnection");
    }

    public static void AddFluentValidationConfig(this IServiceCollection Services)
    {
        Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        
        Services.AddValidatorsFromAssemblyContaining<CreateStudentProfileDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollmentToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollmentToUpdateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<EnrollStudentDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<SchoolyearToCreateDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<StudentFullDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<StudentParentDtoValidator>();
        Services.AddValidatorsFromAssemblyContaining<TransactionToCreateDtoValidator>();
    }
}