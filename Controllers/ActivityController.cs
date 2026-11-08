using _4VGymEF.Dtos;
using _4VGymEF.Repositories;
using _4VGymEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymEF.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityRepository _repository;

        public ActivityController(ActivityRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET /activities
        /// Devuelve el listado de actividades de 4VGym.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActivityResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ActivityResponseDto>> FindActivities()
        {
            try
            {
                List<ActivityResponseDto> activities = _repository.GetAll();

                if (activities.Count == 0)
                {
                    return BadRequest(new ErrorResponse(2, "No activities found"));
                }

                return Ok(activities);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        /// <summary>
        /// GET /activities/byId?id=1
        /// Devuelve la actividad de 4VGym según su identificador.
        /// </summary>
        /// <param name="id">Identificador de la actividad</param>
        [HttpGet("byId")]
        [ProducesResponseType(typeof(ActivityResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<ActivityResponseDto> FindActivitiesById([FromQuery(Name = "id")] int id)
        {
            try
            {
                ActivityResponseDto? activity = _repository.GetById(id);

                if (activity == null)
                {
                    return BadRequest(new ErrorResponse(2, "No activity found"));
                }

                return Ok(activity);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        /// <summary>
        /// POST /activities
        /// Añade una nueva actividad en 4VGym.
        /// </summary>
        /// <param name="activity">Objeto enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        [ProducesResponseType(typeof(ActivityResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status400BadRequest)]
        public ActionResult<ActivityResponseDto> CreateActivity([FromBody] ActivityCreateDto activity)
        {
            try
            {
                ActivityResponseDto? created = _repository.Add(activity, out List<ErrorResponse> errors);

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
