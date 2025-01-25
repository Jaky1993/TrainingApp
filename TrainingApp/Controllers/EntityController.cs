using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json;
using TrainingApp.VIEWMODEL;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainingApp.Controllers
{
    /*
    The new() constraint in C# generics is used to specify that a type argument must have a public parameterless constructor.
    This means that any class used as the type argument must define a public constructor that takes no arguments. 
    This is useful when you need to create instances of the generic type within the generic class or method.
    To summarize, the constraint where T : Entity, new () and where U : EntityViewModel, new () ensures that T and U
    can be instantiated with a new keyword without any arguments.This is particularly useful when you need to create 
    instances of these types within the generic class or method.
    */
    public abstract class EntityController<T,U> : Controller where T : Entity<T>, new() where U : EntityViewModel, new()
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

        [HttpPost]
        public ActionResult DoUpdate(T entity, U entityViewModel)
        {
            entity = _mapper.Map<T>(entityViewModel);

            string viewName = entity.GetType().Name;

            //Validation

            List<Tuple<string, string>> errorList = entity.DataValidation(entity);

            if (errorList.Count > 0)
            {
                entityViewModel = _mapper.Map<U>(entity);

                TempData["errorList"] = JsonSerializer.Serialize(errorList);

                return RedirectToAction("Create", viewName, entityViewModel);
            }

            if (entity.Id == 0)
            {
                entity.VersionId = 1;
            }
            else
            {
                entity.VersionId = entity.VersionId + 1;
                entity.UpdateDate = DateTime.Now;
            }

            _create.Create(entity);

            return RedirectToAction("List", viewName);
        }
        public abstract ActionResult DoDelete(int id);
        public abstract T DoSelect(int id);
        public abstract T DoSelect(Guid guid);
        public virtual List<T> DoSelectList()
        {
            List<T> entityList = new();

            entityList = _select.SelectList();

            return entityList;
        }
        public virtual ActionResult List()
        {
            List<T> entityList = DoSelectList();

            List<U> entityViewModelList = _mapper.Map<List<U>>(entityList);

            return View("List", entityViewModelList);
        }

        public virtual ActionResult Index(int id)
        {
            T entity = DoSelect(id);

            U entityViewModel = _mapper.Map<U>(entity);

            return View("Index", entityViewModel);
        }

        /*
        Sì, un metodo virtuale può essere richiamato. In programmazione orientata agli oggetti,
        un metodo virtuale è una funzione definita nella classe base e può essere sovrascritta nelle classi derivate.
        Quando si utilizza il metodo, il comportamento effettivo è determinato dal tipo di oggetto a cui appartiene,
        non dal tipo della variabile che contiene il riferimento all'oggetto.
        if you do not override the virtual method in the derived class, the base class's virtual method will be called
        */
        public virtual ActionResult Create(U entityViewModel, List<Tuple<string, string>> errorList)
        {
            return View();
        }

        public virtual ActionResult Edit(int id)
        {
            T entity = new T();

            entity = DoSelect(id);

            U entityViewModel = _mapper.Map<U>(entity);

            return View(entityViewModel);
        }

        public string JsonSerializerErrorList(List<Tuple<string,string>> errorList)
        {
            string jsonErrorList = JsonSerializer.Serialize(errorList);

            return jsonErrorList;
        }

        public List<Tuple<string,string>> JsonDeserializerErrorList(string jsonErrorList)
        {
            List<Tuple<string,string>> content = JsonSerializer.Deserialize<List<Tuple<string,string>>>(jsonErrorList.ToString());

            return content;
        }
    }
}
