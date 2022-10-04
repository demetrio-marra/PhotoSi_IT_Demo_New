using Microsoft.AspNetCore.Hosting;
using PhotoSi_IT_Demo_New.Common.Ordini;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Common.Utenti;
using PhotoSi_IT_Demo_New.Services.Ordini.Tests.AppFactories;
using System.Text.Json;
using static PhotoSi_IT_Demo_New.Common.Ordini.CreateOrdineDTO;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Tests
{
    public class IntegrationTest1
    {
        HttpClient ordiniClient;
        HttpClient prodottiClient;
        HttpClient utentiClient;
        HttpClient recapitiClient;

        readonly decimal prezzoProdotto = 15.50M;
        readonly int quantitaInizialeProdotto = 1;
        readonly decimal totaleOrdineIniziale;

        readonly int quantitaModificataProdotto = 3;
        readonly decimal totaleOrdineModificato;


        int ordineCreatoId;
        int prodottoOrdinatoId;
        
        public IntegrationTest1()
        {
            totaleOrdineIniziale = prezzoProdotto * quantitaInizialeProdotto;
            totaleOrdineModificato = quantitaModificataProdotto * prezzoProdotto;
        }

        [SetUp]
        public void Setup()
        {
            ordiniClient = new OrdiniAppFactory().WithWebHostBuilder(c => { }).CreateClient();
            prodottiClient = new ProdottiAppFactory().WithWebHostBuilder(c => { }).CreateClient();
            utentiClient = new UtentiAppFactory().WithWebHostBuilder(c => { }).CreateClient();
            recapitiClient = new RecapitiAppFactory().WithWebHostBuilder(c => { }).CreateClient();
        }


    }
}