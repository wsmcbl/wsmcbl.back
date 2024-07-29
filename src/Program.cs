using Microsoft.EntityFrameworkCore;
using wsmcbl.src.controller.business;
using wsmcbl.src.controller.service;
using wsmcbl.src.database;
using wsmcbl.src.database.context;
using wsmcbl.src.middleware;
using wsmcbl.src.middleware.filter;
using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new RoutePrefixConvention("v1")));

builder.Services.AddFluentValidationConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.getConnectionString()));

builder.Services.AddScoped<DaoFactory, DaoFactoryPostgres>();
builder.Services.AddScoped<ValidateModelFilterAttribute>();

builder.Services.AddTransient<IPrintDocumentsController, PrintDocumentsController>();
builder.Services.AddTransient<ICollectTariffController, CollectTariffController>();
builder.Services.AddTransient<ICreateOfficialEnrollmentController, CreateOfficialEnrollmentController>();
builder.Services.AddTransient<IEnrollStudentController, EnrollStudentController>();


builder.Services.AddSingleton(new TemplateProcessor(Path
    .Combine(AppContext.BaseDirectory,AppContext.BaseDirectory, "..","..", "..", "resource")));
builder.Services.AddSingleton(new PdfGenerator("/usr/bin/pdflatex"));
builder.Services.AddSingleton(new TemplateManager(
    Path.Combine(AppContext.BaseDirectory,AppContext.BaseDirectory, "..","..", "..", "resource"),
    Path.Combine(AppContext.BaseDirectory,AppContext.BaseDirectory, "..","..", "..", "resource/out")
    ));

var app = builder.Build();

//app.UseMiddleware<ApiExceptionHandler>();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerUIConfig());

app.MapControllers();
app.UseHttpsRedirection();
await app.RunAsync();

public abstract partial class Program;