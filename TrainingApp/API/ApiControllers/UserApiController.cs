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

        /*
        Task<>: Indica che il metodo è asincrono (usando async) e quindi può eseguire operazioni di lunga durata
        come chiamate di rete o operazioni di I/O senza bloccare il thread chiamante.
        Task rappresenta una singola operazione che può restituire un valore o non restituire alcun valore. 
         
        */

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id:int", Name = "GetUser")]
        public async Task<ActionResult<ApiResponse>> GetUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }


                User user = await _select.Select(id);

                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }

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
        public async Task<ActionResult<ApiResponse>> GetUserList()
        {
            try
            {
                IEnumerable<User> userList = await _select.SelectList();

                _response.Result = _mapper.Map<List<UserViewModel>>(userList);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        //Questo stato viene utilizzato per segnalare che la richiesta inviata dal client è invalida
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CreateUser(UserViewModel userViewModel)
        {
            try
            {
                List<User> userList = await _select.SelectList();

                if (userList.Find(U => U.Id == userViewModel.Id) != null)
                {
                    _response.ApiErrorList = new List<string> { "User already exist" };

                    return BadRequest(_response);
                }

                if (userViewModel == null)
                {
                    _response.ApiErrorList = new List<string> { "UserViewModel is null" };

                    return BadRequest(_response);
                }

                User user = _mapper.Map<User>(userViewModel);

                List<Tuple<string,string>> entityValidationErrorList = user.DataValidation(user);

                if (entityValidationErrorList.Count > 0)
                {
                    _response.EntityValidationErrorList = entityValidationErrorList;

                    return _response;
                }

                user.VersionId = 1;

                await _create.Create(user);

                _response.Result = _mapper.Map<UserViewModel>(user);
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ApiErrorList = new List<string> { ex.Message };
            }

            /*
            Sì, hai ragione. Anche se il codice entra nel blocco catch, alla fine restituisce comunque _response.
            Questo perché l'istruzione return _response; si trova al di fuori sia del blocco try che del blocco catch,
            quindi sarà eseguita sempre.
            Se desideri restituire una risposta diversa in caso di un'eccezione,
            dovrai includere un’istruzione return anche nel blocco catch
            */

            return _response;
        }

        /*
        In questo esempio, l'annotazione [FromBody] viene utilizzata per specificare che il userViewModel
        deve essere deserializzato dal corpo della richiesta HTTP. 
        */
        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult<ApiResponse>> UpdateUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
                if (userViewModel == null)
                {
                    return BadRequest();
                }

                User user = _mapper.Map<User>(userViewModel);

                user.VersionId = user.VersionId + 1;
                user.UpdateDate = DateTime.Now;

                await _create.Create(user);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = new List<string> { ex.Message };
            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("id:int", Name = "DeleteUser")]
        public async Task<ActionResult<ApiResponse>> DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                DateTime deleteDate = DateTime.Now;

                _delete.Delete(id, deleteDate);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
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
