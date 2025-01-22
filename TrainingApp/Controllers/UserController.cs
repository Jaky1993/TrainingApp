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
        //the subclass can automatically call the constructor of the base class if there are type T
        public UserController(ICreate<User> create, IUpdate<User> update, IDelete<User> delete, ISelect<User> select, IMapper mapper) : base(create, update, delete, select, mapper)
        {

        }

        public override void DoCreate(User entity, UserViewModel entityViewModel)
        {       
            _create.Create(entity);
        }

        public override void DoDelete(int id)
        {

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

        public override void DoUpdate(User user, UserViewModel userViewModel)
        {
            user = _mapper.Map<User>(userViewModel);

            //Validation
            List<string> errorList = TrainingAppData.MODEL.User.UserValidation(user);

            if (errorList.Count != 0)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);

                Create(userViewModel, errorList);
            }

            _create.Create(user);
        }

        public override ActionResult Index(List<User> entityList)
        {
            entityList = DoSelectList();

            List<UserViewModel> userViewModelList = _mapper.Map<List<UserViewModel>>(entityList);

            return View(userViewModelList);
        }

        public override ActionResult Create(UserViewModel userViewModel, List<string> errorList)
        {
            ViewBag.Error = errorList;

            return View("Create", userViewModel);
        }
    }
}
