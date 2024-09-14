using System.Text.Json.Serialization;

namespace HumanResorcues.Entities
{
    public class Education
    {
        [JsonPropertyName("universityName")]
        public string UniversityName { get; set; } 

        [JsonPropertyName("department")]
        public string Department { get; set; } 
    }
}
