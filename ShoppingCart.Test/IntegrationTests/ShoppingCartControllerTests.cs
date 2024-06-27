using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ShoppingCart.API;
using System.Text;


namespace ShoppingCart.Test.IntegrationTests
{
    [TestFixture]
    public class ShoppingCartControllerIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [TearDown]
        public void TearDown()
        {
            _factory?.Dispose();
            _client?.Dispose();
        }


        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<AppDbContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<AppDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetCartTotalAmount_ReturnsNotFound()
        {
            var userId = "testUser";

            var response = await _client.GetAsync($"/api/ShoppingCart/user/{userId}/totalamount");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetCartTotalAmount_ReturnsOkWithTotalAmount()
        {
            var userId = "testUser";

            var items = new List<string> { "coffee", "bread" };
            var content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
            await _client.PostAsync($"/api/ShoppingCart/AddItemsToCart/{userId}", content);

            var response = await _client.GetAsync($"/api/ShoppingCart/user/{userId}/totalamount");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseType>(stringResponse);

   
            Assert.That(result?.Result, Is.EqualTo(4)); 
        }

        [Test]
        public async Task AddItemsToCart_ReturnsBadRequest()
        {
            var userId = "testUser";
            var items = new List<string> { "invalidItem" };
            var content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/ShoppingCart/AddItemsToCart/{userId}", content);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task AddItemsToCart_ReturnsOk()
        {
            var userId = "testUser";
            var items = new List<string> { "coffee", "bread" };
            var content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/ShoppingCart/AddItemsToCart/{userId}", content);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseType>(stringResponse);

            Assert.That(result?.Result, Is.EqualTo(true));
        }

        [Test]
        public async Task AddItemsToCart_CreatesNewCartIfNotExists()
        {
            var userId = "testUser";
            var items = new List<string> { "coffee", "bread" };
            var content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/ShoppingCart/AddItemsToCart/{userId}", content);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseType>(stringResponse);

            Assert.That(result?.Result, Is.EqualTo(true));
        }


        [Test]
        public async Task GetCartTotalAmount_ReturnsUpdatedAmount()
        {
            var userId = "testUser";

            var items = new List<string> { "coffee", "bread" };
            var content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
            await _client.PostAsync($"/api/ShoppingCart/AddItemsToCart/{userId}", content);

            var response = await _client.GetAsync($"/api/ShoppingCart/user/{userId}/totalamount");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseType>(stringResponse);

            Assert.That(result?.Result, Is.EqualTo(4));

            await _client.PostAsync($"/api/ShoppingCart/AddItemsToCart/{userId}", content);

            response = await _client.GetAsync($"/api/ShoppingCart/user/{userId}/totalamount");
            response.EnsureSuccessStatusCode();
            stringResponse = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<ResponseType>(stringResponse);

            Assert.That(result?.Result, Is.EqualTo(8));
        }

    }

    public class ResponseType
    {
        public dynamic? Result { get; set; }
    }
}
