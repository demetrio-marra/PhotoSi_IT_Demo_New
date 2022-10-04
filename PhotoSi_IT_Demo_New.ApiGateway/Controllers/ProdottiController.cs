using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdottiController : ControllerBase
    {
        readonly IPhotoSiServicesBus servicesBus;

        public ProdottiController(IPhotoSiServicesBus servicesBus)
        {
            this.servicesBus = servicesBus;
        }

        public async Task<IActionResult> GetAll()
            => Ok(await servicesBus.GetAllProdotti());

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> GetAll(IEnumerable<int> ids) 
            => Ok(await servicesBus.GetProdotti(ids));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await servicesBus.GetProdotto(id));

        [HttpPost]
        public async Task<IActionResult> Create(ProdottoModel model)
            => Ok(await servicesBus.AddProdotto(model));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, ProdottoModel model)
            => Ok(await servicesBus.UpdateProdotto(id, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
            => await servicesBus.DeleteProdotto(id);
    }
}