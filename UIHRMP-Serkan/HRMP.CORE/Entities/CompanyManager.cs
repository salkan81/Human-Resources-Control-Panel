using HRMP.CORE.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class CompanyManager : BaseEntity
    {
        public CompanyManager()
        {
            Employees = new HashSet<Employee>();
            Mesajlar = new HashSet<Mesaj>();
            AdvancedPayments = new HashSet<AdvancedPayment>();
        }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Title { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Address { get; set; }
        public string? TCNo { get; set; }
        public IFormFile? ManagerPhoto { get; set; }
        public string PhotoPath { get; set; } = "~/img/default.jpg";
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Mesaj> Mesajlar { get; set; }
        public ICollection<AdvancedPayment> AdvancedPayments { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

    }
}
