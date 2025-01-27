using System.Net;

namespace TrainingApp_WebAPI.MODEL.API
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ApiErrorList { get; set; }
        public object Result { get; set; }
        public List<Tuple<string, string>> EntityValidationErrorList { get; set; }
    }
}
