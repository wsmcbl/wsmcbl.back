using System.Net.Http.Headers;
using System.Text;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.service;

public class NextcloudUserCreator
{
    private readonly HttpClient httpClient;

    public NextcloudUserCreator(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task createUser(UserEntity user)
    {
        var authHeaderValue = Convert
            .ToBase64String(Encoding.UTF8.GetBytes($"{getNextcloudUsername()}:{getNextcloudPassword()}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        httpClient.DefaultRequestHeaders.Add("OCS-APIRequest", "true");

        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("userid", user.email),
            new KeyValuePair<string, string>("password", user.password),
            new KeyValuePair<string, string>("email", user.email),
            new KeyValuePair<string, string>("displayName", user.fullName()),
            new KeyValuePair<string, string>("quota", "1GB")
        ]);

        var response = await httpClient.PostAsync(getNextcloudUrl(), content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al enviar la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
        }

        await response.Content.ReadAsStringAsync();
    }

    public async Task assignGroup(string email, string groupName)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(groupName))
        {
            return;
        }
        
        var authHeaderValue = Convert
            .ToBase64String(Encoding.UTF8.GetBytes($"{getNextcloudUsername()}:{getNextcloudPassword()}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        httpClient.DefaultRequestHeaders.Add("OCS-APIRequest", "true");

        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("groupid", groupName)
        ]);

        var url = $"{getNextcloudUrl()}/{email}/groups";
        var response = await httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al enviar la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
        }

        await response.Content.ReadAsStringAsync();
    }

    private static string getNextcloudPassword()
    {
        var value = Environment.GetEnvironmentVariable("NEXTCLOUD_PASSWORD");
        if (value == null)
        {
            throw new InternalException("NEXTCLOUD_PASSWORD environment not found.");
        }

        return value;
    }

    private static string getNextcloudUsername()
    {
        var value = Environment.GetEnvironmentVariable("NEXTCLOUD_USERNAME");
        if (value == null)
        {
            throw new InternalException("NEXTCLOUD_USERNAME environment not found.");
        }

        return value;
    }

    private static string getNextcloudUrl()
    {
        var value = Environment.GetEnvironmentVariable("NEXTCLOUD_URL");
        if (value == null)
        {
            throw new InternalException("NEXTCLOUD_URL environment not found.");
        }

        return value;
    }

    public async Task<List<string>> getGroupList()
    {
        var authHeaderValue = Convert
            .ToBase64String(Encoding.UTF8.GetBytes($"{getNextcloudUsername()}:{getNextcloudPassword()}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        httpClient.DefaultRequestHeaders.Add("OCS-APIRequest", "true");

        var url = $"{getNextcloudUrl()}/groups";
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al enviar la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
        }

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Resultado: {result}");
        
        return [];
    }
}