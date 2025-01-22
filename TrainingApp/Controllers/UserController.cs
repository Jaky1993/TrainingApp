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

        public override ActionResult DoUpdate(User user, UserViewModel userViewModel)
        {
            user = _mapper.Map<User>(userViewModel);

            //Validation
            List<Tuple<string,string>> errorList = TrainingAppData.MODEL.User.UserValidation(user);

            if (errorList.Count > 0)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);

                return RedirectToAction("Create", "User", userViewModel);
            }

            if (user.Id == 0)
            {
                user.VersionId = 1;
            }
            else
            {
                user.VersionId = user.VersionId++;
                user.UpdateDate = DateTime.Now;
            }

            _create.Create(user);

            return RedirectToAction("Index", "User");
        }

        public override ActionResult Index()
        {
            List<User> entityList = DoSelectList();

            List<UserViewModel> userViewModelList = _mapper.Map<List<UserViewModel>>(entityList);

            return View(userViewModelList);
        }

        public override ActionResult Create(UserViewModel userViewModel, List<Tuple<string,string>> errorList)
        {
            User user = new User();

            DoUpdate(user, userViewModel);

            ViewBag.Error = errorList;

            return View("Create", userViewModel);
        }
    }
}
