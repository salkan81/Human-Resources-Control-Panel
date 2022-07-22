using HRMP.CORE.Entities;

namespace UIHRMP.Areas.CompanyManagerArea.Models
{
    public class RequestsVM
    {
        public IEnumerable<Leave> Leaves { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<AdvancedPayment> Advanceds { get; set; }
    }
}
