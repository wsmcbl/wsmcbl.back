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

builder.Services.AddHttpClient();

builder.Services.AddScoped<DaoFactory, DaoFactoryPostgres>();
builder.Services.AddScoped<JwtGenerator>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddScoped<UserAuthenticator>();
builder.Services.AddHostedService<DisablePartialGradeRecordingBackground>();

builder.Services.AddTransient<CollectTariffController>();
builder.Services.AddTransient<UpdateOfficialEnrollmentController>();
builder.Services.AddTransient<EnrollStudentController>();
builder.Services.AddTransient<PrintReportCardByStudentController>();
builder.Services.AddTransient<MoveTeacherGuideFromEnrollmentController>();

builder.Services.AddTransient<CreateStudentProfileController>();
builder.Services.AddTransient<AddingStudentGradesController>();
builder.Services.AddTransient<ResourceController>();
builder.Services.AddTransient<LoginController>();

builder.Services.AddTransient<TransactionReportByDateController>();
builder.Services.AddTransient<CancelTransactionController>();
builder.Services.AddTransient<MoveStudentFromEnrollmentController>();
builder.Services.AddTransient<UpdateStudentProfileController>();
builder.Services.AddTransient<MoveTeacherFromSubjectController>();
builder.Services.AddTransient<ViewGradeOnlineController>();

builder.Services.AddTransient<ForgetDebtController>();
builder.Services.AddTransient<CorrectEducationalLevelController>();
builder.Services.AddTransient<CreateUserController>();
builder.Services.AddTransient<CreateEnrollmentController>();
builder.Services.AddTransient<AssignPermissionsController>();
builder.Services.AddTransient<PrintDocumentController>();//Auxiliary
builder.Services.AddTransient<ViewUserProfileController>();
builder.Services.AddTransient<EnablePartialGradeRecordingController>();
builder.Services.AddTransient<GenerateDebtorReportController>();

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