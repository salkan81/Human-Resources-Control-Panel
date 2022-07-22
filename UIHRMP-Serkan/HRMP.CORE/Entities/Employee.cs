using HRMP.CORE.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            Leaves = new HashSet<Leave>();
            Expenses = new HashSet<Expense>();
            AdvancedPayments = new HashSet<AdvancedPayment>();
            Mesajlar = new HashSet<Mesaj>();
        }

        public string? Name { get; set; }
        public string? Surname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? TCNo { get; set; }
        public string? Password { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HiredDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? QuitDate { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public string? Address { get; set; }
        public string? ActivationCode { get; set; }
        public IFormFile? EmployeePhoto { get; set; }
        public string PhotoPath { get; set; } = "~/img/default.jpg";
        public int CompanyManagerID { get; set; }
        public CompanyManager? CompanyManager { get; set; }
        public ICollection<Mesaj> Mesajlar { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<AdvancedPayment> AdvancedPayments { get; set; }
    }
}
