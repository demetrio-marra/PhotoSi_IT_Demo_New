using Microsoft.AspNetCore.Mvc.Testing;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using System.Text.Json;

namespace PhotoSi_IT_Demo_New.Services.Recapiti.Tests
{
    public class IntegrationTest1
    {
        readonly HttpClient httpClient;

        public IntegrationTest1()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {

                });

            httpClient = application.CreateClient();
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task Integration_CreateRecapito()
        {
            var client = httpClient;

            var chosenGuid = Guid.NewGuid().ToString();

            var createResponse = await client.PostAsync("rpc/v1/recapiti",
                new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new RecapitoModel
                {
                    Cap = "98100",
                    Citta = "Messina",
                    Indirizzo = chosenGuid,
                    OrdineId = 1
                }), System.Text.Encoding.UTF8, "application/json"));
            createResponse.EnsureSuccessStatusCode();
            var createdModel = System.Text.Json.JsonSerializer.Deserialize<RecapitoModel>(
                await createResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            var response = await client.GetAsync("rpc/v1/recapiti/" +
                createdModel!.Id.ToString());

            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            var model = System.Text.Json.JsonSerializer.Deserialize<RecapitoModel>(resp,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })!;

            Assert.That(model.Indirizzo, Is.EqualTo(chosenGuid));
        }
    }
}