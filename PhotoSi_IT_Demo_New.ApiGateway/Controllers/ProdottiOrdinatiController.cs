using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;


namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdottiOrdinatiController : ControllerBase
    {
        readonly IPhotoSiServicesBus servicesBus;

        public ProdottiOrdinatiController(IPhotoSiServicesBus servicesBus)
        {
            this.servicesBus = servicesBus;
        }

        public async Task<IActionResult> GetAll()
            => Ok(await servicesBus.GetProdottiOrdinati());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await servicesBus.GetProdottoOrdinato(id));

        [HttpGet]
        [Route("ordine/{ordineId}")]
        public async Task<IActionResult> GetByOrdine(int ordineId)
           => Ok(await servicesBus.GetProdottiOrdinatiByOrdine(ordineId));

        [HttpPost]
        public async Task<IActionResult> Create(ProdottoOrdinatoModel model)
            => Ok(await servicesBus.AddProdottoOrdinato(model));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, ProdottoOrdinatoModel model)
            => Ok(await servicesBus.UpdateProdottoOrdinato(id, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
            => await servicesBus.DeleteProdottoOrdinato(id);

        [HttpDelete]
        [Route("ordine/{ordineId}")]
        public async Task DeleteByOrdine(int ordineId)
            => await servicesBus.DeleteProdottiOrdinatiByOrdine(ordineId);
    }
}