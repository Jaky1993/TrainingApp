using TrainingApp_WebAPI.MODEL.API;

namespace TrainingApp_WebAPI.SERVICE.INTERFACE
{
    public interface IApiService
    {
        ApiResponse responseModel { get; set; }
        public Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
