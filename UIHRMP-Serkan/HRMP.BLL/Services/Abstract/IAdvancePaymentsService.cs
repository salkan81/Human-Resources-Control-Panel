using HRMP.BLL.Abstract;
using HRMP.CORE.Entities;

namespace HRMP.BLL.Services.Abstract
{
    public interface IAdvancePaymentsService : IGenericService<AdvancedPayment>
    {
        IEnumerable<AdvancedPayment> ListAdvancePaymentsRequestWithEmployee();
        IEnumerable<AdvancedPayment> GetAllByEmployeeId(int id);
        IEnumerable<AdvancedPayment> GetAllByManagerId(int id);
    }
}
