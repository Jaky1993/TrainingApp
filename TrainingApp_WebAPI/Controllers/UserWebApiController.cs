using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrainingApp_WebAPI.MODEL;
using TrainingApp_WebAPI.MODEL.API;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp_WebAPI.VIEWMODEL;
using System.Text.Json;
using System.Net;

namespace TrainingApp_WebAPI.Controllers
{
    public class UserWebApiController : EntityWebApiController
    {

        protected readonly IUserApiService _userServiceApi;

        public UserWebApiController(IUserApiService userServiceApi, IMapper mapper) : base(mapper)
        {
            _userServiceApi = userServiceApi;
        }

        public async Task<ActionResult> List()
        {
            List<UserViewModel> userViewModelList = new();

            //Nella chiamata API, ApiResponse sarà il tipo di ritorno.
            //In una chiamata come quella che hai fornito, l'uso di <ApiResponse> tra le parentesi angolari
            //indica che GetAllAsync è un metodo generico e ApiResponse è il tipo specifico
            //di dato che il metodo deve elaborare
            ApiResponse response = await _userServiceApi.GetAllAsync<ApiResponse>();

            if (response != null && response.IsSuccess)
            {
                userViewModelList = JsonConvert.DeserializeObject<List<UserViewModel>>(Convert.ToString(response.Result));
            }

            return View("~/Views/User/List.cshtml", userViewModelList);
        }

        [HttpPost]
        public async Task<ActionResult> DoUpdate(User user, UserViewModel userViewModel)
        {
            user = _mapper.Map<User>(userViewModel);

            if (user.Id == 0)
            {
                user.VersionId = 1;
            }
            else
            {
                user.VersionId = user.VersionId + 1;
                user.UpdateDate = DateTime.Now;
            }

            string viewName = user.GetType().Name;

            //Quando chiami il metodo con un parametro entity di tipo ApiResponse,
            //il compilatore può dedurre che T deve essere ApiResponse senza bisogno di specificarlo esplicitamente.
            ApiResponse CreateResponse = await _userServiceApi.CreateAsync<ApiResponse>(user);

            if (CreateResponse.EntityValidationErrorList.Count > 0)
            {
                TempData["errorList"] = JsonSerializerErrorList(CreateResponse.EntityValidationErrorList);

                return RedirectToAction("~/Views/User/Create.cshtml", viewName, userViewModel);
            }

            if (CreateResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Error = CreateResponse.ApiErrorList;

                return RedirectToAction("Create", viewName);
            }

            if (CreateResponse.IsSuccess == false)
            {
                ViewBag.Error = CreateResponse.ApiErrorList;

                return RedirectToAction("Create", viewName);
            }

            return RedirectToAction("List", viewName);
        }

        public ActionResult Create(UserViewModel entityViewModel, List<Tuple<string, string>> errorList)
        {
            if (TempData["errorList"] != null)
            {
                ViewBag.Error = JsonDeserializerErrorList(TempData["errorList"].ToString());
            }

            return View("~/Views/User/Create.cshtml", entityViewModel);
        }
    }
}
