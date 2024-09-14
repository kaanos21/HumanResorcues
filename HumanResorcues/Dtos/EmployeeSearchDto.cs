namespace HumanResorcues.Dtos
{
    public class EmployeeSearchDto
    {
        public string Id { get; set; } = null!; // Elasticsearch ID

        public string FirstName { get; set; } = null!; // Adı
        public string LastName { get; set; } = null!; // Soyadı

        public int Age { get; set; } // Yaş

        public string Gender { get; set; } = null!; // Cinsiyet

        public decimal Salary { get; set; } // Maaş

        public string Department { get; set; } = null!; // Bölüm

        public string Position { get; set; } = null!; // Pozisyon

        public DateTime HireDate { get; set; } // İşe Başlama Tarihi
        public string PhoneNumber { get; set; } = null!; // Telefon Numarası
        public string Email { get; set; } = null!; // E-Posta Adresi
        public bool IsActive { get; set; } // Aktif Durumu
        public string UniversityName { get; set; }

        public string EducationDepartment { get; set; }
    }
}
