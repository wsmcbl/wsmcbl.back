using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wsmcbl.src.database.context;
using wsmcbl.src.utilities;

namespace wsmcbl.tests.integration;

public class BaseClassFixture : WebApplicationFactory<PublicProgram>
{
    private PostgresContext context;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                context = scopedServices.GetRequiredService<PostgresContext>();

                context.Database.EnsureCreated();

                try
                {
                    PrepareDatabase(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding data: {ex.Message}");
                }
            }
        });
    }

    public PostgresContext getContext() => context;
    
    private void PrepareDatabase(PostgresContext context)
    {
        clearDatabase(context);
        seedData(context);
        context.SaveChanges();  
    }

    protected virtual void seedData(PostgresContext dbContext){}

    private static void clearDatabase(DbContext dbContext)
    {
        var sql = @"
        DO $$ DECLARE
            r RECORD;
        BEGIN
            -- Desactivar restricciones de llaves foráneas
            PERFORM 'SET session_replication_role = replica';
            
            -- Truncar todas las tablas
            FOR r IN (SELECT tablename FROM pg_tables WHERE schemaname = 'public') LOOP
                EXECUTE 'TRUNCATE TABLE ' || quote_ident(r.tablename) || ' CASCADE';
            END LOOP;
            
            -- Reiniciar todas las secuencias
            FOR r IN (SELECT sequence_name FROM information_schema.sequences WHERE sequence_schema = 'public') LOOP
                EXECUTE 'ALTER SEQUENCE ' || quote_ident(r.sequence_name) || ' RESTART WITH 1';
            END LOOP;

            -- Volver a activar restricciones de llaves foráneas
            PERFORM 'SET session_replication_role = DEFAULT';
        END $$;
    ";
    
        dbContext.Database.ExecuteSqlRaw(sql);
    }
}