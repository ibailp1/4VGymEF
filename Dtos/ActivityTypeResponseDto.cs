using System.Text.Json.Serialization;

namespace _4VGymEF.Dtos
{
    public class ActivityTypeResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("number-monitors")]
        public int NumberMonitors { get; set; }
    }
}
