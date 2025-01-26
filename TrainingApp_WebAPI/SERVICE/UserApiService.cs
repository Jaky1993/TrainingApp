using Microsoft.Extensions.Configuration;
using System.Runtime.ConstrainedExecution;
using TrainingApp_WebAPI.MODEL.API;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp_WebAPI.VIEWMODEL;
using static Utility.ApiLibrary;

namespace TrainingApp_WebAPI.SERVICE
{
    public class UserApiService : ApiService, IUserApiService
    {
        private string url;
        private IHttpClientFactory _httpClient;

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
        public UserApiService(IConfiguration configuration, IHttpClientFactory httpClient) : base(httpClient)
        {
            url = configuration.GetValue<string>("ServiceUrls:TrainingAPI");
            _httpClient = httpClient;
        }

        public Task<T> CreateAsync<T>(UserViewModel userViewModel)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.POST;
            apiRequest.Url = url + "/api/TrainingAppAPI";
            apiRequest.Data = userViewModel;

            return SendAsync<T>(apiRequest);
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.DELETE;
            apiRequest.Url = url + "/api/TrainingAppAPI/id:int?id=" + id;

            return SendAsync<T>(apiRequest);
        }

        public Task<T> GetAllAsync<T>()
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.GET;
            apiRequest.Url = url + "/api/TrainingAppAPI";

            return SendAsync<T>(apiRequest);
        }

        public Task<T> GetAsync<T>(int id)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.GET;
            apiRequest.Url = url + "/api/TrainingAppAPI/id:int?id=" + id;

            return SendAsync<T>(apiRequest);
        }

        public Task<T> UpdateAsync<T>(UserViewModel userViewModel)
        {
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.ApiType = ApiType.PUT;
            apiRequest.Url = url + "/api/TrainingAppAPI";
            apiRequest.Data = userViewModel;

            return SendAsync<T>(apiRequest);
        }
    }
}
