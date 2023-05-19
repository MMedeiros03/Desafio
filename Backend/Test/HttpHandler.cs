using System.Text;
using System.Text.Json;

namespace Tests;

public static class HttpHandler
{
    private static readonly string baseUrl = "https://localhost:7222/api/";

    /// <summary>
    /// Envia uma requisição HTTP para o endpoint especificado com o corpo da requisição serializado como JSON.
    /// </summary>
    /// <typeparam name="TRequest">O tipo do corpo da requisição.</typeparam>
    /// <param name="endpoint">O endpoint da API.</param>
    /// <param name="request">O corpo da requisição.</param>
    /// <param name="methodType">O tipo de método HTTP (GET, POST, PUT, DELETE).</param>
    /// <returns>Uma instância de HttpResponseMessage que representa a resposta HTTP.</returns>
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