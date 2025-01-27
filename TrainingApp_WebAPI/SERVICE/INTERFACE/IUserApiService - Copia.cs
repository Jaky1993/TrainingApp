using System.Runtime.ConstrainedExecution;
using TrainingApp_WebAPI.VIEWMODEL;

namespace TrainingApp_WebAPI.SERVICE.INTERFACE
{
    //U: Viene specificato a livello di interfaccia per garantire che tutti i metodi che richiedono un entityViewModel utilizzino lo stesso tipo(U).
    //T: Specificato a livello di metodo, rendendo i metodi più flessibili perché ciascun metodo può lavorare con differenti tipi di ritorno.
    public interface IEntityApiService<U>
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(U entityViewModel);
        Task<T> UpdateAsync<T>(U entityViewModel);
        Task<T> DeleteAsync<T>(int id);
    }
}
