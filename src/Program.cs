using Microsoft.EntityFrameworkCore;
using wsmcbl.src.controller.business;
using wsmcbl.src.database;
using wsmcbl.src.database.context;
using wsmcbl.src.middleware;
using wsmcbl.src.middleware.filter;
using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new RoutePrefixConvention("v2")));

builder.Services.AddFluentValidationConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.getConnectionString()));

builder.Services.AddScoped<DaoFactory, DaoFactoryPostgres>();
builder.Services.AddScoped<ValidateModelActionFilterAttribute>();

builder.Services.AddTransient<ICollectTariffController, CollectTariffController>();
builder.Services.AddTransient<ICreateOfficialEnrollmentController, CreateOfficialEnrollmentController>();
builder.Services.AddTransient<IEnrollStudentController, EnrollStudentController>();
builder.Services.AddTransient<IPrintReportCardByStudentController, PrintReportCardByStudentController>();
builder.Services.AddTransient<IMoveTeacherGuideFromEnrollmentController, MoveTeacherGuideFromEnrollmentController>();

builder.Services.AddTransient<ICreateStudentProfileController, CreateStudentProfileController>();
builder.Services.AddTransient<IAddingStudentGradesController, AddingStudentGradesController>();

builder.Services.AddTransient<IListController, ListController>();

var app = builder.Build();

app.UseMiddleware<ApiExceptionHandler>();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerUIConfig());

app.MapControllers();
app.UseHttpsRedirection();
await app.RunAsync();

public abstract partial class Program;