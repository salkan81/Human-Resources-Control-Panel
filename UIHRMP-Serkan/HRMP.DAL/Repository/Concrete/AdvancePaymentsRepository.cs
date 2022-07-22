using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HRMP.DAL.Repository.Concrete
{
    public class AdvancePaymentsRepository : GenericRepoistory<AdvancedPayment>, IAdvancePaymentsRepository
    {
        private readonly ApplicationDbContext context;

        public AdvancePaymentsRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<AdvancedPayment> GetAllByEmployeeId(int id)
        {
            return context.AdvancedPayments.Where(x => x.EmployeeId == id);
        }

        public IEnumerable<AdvancedPayment> GetAllByManagerId(int id)
        {
            return context.AdvancedPayments.Where(x => x.CompanyManagerId == id).Include(x => x.Employee).ToList();
        }

        public IEnumerable<AdvancedPayment> ListAdvancePaymentsRequestWithEmployee()
        {
            return context.AdvancedPayments.Where(x => x.IsRead == false || x.IsCorfirmed == CORE.Enum.ConfirmStatus.Pending).Include(x => x.Employee).ToList();
        }
    }
}
