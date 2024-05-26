using Microsoft.EntityFrameworkCore;
using wsmcbl.back.config;
using wsmcbl.back.controller.business;
using wsmcbl.back.database;
using wsmcbl.back.model.dao;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new RoutePrefixConvention("v1")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

builder.Services.AddScoped<DaoFactory, DaoFactoryPostgres>();
builder.Services.AddTransient<ICollectTariffController, CollectTariffController>();
builder.Services.AddTransient<ICreateOfficialEnrollmentController, CreateOfficialEnrollmentController>();


var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WSMCBL API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();