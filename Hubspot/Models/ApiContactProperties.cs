using System.Text.Json.Serialization;

namespace HubSpot.Models
{
    internal class ApiContactProperties
    {
        [JsonPropertyName("internal_user_id")]
        public string InternalUserId { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [JsonPropertyName("date_of_birth")]
        public string BirthDate { get; set; }
    }
}