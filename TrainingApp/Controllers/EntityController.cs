using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingAppData.DB.INTERFACE;

namespace TrainingApp.Controllers
{
    public abstract class EntityController<T,U> : Controller
    {
        protected ICreate<T> _create;
        protected IUpdate<T> _update;
        protected IDelete<T> _delete;
        protected ISelect<T> _select;
        protected IMapper _mapper;

        public EntityController(ICreate<T> create, IUpdate<T> update, IDelete<T> delete, ISelect<T> select, IMapper mapper)
        {
            _create = create;
            _update = update;
            _delete = delete;
            _select = select;
            _mapper = mapper;
        }

        public abstract void DoCreate(T entity, U entityViewModel);
        public abstract void DoUpdate(T entity, U entityViewModel);
        public abstract void DoDelete(int id);
        public abstract T DoSelect(int id);
        public abstract T DoSelect(Guid guid);
        public abstract List<T> DoSelectList();

        public virtual ActionResult Index(List<T> entityList)
        {
            return View();
        }
    }
}
