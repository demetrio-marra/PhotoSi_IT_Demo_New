using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Utenti;
using PhotoSi_IT_Demo_New.Services.Utenti;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("rpc/v1/[controller]")]
    public class UtentiController : ControllerBase
    {
        readonly UtentiService service;

        public UtentiController(UtentiService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> GetAll() =>
            Ok(await service.Get());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id) 
            => Ok(await service.Get(id));

        [HttpPost]
        public async Task<IActionResult> Create(UtenteModel model) 
            => Ok(await service.Create(model));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, UtenteModel model)
            => Ok(await service.Update(id, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id) 
            => await service.Delete(id);
    }
}