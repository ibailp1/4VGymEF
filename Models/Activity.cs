using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using _4VGymEF.Validations;

namespace _4VGymEF.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public int? ActivityTypeId { get; set; }

        public int MaxParticipants { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        // Propiedades de navegación
        // 1 Activity puede tener N Instructors
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

        // 1 Activity pertenece a 1 ActivityType
        public ActivityType? ActivityType { get; set; } = new ActivityType();
    }

}
