using System.Net;

namespace TrainingApp.API.Model
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ApiErrorList { get; set; } = new List<string>();
        public object Result { get; set; }
        public List<Tuple<string, string>> EntityValidationErrorList { get; set; } = new List<Tuple<string, string>>();
    }
}
