using System.Net;

namespace TrainingApp.API.Model
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ApiErrorList { get; set; }
        public object Result { get; set; }
    }
}
