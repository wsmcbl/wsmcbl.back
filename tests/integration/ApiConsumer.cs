using System.Net.Http.Json;
using Newtonsoft.Json;
using wsmcbl.src.dto.output;

namespace wsmcbl.tests.integration;

public class ApiConsumer
{
    private HttpClient client;
    private readonly string _connectionString;

    public ApiConsumer(HttpClient client)
    {
        this.client = client;
        _connectionString = "https://api.example.com";
    }
    
    private string BuildUri(string resource)
    {
        return $"{_connectionString}/{resource.TrimStart('/')}";
    }
    
    
    private string BuildUri(Resources d, string resource)
    {
        var da = d == Resources.Secretary ? "Secreatay" : "";
        
        return $"{_connectionString}/{da}/{resource.TrimStart('/')}";
    }

    public async Task<T> GetAsync<T>(string resource)
    {
        var url = BuildUri(resource);
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
    
    public async Task<List<StudentDto>> GetStudents(string resource)
    {
        var url = BuildUri(resource);
        var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<StudentDto>>();
        }
        
        throw new ArgumentException($"Error al obtener los datos de la API.\nError: {response.ReasonPhrase}");
    }


    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string resource, TRequest data)
    {
        var url = BuildUri(resource);
        var response = await client.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PutAsync<T>(string resource, T data)
    {
        var url = BuildUri(resource);
        var response = await client.PutAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
    }
    
    private static StringContent getContent(string resource)
    {
        return new StringContent(resource, System.Text.Encoding.UTF8, "application/json");
    }

    protected static StringContent getContentByDto(object? entity)
    {
        return getContent(JsonConvert.SerializeObject(entity));
    }

    protected static T? deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);
}