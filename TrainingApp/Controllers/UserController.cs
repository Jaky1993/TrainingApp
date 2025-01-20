using Microsoft.AspNetCore.Mvc;
using TrainingApp.VIEWMODEL;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingApp.Controllers
{
    public class UserController : EntityController<User, UserViewModel>
    {
        public UserController(ICreate<User> create, IUpdate<User> update, IDelete<User> delete, ISelect<User> select) : base(create, update, delete, select)
        {

        }

        public override void DoCreate(User entity, UserViewModel entityViewModel)
        {
            _create.Create(entity);
        }

        public override void DoDelete(int id)
        {
            throw new NotImplementedException();
        }

        public override User DoSelect(int id)
        {
            throw new NotImplementedException();
        }

        public override User DoSelect(Guid guid)
        {
            throw new NotImplementedException();
        }

        public override List<User> DoSelectList()
        {
            throw new NotImplementedException();
        }

        public override void DoUpdate(User entity, UserViewModel entityViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
