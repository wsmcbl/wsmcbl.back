using System.Net.Http.Headers;
using System.Text;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.service;

public class PosteUserCreator
{
    private readonly HttpClient httpClient;

    public PosteUserCreator(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task createUser(UserEntity user)
    {
        var authHeaderValue = Convert
            .ToBase64String(Encoding.UTF8.GetBytes($"{getPosteUsername()}:{getPostePassword()}"));

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        var boxRequest = new BoxDto(user);

        var response = await httpClient.PostAsJsonAsync(getPosteUrl(), boxRequest);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al crear el correo: {response.StatusCode} - {response.ReasonPhrase}");
        }

        await response.Content.ReadAsStringAsync();
    }

    private static string getPostePassword()
    {
        var value = Environment.GetEnvironmentVariable("NEXTCLOUD_PASSWORD");
        if (value == null)
        {
            throw new InternalException("NEXTCLOUD_PASSWORD environment not found.");
        }

        return value;
    }

    private static string getPosteUsername()
    {
        var value = Environment.GetEnvironmentVariable("NEXTCLOUD_USERNAME");
        if (value == null)
        {
            throw new InternalException("NEXTCLOUD_USERNAME environment not found.");
        }

        return value;
    }

    private static string getPosteUrl()
    {
        var value = Environment.GetEnvironmentVariable("NEXTCLOUD_URL");
        if (value == null)
        {
            throw new InternalException("NEXTCLOUD_URL environment not found.");
        }

        return value;
    }
}

internal class BoxDto
{
    public string? name { get; set; }
    public string? email { get; set; }
    public string? passwordPlaintext { get; set; }

    public BoxDto(UserEntity user)
    {
        name = user.fullName();
        email = user.email;
        passwordPlaintext = user.password;
    }
}