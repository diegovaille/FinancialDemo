using FinancialProjectDemo;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FinancialDemo.Tests.Integration.Web
{
    public class HomeControllerIndexShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HomeControllerIndexShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnViewWithCorrectMessage()
        {
            HttpResponseMessage response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("Financial.Web", stringResponse);
        }
    }
}
