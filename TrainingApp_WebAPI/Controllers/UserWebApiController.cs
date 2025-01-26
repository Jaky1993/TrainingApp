using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrainingApp_WebAPI.MODEL.API;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp_WebAPI.VIEWMODEL;

namespace TrainingApp_WebAPI.Controllers
{
    public class UserWebApiController : Controller
    {
        private readonly IUserApiService _userApiService;

        public UserWebApiController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {
            List<UserViewModel> userViewModelList = new();

            var response = await _userApiService.GetAllAsync<ApiResponse>();

            if (response != null && response.IsSuccess)
            {
                userViewModelList = JsonConvert.DeserializeObject<List<UserViewModel>>(Convert.ToString(response.Result));
            }

            return View("~/Views/User/List.cshtml", userViewModelList);
        }
    }
}
