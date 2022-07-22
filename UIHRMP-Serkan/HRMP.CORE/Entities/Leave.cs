using HRMP.CORE.Enum;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class Leave : BaseEntity
    {
        public string? LeaveContent { get; set; }
        public LeaveType LeaveType { get; set; }
        public int? TotalDaysOff { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartLeaveDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndLeaveDate { get; set; }
        public string? Description { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; } = ConfirmStatus.Pending;
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? CompanyManagerId { get; set; }
        public CompanyManager CompanyManager { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
