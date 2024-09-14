using Elastic.Clients.Elasticsearch;
using System.Text.Json.Serialization;

namespace HumanResorcues.Entities
{
    public class Employee
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = null!; // Elasticsearch ID

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!; // Adı

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = null!; // Soyadı

        [JsonPropertyName("age")]
        public int Age { get; set; } // Yaş

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = null!; // Cinsiyet

        [JsonPropertyName("salary")]
        public decimal Salary { get; set; } // Maaş

        [JsonPropertyName("department")]
        public string Department { get; set; } = null!; // Bölüm

        [JsonPropertyName("position")]
        public string Position { get; set; } = null!; // Pozisyon

        [JsonPropertyName("hireDate")]
        public DateTime HireDate { get; set; } // İşe Başlama Tarihi

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = null!; // Telefon Numarası

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!; // E-Posta Adresi

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } // Aktif Durumu

        [JsonPropertyName("education")]
        public Education Education { get; set; } = null!; // Eğitim Bilgileri
    }
}
