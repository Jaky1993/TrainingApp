using Microsoft.AspNetCore.Mvc;
using TrainingApp.VIEWMODEL;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingApp.Controllers
{
    public class UserController : EntityController<User, UserViewModel>
    {
        public override void DoCreate(User entity, UserViewModel entityViewModel)
        {
            Console.WriteLine("Hello");
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
