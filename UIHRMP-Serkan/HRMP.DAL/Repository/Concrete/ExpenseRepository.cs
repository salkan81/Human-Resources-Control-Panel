using HRMP.CORE.Entities;
using HRMP.CORE.Enum;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HRMP.DAL.Repository.Concrete
{
    public class ExpenseRepository : GenericRepoistory<Expense>, IExpenseRepository
    {
        private readonly ApplicationDbContext context;

        public ExpenseRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Expense> GetAllByEmployeeId(int id)
        {
            return context.Expenses.Where(x => x.EmployeeId == id);
        }

        public IEnumerable<Expense> ListAdvanceExpenseWithEmployee()
        {
            return context.Expenses.Where(x => x.IsRead == false || x.ConfirmStatus == ConfirmStatus.Pending).Include(x => x.Employee).ToList();
        }

        public IEnumerable<Expense> GetAllByManagerId(int id)
        {
            return context.Expenses.Where(x => x.CompanyManagerId == id).Include(x => x.Employee).ToList();
        }
    }
}
