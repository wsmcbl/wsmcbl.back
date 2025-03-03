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
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

        var boxRequest = new BoxDto(user);
        var response = await httpClient.PostAsJsonAsync(getPosteUrl(), boxRequest);
        if (!response.IsSuccessStatusCode)
        {
            throw new InternalException($"Error creating email: {response.StatusCode} - {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Poste.io api response: {responseString}");
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
        var value = Environment.GetEnvironmentVariable("POSTE_URL");
        if (value == null)
        {
            throw new InternalException("POSTE_URL environment not found.");
        }

        return value;
    }

    public async Task updateUserPassword(UserEntity user)
    {
        var authHeaderValue = Convert
            .ToBase64String(Encoding.UTF8.GetBytes($"{getPosteUsername()}:{getPostePassword()}"));

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

        var request = new { passwordPlaintext = user.password};
        var response = await httpClient.PatchAsJsonAsync($"{getPosteUrl()}/{user.email}", request);
        if (!response.IsSuccessStatusCode)
        {
            throw new InternalException($"Error update password: {response.StatusCode} - {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Poste.io api response: {responseString}");
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