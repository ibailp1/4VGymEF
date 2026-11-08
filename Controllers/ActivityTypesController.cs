using _4VGymEF.Dtos;
using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymEF.Controllers
{
    [ApiController]
    [Route("activity-types")]
    public class ActivityTypesController : ControllerBase
    {
        private readonly ActivityTypeRepository _repository;

        public ActivityTypesController(ActivityTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET /activity-types
        /// Devuelve el listado de tipos de actividades de 4VGym.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActivityTypeResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ActivityTypeResponseDto>> FindActivityTypes()
        {
            try
            {
                List<ActivityTypeResponseDto> activityTypes = _repository.GetAll();

                if (activityTypes.Count == 0)
                {
                    return BadRequest(new ErrorResponse(2, "No activity types found"));
                }

                return Ok(activityTypes);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        /// <summary>
        /// POST /activity-types
        /// Añade un nuevo tipo de actividad en 4VGym.
        /// </summary>
        /// <param name="activityType">Objeto enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        [ProducesResponseType(typeof(ActivityTypeResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status400BadRequest)]
        public ActionResult<ActivityTypeResponseDto> CreateActivityType([FromBody] ActivityTypeCreateDto activityType)
        {
            try
            {
                ActivityTypeResponseDto? created = _repository.Add(activityType, out List<ErrorResponse> errors);

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
