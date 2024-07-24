using Docker.DotNet;
using Docker.DotNet.Models;

namespace wsmcbl.tests.utilities;

public class IntegrationTestSetup
{
    public static async Task SetupAsync()
    {
        var client = new DockerClientConfiguration()
            .CreateClient();

        await client.Containers.StartContainerAsync("back", new ContainerStartParameters());
        await client.Containers.StartContainerAsync("db", new ContainerStartParameters());

        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", "Server=localhost;Port=5432;Database=mydb;User Id=myuser;Password=mypassword;");
    }
}
