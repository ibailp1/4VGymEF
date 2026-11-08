using System.Text.Json.Serialization;

namespace _4VGymEF.Dtos
{
    public class ActivityResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("activity_type_id")]
        public int? ActivityTypeId { get; set; }

        [JsonPropertyName("instructors_id")]
        public List<int> InstructorsId { get; set; } = new();

        [JsonPropertyName("max_particpants")]
        public int MaxParticipants { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime DateStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime DateEnd { get; set; }
    }
}
