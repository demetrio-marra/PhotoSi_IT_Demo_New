using PhotoSi_IT_Demo_New.Infrastructure.DAL;
using PhotoSi_IT_Demo_New.Services.Ordini.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Data.Repositories
{
    public class OrdiniRepository: GenericRepository<OrdineEntity>
    {
        public OrdiniRepository(OrdiniDbContext context): base(context)
        {
        }

        public async Task<OrdineEntity?> AddOrdine(OrdineEntity objUser)
        {
            return await Create(objUser);
        }

        public async Task DeleteOrdine(int idUser)
        {
            await Delete(idUser);
        }

        public async Task<OrdineEntity?> EditOrdine(int id, OrdineEntity entity)
        {
            var p = await GetById(id);
            if (p == null)
                return null;

            p.AcquirenteId = entity.AcquirenteId;
            p.Data = entity.Data;
            return await Update(p);
        }

        public async Task<IEnumerable<OrdineEntity>> GetAllOrdini()
        {
            return await GetAll();
        }

        public async Task<OrdineEntity?> GetOrdineById(int id)
        {
            return await GetById(id);
        }
    }
}
