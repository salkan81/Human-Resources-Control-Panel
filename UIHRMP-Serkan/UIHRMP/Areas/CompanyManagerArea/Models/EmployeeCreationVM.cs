using HRMP.CORE.Enum;
using System.ComponentModel.DataAnnotations;

namespace UIHRMP.Areas.CompanyManagerArea.Models
{
    public class EmployeeCreationVM
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? TCNo { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HiredDate { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public IFormFile? EmployeePhoto { get; set; }
        public string PhotoPath { get; set; } = "~/img/default.jpg";
    }
}
