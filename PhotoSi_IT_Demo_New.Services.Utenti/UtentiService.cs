using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Utenti;
using PhotoSi_IT_Demo_New.Services.Utenti.Data.Entities;
using PhotoSi_IT_Demo_New.Services.Utenti.Data.Repositories;

namespace PhotoSi_IT_Demo_New.Services.Utenti
{
    public class UtentiService
    {
        readonly UtentiRepository repository;
        readonly IMapper mapper;
        readonly ILogger<UtentiService> logger;

        public UtentiService(UtentiRepository repository,
            IMapper mapper,
            ILogger<UtentiService> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<UtenteModel>> Get()
        {
            logger.LogDebug("Get - start");
            var entities = await repository.GetAllUtenti();
            logger.LogInformation("Get - returning {utentiCount} Utenti", entities.Count());
            logger.LogDebug("Get - end");
            return mapper.Map<IEnumerable<UtenteModel>>(entities);
        }

        public async Task<UtenteModel?> Get(int id)
        {
            logger.LogDebug("Get(id) - start");
            var entity = await repository.GetUtenteById(id);
            if (entity == null)
            {
                logger.LogInformation("Get(id) - Utente {utenteId} not found", id);
                return null;
            }
            logger.LogInformation("Get(id) - Returning username {username} for id {utenteId}",
                entity.Username, entity.Id);
            logger.LogDebug("Get(id) - end");
            return mapper.Map<UtenteModel>(entity);
        }

        public async Task<UtenteModel> Create(UtenteModel model)
        {
            logger.LogDebug("Create - start");
            var ret = await repository.AddUtente(mapper.Map<UtenteEntity>(model));
            logger.LogInformation("Create - added Username: {username}, Denominazione: {nominativo}, Id: {utenteId}",
                ret!.Username, ret.Nominativo, ret.Id);
            logger.LogDebug("Create - end");
            return mapper.Map<UtenteModel>(ret);
        }

        public async Task<UtenteModel?> Update(int id, UtenteModel model)
        {
            logger.LogDebug("Update - start");
            var ret = await repository.EditUtente(id, mapper.Map<UtenteEntity>(model));
            if (ret == null)
            {
                logger.LogInformation("Update - user id {utenteId} not found", id);
                return null;
            }
            logger.LogInformation("Update - updated Username: {username}, Denominazione: {nominativo}, Id: {utenteId}",
                ret!.Username, ret.Nominativo, ret.Id);
            logger.LogDebug("Update - end");

            return mapper.Map<UtenteModel>(ret);
        }

        public async Task Delete(int id)
        {
            logger.LogDebug("Delete - start");
            await repository.DeleteUtente(id);
            logger.LogInformation("Delete - deleted user {utenteId}", id);
            logger.LogDebug("Delete - end");
        }        
    }
}
