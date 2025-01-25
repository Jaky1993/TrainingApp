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
        [ProducesResponseType(StatusCodes.Status201Created)] //ritorna un villa model vuoto
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (await _dbVilla.GetAsync(v => v.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa already exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Villa villa = _mapper.Map<Villa>(createDTO);

                await _dbVilla.CreateAsync(villa);

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        //Questo stato viene utilizzato per segnalare che la richiesta inviata dal client è invalida
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //Non vengono trovati elementi
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateUser(UserViewModel userViewModel)
        {
            try
            {
                if (await _select.Select(userViewModel.Id) != null)
                {
                    _response.ApiErrorList = new List<string> { "User already exist" };

                    return BadRequest(_response);
                }

                if (userViewModel == null)
                {
                    return BadRequest(userViewModel);
                }

                User user = _mapper.Map<User>(userViewModel);

                await _create.Create(user);
            }
            catch (Exception)
            
        }

    }
}
