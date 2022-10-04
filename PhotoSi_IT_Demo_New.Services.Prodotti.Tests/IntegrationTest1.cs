using Microsoft.AspNetCore.Mvc.Testing;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using System.Text.Json;

namespace PhotoSi_IT_Demo_New.Services.Prodotti.Tests
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
        public async Task Integration_CreateProdotto()
        {
            var client = httpClient;

            var chosenGuid = Guid.NewGuid().ToString();

            var createResponse = await client.PostAsync("rpc/v1/prodotti",
                new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new ProdottoModel
                {
                    Categoria = "foto",
                    Codice = chosenGuid,
                    Descrizione = "Descrizione test",
                    Prezzo = 10
                }), System.Text.Encoding.UTF8, "application/json"));
            createResponse.EnsureSuccessStatusCode();
            var createdModel = System.Text.Json.JsonSerializer.Deserialize<ProdottoModel>(
                await createResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            var response = await client.GetAsync("rpc/v1/prodotti/" +
                createdModel!.Id.ToString());

            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            var model = System.Text.Json.JsonSerializer.Deserialize<ProdottoModel>(resp,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })!;

            Assert.That(model.Codice, Is.EqualTo(chosenGuid));
        }
    }
}
