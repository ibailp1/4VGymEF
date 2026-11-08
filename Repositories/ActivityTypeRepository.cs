using _4VGymEF.Dtos;
using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _context;

        public ActivityTypeRepository(GymDbContext context)
        {
            _context = context;
        }

        public List<ActivityTypeResponseDto> GetAll()
        {
            return _context.ActivityTypes.ToList().Select(x => ToDto(x)).ToList();
        }

        public ActivityTypeResponseDto? GetById(int id)
        {
            ActivityType? activityType = _context.ActivityTypes.FirstOrDefault(act => act.Id == id);

            return activityType == null ? null : ToDto(activityType);
        }

        public bool ExistsByName(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return false;
            }

            return _context.ActivityTypes.Any(at => at.Name.ToLower() == nombre.ToLower());
        }

        public ActivityTypeResponseDto? Add(ActivityTypeCreateDto dto, out List<ErrorResponse> errores)
        {
            errores = new List<ErrorResponse>();

            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                errores.Add(new ErrorResponse(1, "El tipo de actividad no puede estar vacio"));
                return null;
            }

            if (ExistsByName(dto.Name))
            {
                errores.Add(new ErrorResponse(5, "El nombre tiene que ser único"));
            }

            if (dto.NumberMonitors <= 0)
            {
                errores.Add(new ErrorResponse(6, "El número de monitores debe ser mayor que 0"));
            }

            if (errores.Count > 0)
            {
                return null;
            }

            return new ActivityTypeResponseDto
            {
                Id = 0,
                Name = dto.Name.Trim(),
                NumberMonitors = dto.NumberMonitors
            };
        }

        private static ActivityTypeResponseDto ToDto(ActivityType act)
        {
            return new ActivityTypeResponseDto
            {
                Id = act.Id,
                Name = act.Name,
                NumberMonitors = act.NumberMonitors
            };
        }
    }
}