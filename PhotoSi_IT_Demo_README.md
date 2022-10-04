### *PhotoSi_IT_Demo* - introduzione ed istruzioni per l'esecuzione.
*30/09/2022 Demetrio Marra*


# Introduzione

Sono stati implementati 4 servizi autonomi, per fornire le funzionalit� di gestione ordini ad un ipotetico frontend/app.
Sono state create le seguenti entit�: Ordini, Prodotti, ProdottiOrdinati, Recapiti e Utenti.
I servizi hanno database separati.

� stato implementato un ApiGateway che funge da proxy alle api esposte dai servizi.

Per semplicit� � stata omessa ogni forma di autenticazione/autorizzazione.

La soluzione non � dotata di un frontend, pertanto � utilizzabile esclusivamente tramite un client HTTP (es: Postman o curl). Sono fornite le collection postman (e gli environments) utilizzate durante lo sviluppo.



# Istruzioni per l'esecuzione

Eseguire con docker-compose. Sono utilizzate le porte locali dalla 5000 alla 5004.
Utilizzare le collection postman allegate per consumare le Api (allegati anche gli environment).



# Workflow principale

Si censiscono innanzitutto i Prodotti e gli Utenti: Api CreateProdotto e CreateUtente.
Gli Id restituiti serviranno per la creazione dell'ordine: si suppone che l'ipotetico client mantenga queste informazioni in un carrello ed in variabili di sessione.

A questo punto � possibile inserire gli Ordini, fornendo l'IdUtente, gli IdProdotto (oltre che le quantit�) che si desiderano includere e le informazioni di Recapito (api CreateOrdine).

I DTO di creazione/modifica dell'ordine, richiedono che siano presenti TUTTI i dati, inclusi quelli eventualmente gi� caricati.
Ad esempio se si aggiorna la lista prodotti ordinati tramite UpdateOrdine, occorre che in lista siano presenti sia i nuovi che quelli gi� presenti, anche se senza alcuna variazione.
NOTA: vedi Workflow alternativo per un'api pi� elementare che non ha queste restrizioni.

La creazione dell'Ordine esegue le seguenti operazioni secondo il flusso descritto:
1. Recupero dei dati dell'Utente dal servizio Utenti (RPC) per verificare che esista.
2. Recupero di uno snapshot dei dati sui Prodotti dal relativo servizio.
3. Salvataggio dell'Ordine.
4. Salvataggio dei Prodotti Ordinati mediante chiamate RPC al servizio Prodotti.
5. Salvataggio del Recapito mediante chiamata RPC al relativo servizio.

Prima del salvataggio dell'ordine, � verificata l'esistenza dei prodotti e dell'utente.
Non � presente alcuna logica di magazzino (non c'� il controllo sulle quantit� residue).

L'aggiornamento dell'ordine � soggetto a queste regole:
- Non si pu� aggiornare l'acquirente.
- Se si aggiorna la lista dei prodotti acquistati, � possibile inserirne di nuovi, cos� come eliminarli. Per i prodotti gi� presenti � consentita la sola variazione della quantit�.



# Workflow alternativo

Utilizzando le api CRUD pure � possibile creare l'ordine vuoto, per poi inserirvi i prodotti uno per volta e/o massivamente.
Come per il workflow principale, occorre innanzitutto creare i prodotti (Catalogo) e gli utenti.
In seguito � possibile creare l'ordine (senza lista prodotti) ed il relativo recapito.
Infine, utilizzando l'api dei ProdottiOrdinati, aggiungere/rimuovere i prodotti dall'ordine.



# Annotazioni sulle relazioni tra le entit�

*Le relazioni sono documentate nelle immagini allegate.*

Le informazioni sui Prodotti Ordinati sono duplicate in un archivio separato e non
tramite una semplice relazione tra Ordine e Prodotti (catalogo).
Questo per evitare che variazioni al catagolo prodotti, modifichino gli ordini gi�
creati in passato.

Il recapito � sempre creato insieme all'ordine. Anche se � creato un nuovo ordine con gli stessi identici dati di recapito di un ordine gi� presente, sar� comunque generato un nuovo record recapito.
Di fatto la tabella recapiti � un'estensione di quella degli ordini (relazione 1:1).

L'Utente (Acquirente) � semplicemente una propriet� dell'ordine stesso (come fosse una FK).
Se un Utente che ha effettuato un Ordine viene eliminato, usando la relativa api Delete, l'ordine risulter� privo di utente.
Ho preferito questo invece del salvataggio in un archivio a parte (come per i prodotti), semplicemente per avere pi� tipi di logiche nella gestione della persistenza del dato.



# Struttura della soluzione

La soluzione � composta da:
- un progetto Common contenente le definizioni dei modelli.
- 2 progetti di "infrastruttura" contenenti definizione ed implementazione di un service bus molto basilare.
- 4 progetti relativi ai servizi: Ordini, Prodotti, Recapiti ed Utenti.
- 4 integration test.
- un progetto ApiGateway.

Le api dei servizi Prodotti, Recapiti ed Utenti sono delle CRUD pure.
L'api di creazione/modifica Ordini invece non si limita ad agire sul database degli ordini, ma chiama le api dei servizi Recapiti, Utenti e Prodotti.
Le chiamate RPC (sincrone) avvengono mediante un service bus.
Per questa demo si � utilizzato un semplice proxy http delle api dei servizi.

Ciascuno dei 4 servizi espone delle proprie WebApi all'url http://\<server>:\<port>/rpc/v1/
Questi url si intendono "non visibili" al mondo esterno e raggiungibili unicamente dall'ApiGateway.
Tuttavia � possibile testare direttamente queste api (scavalcando l'apigateway) usando la collection postman allegata.

L'ApiGateway a sua volta riespone le stesse api dei servizi all'url
http://\<server>:\<port>/api/v1/
Questo � l'URL esposto al mondo esterno. Anche per queste api � fornita collection postman.



