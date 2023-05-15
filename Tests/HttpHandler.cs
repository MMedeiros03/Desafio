using System.Text;
using System.Text.Json;

public static class HttpHandler
{
    private static readonly string baseUrl = "http://localhost:7283/api/";

    public static async Task<HttpResponseMessage> SendRequest<TRequest>(string endpoint, TRequest request, string methodType)
    {
        HttpClient client = new();
        var requestJson = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

        switch (methodType)
        {
            case "GET":
                return await client.GetAsync(baseUrl + endpoint);
                break;
            case "POST":
                return await client.PostAsync(baseUrl + endpoint, httpContent);
                break;
            case "PUT":
                return await client.PutAsync(baseUrl + endpoint, httpContent);
                break;
            case "DELETE":
                return await client.DeleteAsync(baseUrl + endpoint);
                break;
            default:
                throw new Exception("Método inválido");
        }
    }
}