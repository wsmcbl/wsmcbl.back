using System.Net.Http.Headers;
using System.Text;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.service;

public class NextcloudUserCreator
{
    public async Task<string> createUser(HttpClient httpClient, UserEntity user)
    {
        var authHeaderValue = Convert
            .ToBase64String(Encoding.UTF8.GetBytes($"{getNextcloudUsername()}:{getNextcloudPassword()}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        httpClient.DefaultRequestHeaders.Add("OCS-APIRequest", "true");

        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("userid", user.email),
            new KeyValuePair<string, string>("password", user.password)
        ]);

        var response = await httpClient.PostAsync(getNextcloudUrl(), content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al enviar la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
        }

        return await response.Content.ReadAsStringAsync();
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
}