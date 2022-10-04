using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Services.Prodotti;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("rpc/v1/[controller]")]
    public class ProdottiController : ControllerBase
    {
        readonly ProdottiService service;

        public ProdottiController(ProdottiService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> GetAll()
            => Ok(await service.Get());

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> Get(IEnumerable<int> ids)
            => Ok(await service.Get(ids));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await service.Get(id));

        [HttpPost]
        public async Task<IActionResult> Create(ProdottoModel model)
            => Ok(await service.Create(model));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, ProdottoModel model)
            => Ok(await service.Update(id, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id) 
            => await service.Delete(id);
    }
}