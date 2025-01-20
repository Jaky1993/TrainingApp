using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.VIEWMODEL
{
    public class UserViewModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
