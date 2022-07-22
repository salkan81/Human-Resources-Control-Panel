using HRMP.CORE.Entities;

namespace UIHRMP.Areas.EmployeeArea.Models
{
    public class IndexViewModel
    {
        public List<Expense> Expenses { get; set; }
        public DateTime DateOfExpense { get; set; }
        public int Amount { get; set; }
        public string Explanation { get; set; }
        public int ExpenseTypeID { get; set; }
        public IFormFile CheckDocument { get; set; }
    }
}
