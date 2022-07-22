using HRMP.CORE.Entities;

namespace HRMP.DAL.Repository.Abstract
{
    public interface IAdvancePaymentsRepository : IRepository<AdvancedPayment>
    {
        IEnumerable<AdvancedPayment> ListAdvancePaymentsRequestWithEmployee();
        IEnumerable<AdvancedPayment> GetAllByEmployeeId(int id);
        IEnumerable<AdvancedPayment> GetAllByManagerId(int id);
    }
}
