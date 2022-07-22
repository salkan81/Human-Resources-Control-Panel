using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class Mesaj : BaseEntity
    {
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public bool Okundu { get; set; } = false;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now;
        public int? CompanyManagerID { get; set; }
        public CompanyManager CompanyManager { get; set; }
        public int? EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
