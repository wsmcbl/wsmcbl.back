using Microsoft.EntityFrameworkCore;
using wsmcbl.back.controller.business;
using wsmcbl.back.database;
using wsmcbl.back.model.accounting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreStCo")));

builder.Services.AddScoped<DaoFactoryPostgre>();
builder.Services.AddScoped<IStudentDao>(sp => sp.GetRequiredService<DaoFactoryPostgre>().studentDao());
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