using FluentValidation;
using FluentValidation.AspNetCore;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;
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
}