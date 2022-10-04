using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Services.Recapiti.Data.Entities;
using PhotoSi_IT_Demo_New.Services.Recapiti.Data.Repositories;

namespace PhotoSi_IT_Demo_New.Services.Recapiti
{
    public class RecapitiService
    {
        readonly RecapitiRepository repository;
        readonly IMapper mapper;
        readonly ILogger<RecapitiService> logger;

        public RecapitiService(RecapitiRepository repository,
            IMapper mapper,
            ILogger<RecapitiService> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<RecapitoModel>> Get()
        {
            var entities = await repository.GetAllRecapiti();
            return mapper.Map<IEnumerable<RecapitoModel>>(entities);
        }

        public async Task<RecapitoModel?> Get(int id)
            => mapper.Map<RecapitoModel>(await repository.GetRecapitoById(id));

        public async Task<RecapitoModel?> GetByOrdine(int ordineId)
            => mapper.Map<RecapitoModel>(await repository.GetRecapitoByOrdine(ordineId));

        public async Task<RecapitoModel> Create(RecapitoModel model)
        {
            var ret = await repository.AddRecapito(mapper.Map<RecapitoEntity>(model));
            return mapper.Map<RecapitoModel>(ret);
        }

        public async Task<RecapitoModel> Update(int id, RecapitoModel model)
        {
            var ret = await repository.EditRecapito(id, mapper.Map<RecapitoEntity>(model));
            return mapper.Map<RecapitoModel>(ret);
        }

        public async Task<RecapitoModel> UpdateByOrdine(int ordineId, RecapitoModel model)
        {
            var ret = await repository.EditRecapitoByOrdine(ordineId, mapper.Map<RecapitoEntity>(model));
            return mapper.Map<RecapitoModel>(ret);
        }

        public async Task Delete(int id) => await repository.DeleteRecapito(id);
        public async Task DeleteByOrdine(int id) => await repository.DeleteRecapitoByOrdine(id);
    }
}
