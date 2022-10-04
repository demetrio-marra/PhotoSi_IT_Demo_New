using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Infrastructure.DAL;
using PhotoSi_IT_Demo_New.Services.Prodotti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Prodotti.Data.Repositories
{
    public class ProdottiRepository: GenericRepository<ProdottoEntity>
    {
        readonly ProdottiDbContext context;

        public ProdottiRepository(ProdottiDbContext context): base(context)
        {
            this.context = context;
        }

        public async Task<ProdottoEntity?> AddProdotto(ProdottoEntity objUser)
        {
            return await Create(objUser);
        }

        public async Task DeleteProdotto(int idUser)
        {
            await Delete(idUser);
        }

        public async Task<ProdottoEntity?> EditProdotto(int id, ProdottoEntity entity)
        {
            var p = await GetById(id);
            if (p == null)
                return null;

            p.Descrizione = entity.Descrizione;
            p.Categoria = entity.Categoria;
            p.Prezzo = entity.Prezzo;
            p.Codice = entity.Codice;
            return await Update(p);
        }

        public async Task<IEnumerable<ProdottoEntity>> GetAllProdotti()
        {
            return await GetAll();
        }

        public async Task<ProdottoEntity?> GetProdottoById(int id)
        {
            return await GetById(id);
        }

        public async Task<IEnumerable<ProdottoEntity>> GetProdotti(IEnumerable<int> ids)
        {
            return await context.Prodotti.Where(p => ids.Contains(p.Id))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
