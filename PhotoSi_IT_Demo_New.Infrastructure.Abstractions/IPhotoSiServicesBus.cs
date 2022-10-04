using PhotoSi_IT_Demo_New.Common.Ordini;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Common.Utenti;

namespace PhotoSi_IT_Demo_New.Infrastructure.Abstractions
{
    /// <summary>
    /// Bus di interconnessione tra servizi e tra questi e l'ApiGateway
    /// Un'interfaccia così non è molto comoda in quanto all'aumentare
    /// dei servizi il codice aumenta sempre di più.
    /// 
    /// La soluzione potrebbe essere quella di creare dei progetti separati
    /// con le funzionalità di un singolo servizio, da referenziare nei
    /// progetti che ne devono fare uso.
    /// 
    /// Un'altra soluzione, è quella di fare un solo servicesBus che usi la reflection per
    /// capire il destinatario.
    /// 
    /// Entrambe le soluzioni richiedono la modifica alle classi client
    /// </summary>
    public interface IPhotoSiServicesBus
    {
        #region ordini
        Task<IEnumerable<OrdineDTO>> GetOrdini();
        Task<OrdineDTO?> GetOrdine(int id);
        Task<OrdineDTO?> AddOrdine(CreateOrdineDTO command);
        Task<OrdineDTO?> UpdateOrdine(int id, UpdateOrdineDTO command);
        Task DeleteOrdine(int id);
        #endregion

        #region prodotti
        Task<IEnumerable<ProdottoModel>> GetAllProdotti();
        Task<IEnumerable<ProdottoModel>> GetProdotti(IEnumerable<int> ids);
        Task<ProdottoModel?> GetProdotto(int id);
        Task<ProdottoModel?> AddProdotto(ProdottoModel model);
        Task<ProdottoModel?> UpdateProdotto(int id, ProdottoModel model);
        Task DeleteProdotto(int id);
        #endregion

        #region prodotti ordinati

        /// <summary>
        /// Recupera i prodotti di tutti gli ordini effettuati (pesante senza paginazione)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProdottoOrdinatoModel>> GetProdottiOrdinati();
        Task<IEnumerable<ProdottoOrdinatoModel>> GetProdottiOrdinatiByOrdine(int ordineId);
        Task<ProdottoOrdinatoModel?> GetProdottoOrdinato(int id);
        Task<ProdottoOrdinatoModel?> AddProdottoOrdinato(ProdottoOrdinatoModel prodottoModel);
        Task<IEnumerable<ProdottoOrdinatoModel>> AddProdottiOrdinati(IEnumerable<ProdottoOrdinatoModel> list);
        Task<ProdottoOrdinatoModel?> UpdateProdottoOrdinato(int id, ProdottoOrdinatoModel prodottoModel);
        Task<IEnumerable<ProdottoOrdinatoModel>> UpdateProdottiOrdinati(IEnumerable<ProdottoOrdinatoModel> list);
        Task DeleteProdottoOrdinato(int id);
        Task DeleteProdottiOrdinati(IEnumerable<ProdottoOrdinatoModel> list);

        /// <summary>
        /// Cancella tutti i prodotti per l'ordine specificato
        /// </summary>
        /// <param name="ordineId"></param>
        /// <returns></returns>
        Task DeleteProdottiOrdinatiByOrdine(int ordineId);
        #endregion

        #region recapiti
        Task<IEnumerable<RecapitoModel>> GetRecapiti();
        Task<RecapitoModel?> GetRecapito(int id);
        Task<RecapitoModel?> GetRecapitoByOrdine(int ordineId);
        Task<RecapitoModel?> AddRecapito(RecapitoModel model);
        Task<RecapitoModel?> UpdateRecapito(int id, RecapitoModel model);
        Task<RecapitoModel?> UpdateRecapitoByOrdine(int ordineId, RecapitoModel model);
        Task DeleteRecapito(int id);
        Task DeleteRecapitoByOrdine(int ordineId);
        #endregion

        #region utenti
        Task<IEnumerable<UtenteModel>> GetUtenti();
        Task<UtenteModel?> GetUtente(int id);
        Task<UtenteModel?> AddUtente(UtenteModel model);
        Task<UtenteModel?> UpdateUtente(int id, UtenteModel model);
        Task DeleteUtente(int id);

        #endregion
    }
}
