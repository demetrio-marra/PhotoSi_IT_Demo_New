using Microsoft.AspNetCore.Mvc.Testing;
using PhotoSi_IT_Demo_New.Common.Utenti;
using System.Text.Json;

namespace PhotoSi_IT_Demo_New.Services.Utenti.Tests
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
        public async Task Integration_CreateUser()
        {
            var client = httpClient;

            var chosenGuid = Guid.NewGuid().ToString();

            var createUserResponse = await client.PostAsync("rpc/v1/utenti",
                new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new UtenteModel
                {
                    Nominativo = "test1",
                    Username = chosenGuid

                }), System.Text.Encoding.UTF8, "application/json"));
            createUserResponse.EnsureSuccessStatusCode();
            var createdUserModel = System.Text.Json.JsonSerializer.Deserialize<UtenteModel>(
                await createUserResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            var response = await client.GetAsync("rpc/v1/utenti/" +
                createdUserModel!.Id.ToString());

            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            var model = System.Text.Json.JsonSerializer.Deserialize<UtenteModel>(resp,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })!;

            Assert.That(model.Username, Is.EqualTo(chosenGuid));
        }
    }
}