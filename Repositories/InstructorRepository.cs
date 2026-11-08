using _4VGymEF.Dtos;
using _4VGymEF.Data;
using _4VGymEF.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Repositories
{
    public class InstructorRepository
    {
        private readonly GymDbContext _context;

        public InstructorRepository(GymDbContext context)
        {
            _context = context;
        }

        public List<InstructorResponseDto> GetAll()
        {
            return _context.Instructors.ToList().Select(x => ToDto(x)).ToList();
        }

        public List<InstructorResponseDto> GetByFirstInitial(string? firstInitial = null)
        {
            if (string.IsNullOrWhiteSpace(firstInitial))
            {
                return GetAll();
            }

            return _context.Instructors.Where(inst => inst.Name.StartsWith(firstInitial)).ToList().Select(x => ToDto(x)).ToList();
        }

        public InstructorResponseDto? GetById(int id)
        {
            Instructor? instructor = _context.Instructors.FirstOrDefault(inst => inst.Id == id);

            return instructor == null ? null : ToDto(instructor);
        }

        public InstructorResponseDto? Add(InstructorCreateDto dto, out List<ErrorResponse> errores)
        {
            errores = new List<ErrorResponse>();

            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                errores.Add(new ErrorResponse(1, "El nombre del monitor no puede estar vacio"));
                return null;
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                errores.Add(new ErrorResponse(1, "El email no puede estar vacio"));
            }
            else if (!new EmailAddressAttribute().IsValid(dto.Email))
            {
                errores.Add(new ErrorResponse(6, "El email no tiene un formato válido"));
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
            {
                errores.Add(new ErrorResponse(7, "El teléfono es obligatorio"));
            }
            else if (!new PhoneAttribute().IsValid(dto.Phone))
            {
                errores.Add(new ErrorResponse(8, "El formato del teléfono no es válido"));
            }

            if (!Uri.IsWellFormedUriString(dto.PhotoUrl, UriKind.Absolute))
            {
                errores.Add(new ErrorResponse(9, "La fotografía debe ser una URL válida"));
            }

            if (errores.Count > 0)
            {
                return null;
            }

            string nombre = dto.Name.Trim();
            string email = dto.Email!.Trim();

            bool existe = _context.Instructors.Any(inst =>inst.Name.ToLower() == nombre.ToLower() && inst.Email.ToLower() == email.ToLower());

            if (existe)
            {
                errores.Add(new ErrorResponse(5, "El nombre y el email tienen que ser únicos"));
                return null;
            }

            return new InstructorResponseDto
            {
                Id = 0,
                Name = nombre,
                Email = email,
                PhotoUrl = dto.PhotoUrl,
                Phone = dto.Phone!.Trim()
            };
        }

        private static InstructorResponseDto ToDto(Instructor instructor)
        {
            return new InstructorResponseDto
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Email = instructor.Email,
                PhotoUrl = instructor.PhotoUrl,
                Phone = instructor.Phone
            };
        }
    }
}