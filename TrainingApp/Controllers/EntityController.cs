using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TrainingAppData.DB.INTERFACE;

namespace TrainingApp.Controllers
{
    public abstract class EntityController<T,U> : Controller
    {
        /*
        La parola chiave readonly in C# è utilizzata per dichiarare un campo che può essere assegnato solo
        durante l'inizializzazione o all'interno del costruttore della classe in cui è definito.
        Una volta assegnato, il valore di un campo readonly non può essere modificato
        */

        protected readonly ICreate<T> _create;
        protected readonly IUpdate<T> _update;
        protected readonly IDelete<T> _delete;
        protected readonly ISelect<T> _select;
        protected readonly IMapper _mapper;

        public EntityController(ICreate<T> create, IUpdate<T> update, IDelete<T> delete, ISelect<T> select, IMapper mapper)
        {
            _create = create;
            _update = update;
            _delete = delete;
            _select = select;
            _mapper = mapper;
        }

        public abstract void DoCreate(T entity, U entityViewModel);
        public abstract ActionResult DoUpdate(T entity, U entityViewModel);
        public abstract void DoDelete(int id);
        public abstract T DoSelect(int id);
        public abstract T DoSelect(Guid guid);
        public abstract List<T> DoSelectList();

        public virtual ActionResult Index()
        {
            return View();
        }

        /*
        Sì, un metodo virtuale può essere richiamato. In programmazione orientata agli oggetti,
        un metodo virtuale è una funzione definita nella classe base e può essere sovrascritta nelle classi derivate.
        Quando si utilizza il metodo, il comportamento effettivo è determinato dal tipo di oggetto a cui appartiene,
        non dal tipo della variabile che contiene il riferimento all'oggetto.
        */
        public virtual ActionResult Create(U entityViewModel)
        {
            return View(entityViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(U entityViewModel, List<Tuple<string, string>> errorList)
        {
            return View();
        }

        public virtual ActionResult Edit(U entityViewModel)
        {
            return View();
        }
    }
}
