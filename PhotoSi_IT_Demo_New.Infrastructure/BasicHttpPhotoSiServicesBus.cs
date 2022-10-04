using PhotoSi_IT_Demo_New.Common.Ordini;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Common.Utenti;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;
using System.Net.Http.Json;
using System.Text.Json;

namespace PhotoSi_IT_Demo_New.Infrastructure
{
    /// <summary>
    /// ServiceBus di base. I servizi sono chiamati direttamente attraverso
    /// la loro interfaccia HTTP. Non ci sono code come in un vero broker.
    /// </summary>
    public class BasicHttpPhotoSiServicesBus: IPhotoSiServicesBus
    {
        readonly HttpClient httpClient;

        // produzione
        static readonly string OrdiniServiceBaseUrl =           "http://ordini/rpc/v1/ordini/";
        static readonly string UtentiServiceBaseUrl =           "http://utenti/rpc/v1/utenti/";
        static readonly string ProdottiServiceBaseUrl =         "http://prodotti/rpc/v1/prodotti/";
        static readonly string ProdottiOrdinatiServiceBaseUrl = "http://prodotti/rpc/v1/prodottiOrdinati/";
        static readonly string RecapitiServiceBaseUrl =         "http://recapiti/rpc/v1/recapiti/";

        //// Se devi eseguire in debug direttamente da visual studio
        //static readonly string OrdiniServiceBaseUrl = "http://localhost:5001/rpc/v1/ordini/";
        //static readonly string UtentiServiceBaseUrl = "http://localhost:5002/rpc/v1/utenti/";
        //static readonly string ProdottiServiceBaseUrl = "http://localhost:5003/rpc/v1/prodotti/";
        //static readonly string ProdottiOrdinatiServiceBaseUrl = "http://localhost:5003/rpc/v1/prodottiOrdinati/";
        //static readonly string RecapitiServiceBaseUrl = "http://localhost:5004/rpc/v1/recapiti/";

        public BasicHttpPhotoSiServicesBus(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        #region ordini
        async Task<IEnumerable<OrdineDTO>> IPhotoSiServicesBus.GetOrdini()
        {
            return await GetAndDeserialize<IEnumerable<OrdineDTO>>(
                $"{OrdiniServiceBaseUrl}") ?? Enumerable.Empty<OrdineDTO>();
        }

        async Task<OrdineDTO?> IPhotoSiServicesBus.GetOrdine(int id)
        {
            return await GetAndDeserialize<OrdineDTO>(
                $"{OrdiniServiceBaseUrl}{id}");
        }

        async Task<OrdineDTO?> IPhotoSiServicesBus.AddOrdine(CreateOrdineDTO command)
        {
            return await PostAndDeserialize<OrdineDTO>(
                $"{OrdiniServiceBaseUrl}", command);
        }

        async Task<OrdineDTO?> IPhotoSiServicesBus.UpdateOrdine(int id, UpdateOrdineDTO command)
        {
            return await PutAndDeserialize<OrdineDTO>(
                $"{OrdiniServiceBaseUrl}{id}", command);
        }

        async Task IPhotoSiServicesBus.DeleteOrdine(int id)
        {
            await JustDelete($"{OrdiniServiceBaseUrl}{id}");
        }

        #endregion

        #region prodotti

        async Task<IEnumerable<ProdottoModel>> IPhotoSiServicesBus.GetAllProdotti()
        {
            return await GetAndDeserialize<IEnumerable<ProdottoModel>>(
                $"{ProdottiServiceBaseUrl}") ?? Enumerable.Empty<ProdottoModel>();
        }

        async Task<IEnumerable<ProdottoModel>> IPhotoSiServicesBus.GetProdotti(IEnumerable<int> ids)
        {
            return await PostAndDeserialize<IEnumerable<ProdottoModel>>(
                $"{ProdottiServiceBaseUrl}multiple", ids) ?? Enumerable.Empty<ProdottoModel>();
        }

        async Task<ProdottoModel?> IPhotoSiServicesBus.GetProdotto(int id)
        {
            return await GetAndDeserialize<ProdottoModel>(
               $"{ProdottiServiceBaseUrl}{id}");
        }

        async Task<ProdottoModel?> IPhotoSiServicesBus.AddProdotto(ProdottoModel model)
        {
            return await PostAndDeserialize<ProdottoModel>(
                $"{ProdottiServiceBaseUrl}", model);
        }

        async Task<ProdottoModel?> IPhotoSiServicesBus.UpdateProdotto(int id, ProdottoModel model)
        {
            return await PutAndDeserialize<ProdottoModel>(
              $"{ProdottiServiceBaseUrl}{id}", model);
        }

        async Task IPhotoSiServicesBus.DeleteProdotto(int id)
        {
            await JustDelete($"{ProdottiServiceBaseUrl}{id}");
        }

        #endregion

        #region prodotti ordinati

        async Task<IEnumerable<ProdottoOrdinatoModel>> IPhotoSiServicesBus.GetProdottiOrdinati()
        {
            return await GetAndDeserialize<IEnumerable<ProdottoOrdinatoModel>>(
              $"{ProdottiOrdinatiServiceBaseUrl}") ?? Enumerable.Empty<ProdottoOrdinatoModel>();
        }

        async Task<IEnumerable<ProdottoOrdinatoModel>> 
            IPhotoSiServicesBus.GetProdottiOrdinatiByOrdine(int ordineId)
        {
            return await GetAndDeserialize<IEnumerable<ProdottoOrdinatoModel>>(
                $"{ProdottiOrdinatiServiceBaseUrl}ordine/{ordineId}")
                ?? Enumerable.Empty<ProdottoOrdinatoModel>(); 
        }

        async Task<ProdottoOrdinatoModel?> IPhotoSiServicesBus.GetProdottoOrdinato(int id)
        {
            return await GetAndDeserialize<ProdottoOrdinatoModel>(
                $"{ProdottiOrdinatiServiceBaseUrl}{id}");
        }

        async Task<ProdottoOrdinatoModel?> 
            IPhotoSiServicesBus.AddProdottoOrdinato(ProdottoOrdinatoModel prodottoModel)
        {
            return await PostAndDeserialize<ProdottoOrdinatoModel>(
                $"{ProdottiOrdinatiServiceBaseUrl}", prodottoModel);
        }

        async Task<IEnumerable<ProdottoOrdinatoModel>> IPhotoSiServicesBus.AddProdottiOrdinati(
            IEnumerable<ProdottoOrdinatoModel> list)
        {
            return await PostAndDeserialize<IEnumerable<ProdottoOrdinatoModel>>(
                $"{ProdottiOrdinatiServiceBaseUrl}multiple", list)
                ?? Enumerable.Empty<ProdottoOrdinatoModel>();
        }

        async Task<IEnumerable<ProdottoOrdinatoModel>> IPhotoSiServicesBus.UpdateProdottiOrdinati(
            IEnumerable<ProdottoOrdinatoModel> list)
        {
            return await PutAndDeserialize<IEnumerable<ProdottoOrdinatoModel>>(
              $"{ProdottiOrdinatiServiceBaseUrl}multiple", list)
              ?? Enumerable.Empty<ProdottoOrdinatoModel>();
        }

        async Task<ProdottoOrdinatoModel?> IPhotoSiServicesBus.UpdateProdottoOrdinato(int id,
            ProdottoOrdinatoModel prodottoModel)
        {
            return await PutAndDeserialize<ProdottoOrdinatoModel>(
               $"{ProdottiOrdinatiServiceBaseUrl}{id}", prodottoModel);
        }

        async Task IPhotoSiServicesBus.DeleteProdottoOrdinato(int id)
        {
            await JustDelete($"{ProdottiOrdinatiServiceBaseUrl}{id}");
        }

        async Task IPhotoSiServicesBus.DeleteProdottiOrdinatiByOrdine(int ordineId)
        {
            await JustDelete($"{ProdottiOrdinatiServiceBaseUrl}ordine/{ordineId}");
        }

        async Task IPhotoSiServicesBus.DeleteProdottiOrdinati(IEnumerable<ProdottoOrdinatoModel> list)
        {
            await PostAndDeserialize<IEnumerable<ProdottoOrdinatoModel>>(
               $"{ProdottiOrdinatiServiceBaseUrl}multiple-del", list);
        }

        #endregion

        #region recapiti

        async Task<IEnumerable<RecapitoModel>> IPhotoSiServicesBus.GetRecapiti()
        {
            return await GetAndDeserialize<IEnumerable<RecapitoModel>>(
                $"{RecapitiServiceBaseUrl}") ?? Enumerable.Empty<RecapitoModel>();
        }

        async Task<RecapitoModel?> IPhotoSiServicesBus.GetRecapito(int id)
        {
            return await GetAndDeserialize<RecapitoModel>(
                $"{RecapitiServiceBaseUrl}{id}");
        }

        async Task<RecapitoModel?> IPhotoSiServicesBus.GetRecapitoByOrdine(int ordineId)
        {
            return await GetAndDeserialize<RecapitoModel>(
                $"{RecapitiServiceBaseUrl}ordine/{ordineId}");
        }

        async Task<RecapitoModel?> IPhotoSiServicesBus.AddRecapito(RecapitoModel model)
        {
            return await PostAndDeserialize<RecapitoModel>(
                $"{RecapitiServiceBaseUrl}", model);
        }

        async Task<RecapitoModel?> IPhotoSiServicesBus.UpdateRecapito(int id, RecapitoModel model)
        {
            return await PutAndDeserialize<RecapitoModel>(
               $"{RecapitiServiceBaseUrl}{id}", model);
        }

        async Task<RecapitoModel?> IPhotoSiServicesBus.UpdateRecapitoByOrdine(int ordineId, RecapitoModel model)
        {
            return await PutAndDeserialize<RecapitoModel>(
               $"{RecapitiServiceBaseUrl}ordine/{ordineId}", model);
        }

        async Task IPhotoSiServicesBus.DeleteRecapito(int id)
        {
            await JustDelete($"{RecapitiServiceBaseUrl}{id}");
        }       

        async Task IPhotoSiServicesBus.DeleteRecapitoByOrdine(int ordineId)
        {
            await JustDelete($"{RecapitiServiceBaseUrl}ordine/{ordineId}");
        }

        #endregion

        #region utenti

        async Task<IEnumerable<UtenteModel>> IPhotoSiServicesBus.GetUtenti()
        {
            return await GetAndDeserialize<IEnumerable<UtenteModel>>(
                $"{UtentiServiceBaseUrl}") ?? Enumerable.Empty<UtenteModel>();
        }

        async Task<UtenteModel?> IPhotoSiServicesBus.GetUtente(int id)
        {
            return await GetAndDeserialize<UtenteModel>($"{UtentiServiceBaseUrl}{id}");
        }

        async Task<UtenteModel?> IPhotoSiServicesBus.AddUtente(UtenteModel model)
        {
            return await PostAndDeserialize<UtenteModel>($"{UtentiServiceBaseUrl}", model);
        }

        async Task<UtenteModel?> IPhotoSiServicesBus.UpdateUtente(int id, UtenteModel model)
        {
            return await PutAndDeserialize<UtenteModel>($"{UtentiServiceBaseUrl}{id}", model);
        }

        async Task IPhotoSiServicesBus.DeleteUtente(int id)
        {
            await JustDelete($"{UtentiServiceBaseUrl}{id}");
        }

        #endregion

        #region helpers

        async Task<T?> GetAndDeserialize<T>(string url)
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return DeserializeOrNull<T>(responseContent);
        }

        async Task<T?> PostAndDeserialize<T>(string url, object payload)
        {
            var response = await httpClient.PostAsJsonAsync(url, payload);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return DeserializeOrNull<T>(responseContent);
        }

        async Task<T?> PutAndDeserialize<T>(string url, object payload)
        {
            var response = await httpClient.PutAsJsonAsync(url, payload);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return DeserializeOrNull<T>(responseContent);
        }

        T? DeserializeOrNull<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);
         
                var x =  JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return x;
                
        }

        async Task JustDelete(string url)
        {
            var response = await httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
