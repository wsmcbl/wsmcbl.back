var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hola, este es el back del proyecto wsmcbl");

app.Run();
