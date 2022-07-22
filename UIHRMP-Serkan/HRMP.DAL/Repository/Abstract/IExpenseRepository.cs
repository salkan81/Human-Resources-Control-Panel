using HRMP.CORE.Entities;

namespace HRMP.DAL.Repository.Abstract
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        IEnumerable<Expense> ListAdvanceExpenseWithEmployee();
        IEnumerable<Expense> GetAllByEmployeeId(int id);
        IEnumerable<Expense> GetAllByManagerId(int id);
    }
}
