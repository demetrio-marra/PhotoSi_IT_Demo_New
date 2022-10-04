using System.ComponentModel.DataAnnotations;

namespace PhotoSi_IT_Demo_New.Common.Ordini
{
    public class CreateOrdineDTO
    {
        [Required]
        public int? AcquirenteId { get; set; }

       [Required]
        public string? RecapitoIndirizzo { get; set; }

        [Required]
        public string? RecapitoCap { get; set; }

        [Required]
        public string? RecapitoCitta { get; set; }

        [Required, MinLength(1)]
        public IEnumerable<CreateOrdineCommandDettagliProdotti>? Prodotti { get; set; }

        public class CreateOrdineCommandDettagliProdotti
        {
            [Required]
            public int? ProdottoId { get; set; }

            [Required]
            public int? Quantita { get; set; }
        }
    }
}
