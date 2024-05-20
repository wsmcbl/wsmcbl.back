using Microsoft.EntityFrameworkCore;
using wsmcbl.back.config;
using wsmcbl.back.controller.business;
using wsmcbl.back.database;
using wsmcbl.back.model.accounting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new RoutePrefixConvention("v1")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

builder.Services.AddScoped<DaoFactoryPostgre>();
builder.Services.AddScoped<ICashierDao>(sp => sp.GetRequiredService<DaoFactoryPostgre>().cashierDao());
builder.Services.AddScoped<IStudentDao>(sp => sp.GetRequiredService<DaoFactoryPostgre>().studentDao());
builder.Services.AddScoped<ITariffDao>(sp => sp.GetRequiredService<DaoFactoryPostgre>().tariffDao());
builder.Services.AddScoped<ITransactionDao>(sp => sp.GetRequiredService<DaoFactoryPostgre>().transactionDao());
builder.Services.AddTransient<ICollectTariffController, CollectTariffController>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();