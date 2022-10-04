using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Infrastructure.DAL;
using PhotoSi_IT_Demo_New.Services.Prodotti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Prodotti.Data.Repositories
{
    public class ProdottiOrdinatiRepository: GenericRepository<ProdottoOrdinatoEntity>
    {
        readonly ProdottiDbContext _context;

        public ProdottiOrdinatiRepository(ProdottiDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<ProdottoOrdinatoEntity?> AddProdottoOrdinato(ProdottoOrdinatoEntity objUser)
        {
            return await Create(objUser);
        }

        public async Task<IEnumerable<ProdottoOrdinatoEntity>> AddProdottiOrdinati(IEnumerable<ProdottoOrdinatoEntity> list)
        {
            await _context.AddRangeAsync(list);
            await _context.SaveChangesAsync();
            return list; ;
        }

        public async Task DeleteProdottoOrdinato(int idUser)
        {
            await Delete(idUser);
        }

        public async Task<ProdottoOrdinatoEntity?> EditProdottoOrdinato(int id, ProdottoOrdinatoEntity entity)
        {
            var p = await GetById(id);
            if (p == null)
                return null;

            p.Descrizione = entity.Descrizione;
            p.Quantita = entity.Quantita;
            p.Categoria = entity.Categoria;
            p.OrdineId = entity.OrdineId;
            p.Prezzo = entity.Prezzo;
            p.ProdottoId = entity.ProdottoId;
            p.Codice = entity.Codice;

            return await Update(p);
        }

        public async Task<IEnumerable<ProdottoOrdinatoEntity>> EditProdottiOrdinati(IEnumerable<ProdottoOrdinatoEntity> listParam)
        {
            // update diretto senza select
            foreach(var l in listParam)
                _context.Entry(l).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return listParam;
        }

        public async Task<IEnumerable<ProdottoOrdinatoEntity>> GetAllProdottiOrdinati()
        {
            return await GetAll();
        }

        public async Task<ProdottoOrdinatoEntity?> GetProdottoOrdinatoById(int id)
        {
            return await GetById(id);
        }

        public async Task<IEnumerable<ProdottoOrdinatoEntity>> GetProdottiOrdinatiByOrdine(int ordineId)
        {
            return await _context.ProdottiOrdinati.Where(p => p.OrdineId == ordineId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteProdottiOrdinatiByOrdine(int ordineId)
        {
            var ents = await _context.ProdottiOrdinati.Where(p => p.OrdineId == ordineId)
                .ToListAsync();
            _context.RemoveRange(ents);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProdottiOrdinati(IEnumerable<ProdottoOrdinatoEntity> list)
        {
            foreach(var l in list)
                _context.Entry(l).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
