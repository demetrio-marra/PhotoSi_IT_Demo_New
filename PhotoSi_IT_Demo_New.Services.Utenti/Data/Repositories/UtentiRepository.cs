using PhotoSi_IT_Demo_New.Infrastructure.DAL;
using PhotoSi_IT_Demo_New.Services.Utenti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Utenti.Data.Repositories
{
    public class UtentiRepository : GenericRepository<UtenteEntity>
    {
        public UtentiRepository(UtentiDbContext context) : base(context)
        {
        }

        public async Task<UtenteEntity?> AddUtente(UtenteEntity objUser)
        {
            return await Create(objUser);
        }
        
        public async Task DeleteUtente(int idUser)
        {
            await Delete(idUser);
        }

        public async Task<UtenteEntity?> EditUtente(int id, UtenteEntity objUser)
        {
            var inputUser = await GetById(id);
            if (inputUser == null)
                return null;

            inputUser.Username = objUser.Username;
            inputUser.Nominativo = objUser.Nominativo;
            return await Update(inputUser);
        }

        public async Task<IEnumerable<UtenteEntity>> GetAllUtenti()
        {
            return await GetAll();
        }

        public async Task<UtenteEntity?> GetUtenteById(int id)
        {
            return await GetById(id);
        }
    }
}
