using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;


namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RecapitiController : ControllerBase
    {
        readonly IPhotoSiServicesBus servicesBus;

        public RecapitiController(IPhotoSiServicesBus servicesBus)
        {
            this.servicesBus = servicesBus;
        }

        public async Task<IActionResult> GetAll()
            => Ok(await servicesBus.GetRecapiti());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await servicesBus.GetRecapito(id));

        [HttpPost]
        public async Task<IActionResult> Create(RecapitoModel model)
            => Ok(await servicesBus.AddRecapito(model));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, RecapitoModel model)
            => Ok(await servicesBus.UpdateRecapito(id, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
            => await servicesBus.DeleteRecapito(id);
    }
}