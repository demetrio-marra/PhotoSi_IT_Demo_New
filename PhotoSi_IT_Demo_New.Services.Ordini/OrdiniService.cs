using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Ordini;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;
using PhotoSi_IT_Demo_New.Services.Ordini.Data.Entities;
using PhotoSi_IT_Demo_New.Services.Ordini.Data.Repositories;

namespace PhotoSi_IT_Demo_New.Services.Ordini
{
    public class OrdiniService
    {
        readonly OrdiniRepository repository;
        readonly IPhotoSiServicesBus servicesBus;
        readonly IMapper mapper;

        public OrdiniService(OrdiniRepository repository,
            IPhotoSiServicesBus servicesBus,
            IMapper mapper)
        {
            this.repository = repository;
            this.servicesBus = servicesBus;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<OrdineDTO>> Get()
        {
            var ordini = await repository.GetAllOrdini();
            var ordiniModel = new List<OrdineDTO>();

            // da ottimizzare per richieste massive?
            // questo metodo non sarà mai usato per TUTTI gli ordini presenti.
            // Probabilmente lo useranno delle griglie (quindi c'è la paginazione a mitigare).
            foreach(var o in ordini)
            {
                var utente = await servicesBus.GetUtente(o.AcquirenteId);
                var prodotti = await servicesBus.GetProdottiOrdinatiByOrdine(o.Id);
                var recapito = await servicesBus.GetRecapitoByOrdine(o.Id);

                ordiniModel.Add(new OrdineDTO
                {
                    Acquirente = utente,
                    Data = o.Data,
                    Id = o.Id,
                    Prodotti = prodotti,
                    RecapitoConsegna = recapito
                });
            }

            return ordiniModel;
        }

        public async Task<OrdineDTO?> Get(int id)
        {
            var entity = await repository.GetOrdineById(id);
            if (entity == null)
                return null;

            var utente = await servicesBus.GetUtente(entity.AcquirenteId);
            var prodotti = await servicesBus.GetProdottiOrdinatiByOrdine(entity.Id);
            var recapito = await servicesBus.GetRecapitoByOrdine(entity.Id);

            return new OrdineDTO
            {
                Id = entity.Id,
                Acquirente = utente,
                Data = entity.Data,
                Prodotti = prodotti,
                RecapitoConsegna = recapito
            };
        }

        public async Task<OrdineDTO?> Create(CreateOrdineDTO dto)
        {
            var creationDate = DateTime.Now;

            var prodottiRichiesti = dto.Prodotti!;

            var acquirente = await servicesBus.GetUtente(dto.AcquirenteId!.Value);
            if (acquirente == null)
                throw new Exception($"User {dto.AcquirenteId} does not exists");

            var prodottiCatalogo = await servicesBus.GetProdotti(prodottiRichiesti
                .Select(p => p.ProdottoId!.Value)
                .ToList());

            // se manca anche un solo prodotto: errore
            var missingProducts = prodottiRichiesti.Select(p => p.ProdottoId!.Value)
                .Except(prodottiCatalogo.Select(p => p.Id));
            if (missingProducts.Any())
                throw new Exception($"Prodotti {string.Join(",", missingProducts)} not found");

            // ottimizza per evitare di eseguire più volte la stessa ricerca nell'espressione LINQ select sotto
            var quantitaDictionary = dto.Prodotti!.ToDictionary(p => p.ProdottoId!.Value,
                p => p.Quantita);

            var prodottiDaInserire = prodottiCatalogo.Select(archivioProdotto => new ProdottoOrdinatoModel
            {
                Categoria = archivioProdotto.Categoria,
                Codice = archivioProdotto.Codice,
                Descrizione = archivioProdotto.Descrizione,
                Prezzo = archivioProdotto.Prezzo,
                Quantita = quantitaDictionary[archivioProdotto.Id], // ottimizzazione
                ProdottoId = archivioProdotto.Id,
            }).ToList();

            // creo ordine
            var entity = new OrdineEntity
            {
                AcquirenteId = acquirente.Id,
                Data = creationDate,
            };
            entity = await repository.AddOrdine(entity);

            // associo i prodotti all'ordine
            foreach (var p in prodottiDaInserire)
                p.OrdineId = entity!.Id;
            
            await servicesBus.AddProdottiOrdinati(prodottiDaInserire);

            var recapito = await servicesBus.AddRecapito(new RecapitoModel
            {
                Cap = dto.RecapitoCap,
                Citta = dto.RecapitoCitta,
                Indirizzo = dto.RecapitoIndirizzo,
                OrdineId = entity!.Id
            });

            return await Get(entity.Id);
        }

        public async Task<OrdineDTO?> Update(int id, UpdateOrdineDTO dto)
        {
            // regole di update: sono consentite
            // - modifiche al recapito
            // - modificare la quantità di prodotto già presente e inserire/eliminare

            // prendo
            var entity = await repository.GetOrdineById(id);
            if (entity == null)
                throw new Exception($"Order {id} not found");

            // recupero i prodotti dall'archivio
            var prodottiPresenti = await servicesBus.GetProdottiOrdinatiByOrdine(entity.Id);

            // da ottimizzare for ad api massiva come già fatto in CREATE
            var prodottiDaInserire = new List<ProdottoOrdinatoModel>();
            foreach (var p in dto.Prodotti!)
            {
                // il prodotto è già associato all'ordine?
                var prodottoPresente = prodottiPresenti.FirstOrDefault(pp =>
                    p.ProdottoId == pp.ProdottoId);

                // ne modifico solo la quantità per evitare di variare
                // categorie, prezzi e descrizioni (che potrebbero nel
                // frattempo esser stati modificati nel catalogo)
                if (prodottoPresente != null)
                {
                    prodottoPresente.Quantita = p.Quantita;
                    prodottiDaInserire.Add(prodottoPresente);
                    continue;
                }

                // prodotti non ancora associati all'ordine
                var archivioProdotto = await servicesBus.GetProdotto(p.ProdottoId!.Value);
                if (archivioProdotto == null)
                    throw new Exception($"Prodotto {p.ProdottoId} not found");

                prodottiDaInserire.Add(new ProdottoOrdinatoModel
                {
                    Categoria = archivioProdotto.Categoria,
                    Codice = archivioProdotto.Codice,
                    Descrizione = archivioProdotto.Descrizione,
                    ProdottoId = archivioProdotto.Id,
                    Prezzo = archivioProdotto.Prezzo,
                    Quantita = p.Quantita,
                    OrdineId = entity.Id
                });
            }

            var prodottiDaEliminare = prodottiPresenti.Where(p =>
                !prodottiDaInserire.Any(pi => pi.ProdottoId == p.ProdottoId))
                .ToList();

            var prodottiDaCreare = prodottiDaInserire.Where(p => p.Id == 0).ToList();
            await servicesBus.AddProdottiOrdinati(prodottiDaCreare);

            var prodottiDaAggiornare = prodottiDaInserire.Where(p => p.Id != 0).ToList();
            await servicesBus.UpdateProdottiOrdinati(prodottiDaAggiornare);

            await servicesBus.DeleteProdottiOrdinati(prodottiDaEliminare);

            // RECAPITI
            await servicesBus.UpdateRecapitoByOrdine(entity.Id,
                new RecapitoModel
                {
                    Cap = dto.RecapitoCap,
                    Citta = dto.RecapitoCitta,
                    Indirizzo = dto.RecapitoIndirizzo,
                    OrdineId = entity.Id
                });

            return await Get(entity.Id);
        }

        public async Task Delete(int id)
        {
            var ordine = await repository.GetOrdineById(id);
            if (ordine == null)
                return;

            // eliminiamo i prodotti acquistati
            await servicesBus.DeleteProdottiOrdinatiByOrdine(id);

            // eliminiamo i recapiti
            await servicesBus.DeleteRecapitoByOrdine(id);

            // eliminiamo l'ordine
            await repository.DeleteOrdine(id);
        }

        #region pure crud functions

        public async Task<IEnumerable<OrdineModel>> GetCRUD()
        {
            var entities = await repository.GetAllOrdini();
            return mapper.Map<IEnumerable<OrdineModel>>(entities);
        }

        public async Task<OrdineModel?> GetCRUD(int id)
        {
            var entity = await repository.GetOrdineById(id);
            if (entity == null)
                return null;

            return mapper.Map<OrdineModel>(entity);
        }

        public async Task<OrdineModel> CreateCRUD(OrdineModel model)
        {
            var ret = await repository.AddOrdine(mapper.Map<OrdineEntity>(model));
            return mapper.Map<OrdineModel>(ret);
        }

        public async Task<OrdineModel> UpdateCRUD(int id, OrdineModel model)
        {
            var ret = await repository.EditOrdine(id, mapper.Map<OrdineEntity>(model));
            return mapper.Map<OrdineModel>(ret);
        }

        public async Task DeleteCRUD(int id) => await repository.DeleteOrdine(id);
        #endregion

    }
}
