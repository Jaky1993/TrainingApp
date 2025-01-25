using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrainingApp.API.Model;
using TrainingApp.VIEWMODEL;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingApp.API.ApiControllers
{
    [Route("api/TrainingAppAPI")]
    [ApiController]
    public class UserApiController : EntityApiController<User, UserViewModel>
    {
        public UserApiController(ICreate<User> create, IDelete<User> delete, ISelect<User> select, IUpdate<User> update, IMapper mapper) : base(create, delete, select, update, mapper)
        {

        }

        [HttpGet]

        /*
            [ProducesResponseType]: Specifica il tipo di risposta che l'azione può produrre.
            StatusCodes.Status200OK: Indica che la risposta sarà HTTP 200, che significa "OK"
            ovvero la richiesta è stata elaborata con successo.

            [ProducesResponseType(StatusCodes.Status404NotFound)]: Documenta che la risposta con stato 404 può essere prodotta
            se non vengono trovati elementi.

            La notazione [ProducesResponseType(StatusCodes.Status400BadRequest)] in ASP.NET Core indica
            che un'azione può produrre una risposta con stato HTTP 400 (Bad Request). 
            Questo stato viene utilizzato per segnalare che la richiesta inviata dal client è invalida.
        */

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        /*
        Task<>: Indica che il metodo è asincrono (usando async) e quindi può eseguire operazioni di lunga durata
        come chiamate di rete o operazioni di I/O senza bloccare il thread chiamante.
        Task rappresenta una singola operazione che può restituire un valore o non restituire alcun valore. 
         
        */
        [HttpGet("id:int", Name = "GetUser")]
        public ActionResult<ApiResponse> GetUser(int id)
        {
            try
            {
                User user = _select.Select(id);

                _response.Result = _mapper.Map<UserViewModel>(user);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ApiErrorList = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpGet(Name = "GetUserList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApiResponse> GetUserList()
        {
            try
            {
                IEnumerable<User> userList = _select.SelectList();
                _response.Result = _mapper.Map<UserViewModel>(userList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ApiErrorList = new List<string> { ex.Message };
            }

            return _response;
        }
    }
}
