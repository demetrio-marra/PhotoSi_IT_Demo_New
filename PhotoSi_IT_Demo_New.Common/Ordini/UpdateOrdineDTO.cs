using System.ComponentModel.DataAnnotations;

namespace PhotoSi_IT_Demo_New.Common.Ordini
{
    public class UpdateOrdineDTO
    {
        [Required]
        public string? RecapitoIndirizzo { get; set; }

        [Required]
        public string? RecapitoCap { get; set; }

        [Required]
        public string? RecapitoCitta { get; set; }

        [Required, MinLength(1)]
        public IEnumerable<UpdateOrdineCommandDettagliProdotti>? Prodotti { get; set; } 

        public class UpdateOrdineCommandDettagliProdotti
        {
            [Required]
            public int? ProdottoId { get; set; }

            [Required]
            public int? Quantita { get; set; }
        }
    }
}
