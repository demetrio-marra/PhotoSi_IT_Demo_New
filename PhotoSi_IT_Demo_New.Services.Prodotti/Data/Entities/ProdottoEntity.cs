namespace PhotoSi_IT_Demo_New.Services.Prodotti.Data.Entities
{
    public class ProdottoEntity
    {
        public int Id { get; set; }
        public string? Codice { get; set; }
        public string? Descrizione { get; set; }
        public string? Categoria { get; set; }
        public decimal Prezzo { get; set; }
    }
}
