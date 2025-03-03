using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.service;

public class NextcloudUserCreator
{
    private readonly HttpClient httpClient;

    public NextcloudUserCreator(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        initConfiguration();
    }

    public async Task createUser(UserEntity user)
    {
        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("userid", user.email),
            new KeyValuePair<string, string>("password", user.password),
            new KeyValuePair<string, string>("email", user.email),
            new KeyValuePair<string, string>("displayName", user.fullName()),
            new KeyValuePair<string, string>("quota", "1GB")
        ]);

        var response = await httpClient.PostAsync($"{getNextcloudUrl()}/users", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new InternalException("Error creating user to Nextcloud.");
        }
    }

    public async Task assignGroup(string email, string groupName)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(groupName))
        {
            return;
        }
        
        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("groupid", groupName)
        ]);

        var url = $"{getNextcloudUrl()}/users/{email}/groups";
        var response = await httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error assigning user to group.");
        }
    }

    public async Task<List<string>> getGroupList()
    {
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var url = $"{getNextcloudUrl()}/groups";
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new InternalException("Error getting list of groups.");
        }
        
        try
        {
            var json = await response.Content.ReadAsStringAsync();
            var parsedJson = JObject.Parse(json);
            var groupsArray = (JArray)parsedJson["ocs"]?["data"]?["groups"]!;
            return groupsArray.ToObject<List<string>>()!;
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<string> getGroupByUserMail(string mail)
    {
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var url = $"{getNextcloudUrl()}users/{mail}/groups";
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new InternalException("Error getting list of groups.");
        }
        
        try
        {
            var json = await response.Content.ReadAsStringAsync();
            var parsedJson = JObject.Parse(json);
            var groupsArray = (JArray)parsedJson["ocs"]?["data"]?["groups"]!;
            var list = groupsArray.ToObject<List<string>>();
            return list!.First();
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public async Task updateUserPassword(UserEntity user)
    {
        var content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("key", "password"),
            new KeyValuePair<string, string>("value", user.password)
        ]);

        var response = await httpClient.PutAsync($"{getNextcloudUrl()}/users/{user.email}", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new InternalException("Error creating user to Nextcloud.");
        }
    }

    private void initConfiguration()
    {
        var authHeaderValue = Convert
            .ToBase64String(Encoding.UTF8.GetBytes($"{getNextcloudUsername()}:{getNextcloudPassword()}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        httpClient.DefaultRequestHeaders.Add("OCS-APIRequest", "true");
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