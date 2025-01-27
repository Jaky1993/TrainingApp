using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TrainingApp_WebAPI.MODEL;
using TrainingApp_WebAPI.MODEL.API;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp_WebAPI.VIEWMODEL;

namespace TrainingApp_WebAPI.Controllers
{
    //In questo caso, Entity() e EntityViewModel() possono creare nuove istanze di T e U perché
    //i constraint new() garantiscono che tali tipi hanno un costruttore predefinito
    public class EntityWebApiController : Controller
    {
        protected readonly IMapper _mapper;
        public EntityWebApiController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        public string JsonSerializerErrorList(List<Tuple<string, string>> errorList)
        {
            string jsonErrorList = JsonSerializer.Serialize(errorList);

            return jsonErrorList;
        }

        public List<Tuple<string, string>> JsonDeserializerErrorList(string jsonErrorList)
        {
            List<Tuple<string, string>> content = JsonSerializer.Deserialize<List<Tuple<string, string>>>(jsonErrorList.ToString());

            return content;
        }
    }
}
