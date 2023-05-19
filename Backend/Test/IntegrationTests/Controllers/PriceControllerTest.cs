using Infrastructure.Entities;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Tests.IntegrationTests.Controllers;

public class PriceControllerTest
{
    /// <summary>
    /// Testa o método GetAllPriceAsync para retornar 
    /// uma lista de todos os registros da tabela Price.
    /// Verifica se a requisição retorna status Ok e
    /// valida se o valor retornado é do tipo Price
    /// </summary>
    [Fact]
    public async Task GetAllPriceAsync_ReturnsOk()
    {
        var response = await HttpHandler.SendRequest("Price", "", "GET");
        var responseContent = await response.Content.ReadAsStringAsync();
        var priceResponse = JsonConvert.DeserializeObject<List<Price>>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(priceResponse);
        Assert.IsType<List<Price>>(priceResponse);
    }

    /// <summary>
    /// Testa o método GetByIdPriceAsync para retornar um registro baseado
    /// no id informado. Verifica se a requisição retorna status Ok,
    /// valida se o retorno não é nulo e valida  se o valor retornado é do tipo Price.
    /// </summary>
    [Fact]
    public async Task GetByIdPriceAsync_ReturnsOk()
    {
        var response = await HttpHandler.SendRequest("Price?id=1", "", "GET");
        var responseContent = await response.Content.ReadAsStringAsync();
        var priceResponse = JsonConvert.DeserializeObject<Price>(responseContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(priceResponse);
        Assert.IsType<Price>(priceResponse);
    }

    /// <summary>
    /// Testa o método PostPriceAsync para criar um novo registro.
    /// Verifica se a requisição retorna status Ok,
    /// valida se o retorno não é nulo e valida  se o valor retornado é do tipo Price.
    /// </summary>
    [Fact]
    public async Task PostPriceAsync_ReturnsOk()
    {
        Price newPrice = new Price()
        {
            InitialDate = DateTime.Now,
            FinalDate = DateTime.Now.AddYears(1),
            InitialTime = 60,
            InitialTimeValue = 6,
            AdditionalHourlyValue = 2,
        };
        var response = await HttpHandler.SendRequest("Price", newPrice, "POST");
        var responseContent = await response.Content.ReadAsStringAsync();
        var priceResponse = JsonConvert.DeserializeObject<Price>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<Price>(priceResponse);
        Assert.NotNull(priceResponse);
    }

    /// <summary>
    /// Testa o método PutPriceAsync para atualizar um registro.
    /// Verifica se a requisição retorna status Ok,
    /// valida se o retorno não é nulo e valida se o valor retornado é do tipo Price.
    /// </summary>
    [Fact]
    public async Task PutPriceAsync_ReturnsOk()
    {
        Price  price = new Price()
        {
            Id = 2,
            InitialDate = DateTime.Now,
            FinalDate = DateTime.Now.AddYears(1),
            InitialTime = 60,
            InitialTimeValue = 7,
            AdditionalHourlyValue = 3
        };

        var response = await HttpHandler.SendRequest("Price", price, "PUT");
        var responseContent = await response.Content.ReadAsStringAsync();
        var priceResponse = JsonConvert.DeserializeObject<Price>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<Price>(priceResponse);
        Assert.NotNull(priceResponse);
    }

    /// <summary>
    /// Testa o método DeletePriceAsync para excuir um registro.
    /// Verifica se a requisição retorna status Ok e
    /// valida se o valor retornado é do tipo Price.
    /// </summary>
    [Fact]
    public async Task DeletePriceAsync_ReturnsOk()
    {
        var response = await HttpHandler.SendRequest("Price?id=3", "", "DELETE");
        var responseContent = await response.Content.ReadAsStringAsync();
        var priceResponse = JsonConvert.DeserializeObject<Price>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<Price>(priceResponse);
    }
}