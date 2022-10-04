using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Ordini;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("rpc/v1/[controller]")]
    public class OrdiniController : ControllerBase
    {
        readonly OrdiniService service;

        public OrdiniController(OrdiniService ordiniService)
        {
            service = ordiniService;
        }

        public async Task<IActionResult> GetAll() 
            => Ok(await service.Get());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id) 
            => Ok(await service.Get(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateOrdineDTO command)
            => Ok(await service.Create(command));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, UpdateOrdineDTO command)
            => Ok(await service.Update(id, command));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id) 
            => await service.Delete(id);

        #region pure crud functions

        [Route("crud")]
        public async Task<IActionResult> GetCRUDAll() 
            => Ok(await service.GetCRUD());

        [HttpGet]
        [Route("crud/{id}")]
        public async Task<IActionResult> GetCRUD(int id) 
            => Ok(await service.GetCRUD(id));

        [HttpPost]
        [Route("crud")]
        public async Task<IActionResult> CreateCRUD([FromBody] OrdineModel model)
            => Ok(await service.CreateCRUD(model));

        [HttpPut]
        [Route("crud/{id}")]
        public async Task<IActionResult> UpdateCRUD(int id, OrdineModel model)
            => Ok(await service.UpdateCRUD(id, model));

        [HttpDelete]
        [Route("crud/{id}")]
        public async Task DeleteCRUD(int id) 
            => await service.DeleteCRUD(id);

        #endregion
    }
}