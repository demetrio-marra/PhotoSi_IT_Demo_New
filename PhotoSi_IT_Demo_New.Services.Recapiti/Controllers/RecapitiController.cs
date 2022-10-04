using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Services.Recapiti;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("rpc/v1/[controller]")]
    public class RecapitiController : ControllerBase
    {
        readonly RecapitiService service;

        public RecapitiController(RecapitiService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> GetAll() =>
            Ok(await service.Get());

        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await service.Get(id));

        [Route("ordine/{ordineId}")]
        public async Task<IActionResult> GetByOrdine(int ordineId)
            => Ok(await service.GetByOrdine(ordineId));

        [HttpPost]
        public async Task<IActionResult> Create(RecapitoModel model)
            => Ok(await service.Create(model));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, RecapitoModel model)
            => Ok(await service.Update(id, model));

        [HttpPut]
        [Route("ordine/{ordineId}")]
        public async Task<IActionResult> UpdateByOrdine(int ordineId, RecapitoModel model)
           => Ok(await service.UpdateByOrdine(ordineId, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
            => await service.Delete(id);

        [HttpDelete]
        [Route("ordine/{ordineId}")]
        public async Task DeleteByOrdine(int ordineId)
            => await service.DeleteByOrdine(ordineId);
    }
}