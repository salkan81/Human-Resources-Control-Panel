using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Services.Concrete
{
    public class AdvancePaymentsService : GenericService<AdvancedPayment>, IAdvancePaymentsService
    {
        private readonly IAdvancePaymentsRepository advancePaymentsRepository;

        public AdvancePaymentsService(IAdvancePaymentsRepository advancePaymentsRepository) : base(advancePaymentsRepository)
        {
            this.advancePaymentsRepository = advancePaymentsRepository;
        }

        public IEnumerable<AdvancedPayment> GetAllByEmployeeId(int id)
        {
            return advancePaymentsRepository.GetAllByEmployeeId(id);
        }

        public IEnumerable<AdvancedPayment> GetAllByManagerId(int id)
        {
            return advancePaymentsRepository.GetAllByManagerId(id);
        }

        public IEnumerable<AdvancedPayment> ListAdvancePaymentsRequestWithEmployee()
        {
            return advancePaymentsRepository.ListAdvancePaymentsRequestWithEmployee();
        }
    }
}
