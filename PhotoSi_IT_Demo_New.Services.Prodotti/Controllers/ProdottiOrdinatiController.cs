using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Services.Prodotti;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("rpc/v1/[controller]")]
    public class ProdottiOrdinatiController : ControllerBase
    {
        readonly ProdottiService service;

        public ProdottiOrdinatiController(ProdottiService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> GetAll()
          => Ok(await service.GetProdottiOrdinati());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await service.GetProdottoOrdinato(id));

        [HttpGet]
        [Route("ordine/{ordineId}")]
        public async Task<IActionResult> GetByOrdine(int ordineId)
            => Ok(await service.GetProdottiOrdinatiByOrdine(ordineId));

        [HttpPost]
        public async Task<IActionResult> Create(ProdottoOrdinatoModel model)
            => Ok(await service.CreateProdottoOrdinato(model));

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> CreateMultiple(IEnumerable<ProdottoOrdinatoModel> list)
            => Ok(await service.CreateProdottiOrdinati(list));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, ProdottoOrdinatoModel model)
            => Ok(await service.UpdateProdottoOrdinato(id, model));

        [HttpPut]
        [Route("multiple")]
        public async Task<IActionResult> Update(IEnumerable<ProdottoOrdinatoModel> list)
            => Ok(await service.UpdateProdottiOrdinati(list));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id) 
            => await service.DeleteProdottoOrdinato(id);

        [HttpDelete]
        [Route("ordine/{ordineId}")]
        public async Task DeleteByOrdine(int ordineId)
            => await service.DeleteProdottiOrdinatiByOrdine(ordineId);

        [HttpPost]
        [Route("multiple-del")]
        public async Task Delete(IEnumerable<ProdottoOrdinatoModel> list)
            => await service.DeleteProdottiOrdinati(list);
    }
}