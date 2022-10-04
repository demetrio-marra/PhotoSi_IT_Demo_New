using System.ComponentModel.DataAnnotations;

namespace PhotoSi_IT_Demo_New.Common.Prodotti
{
    /// <summary>
    /// Prodotto ordinato.
    /// Si duplicano tutte le properties del prodotto
    /// invece che referenziare l'archivio prodotti
    /// per evitare che modificando un prodotto si modifichino
    /// ordini già creati
    /// </summary>
    public class ProdottoOrdinatoModel
    {
        public int Id { get; set; }

        /// <summary>
        /// IdProdotto originale da catagolo
        /// </summary>
        [Required]
        public int? ProdottoId { get; set; }

        [Required]
        public int? OrdineId { get; set; }

        [Required]
        public string? Codice { get; set; }

        [Required]
        public string? Descrizione { get; set; }

        [Required]
        public string? Categoria { get; set; }

        [Required]
        public decimal? Prezzo { get; set; }

        [Required]
        public int? Quantita { get; set; }

        public decimal? Subtotale { get => Prezzo * Quantita; }
    }
}
