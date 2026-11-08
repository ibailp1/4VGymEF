using _4VGymEF.Dtos;
using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Repositories
{
    public class ActivityRepository
    {
        private readonly GymDbContext _context;

        public ActivityRepository(GymDbContext context)
        {
            _context = context;
        }

        public List<ActivityResponseDto> GetAll()
        {
            return _context.Activities.Include(act => act.Instructors).ToList().Select(act => ToDto(act)).ToList();
        }

        public ActivityResponseDto? GetById(int id)
        {
            Activity? activity = _context.Activities.Include(act => act.Instructors).FirstOrDefault(act => act.Id == id);

            return activity == null ? null : ToDto(activity);
        }

        public ActivityResponseDto? Add(ActivityCreateDto dto, out List<ErrorResponse> errores)
        {
            errores = new List<ErrorResponse>();

            if (dto == null)
            {
                errores.Add(new ErrorResponse(1, "Datos no válidos"));
                return null;
            }

            List<int> instructorIds = dto.InstructorsId ?? new List<int>();

            if (!_context.ActivityTypes.Any(act => act.Id == dto.ActivityTypeId))
            {
                errores.Add(new ErrorResponse(34, "El tipo de actividad no existe"));
            }

            if (instructorIds.Distinct().Count() != instructorIds.Count)
            {
                errores.Add(new ErrorResponse(36, "La lista de monitores tiene monitores repetidos"));
            }

            List<Instructor> instructors = _context.Instructors.Where(inst => instructorIds.Contains(inst.Id)).ToList();

            foreach (int id in instructorIds.Distinct())
            {
                if (!instructors.Any(inst => inst.Id == id))
                {
                    errores.Add(new ErrorResponse(35, "El instructor no existe"));
                }
            }

            if (dto.DateStart > dto.DateEnd)
            {
                errores.Add(new ErrorResponse(2, "La fecha de fin debe ser posterior a la de inicio"));
            }

            if (dto.DateStart < DateTime.Now || dto.DateEnd < DateTime.Now)
            {
                errores.Add(new ErrorResponse(4, "Las fechas deben de ser en el futuro"));
            }

            if (dto.MaxParticipants <= 0)
            {
                errores.Add(new ErrorResponse(5, "El máximo de participantes debe ser mayor a 0"));
            }

            if (errores.Count > 0)
            {
                return null;
            }

            return new ActivityResponseDto
            {
                Id = 0,
                ActivityTypeId = dto.ActivityTypeId,
                InstructorsId = instructorIds,
                MaxParticipants = dto.MaxParticipants,
                DateStart = dto.DateStart,
                DateEnd = dto.DateEnd
            };
        }

        private static ActivityResponseDto ToDto(Activity act)
        {
            return new ActivityResponseDto
            {
                Id = act.Id,
                ActivityTypeId = act.ActivityTypeId,
                InstructorsId = act.Instructors.Select(inst => inst.Id).ToList(),
                MaxParticipants = act.MaxParticipants,
                DateStart = act.DateStart,
                DateEnd = act.DateEnd
            };
        }
    }
}