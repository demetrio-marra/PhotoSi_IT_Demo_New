using System.ComponentModel.DataAnnotations;

namespace PhotoSi_IT_Demo_New.Common.Prodotti
{
    public class ProdottoModel
    {
        public int Id { get; set; }

        [Required]
        public string? Codice { get; set; }
        [Required]
        public string? Descrizione { get; set; }
        [Required]
        public string? Categoria { get; set; }
        [Required]
        public decimal? Prezzo { get; set; }
    }
}
