

using TrainingApp_WebAPI.VIEWMODEL;

namespace TrainingApp_WebAPI.SERVICE.INTERFACE
{
    public interface IUserApiService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(UserViewModel userViewModel);
        Task<T> UpdateAsync<T>(UserViewModel userViewModel);
        Task<T> DeleteAsync<T>(int id);
    }
}
