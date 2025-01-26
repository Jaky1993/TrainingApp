using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrainingApp.VIEWMODEL;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp.API.Model;

namespace TrainingApp_WebAPI.Controllers
{
    public class UserWebApiController : Controller
    {
        private readonly IUserApiService _userApiService;
        private readonly IMapper _mapper;

        public UserWebApiController(IUserApiService userApiService, IMapper mapper)
        {
            _userApiService = userApiService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new();

            var response = await _villaService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSucces)
            {
                /*
                    object obj = new SomeClass(); 
                    string str = obj.ToString();

                    convert object to String in c#
                */

                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<ActionResult> List()
        {
            List <UserViewModel> userViewModelList = new();

            var response = await _userApiService.GetAllAsync<ApiResponse>();

            return View(userViewModelList);
        }
    }
}
