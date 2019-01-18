using FinancialDemo.Core.DTOs;
using FinancialDemo.Core.Entities;
using FinancialProjectDemo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinancialDemo.Tests.Integration.Web
{
    public class AccountsControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AccountsControllerShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsTwoAccounts()
        {
            var response = await _client.GetAsync("/accounts");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AccountDTO>>(stringResponse).ToList();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.Count(a => a.Name == "Test Acc 1"));
            Assert.Equal(1, result.Count(a => a.Name == "Test Acc 2"));
        }
    }
}
