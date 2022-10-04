using System.ComponentModel.DataAnnotations;

namespace PhotoSi_IT_Demo_New.Common.Recapiti
{
    public class RecapitoModel
    {
        public int Id { get; set; }

        [Required]
        public int? OrdineId { get; set; }

        [Required]
        public string? Indirizzo { get; set; }

        [Required]
        public string? Cap { get; set; }

        [Required]
        public string? Citta { get; set; }
    }
}
