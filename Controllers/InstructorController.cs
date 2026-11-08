using _4VGymEF.Dtos;
using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymEF.Controllers
{
    [ApiController]
    [Route("monitors")]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorRepository _repository;

        public InstructorsController(InstructorRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET /monitors/all
        /// Devuelve el listado de monitores de 4VGym.
        /// </summary>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<InstructorResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<InstructorResponseDto>> FindAllMonitors()
        {
            try
            {
                List<InstructorResponseDto> monitors = _repository.GetAll();

                if (monitors.Count == 0)
                {
                    return BadRequest(new ErrorResponse(2, "No monitors found"));
                }

                return Ok(monitors);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        /// <summary>
        /// GET /monitors/byFirstInitial?first_initial=M
        /// Devuelve el listado de monitores (filtrado opcionalmente por la inicial del nombre).
        /// </summary>
        /// <param name="firstInitial">Filtro opcional para la inicial del monitor</param>
        [HttpGet("byFirstInitial")]
        [ProducesResponseType(typeof(IEnumerable<InstructorResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<InstructorResponseDto>> FindMonitorsByFirstInitial([FromQuery(Name = "first_initial")] string? firstInitial)
        {
            try
            {
                return Ok(_repository.GetByFirstInitial(firstInitial));
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        /// <summary>
        /// GET /monitors/byId?id=1
        /// Devuelve el monitor de 4VGym según su identificador.
        /// </summary>
        /// <param name="id">Identificador del monitor</param>
        [HttpGet("byId")]
        [ProducesResponseType(typeof(InstructorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<InstructorResponseDto> FindMonitorsById([FromQuery(Name = "id")] int id)
        {
            try
            {
                InstructorResponseDto? monitor = _repository.GetById(id);

                if (monitor == null)
                {
                    return BadRequest(new ErrorResponse(2, "No monitors found"));
                }

                return Ok(monitor);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        /// <summary>
        /// POST /monitors
        /// Añade un nuevo instructor en 4VGym.
        /// </summary>
        /// <param name="instructor">Objeto enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        [ProducesResponseType(typeof(InstructorResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status400BadRequest)]
        public ActionResult<InstructorResponseDto> CreateInstructor([FromBody] InstructorCreateDto instructor)
        {
            try
            {
                InstructorResponseDto? created = _repository.Add(instructor, out List<ErrorResponse> errors);

                if (created == null)
                {
                    return BadRequest(errors);
                }

                return StatusCode(StatusCodes.Status201Created, created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(1, "An internal server error occurred"));
            }
        }
    }
}
