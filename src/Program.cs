using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wsmcbl.src.controller.business;
using wsmcbl.src.controller.service;
using wsmcbl.src.database;
using wsmcbl.src.database.context;
using wsmcbl.src.middleware;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

var builder = WebApplication.CreateBuilder(args);

builder.AddJWTAuthentication();
builder.AddAuthorization();

builder.Services.AddControllersOptions();
builder.Services.AddFluentValidationConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.getConnectionString()));

builder.Services.AddScoped<DaoFactory, DaoFactoryPostgres>();
builder.Services.AddScoped<JwtGenerator>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddScoped<UserAuthenticator>();

builder.Services.AddTransient<ICollectTariffController, CollectTariffController>();
builder.Services.AddTransient<ICreateOfficialEnrollmentController, CreateOfficialEnrollmentController>();
builder.Services.AddTransient<IEnrollStudentController, EnrollStudentController>();
builder.Services.AddTransient<IPrintReportCardByStudentController, PrintReportCardByStudentController>();
builder.Services.AddTransient<IMoveTeacherGuideFromEnrollmentController, MoveTeacherGuideFromEnrollmentController>();

builder.Services.AddTransient<ICreateStudentProfileController, CreateStudentProfileController>();
builder.Services.AddTransient<IAddingStudentGradesController, AddingStudentGradesController>();
builder.Services.AddTransient<IResourceController, ResourceController>();
builder.Services.AddTransient<ILoginController, LoginController>();

builder.Services.AddTransient<ITransactionReportByDateController, TransactionReportByDateController>();
builder.Services.AddTransient<ICancelTransactionController, CancelTransactionController>();
builder.Services.AddTransient<IMoveStudentFromEnrollmentController, MoveStudentFromEnrollmentController>();

var app = builder.Build();

app.UseMiddleware<ApiExceptionHandler>();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerUIConfig());

app.MapControllers();
app.UseHttpsRedirection();
await app.RunAsync();

public abstract partial class Program;