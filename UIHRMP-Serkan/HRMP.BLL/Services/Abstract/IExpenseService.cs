using HRMP.BLL.Abstract;
using HRMP.CORE.Entities;

namespace HRMP.BLL.Services.Abstract
{
    public interface IExpenseService : IGenericService<Expense>
    {
        IEnumerable<Expense> ListAdvanceExpenseWithEmployee();
        IEnumerable<Expense> GetAllByEmployeeId(int id);
        IEnumerable<Expense> GetAllByManagerId(int id);
    }
}
