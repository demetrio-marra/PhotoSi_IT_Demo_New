using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Infrastructure.DAL;
using PhotoSi_IT_Demo_New.Services.Recapiti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Recapiti.Data.Repositories
{
    public class RecapitiRepository: GenericRepository<RecapitoEntity>
    {
        readonly RecapitiDbContext context;

        public RecapitiRepository(RecapitiDbContext context): base(context)
        {
            this.context = context;
        }

        public async Task<RecapitoEntity?> AddRecapito(RecapitoEntity objUser)
        {
            return await Create(objUser);
        }

        public async Task DeleteRecapito(int idUser)
        {
            await Delete(idUser);
        }

        public async Task<RecapitoEntity?> EditRecapito(int id, RecapitoEntity entity)
        {
            var p = await GetById(id);
            if (p == null)
                return null;

            p.Citta = entity.Citta;
            p.Cap = entity.Cap;
            p.OrdineId = entity.OrdineId;
            p.Indirizzo = entity.Indirizzo;

            return await Update(p);
        }

        public async Task<RecapitoEntity?> EditRecapitoByOrdine(int ordineId, RecapitoEntity entity)
        {
            var p = await context.Recapiti.SingleAsync(r => r.OrdineId == ordineId);

            p.Citta = entity.Citta;
            p.Cap = entity.Cap;
            p.OrdineId = entity.OrdineId;
            p.Indirizzo = entity.Indirizzo;

            return await Update(p);
        }

        public async Task<IEnumerable<RecapitoEntity>> GetAllRecapiti()
        {
            return await GetAll();
        }

        public async Task<RecapitoEntity?> GetRecapitoById(int id)
        {
            return await GetById(id);
        }

        public async Task<RecapitoEntity?> GetRecapitoByOrdine(int ordineId)
            => await context.Recapiti.FirstOrDefaultAsync(r => r.OrdineId == ordineId); 

        public async Task DeleteRecapitoByOrdine(int ordineId)
        {
            var recapito = context.Recapiti.FirstOrDefaultAsync(r => r.OrdineId == ordineId);
            context.Remove(recapito);
            await context.SaveChangesAsync();
        }
    }
}
