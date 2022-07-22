using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Services.Concrete
{
    public class ExpenseService : GenericService<Expense>, IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository) : base(expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public IEnumerable<Expense> GetAllByEmployeeId(int id)
        {
            return expenseRepository.GetAllByEmployeeId(id);
        }

        public IEnumerable<Expense> ListAdvanceExpenseWithEmployee()
        {
            return expenseRepository.ListAdvanceExpenseWithEmployee();
        }

        public IEnumerable<Expense> GetAllByManagerId(int id)
        {
            return expenseRepository.GetAllByManagerId(id);
        }
    }
}
