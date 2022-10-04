using Microsoft.AspNetCore.Mvc;
using PhotoSi_IT_Demo_New.Common.Utenti;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;


namespace PhotoSi_IT_Demo_New.Services.Ordini.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UtentiController : ControllerBase
    {
        readonly IPhotoSiServicesBus servicesBus;

        public UtentiController(IPhotoSiServicesBus servicesBus)
        {
            this.servicesBus = servicesBus;
        }

        public async Task<IActionResult> GetAll()
            => Ok(await servicesBus.GetUtenti());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await servicesBus.GetUtente(id));

        [HttpPost]
        public async Task<IActionResult> Create(UtenteModel model)
            => Ok(await servicesBus.AddUtente(model));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, UtenteModel model)
            => Ok(await servicesBus.UpdateUtente(id, model));

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
            => await servicesBus.DeleteUtente(id);
    }
}