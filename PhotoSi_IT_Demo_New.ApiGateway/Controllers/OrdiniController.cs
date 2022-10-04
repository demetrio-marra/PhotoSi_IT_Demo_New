using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Ordini;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;

namespace PhotoSi_IT_Demo_New.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdiniController : ControllerBase
    {
        readonly IPhotoSiServicesBus servicesBus;

        public OrdiniController(IPhotoSiServicesBus servicesBus)
        {
            this.servicesBus = servicesBus;
        }

        public async Task<IActionResult> GetAll() 
            => Ok(await servicesBus.GetOrdini());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id) 
            => Ok(await servicesBus.GetOrdine(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrdineDTO command)
            => Ok(await servicesBus.AddOrdine(command));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, UpdateOrdineDTO command)
            => Ok(await servicesBus.UpdateOrdine(id, command));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id) 
            => await servicesBus.DeleteOrdine(id);
    }
}