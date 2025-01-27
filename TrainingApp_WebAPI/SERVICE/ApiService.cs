using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Text;
using TrainingApp_WebAPI.MODEL.API;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using Utility;
using static System.Net.Mime.MediaTypeNames;

namespace TrainingApp_WebAPI.SERVICE
{
    public class ApiService : IApiService
    {
        public ApiResponse responseModel { get; set; }

        //Using IHttpClientFactory in your application is a powerful approach to managing HttpClient
        //IHttpClientFactory is a built-in .NET Core feature that allows for the centralized
        //configuration of HttpClient instances
        //È possibile registrare e usare un'interfaccia IHttpClientFactory per configurare e creare
        //istanze di HttpClient in un'app.

        //HttpClient: The HttpClient class in .NET is a fundamental part of the System.Net.Http namespace.
        //It's designed to provide a flexible and efficient way to send HTTP requests and receive HTTP responses from
        //a resource identified by a URI.
        protected IHttpClientFactory _httpClient { get; set; }

        public ApiService(IHttpClientFactory httpClient)
        {
            responseModel = new();
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("TrainingApp"); //Creo una nuova istanza della classe HttpClient

                //HttpRequestMessage instance is a great way to customize HTTP requests
                HttpRequestMessage message = new HttpRequestMessage();

                //The key is "Accept", and the value is "application/json". This header informs the server
                //that the client prefers the response body to be in JSON (JavaScript Object Notation) format.
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                //If apiRequest.Data contains data, it will be serialized to JSON and included in the body
                //of the HttpRequestMessage

                if (apiRequest.Data != null)
                {
                    //creates a new StringContent object, which is a way to represent the HTTP content in a text format
                    //UTF-8 (Unicode Transformation Format - 8-bit) is a character encoding that can represent
                    //any character in the Unicode character set
                    //Encoding Forms: Unicode can be encoded in different formats, such as UTF-8, UTF-16, and UTF-32.
                    //UTF-8 is the most widely used, offering backward compatibility with ASCII and efficiency.
                    //media type set to application / json
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case ApiLibrary.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case ApiLibrary.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case ApiLibrary.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                //HttpClient.SendAsync is a method in C# used to send HTTP requests and receive HTTP responses asynchronously 
                apiResponse = await client.SendAsync(message);

                //Reading the response content as a string
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                //deserialize the content in object of type T
                
                T response = JsonConvert.DeserializeObject<T>(apiContent);

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse
                {
                    ApiErrorList = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };

                var result = JsonConvert.SerializeObject(errorResponse);

                T response = JsonConvert.DeserializeObject<T>(result);

                return response;
            }
        }
    }
}
