using Microsoft.Extensions.Configuration;
using System.Runtime.ConstrainedExecution;
using TrainingApp_WebAPI.MODEL;
using TrainingApp_WebAPI.MODEL.API;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp_WebAPI.VIEWMODEL;
using static Utility.ApiLibrary;

namespace TrainingApp_WebAPI.SERVICE
{
    public class UserApiService : ApiService, IUserApiService
    {
        private string url;

        //base(httpClient) -> assicura che il costruttore di ApiService venga chiamato con il parametro httpClient.
        //Questa chiamata è necessaria per completare l'inizializzazione iniziata dal costruttore di ApiService.
        //Chiamando : base(httpClient) in UserApiService, assicuri che httpClientFactory sia correttamente configurato
        //e che qualsiasi codice di inizializzazione aggiuntivo nel costruttore di ApiService venga eseguito.
        /*
        Chiamare il costruttore della classe base:
        Assicura la Corretta Inizializzazione: Tutta la configurazione essenziale eseguita dal costruttore 
        di ApiService viene effettuata.
        Mantiene l'Iniezione delle Dipendenze: Le dipendenze iniettate nella classe base sono disponibili
        per la classe derivata.
        */
        /*
        In pratica, il costruttore della classe derivata deve avere tutti i parametri richiesti per inizializzare
        sia sé stessa che la classe base. Anche se la classe base gestisce l'istanza di IHttpClientFactory,
        devi comunque passargliela dall'esterno affinché possa essere correttamente inizializzata.
        */
        public UserApiService(IConfiguration configuration, IHttpClientFactory httpClient) : base(httpClient)
        {
            url = configuration.GetValue<string>("ServiceUrls:TrainingAPI");
        }

        public Task<Z> CreateAsync<Z>(User user)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.POST;
            apiRequest.Url = url + "/api/TrainingAppAPI";
            apiRequest.Data = user;

            return SendAsync<Z>(apiRequest);
        }

        public Task<Z> DeleteAsync<Z>(int id)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.DELETE;
            apiRequest.Url = url + "/api/TrainingAppAPI/id:int?id=" + id;

            return SendAsync<Z>(apiRequest);
        }
        /*
        Task<T>: Il tipo Task<T> rappresenta un'operazione asincrona che può restituire un valore del tipo T.
        Questo è comunemente usato con i metodi asincroni. Per esempio, se T fosse string,
        allora Task<string> rappresenterebbe un'operazione asincrona che restituirà una stringa

        GetAllAsync<T>: GetAllAsync<T> è un metodo asincrono generico che restituisce un Task<T>.
        La parte "Async" nel nome del metodo è una convenzione che indica che il metodo è asincrono.
        Mettere <T> dopo GetAllAsync nel metodo GetAllAsync<T> è necessario per dichiarare
        che il metodo è generico. Questo dichiara che il metodo può operare su qualsiasi tipo 
        di dati specificato al momento della chiamata del metodo
        */
        public Task<Z> GetAllAsync<Z>()
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.GET;
            apiRequest.Url = url + "/api/TrainingAppAPI";

            return SendAsync<Z>(apiRequest);
        }

        public Task<Z> GetAsync<Z>(int id)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.GET;
            apiRequest.Url = url + "/api/TrainingAppAPI/id:int?id=" + id;

            return SendAsync<Z>(apiRequest);
        }

        public Task<Z> UpdateAsync<Z>(User user)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.PUT;
            apiRequest.Url = url + "/api/TrainingAppAPI";
            apiRequest.Data = user;

            return SendAsync<Z>(apiRequest);
        }
    }
}
