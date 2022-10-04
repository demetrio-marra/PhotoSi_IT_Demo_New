using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Common.Utenti;

namespace PhotoSi_IT_Demo_New.Common.Ordini
{
    public class OrdineDTO
    {
        public int Id { get; set; }

        public IEnumerable<ProdottoOrdinatoModel> Prodotti { get; set; } = Enumerable.Empty<ProdottoOrdinatoModel>();

        public UtenteModel? Acquirente { get; set; }

        public DateTime Data { get; set; }

        public RecapitoModel? RecapitoConsegna { get; set; }

        public decimal? Totale { get => Prodotti.Sum(p => p.Quantita * p.Prezzo); }
    }
}
