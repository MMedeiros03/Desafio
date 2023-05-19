using DesafioBenner.DTO;
using Infrastructure.Entities;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Tests.IntegrationTests.Controllers;

public class ParkingControllerTest
{

    /// <summary>
    /// Testa o método GetAllParkingAsync para retornar 
    /// uma lista de todos os registros da tabela Parking.
    /// Verifica se a requisição retorna status Ok e
    /// valida se o valor retornado é do tipo Parking
    /// </summary>
    [Fact]
    public async Task GetAllParkingAsync_ReturnsOk()
    {
        var response = await HttpHandler.SendRequest("Parking", "", "GET");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<List<Parking>>(responseContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<List<Parking>>(parkingResponse);
    }

    /// <summary>
    /// Testa o método GetByIdParkingAsync para retornar um registro baseado
    /// no id informado. Verifica se a requisição retorna status Ok,
    /// valida se o retorno não é nulo e valida  se o valor retornado é do tipo Parking.
    /// </summary>
    [Fact]
    public async Task GetByIdParkingAsync_ReturnsOk()
    {
        var response = await HttpHandler.SendRequest("Parking?id=1", "", "GET");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(parkingResponse);
        Assert.IsType<Parking>(parkingResponse);
    }

    /// <summary>
    /// Testa o método PostParkingAsync para criar um novo registro.
    /// Verifica se a requisição retorna status Ok,
    /// valida se o retorno não é nulo e valida  se o valor retornado é do tipo Parking.
    /// </summary>
    [Fact]
    public async Task PostParkingAsync_ReturnsOk()
    {
        Parking newParking = new Parking()
        {
            EntryDate = DateTime.Now,
            LicensePlate = "POST-3123"
        };
        var response = await HttpHandler.SendRequest("Parking", newParking, "POST");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(parkingResponse);
        Assert.IsType<Parking>(parkingResponse);
    }

    /// <summary>
    /// Testa o método PutParkingAsync para atualizar um registro.
    /// Verifica se a requisição retorna status Ok,
    /// valida se o retorno não é nulo e valida se o valor retornado é do tipo Parking.
    /// </summary>
    [Fact]
    public async Task PutParkingAsync_ReturnsOk()
    {
        ParkingDepartureDTO dtoParking = new ParkingDepartureDTO()
        {
             LicensePlate = "GHI-8910",
             DepartureDate = DateTime.Now.AddHours(3)
        };

        var response = await HttpHandler.SendRequest("Parking", dtoParking, "PUT");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(parkingResponse);
        Assert.IsType<Parking>(parkingResponse);
    }

    /// <summary>
    /// Testa o método DeleteParkingAsync para excuir um registro.
    /// Verifica se a requisição retorna status Ok e
    /// valida se o valor retornado é do tipo Parking.
    /// </summary>
    [Fact]
    public async Task DeleteParkingAsync_ReturnsOk()
    {
        var response = await HttpHandler.SendRequest("Parking?id=4", "", "DELETE");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<Parking>(parkingResponse);
    }
}
