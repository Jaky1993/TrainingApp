using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using TrainingApp.VIEWMODEL;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingApp.Controllers
{
    /*
    Sì, esattamente.In ASP.NET MVC e ASP.NET Core, per ogni richiesta HTTP viene creato un nuovo oggetto del controller.
    Questo fa parte del ciclo di vita delle richieste che consente la costruzione di applicazioni web stateless.
    Ogni volta che viene effettuata una richiesta a un controller, un nuovo istanza di tale controller viene instanziata,
    gestendo la richiesta e poi eliminata una volta completata l'elaborazione.
    Dunque, ogni volta che fai una richiesta, come ad esempio un RedirectToAction,
    viene creata una nuova istanza di EntityController e UserController.
    */

    public class UserController : EntityController<User, UserViewModel>
    {
        //when you create a subclass that inherits from a base class,
        //the subclass can automatically call the constructor of the base class if there are type T
        public UserController(ICreate<User> create, IUpdate<User> update, IDelete<User> delete, ISelect<User> select, IMapper mapper) : base(create, update, delete, select, mapper)
        {

        }

        public override void DoDelete(int id)
        {

        }

        public override User DoSelect(int id)
        {
            User user = _select.Select(id);

            return user;
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

        /*
        [HttpPost]
        public override ActionResult DoUpdate(User user, UserViewModel userViewModel)
        {
            user = _mapper.Map<User>(userViewModel);

            //Validation
            List<Tuple<string,string>> errorList = TrainingAppData.MODEL.User.UserValidation(user);

            if (errorList.Count > 0)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);

                TempData["errorList"] = JsonSerializer.Serialize(errorList);

                return RedirectToAction("Create","User", userViewModel);
            }

            if (user.Id == 0)
            {
                user.VersionId = 1;
            }
            else
            {
                user.VersionId = user.VersionId + 1;
                user.UpdateDate = DateTime.Now;
            }

            _create.Create(user);

            return RedirectToAction("Index", "User");
        }
        */

        public override ActionResult Index()
        {
            List<User> entityList = DoSelectList();

            List<UserViewModel> userViewModelList = _mapper.Map<List<UserViewModel>>(entityList);

            return View(userViewModelList);
        }

        public override ActionResult Create(UserViewModel entityViewModel, List<Tuple<string,string>> errorList)
        {
            if (TempData["errorList"] != null)
            {
                ViewBag.Error = JsonDeserializerErrorList(TempData["errorList"].ToString());
            }

            return View("Create", entityViewModel);
        }
    }
}
