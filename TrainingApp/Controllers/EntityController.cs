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

        public EntityController(ICreate<T> create, IUpdate<T> update, IDelete<T> delete, ISelect<T> select)
        {
            _create = create;
            _update = update;
            _delete = delete;
            _select = select;
        }

        public abstract void DoCreate(T entity, U entityViewModel);
        public abstract void DoUpdate(T entity, U entityViewModel);
        public abstract void DoDelete(int id);
        public abstract T DoSelect(int id);
        public abstract T DoSelect(Guid guid);
        public abstract List<T> DoSelectList();
    }
}
