using DesafioBenner.DTO;
using Infrastructure.Entities;
using Newtonsoft.Json;
using System.Net;
using Xunit;


namespace Tests.IntegrationTests.Controllers
{
    public class ParkingControllerTest
    {
        [Fact]
        public async Task GetAllParkingAsync_ReturnsOk()
        {
            var response = await HttpHandler.SendRequest("Parking", "", "GET");
            var responseContent = await response.Content.ReadAsStringAsync();
            var parkingResponse = JsonConvert.DeserializeObject<List<Parking>>(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(parkingResponse);
            Assert.IsType<List<Parking>>(parkingResponse);
        }

        [Fact]
        public async Task GetByIdParkingAsync_ReturnsOk()
        {
            var response = await HttpHandler.SendRequest("Parking/GetById?id=1", "", "GET");
            var responseContent = await response.Content.ReadAsStringAsync();
            var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(parkingResponse);
            Assert.IsType<Parking>(parkingResponse);
        }

        [Fact]
        public async Task PostParkingAsync_ReturnsOk()
        {
            Parking newParking = new Parking()
            {
                EntryDate = DateTime.Now,
                LicensePlate = "POST-3123"
            };
            var response = await HttpHandler.SendRequest("Client", newParking, "POST");
            var responseContent = await response.Content.ReadAsStringAsync();
            var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<Parking>(parkingResponse);
            Assert.NotNull(parkingResponse);
        }

        [Fact]
        public async Task PutParkingAsync_ReturnsOk()
        {

            ParkingDepartureDTO dtoParking = new ParkingDepartureDTO()
            {
                 LicensePlate = "GHI-8910",
                 DepartureDate = DateTime.Now.AddHours(3).AddMinutes(35),
            };

            var response = await HttpHandler.SendRequest("Parking", dtoParking, "PUT");
            var responseContent = await response.Content.ReadAsStringAsync();
            var parkingResponse = JsonConvert.DeserializeObject<Parking>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<Parking>(parkingResponse);
            Assert.NotNull(parkingResponse);
        }

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
}
