using TrainingApp.API.Model;
using TrainingApp_WebAPI.Models.API;

namespace TrainingApp_WebAPI.SERVICE.INTERFACE
{
    public interface IApiService
    {
        ApiResponse responseModel { get; set; }
        public Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
