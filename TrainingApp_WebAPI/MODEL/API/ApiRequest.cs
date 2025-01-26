using System.Security.AccessControl;
using static Utility.ApiLibrary;

namespace TrainingApp_WebAPI.MODEL.API
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
