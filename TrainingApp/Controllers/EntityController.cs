using Microsoft.AspNetCore.Mvc;
using TrainingAppData.DB.INTERFACE;

namespace TrainingApp.Controllers
{
    public abstract class EntityController<T,U> : Controller
    {
        private ICreate<T> _create;

        public EntityController(ICreate<T> create)
        {
            _create = create;
        }

        public abstract void DoCreate(T entity, U entityViewModel);
        public abstract void DoUpdate(T entity, U entityViewModel);
        public abstract void DoDelete(int id);
        public abstract T DoSelect(int id);
        public abstract T DoSelect(Guid guid);
        public abstract List<T> DoSelectList();
    }
}
