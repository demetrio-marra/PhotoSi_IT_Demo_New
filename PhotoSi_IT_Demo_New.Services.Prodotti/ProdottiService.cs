using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Services.Prodotti.Data.Entities;
using PhotoSi_IT_Demo_New.Services.Prodotti.Data.Repositories;

namespace PhotoSi_IT_Demo_New.Services.Prodotti
{
    public class ProdottiService
    {
        readonly ProdottiRepository repository;
        readonly ProdottiOrdinatiRepository prodottiOrdinatiRepository;
        readonly IMapper mapper;

        public ProdottiService(ProdottiRepository repository,
            ProdottiOrdinatiRepository prodottiOrdinatiRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.prodottiOrdinatiRepository = prodottiOrdinatiRepository;
        }

        #region archivio prodotti

        public async Task<IEnumerable<ProdottoModel>> Get()
        {
            var entities = await repository.GetAllProdotti();
            return mapper.Map<IEnumerable<ProdottoModel>>(entities);
        }

        public async Task<ProdottoModel?> Get(int id)
        {
            var entity = await repository.GetProdottoById(id);
            if (entity == null)
                return null;

            return mapper.Map<ProdottoModel>(entity);
        }

        public async Task<IEnumerable<ProdottoModel>> Get(IEnumerable<int> ids)
        {
            var entities = await repository.GetProdotti(ids);
            return mapper.Map<IEnumerable<ProdottoModel>>(entities);
        }

        public async Task<ProdottoModel> Create(ProdottoModel model)
        {
            var ret = await repository.AddProdotto(mapper.Map<ProdottoEntity>(model));
            return mapper.Map<ProdottoModel>(ret);
        }

        public async Task<ProdottoModel> Update(int id, ProdottoModel model)
        {
            var ret = await repository.EditProdotto(id, mapper.Map<ProdottoEntity>(model));
            return mapper.Map<ProdottoModel>(ret);
        }

        public async Task Delete(int id) => await repository.DeleteProdotto(id);
        #endregion

        #region prodotti ordinati

        public async Task<IEnumerable<ProdottoOrdinatoModel>> GetProdottiOrdinati()
        {
            var entities = await prodottiOrdinatiRepository.GetAllProdottiOrdinati();
            return mapper.Map<IEnumerable<ProdottoOrdinatoModel>>(entities);
        }

        public async Task<ProdottoOrdinatoModel?> GetProdottoOrdinato(int id)
        {
            var entity = await prodottiOrdinatiRepository.GetProdottoOrdinatoById(id);
            if (entity == null)
                return null;

            return mapper.Map<ProdottoOrdinatoModel>(entity);
        }

        public async Task<ProdottoOrdinatoModel> CreateProdottoOrdinato(
            ProdottoOrdinatoModel model)
        {
            var ret = await prodottiOrdinatiRepository.AddProdottoOrdinato(
                mapper.Map<ProdottoOrdinatoEntity>(model));
            return mapper.Map<ProdottoOrdinatoModel>(ret);
        }

        public async Task<IEnumerable<ProdottoOrdinatoModel>> CreateProdottiOrdinati(
          IEnumerable<ProdottoOrdinatoModel> list)
        {
            var ret = await prodottiOrdinatiRepository.AddProdottiOrdinati(
                mapper.Map<IEnumerable<ProdottoOrdinatoEntity>>(list));
            return mapper.Map<IEnumerable<ProdottoOrdinatoModel>>(ret);
        }

        public async Task<ProdottoOrdinatoModel> UpdateProdottoOrdinato(int id,
            ProdottoOrdinatoModel model)
        {
            var ret = await prodottiOrdinatiRepository.EditProdottoOrdinato(id,
                mapper.Map<ProdottoOrdinatoEntity>(model));
            return mapper.Map<ProdottoOrdinatoModel>(ret);
        }

        public async Task<IEnumerable<ProdottoOrdinatoModel>> UpdateProdottiOrdinati(
            IEnumerable<ProdottoOrdinatoModel> list)
        {
            var ret = await prodottiOrdinatiRepository.EditProdottiOrdinati(
                mapper.Map<IEnumerable<ProdottoOrdinatoEntity>>(list));
            return mapper.Map<IEnumerable<ProdottoOrdinatoModel>>(ret);
        }

        public async Task DeleteProdottoOrdinato(int id) => 
            await prodottiOrdinatiRepository.DeleteProdottoOrdinato(id);

        public async Task<IEnumerable<ProdottoOrdinatoModel>> GetProdottiOrdinatiByOrdine(int ordineId)
        {
            var ret = await prodottiOrdinatiRepository.GetProdottiOrdinatiByOrdine(ordineId);
            return mapper.Map<IEnumerable<ProdottoOrdinatoModel>>(ret);                
        }

        public async Task DeleteProdottiOrdinatiByOrdine(int ordineId) =>
           await prodottiOrdinatiRepository.DeleteProdottiOrdinatiByOrdine(ordineId);

        public async Task DeleteProdottiOrdinati(IEnumerable<ProdottoOrdinatoModel> list)
        {
            await prodottiOrdinatiRepository.DeleteProdottiOrdinati(
                mapper.Map<IEnumerable<ProdottoOrdinatoEntity>>(list));
        }
        #endregion
    }
}
