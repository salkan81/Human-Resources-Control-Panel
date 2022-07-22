using HRMP.CORE.Enum;


namespace UIHRMP.Areas.CompanyManagerArea.Models
{
    public class ManagerCheckVM
    {
        public List<ManagerLeaveList> Leaves { get; set; }
        public List<ManagerExpenseList> Expenses { get; set; }
    }
    public class ManagerLeaveList
    {
        public int Id { get; set; }
        public int TotalDaysOff { get; set; }
        public DateTime StartLeaveDate { get; set; }
        public DateTime EndLeaveDate { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LeaveDocument { get; set; }
        public string LeaveTypeName { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; }
    }
    public class ManagerExpenseList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CheckDocument { get; set; }
        public double Amount { get; set; }
        public string Explanation { get; set; }
        public DateTime DateOfExpense { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; }
        public string ExpenseTypeName { get; set; }
    }
}
