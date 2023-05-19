using DesafioBenner.DTO;
using Infrastructure.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
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
    /// Testa o método GetByIdParkingAsync para retornar um erro caso o
    /// id informado não exista na tabela. Verifica se a requisição retorna status NotFound,
    /// valida se o retorno não é nulo.
    /// </summary>
    [Fact]
    public async Task GetByIdParkingAsync_ReturnsNotFound()
    {
        var response = await HttpHandler.SendRequest("Parking?id=12312", "", "GET");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.NotNull(parkingResponse);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    
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
            EntryDate = DateTime.Now.AddHours(5),
            LicensePlate = "POST-3125"
        };
        var response = await HttpHandler.SendRequest("Parking", newParking, "POST");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(parkingResponse);
        Assert.IsType<Parking>(parkingResponse);
    }

    /// <summary>
    /// Testa o método PostParkingAsync para criar um novo registro com uma placa 
    /// ja existente no banco.
    /// Verifica se a requisição retorna status BadRequest e valida se o retorno não é nulo 
    /// </summary>
    [Fact]
    public async Task PostParkingAsync_DuplicateLicensePlateBadRequest()
    {
        Parking newParking = new Parking()
        {
            EntryDate = DateTime.Now,
            LicensePlate = "ABC-1234"
        };
        var response = await HttpHandler.SendRequest("Parking", newParking, "POST");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.NotNull(parkingResponse);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    /// <summary>
    /// Testa o método PostParkingAsync para criar um novo registro com uma placa 
    /// ja existente no banco.
    /// Verifica se a requisição retorna status BadRequest e valida se o retorno não é nulo 
    /// </summary>
    [Fact]
    public async Task PostParkingAsync_CurrentPriceNotFound()
    {
        Parking newParking = new Parking()
        {
            EntryDate = new DateTime(2003,8,5),
            LicensePlate = "ABC-1234"
        };
        var response = await HttpHandler.SendRequest("Parking", newParking, "POST");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.NotNull(parkingResponse);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
             LicensePlate = "ABC-1234",
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
    /// Testa o método PutParkingAsync para tentar atualizar um registro 
    /// com uma placa que não existe no banco.
    /// Verifica se a requisição retorna status NotFound e
    /// valida se o retorno não é nulo.
    /// </summary>
    [Fact]
    public async Task PutParkingAsync_LicensePlateNotFound()
    {
        ParkingDepartureDTO dtoParking = new ParkingDepartureDTO
        {
            LicensePlate = "TEST-PUT",
            DepartureDate = DateTime.Now.AddHours(3)
        };

        var response = await HttpHandler.SendRequest("Parking", dtoParking, "PUT");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.NotNull(parkingResponse);
    }

    /// <summary>
    /// Testa o método PutParkingAsync para tentar atualizar um registro
    /// com uma data de saida fora do periodo das datas vigentes no banco
    /// Verifica se a requisição retorna status NotFound e 
    /// valida se o retorno não é nulo.
    /// </summary>
    [Fact]
    public async Task PutParkingAsync_CurrentPriceNotFound()
    {
        ParkingDepartureDTO dtoParking = new ParkingDepartureDTO
        {
            LicensePlate = "TEST-PUT",
            DepartureDate = new DateTime(2008, 6, 1)
        };

        var response = await HttpHandler.SendRequest("Parking", dtoParking, "PUT");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.NotNull(parkingResponse);
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

        Assert.NotNull(parkingResponse);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Testa o método DeleteParkingAsync para excuir um registro que 
    /// não existe na tabela Parking.
    /// Verifica se a requisição retorna status NotFound e 
    /// valida se o retorno não é nulo.
    /// </summary>
    [Fact]
    public async Task DeleteParkingAsync_ReturnsNotFound()
    {
        var response = await HttpHandler.SendRequest("Parking?id=2423", "", "DELETE");
        var responseContent = await response.Content.ReadAsStringAsync();
        var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.NotNull(parkingResponse);
    }
}
