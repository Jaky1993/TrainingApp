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

        public override ActionResult DoDelete(int id)
        {
            DateTime deleteDate = DateTime.Now;

            _delete.Delete(id, deleteDate);

            return RedirectToAction("List", "User");
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
