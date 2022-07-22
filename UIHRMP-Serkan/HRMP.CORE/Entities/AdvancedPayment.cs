using HRMP.CORE.Enum;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class AdvancedPayment : BaseEntity
    {
        public string? PaymentContent { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }
        public string? Description { get; set; }
        public ConfirmStatus IsCorfirmed { get; set; } = ConfirmStatus.Pending;
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? CompanyManagerId { get; set; }
        public CompanyManager CompanyManager { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
