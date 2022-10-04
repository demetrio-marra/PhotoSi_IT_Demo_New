using System.ComponentModel.DataAnnotations;

namespace PhotoSi_IT_Demo_New.Common.Ordini
{
    public class OrdineModel
    {
        public int Id { get; set; }

        [Required]
        public int AcquirenteId { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
