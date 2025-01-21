using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.VIEWMODEL;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingApp.Controllers
{
    public class UserController : EntityController<User, UserViewModel>
    {
        //when you create a subclass that inherits from a base class,
        //the subclass can automatically call the constructor of the base class
        public UserController(ICreate<User> create, IUpdate<User> update, IDelete<User> delete, ISelect<User> select, IMapper mapper) : base(create, update, delete, select, mapper)
        {

        }

        public override void DoCreate(User entity, UserViewModel entityViewModel)
        {
            //_create.Create(entity);
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
            List<User> userList = new();

            userList = _select.SelectList();

            return userList;
        }

        public override void DoUpdate(User entity, UserViewModel entityViewModel)
        {
            throw new NotImplementedException();
        }

        public override ActionResult Index(List<User> entityList)
        {
            entityList = DoSelectList();

            List<UserViewModel> userViewModelList = _mapper.Map<List<UserViewModel>>(entityList);

            return View(userViewModelList);
        }
    }
}
