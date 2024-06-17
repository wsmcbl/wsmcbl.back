using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using wsmcbl.src.controller.business;
using wsmcbl.src.database;
using wsmcbl.src.middleware;
using wsmcbl.src.middleware.filter;
using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new RoutePrefixConvention("v1")));
builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Version = "v1", Title = "WSMCBL API",
            Description = "API for the Web System for Management of Colegio Bautista Libertad developed in .net 8",
            Contact = new OpenApiContact { Name = "Client application", Url = new Uri("https://cbl.somee.com") }
        });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

builder.Services.AddScoped<DaoFactory, DaoFactoryPostgres>();
builder.Services.AddScoped<ValidateModelFilterAttribute>();
builder.Services.AddTransient<ICollectTariffController, CollectTariffController>();
builder.Services.AddTransient<ICreateOfficialEnrollmentController, CreateOfficialEnrollmentController>();

var app = builder.Build();

app.UseMiddleware<CustomStatusCodeMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WSMCBL API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.UseHttpsRedirection();
await app.RunAsync();