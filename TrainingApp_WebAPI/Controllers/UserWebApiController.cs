using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp_WebAPI.MODEL;
using TrainingApp_WebAPI.MODEL.API;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp_WebAPI.VIEWMODEL;
using System.Net;
using Azure;
using Newtonsoft.Json;

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

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.Error = response.ApiErrorList;

                return View("~/Views/User/List.cshtml");
            }

            if (response.IsSuccess == false)
            {
                ViewBag.Error = response.ApiErrorList;

                return View("~/Views/User/List.cshtml");
            }

            if (response.StatusCode == HttpStatusCode.OK && response.IsSuccess)
            {
                userViewModelList = JsonConvert.DeserializeObject<List<UserViewModel>>(Convert.ToString(response.Result));
            }

            return View("~/Views/User/List.cshtml", userViewModelList);
        }

        [HttpPost]
        public async Task<ActionResult> DoUpdate(User user, UserViewModel userViewModel)
        {
            user = _mapper.Map<User>(userViewModel);

            string redirectViewName;

            if (user.Id == 0)
            {
                user.VersionId = 1;

                redirectViewName = "Create";
            }
            else
            {
                user.VersionId = user.VersionId + 1;
                user.UpdateDate = DateTime.Now;

                redirectViewName = "Edit";
            }

            //Quando chiami il metodo con un parametro entity di tipo ApiResponse,
            //il compilatore può dedurre che T deve essere ApiResponse senza bisogno di specificarlo esplicitamente.
            ApiResponse ApiResponse = await _userServiceApi.CreateAsync<ApiResponse>(user);

            if (ApiResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                if (ApiResponse.ApiErrorList.Count > 0)
                {
                    TempData["ApiErrorList"] = JsonConvert.SerializeObject(ApiResponse.ApiErrorList);

                    return RedirectToAction(redirectViewName, userViewModel);
                }

                if (ApiResponse.EntityValidationErrorList.Count > 0)
                {
                    TempData["EntityValidationErrorList"] = JsonSerializerErrorList(ApiResponse.EntityValidationErrorList);

                    return RedirectToAction(redirectViewName, userViewModel);
                }
            }

            if (ApiResponse.IsSuccess == false)
            {
                TempData["ApiErrorList"] = ApiResponse.ApiErrorList;

                return RedirectToAction(redirectViewName);
            }

            return RedirectToAction("List");
        }

        public async Task<ActionResult> DoDelete(int userId)
        {
            ApiResponse response = await _userServiceApi.DeleteAsync<ApiResponse>(userId);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Error = JsonConvert.SerializeObject(response.ApiErrorList);

                return RedirectToAction("List");
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.Error = JsonConvert.SerializeObject(response.ApiErrorList);

                return RedirectToAction("List");
            }

            if (response.IsSuccess == false)
            {
                ViewBag.Error = JsonConvert.SerializeObject(response.ApiErrorList);

                return RedirectToAction("List");
            }

            return RedirectToAction("List");
        }

        public ActionResult Create(UserViewModel entityViewModel)
        {
            if (TempData["ApiErrorList"] != null)
            {
                ViewBag.ApiErrorList = TempData["ApiErrorList"];
            }

            if (TempData["EntityValidationErrorList"] != null)
            {
                ViewBag.EntityValidationErrorList = TempData["EntityValidationErrorList"];
            }

            return View("~/Views/User/Create.cshtml", entityViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int userId, UserViewModel userViewModel)
        {
            if (userViewModel.Id == 0)
            {
                ApiResponse response = await _userServiceApi.GetAsync<ApiResponse>(userId);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ApiErrorList = response.ApiErrorList;

                    return View("~/Views/User/Edit.cshtml");
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    ViewBag.ApiErrorList = response.ApiErrorList;

                    return View("~/Views/User/Edit.cshtml");
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    userViewModel = JsonConvert.DeserializeObject<UserViewModel>(Convert.ToString(response.Result));
                }
            }

            if (TempData["ApiErrorList"] != null)
            {
                ViewBag.ApiErrorList = TempData["ApiErrorList"];
            }

            if (TempData["EntityValidationErrorList"] != null)
            {
                ViewBag.EntityValidationErrorList = TempData["EntityValidationErrorList"];
            }

            return View("~/Views/User/Edit.cshtml", userViewModel);
        }
    }
}
