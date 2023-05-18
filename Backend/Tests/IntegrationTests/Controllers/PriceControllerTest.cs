using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.IntegrationTests.Controllers
{
    public class PriceControllerTest
    {
        [Fact]
        public async Task GetAllClientAsync_ReturnsOk()
        {
            var response = await HttpHandler.SendRequest("Client", "", "GET");
            var responseContent = await response.Content.ReadAsStringAsync();
            var roleResponse = JsonConvert.DeserializeObject<List<Client>>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(roleResponse);
        }

        [Fact]
        public async Task GetAllClientAsync_ReturnsOk()
        {
            var response = await HttpHandler.SendRequest("Client", "", "GET");
            var responseContent = await response.Content.ReadAsStringAsync();
            var roleResponse = JsonConvert.DeserializeObject<List<Client>>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(roleResponse);
        }

        [Fact]
        public async Task GetAllClientAsync_ReturnsOk()
        {
            var response = await HttpHandler.SendRequest("Client", "", "GET");
            var responseContent = await response.Content.ReadAsStringAsync();
            var roleResponse = JsonConvert.DeserializeObject<List<Client>>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(roleResponse);
        }

        [Fact]
        public async Task GetAllClientAsync_ReturnsOk()
        {
            var response = await HttpHandler.SendRequest("Client", "", "GET");
            var responseContent = await response.Content.ReadAsStringAsync();
            var roleResponse = JsonConvert.DeserializeObject<List<Client>>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(roleResponse);
        }
    }
}
