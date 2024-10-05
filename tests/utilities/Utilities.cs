using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace wsmcbl.tests.utilities;

public static class Utilities
{
    public static async Task EnsureSuccess(this HttpResponseMessage response)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseContent);

            var message = problemDetails == null ? $"\n\tError message: {e.Message} \n\tTrace: {e.StackTrace}" :
                $"\n\n\tTitle: {problemDetails.Title}\n\tDetail: {problemDetails.Detail}\n\tStatus: {problemDetails.Status}.\n";
            throw new ArgumentException(message);
        }
    }
    
    public static T? deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);
    
    public static async Task add<T, ID>(this DbContext context, T entity, ID id) where T : class
    {
        var element = await context.Set<T>().FindAsync(id);
        
        if(element != null)
            return;
            
        context.Add(entity);
        await context.SaveChangesAsync();
    }
}