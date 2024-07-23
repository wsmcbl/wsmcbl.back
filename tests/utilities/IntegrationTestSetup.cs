using Docker.DotNet;
using Docker.DotNet.Models;

namespace wsmcbl.tests.utilities;

public class IntegrationTestSetup
{
    public static async Task SetupAsync()
    {
        // Crea un cliente de Docker
        var client = new DockerClientConfiguration()
            .CreateClient();

        // Levanta los contenedores definidos en el docker-compose.yml
        await client.Containers.StartContainerAsync("back", new ContainerStartParameters());
        await client.Containers.StartContainerAsync("db", new ContainerStartParameters());

        // Configura la conexión a la base de datos
        // Puedes usar variables de entorno o un archivo de configuración
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", "Server=localhost;Port=5432;Database=mydb;User Id=myuser;Password=mypassword;");
    }
}
