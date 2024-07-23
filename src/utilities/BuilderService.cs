namespace wsmcbl.src.utilities;

public static class BuilderService
{
    public static string? getConnectionString(this WebApplicationBuilder builder)
    {
        return builder.Environment.IsDevelopment()
            ? builder.Configuration.GetConnectionString("PostgresConnectionString.Test")
            : builder.Configuration.GetConnectionString("PostgresConnectionString");
    }
}