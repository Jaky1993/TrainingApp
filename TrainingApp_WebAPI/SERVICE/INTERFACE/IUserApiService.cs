﻿using TrainingApp_WebAPI.MODEL;

namespace TrainingApp_WebAPI.SERVICE.INTERFACE
{
    //U: Viene specificato a livello di interfaccia per garantire che tutti i metodi che richiedono un entityViewModel utilizzino lo stesso tipo(U).
    //T: Specificato a livello di metodo, rendendo i metodi più flessibili perché ciascun metodo può lavorare con differenti tipi di ritorno.
    public interface IUserApiService
    {
        //Z = ApiResponse
        Task<Z> GetAllAsync<Z>();
        Task<Z> GetAsync<Z>(int id);
        Task<Z> CreateAsync<Z>(User user);
        Task<Z> UpdateAsync<Z>(User user);
        Task<Z> DeleteAsync<Z>(int id);
    }
}
