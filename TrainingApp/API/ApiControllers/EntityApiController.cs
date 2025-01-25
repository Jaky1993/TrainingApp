using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrainingApp.API.Model;
using TrainingApp.VIEWMODEL;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingApp.API.ApiControllers
{
    public abstract class EntityApiController<T,U> : ControllerBase where T : Entity<T> where U : EntityViewModel
    {
        protected ApiResponse _response;

        /*
        La differenza tra 
        - protected readonly IMapper _mapper;
        - protected IMapper _mapper; 
        riguarda la variabilità della variabile _mapper dopo la sua inizializzazione.
        
        protected readonly IMapper _mapper;
        readonly significa che la variabile può essere assegnata una sola volta, sia al momento della dichiarazione che nel costruttore della classe.
        Dopo l'inizializzazione, il valore di _mapper non può più essere cambiato.
        
        protected IMapper _mapper;
        In questo caso, _mapper può essere modificato più volte durante il ciclo di vita dell'oggetto.
        Non ha le restrizioni readonly, quindi puoi assegnarli un nuovo valore in qualsiasi metodo della classe.
        */

        protected readonly ICreate<T> _create;
        protected readonly IDelete<T> _delete;
        protected readonly ISelect<T> _select;
        protected readonly IUpdate<T> _update;
        protected readonly IMapper _mapper;

        public EntityApiController(ICreate<T> create, IDelete<T> delete, ISelect<T> select, IUpdate<T> update, IMapper mapper)
        {
            _response = new();
            _create = create;
            _delete = delete;
            _select = select;
            _update = update;
            _mapper = mapper;
        }
    }
}
